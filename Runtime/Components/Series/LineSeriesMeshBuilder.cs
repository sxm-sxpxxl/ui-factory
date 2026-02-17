using System;
using System.Collections.Generic;
using System.Linq;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class LineSeriesMeshBuilder : MeshBuilder<LineSeriesMeshDescription>
    {
        private readonly List<MeshHandle> _lineHandles = new();

        protected override IEnumerable<MeshData> Build(LineSeriesMeshDescription description)
        {
            if (description.Positions.Count == 0)
                return Array.Empty<MeshData>();

            var linesCount = description.Positions.Count - (description.Closed ? 0 : 1);

            if (linesCount != _lineHandles.Count)
            {
                DisposeHandles();
            }

            IEnumerable<MeshData> linesData = Array.Empty<MeshData>();
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

                if (currentPositionIndex == _lineHandles.Count)
                {
                    _lineHandles.Add(new MeshHandle());
                }

                var lineData = UIFactoryManager.Build(lineDescription, _lineHandles[currentPositionIndex]);
                linesData = linesData.Concat(lineData);
            }

            return linesData;
        }

        public override void Dispose()
        {
            DisposeHandles();
        }

        private void DisposeHandles()
        {
            _lineHandles.ForEach(handle => handle.Dispose());
            _lineHandles.Clear();
        }
    }
}