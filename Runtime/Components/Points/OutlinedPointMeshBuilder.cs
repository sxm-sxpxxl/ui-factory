using System;
using System.Collections.Generic;
using Sxm.UIFactory.Components.Series;
using UnityEngine;

namespace Sxm.UIFactory.Components.Points
{
    public sealed class OutlinedPointMeshBuilder : MeshBuilder<OutlinedPointMeshDescription>
    {
        private readonly MeshHandle _lineSeriesHandle = new();

        protected override IEnumerable<MeshData> Build(OutlinedPointMeshDescription description)
        {
            var vertices = description.Shape switch
            {
                PointShape.Circle => MeshUtils.GetVerticesOnCircumference(hasOriginPoint: false, resolution: 32, 0.5f * description.Size, description.Origin),
                PointShape.Square => MeshUtils.GetVerticesOnRectangle(buildOrder: MeshUtils.RectangleVerticesBuildOrder.Cyclic, angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin),
                PointShape.Triangle => MeshUtils.GetVerticesOnEquilateralTriangle(angleAroundOriginInDeg: 180f, description.Size, description.Origin),
                _ => throw new ArgumentOutOfRangeException()
            };

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: description.Outline,
                Padding: 0f,
                Closed: true,
                Positions: vertices,
                ForceBuild: description.ForceBuild
            );

            return UIFactoryManager.Build(lineSeriesDescription, _lineSeriesHandle);
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();
        }
    }
}