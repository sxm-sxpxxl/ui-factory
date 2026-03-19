using System.Collections.Generic;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private List<MeshHandle> _pointHandles;

        protected override void Build(PointSeriesMeshDescription description, List<MeshData> result)
        {
            var positionsCount = description.Positions.Count;

            if (positionsCount == 0)
                return;

            _pointHandles ??= ListPool<MeshHandle>.Get();
            ResizeHandles(_pointHandles, positionsCount);

            for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
            {
                if (description.IgnoredPointIndices != null && description.IgnoredPointIndices.Contains(positionIndex))
                    continue;

                var position = description.Positions[positionIndex];
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