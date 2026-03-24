using System.Collections.Generic;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private List<MeshHandle> _pointHandles;

        protected override void Build(PointSeriesMeshDescription description, List<MeshData> result)
        {
            var positions = description.Positions.Collection;
            var positionsCount = positions.Count;

            if (positionsCount == 0)
                return;

            _pointHandles ??= ListPool<MeshHandle>.Get();
            _pointHandles.ResizeHandles(positionsCount);

            var ignoredIndices = description.IgnoredPointIndices?.Collection;

            for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
            {
                if (ignoredIndices != null && ignoredIndices.Contains(positionIndex))
                    continue;

                var position = positions[positionIndex];
                var pointDescription = description.Point with
                {
                    ForceBuild = description.ForceBuild || description.Point.ForceBuild,
                    Origin = position
                };

                _pointHandles[positionIndex] = UIFactoryManager.BuildMesh(pointDescription, result, _pointHandles[positionIndex]);
            }
        }

        public override void Dispose()
        {
            foreach (var handle in _pointHandles)
            {
                handle.Dispose();
            }

            ListPool<MeshHandle>.Release(_pointHandles);
            _pointHandles = null;
        }
    }
}