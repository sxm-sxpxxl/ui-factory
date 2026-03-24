using System;
using System.Collections.Generic;
using SxmTools.UIFactory.Components.Series;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    internal sealed class OutlinedPointMeshBuilder : MeshBuilder<OutlinedPointMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;
        private readonly VersionedList<Vector2> _positions = new();

        protected override void Build(OutlinedPointMeshDescription description, List<MeshData> result)
        {
            var vertices = description.Shape switch
            {
                PointShape.Circle => MeshUtils.GetVerticesOnCircumference(0.5f * description.Size, description.Origin),
                PointShape.Square => MeshUtils.GetVerticesOnRectangle(buildOrder: MeshUtils.RectangleVerticesBuildOrder.Cyclic, angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin),
                PointShape.Triangle => MeshUtils.GetVerticesOnEquilateralTriangle(angleAroundOriginInDeg: 180f, description.Size, description.Origin),
                _ => throw new ArgumentOutOfRangeException()
            };

            _positions.Clear();
            for (var i = 0; i < vertices.Length; i++)
                _positions.Add(vertices[i]);

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: description.Outline,
                Padding: 0f,
                Closed: true,
                Positions: new Snapshot<VersionedList<Vector2>>(_positions),
                ForceBuild: description.ForceBuild
            );

            _lineSeriesHandle = UIFactoryManager.BuildMesh(lineSeriesDescription, result, _lineSeriesHandle);
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();
        }
    }
}