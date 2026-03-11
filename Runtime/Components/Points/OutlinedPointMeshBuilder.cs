using System;
using System.Collections.Generic;
using SxmTools.UIFactory.Components.Series;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    internal sealed class OutlinedPointMeshBuilder : MeshBuilder<OutlinedPointMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;

        protected override void Build(OutlinedPointMeshDescription description, List<MeshData> result)
        {
            var vertices = description.Shape switch
            {
                PointShape.Circle => MeshUtils.RentVerticesOnCircumference(hasOriginPoint: false, resolution: 32, 0.5f * description.Size, description.Origin),
                PointShape.Square => MeshUtils.RentVerticesOnRectangle(buildOrder: MeshUtils.RectangleVerticesBuildOrder.Cyclic, angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin),
                PointShape.Triangle => MeshUtils.RentVerticesOnEquilateralTriangle(angleAroundOriginInDeg: 180f, description.Size, description.Origin),
                _ => throw new ArgumentOutOfRangeException()
            };

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: description.Outline,
                Padding: 0f,
                Closed: true,
                Positions: vertices,
                ForceBuild: description.ForceBuild
            );

            _lineSeriesHandle = UIFactoryManager.BuildMesh(lineSeriesDescription, result, _lineSeriesHandle);
            // todo@sxm: странно, нет?
            // MeshUtils.ReturnVertices(vertices);
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();
        }
    }
}