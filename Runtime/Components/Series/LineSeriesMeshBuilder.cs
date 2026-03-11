using System.Collections.Generic;

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

            _lineHandles ??= new List<MeshHandle>(capacity: linesCount);

            if (linesCount != _lineHandles.Count)
            {
                DisposeHandles();
            }

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

                _lineHandles[currentPositionIndex] = UIFactoryManager.BuildMesh(lineDescription, result, _lineHandles[currentPositionIndex]);
            }
        }

        public override void Dispose()
        {
            DisposeHandles();
        }

        private void DisposeHandles()
        {
            foreach (var handle in _lineHandles) handle.Dispose();
            _lineHandles.Clear();
        }
    }
}