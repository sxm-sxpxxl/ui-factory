using System.Collections.Generic;
using SxmTools.UIFactory.Components.Series;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class DashLineMeshBuilder : MeshBuilder<DashLineMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;
        private readonly VersionedList<Vector2> _positions = new();

        protected override void Build(DashLineMeshDescription description, List<MeshData> result)
        {
            var dashWidth = description.DashWidth;
            var dashGap = description.DashGap;

            var dashesCount = Mathf.CeilToInt((description.LineLength + dashGap) / (dashWidth + dashGap));
            var positionsCount = dashesCount + 1;

            _positions.Clear();

            for (var i = 0; i < positionsCount; i++)
            {
                var position = Vector2.Lerp(description.StartPosition, description.EndPosition, (float) i / dashesCount);
                _positions.Add(position);
            }

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: new SolidLineMeshDescription(
                    Thickness: description.Thickness,
                    Color: description.Color
                ),
                Padding: 0.5f * description.DashGap,
                Closed: false,
                Positions: new Snapshot<VersionedList<Vector2>>(_positions)
            );

            _lineSeriesHandle = UIFactoryManager.BuildMesh(lineSeriesDescription, result, _lineSeriesHandle);
        }

        public override void Dispose()
        {
            _lineSeriesHandle?.Dispose();
        }
    }
}