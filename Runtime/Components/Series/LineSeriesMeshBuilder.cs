using System.Collections.Generic;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class LineSeriesMeshBuilder : MeshBuilder<LineSeriesMeshDescription>
    {
        private List<MeshHandle> _lineHandles;

        protected override void Build(LineSeriesMeshDescription description, List<MeshData> result)
        {
            var linesCount = description.Positions.Count - (description.Closed ? 0 : 1);

            if (linesCount <= 0)
                return;

            _lineHandles ??= ListPool<MeshHandle>.Get();
            ResizeHandles(_lineHandles, linesCount);

            for (var currentPositionIndex = 0; currentPositionIndex < linesCount; currentPositionIndex++)
            {
                var isLastLine = currentPositionIndex == linesCount - 1;
                var nextPositionIndex = isLastLine && description.Closed ? 0 : currentPositionIndex + 1;

                var startPosition = description.Positions[currentPositionIndex];
                var endPosition = description.Positions[nextPositionIndex];

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

        private static void ResizeHandles(List<MeshHandle> handles, int targetCount)
        {
            for (var i = handles.Count - 1; i >= targetCount; i--)
            {
                handles[i].Dispose();
                handles.RemoveAt(i);
            }

            for (var i = handles.Count; i < targetCount; i++)
            {
                handles.Add(new MeshHandle());
            }
        }
    }
}