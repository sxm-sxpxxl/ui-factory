using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal class LineSeriesComponent : MeshComponent<LineSeriesParameters>
    {
        private List<Guid?> _linesCachedIds;

        protected override void BuildInternal(ref List<MeshData> meshes, LineSeriesParameters parameters)
        {
            Assert.IsNotNull(parameters.Points, "parameters.Points != null");
            Assert.IsTrue(parameters.PointOffset >= 0f, "parameters.PointOffset >= 0f");
            Assert.IsNotNull(parameters.LineParameters);

            if (parameters.Points.Count == 0)
                return;

            var linesCount = parameters.Points.Count - (parameters.Closed ? 0 : 1);
            if (_linesCachedIds == null || _linesCachedIds.Count != linesCount)
            {
                _linesCachedIds = GetRawCachedIds(linesCount);
            }

            for (var currentPointIndex = 0; currentPointIndex < linesCount; currentPointIndex++)
            {
                var isLastLine = currentPointIndex == linesCount - 1;
                var nextPointIndex = isLastLine && parameters.Closed ? 0 : (currentPointIndex + 1);

                var startPoint = parameters.Points[currentPointIndex];
                var endPoint = parameters.Points[nextPointIndex];

                var direction = (endPoint - startPoint).normalized;
                var delta = parameters.PointOffset * direction;

                var lineSegmentParameters = parameters.LineParameters.CloneAsSegment();

                lineSegmentParameters.ForceBuild = parameters.ForceBuild || parameters.LineParameters.ForceBuild;
                lineSegmentParameters.StartPoint = startPoint + delta;
                lineSegmentParameters.EndPoint = endPoint - delta;
                lineSegmentParameters.Color = parameters.LineParameters.Color;

                var instance = UIMeshFactory
                    .Build(lineSegmentParameters, _linesCachedIds[currentPointIndex])
                    .CacheComponentIdTo(ref _linesCachedIds, sourceIndex: currentPointIndex);

                meshes.AddRange(instance.Data);
            }
        }

        private static List<Guid?> GetRawCachedIds(int count) => new List<Guid?>(new Guid?[count]);
    }
}