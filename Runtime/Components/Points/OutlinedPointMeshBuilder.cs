using System;
using System.Collections.Generic;
using SxmTools.UIFactory.Components.Series;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    internal sealed class OutlinedPointMeshBuilder : MeshBuilder<OutlinedPointMeshDescription>
    {
        private readonly MeshHandle _lineSeriesHandle = new();

        protected override IReadOnlyList<MeshData> Build(OutlinedPointMeshDescription description)
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

            var result = UIFactoryManager.Build(lineSeriesDescription, _lineSeriesHandle);
            MeshUtils.ReturnVertices(vertices);

            return result;
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();
        }
    }
}