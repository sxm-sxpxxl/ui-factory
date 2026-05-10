using System.Collections.Generic;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Points;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private List<MeshHandle> _pointHandles;
        private FilledPointMeshDescription _reusableFilled;
        private OutlinedPointMeshDescription _reusableOutlined;

        protected override void Build(PointSeriesMeshDescription description, List<MeshData> result)
        {
            var positions = description.Positions.Collection;
            var positionsCount = positions.Count;

            if (positionsCount == 0)
                return;

            _pointHandles ??= ListPool<MeshHandle>.Get();
            _pointHandles.ResizeHandles(positionsCount);

            var ignoredIndices = description.IgnoredPointIndices?.Collection;
            var selectionIndices = description.SelectionPointIndices?.Collection;

            for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
            {
                if (ignoredIndices != null && ignoredIndices.Contains(positionIndex))
                    continue;

                var position = positions[positionIndex];

                var sourceDescription = selectionIndices?.Contains(positionIndex) ?? false ? description.SelectionPoint : description.Point;
                sourceDescription ??= description.Point;

                var reusableDescription = GetReusableMatching(sourceDescription);
                if (reusableDescription == null)
                    continue;

                reusableDescription.Origin = position;

                _pointHandles[positionIndex] = UIFactoryManager.BuildMesh(reusableDescription, result, _pointHandles[positionIndex]);
            }

            return;

            [CanBeNull]
            PointMeshDescription GetReusableMatching(PointMeshDescription source)
            {
                switch (source)
                {
                    case FilledPointMeshDescription filled:
                    {
                        _reusableFilled ??= new FilledPointMeshDescription(default, 0f, default, ForceBuild: true);
                        _reusableFilled.Color = filled.Color;
                        _reusableFilled.Size = filled.Size;
                        _reusableFilled.Shape = filled.Shape;
                        return _reusableFilled;
                    }
                    case OutlinedPointMeshDescription outlined:
                    {
                        _reusableOutlined ??= new OutlinedPointMeshDescription(null, 0f, default, ForceBuild: true);
                        _reusableOutlined.Outline = outlined.Outline;
                        _reusableOutlined.Size = outlined.Size;
                        _reusableOutlined.Shape = outlined.Shape;
                        return _reusableOutlined;
                    }
                    default:
                    {
                        return null;
                    }
                }
            }
        }

        public override void Dispose()
        {
            if (_pointHandles == null)
                return;

            foreach (var handle in _pointHandles)
            {
                handle.Dispose();
            }

            ListPool<MeshHandle>.Release(_pointHandles);
            _pointHandles = null;
        }
    }
}