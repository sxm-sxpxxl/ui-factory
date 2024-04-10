using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class GraphComponent : MeshComponent<GraphParameters>
    {
        private Guid? _pointSeriesCachedId;
        private Guid? _lineSeriesCachedId;

        protected override void BuildInternal(ref List<MeshData> meshes, GraphParameters parameters)
        {
            Assert.IsNotNull(parameters.Points, "parameters.Points != null");
            Assert.IsNotNull(parameters.DotParameters, "parameters.DotParameters != null");
            Assert.IsNotNull(parameters.LineParameters, "parameters.LineParameters != null");

            var lineSeriesParameters = new LineSeriesParameters
            {
                ForceBuild = parameters.ForceBuild,
                LineParameters = parameters.LineParameters,
                Points = parameters.Points,
                PointOffset = 0f,
                Closed = false
            };

            var pointSeriesParameters = new PointSeriesParameters
            {
                ForceBuild = parameters.ForceBuild,
                DotParameters = parameters.DotParameters,
                Points = parameters.Points
            };

            var lineSeries = UIMeshFactory
                .Build(lineSeriesParameters, _lineSeriesCachedId)
                .CacheComponentIdTo(ref _lineSeriesCachedId);

            var pointSeries = UIMeshFactory
                .Build(pointSeriesParameters, _pointSeriesCachedId)
                .CacheComponentIdTo(ref _pointSeriesCachedId);

            meshes.AddRange(lineSeries.Data);
            meshes.AddRange(pointSeries.Data);
        }
    }
}