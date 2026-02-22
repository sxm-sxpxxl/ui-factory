using System;
using System.Collections.Generic;
using System.Linq;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private readonly List<MeshHandle> _pointHandles = new();

        protected override IEnumerable<MeshData> Build(PointSeriesMeshDescription description)
        {
            var positionsCount = description.Positions.Count;

            if (positionsCount == 0)
                return Array.Empty<MeshData>();

            if (positionsCount != _pointHandles.Count)
            {
                DisposeHandles();
            }

            IEnumerable<MeshData> pointsData = Array.Empty<MeshData>();
            for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
            {
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

                var pointData = UIFactoryManager.Build(pointDescription, _pointHandles[positionIndex]);
                pointsData = pointsData.Concat(pointData);
            }

            return pointsData;
        }

        public override void Dispose()
        {
            DisposeHandles();
        }

        private void DisposeHandles()
        {
            _pointHandles.ForEach(handle => handle.Dispose());
            _pointHandles.Clear();
        }
    }
}