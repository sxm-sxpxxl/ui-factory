using System.Collections.Generic;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class LineSeriesMeshBuilder : MeshBuilder<LineSeriesMeshDescription>
    {
        private List<MeshHandle> _lineHandles;

        protected override void Build(LineSeriesMeshDescription description, List<MeshData> result)
        {
            var positions = description.Positions.Collection;
            var linesCount = positions.Count - (description.Closed ? 0 : 1);

            if (linesCount <= 0)
                return;

            _lineHandles ??= ListPool<MeshHandle>.Get();
            _lineHandles.ResizeHandles(linesCount);

            for (var currentPositionIndex = 0; currentPositionIndex < linesCount; currentPositionIndex++)
            {
                var isLastLine = currentPositionIndex == linesCount - 1;
                var nextPositionIndex = isLastLine && description.Closed ? 0 : currentPositionIndex + 1;

                var startPosition = positions[currentPositionIndex];
                var endPosition = positions[nextPositionIndex];

                var lineDirection = (endPosition - startPosition).normalized;
                var paddingOffset = description.Padding * lineDirection;

                var lineDescription = description.Line with
                {
                    ForceBuild = description.ForceBuild || description.Line.ForceBuild,
                    StartPosition = startPosition + paddingOffset,
                    EndPosition = endPosition - paddingOffset
                };

                _lineHandles[currentPositionIndex] = UIFactoryManager.BuildMesh(lineDescription, result, _lineHandles[currentPositionIndex]);
            }
        }

        public override void Dispose()
        {
            foreach (var handle in _lineHandles)
            {
                handle.Dispose();
            }

            ListPool<MeshHandle>.Release(_lineHandles);
            _lineHandles = null;
        }
    }
}