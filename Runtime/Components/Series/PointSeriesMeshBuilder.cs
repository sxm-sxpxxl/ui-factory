using System.Collections.Generic;
using UnityEngine;
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

            if (positionsCount != _pointHandles.Count)
            {
                // todo@sxm: странный момент, здесь нужно не диспоузить и умно переиспользовать уже выделенную память
                DisposeHandles();
            }

            for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
            {
                // todo@sxm: странный момент, почему не после IgnoredPointIndices?
                if (positionIndex == _pointHandles.Count)
                {
                    _pointHandles.Add(new MeshHandle());
                }

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
            DisposeHandles();

            ListPool<MeshHandle>.Release(_pointHandles);
            _pointHandles = null;
        }

        private void DisposeHandles()
        {
            foreach (var handle in _pointHandles)
            {
                handle.Dispose();
            }
            _pointHandles.Clear();
        }
    }
}