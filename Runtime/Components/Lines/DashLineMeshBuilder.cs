using System.Collections.Generic;
using SxmTools.UIFactory.Components.Series;
using UnityEngine;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class DashLineMeshBuilder : MeshBuilder<DashLineMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;
        private List<Vector2> _positions;

        protected override void Build(DashLineMeshDescription description, List<MeshData> result)
        {
            var dashWidth = description.DashWidth;
            var dashGap = description.DashGap;

            var dashesCount = Mathf.CeilToInt((description.LineLength + dashGap) / (dashWidth + dashGap));
            var positionsCount = dashesCount + 1;

            _positions ??= ListPool<Vector2>.Get();
            _positions.Clear();

            for (var i = 0; i < positionsCount; i++)
            {
                var position = Vector2.Lerp(description.StartPosition, description.EndPosition, (float) i / dashesCount);
                _positions.Add(position);
            }

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: new SolidLineMeshDescription(
                    Thickness: description.Thickness,
                    Color: description.Color,
                    ForceBuild: description.ForceBuild
                ),
                Padding: 0.5f * description.DashGap,
                Closed: false,
                Positions: _positions,
                ForceBuild: description.ForceBuild
            );

            _lineSeriesHandle = UIFactoryManager.BuildMesh(lineSeriesDescription, result, _lineSeriesHandle);
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();

            _positions.Clear();
            ListPool<Vector2>.Release(_positions);

            _positions = null;
        }
    }
}