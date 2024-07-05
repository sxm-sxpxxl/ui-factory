using System;
using System.Collections.Generic;
using System.Linq;

namespace Sxm.UIFactory.Components.Series
{
    public sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private Guid?[] _cachedPointIds;

        public override IEnumerable<MeshData> Build(PointSeriesMeshDescription description)
        {
            if (description.Positions.Count == 0)
                return Array.Empty<MeshData>();


            if (_cachedPointIds == null || _cachedPointIds.Length != description.Positions.Count)
            {
                // todo@sxm: maybe should use ArrayPool for best performance? But first need to evaluate the effectiveness of the solution with GC (#2)
                _cachedPointIds = new Guid?[description.Positions.Count];
            }

            IEnumerable<MeshData> result = Array.Empty<MeshData>();
            for (var positionIndex = 0; positionIndex < description.Positions.Count; positionIndex++)
            {
                var pointDescription = description.Point with
                {
                    ForceBuild = description.ForceBuild || description.Point.ForceBuild,
                    Origin = description.Positions[positionIndex]
                };

                var instance = UIFactoryManager.Build(pointDescription, _cachedPointIds[positionIndex]).CacheAssignedMeshBuilderId(ref _cachedPointIds, sourceIndex: positionIndex);
                result = result.Concat(instance.Result);
            }

            return result;
        }
    }
}