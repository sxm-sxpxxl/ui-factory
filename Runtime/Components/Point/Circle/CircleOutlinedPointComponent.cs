using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class CircleOutlinedPointComponent : MeshComponent<CircleOutlinedPointParameters>
    {
        private const int Resolution = 32;

        private Guid? _lineSeriesCachedId;

        protected override void BuildInternal(ref List<MeshData> meshes, CircleOutlinedPointParameters parameters)
        {
            Assert.IsTrue(parameters.Size > 0f, "parameters.Size > 0f");
            Assert.IsNotNull(parameters.LineParameters, "parameters.LineParameters != null");

            var points = MeshUtils.GetVerticesOnCircumference(
                0.5f * parameters.Size,
                Resolution,
                hasOriginPoint: false,
                parameters.Origin
            );

            parameters.LineParameters.Color = parameters.Color;

            var instance = UIMeshFactory
                .Build(new LineSeriesParameters
                {
                    ForceBuild = parameters.ForceBuild,
                    Points = points,
                    PointOffset = 0f,
                    LineParameters = parameters.LineParameters,
                    Closed = true
                }, _lineSeriesCachedId)
                .CacheComponentIdTo(ref _lineSeriesCachedId);

            meshes.AddRange(instance.Data);
        }
    }
}