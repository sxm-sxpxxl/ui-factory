using System;
using System.Collections.Generic;
using System.Linq;

namespace Sxm.UIFactory.Components.Series
{
    internal sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private readonly List<MeshHandle> _pointHandles = new();

        protected override IEnumerable<MeshData> Build(PointSeriesMeshDescription description)
        {
            if (description.Positions.Count == 0)
                return Array.Empty<MeshData>();

            if (description.Positions.Count != _pointHandles.Count)
            {
                DisposeHandles();
            }

            IEnumerable<MeshData> pointsData = Array.Empty<MeshData>();
            for (var currentPositionIndex = 0; currentPositionIndex < description.Positions.Count; currentPositionIndex++)
            {
                var position = description.Positions[currentPositionIndex];
                var pointDescription = description.Point with
                {
                    ForceBuild = description.ForceBuild || description.Point.ForceBuild,
                    Origin = position
                };

                if (currentPositionIndex == _pointHandles.Count)
                {
                    _pointHandles.Add(new MeshHandle());
                }

                var pointData = UIFactoryManager.Build(pointDescription, _pointHandles[currentPositionIndex]);
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