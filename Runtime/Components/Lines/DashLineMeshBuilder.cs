using System.Collections.Generic;
using SxmTools.UIFactory.Components.Series;
using UnityEngine;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class DashLineMeshBuilder : MeshBuilder<DashLineMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;
        private List<Vector2> _previousPositions;

        protected override void Build(DashLineMeshDescription description, List<MeshData> result)
        {
            var dashWidth = description.DashWidth;
            var dashGap = description.DashGap;

            var dashesCount = Mathf.CeilToInt((description.LineLength + dashGap) / (dashWidth + dashGap));
            var positionsCount = dashesCount + 1;

            var positions = ListPool<Vector2>.Get();

            for (var i = 0; i < positionsCount; i++)
            {
                var position = Vector2.Lerp(description.StartPosition, description.EndPosition, (float) i / dashesCount);
                positions.Add(position);
            }

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: new SolidLineMeshDescription(
                    Thickness: description.Thickness,
                    Color: description.Color
                ),
                Padding: 0.5f * description.DashGap,
                Closed: false,
                Positions: positions
            );

            _lineSeriesHandle = UIFactoryManager.BuildMesh(lineSeriesDescription, result, _lineSeriesHandle);

            if (_previousPositions != null)
            {
                ListPool<Vector2>.Release(_previousPositions);
            }
            _previousPositions = positions;
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();

            if (_previousPositions != null)
            {
                ListPool<Vector2>.Release(_previousPositions);
            }
            _previousPositions = null;
        }
    }
}