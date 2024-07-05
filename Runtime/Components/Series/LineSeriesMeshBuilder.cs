using System;
using System.Collections.Generic;
using System.Linq;

namespace Sxm.UIFactory.Components.Series
{
    public sealed class LineSeriesMeshBuilder : MeshBuilder<LineSeriesMeshDescription>
    {
        private Guid?[] _cachedLineIds;

        public override IEnumerable<MeshData> Build(LineSeriesMeshDescription description)
        {
            if (description.Positions.Count == 0)
                return Array.Empty<MeshData>();

            var linesCount = description.Positions.Count - (description.Closed ? 0 : 1);
            if (_cachedLineIds == null || _cachedLineIds.Length != linesCount)
            {
                // todo@sxm: maybe should use ArrayPool for best performance? But first need to evaluate the effectiveness of the solution with GC (#1)
                _cachedLineIds = new Guid?[linesCount];
            }

            IEnumerable<MeshData> result = Array.Empty<MeshData>();
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

                var instance = UIFactoryManager.Build(lineDescription, _cachedLineIds[currentPositionIndex]).CacheAssignedMeshBuilderId(ref _cachedLineIds, sourceIndex: currentPositionIndex);
                result = result.Concat(instance.Result);
            }

            return result;
        }
    }
}