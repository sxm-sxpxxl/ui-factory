using System;
using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    internal sealed class PointSeriesMeshBuilder : MeshBuilder<PointSeriesMeshDescription>
    {
        private readonly List<MeshHandle> _pointHandles = new();
        private List<MeshData> _result;

        protected override IReadOnlyList<MeshData> Build(PointSeriesMeshDescription description)
        {
            var positionsCount = description.Positions.Count;

            if (positionsCount == 0)
                return Array.Empty<MeshData>();

            if (positionsCount != _pointHandles.Count)
            {
                Dispose();
            }
            _result ??= new List<MeshData>(capacity: positionsCount);
            _result.Clear();

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

                var meshData = UIFactoryManager.Build(pointDescription, _pointHandles[positionIndex]);
                _result.AddRange(meshData);
            }

            // Debug.Log($"_result.Count: {_result.Count}");

            return _result;
        }

        public override void Dispose()
        {
            DisposeHandles();
            DisposeResult();
        }

        private void DisposeHandles()
        {
            _pointHandles.ForEach(handle => handle.Dispose());
            _pointHandles.Clear();
        }

        private void DisposeResult()
        {
            // _result.ForEach(meshData => meshData.Dispose());
            // _result.Clear();
        }
    }
}