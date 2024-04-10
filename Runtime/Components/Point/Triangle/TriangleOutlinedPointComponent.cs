using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class TriangleOutlinedPointComponent : MeshComponent<TriangleOutlinedPointParameters>
    {
        private Guid? _lineSeriesCachedId;

        protected override void BuildInternal(ref List<MeshData> meshes, TriangleOutlinedPointParameters parameters)
        {
            Assert.IsTrue(parameters.Size > 0f, "parameters.Size > 0f");
            Assert.IsNotNull(parameters.LineParameters, "parameters.LineParameters != null");

            var points = MeshUtils.GetVerticesOnEquilateralTriangle(
                parameters.Size,
                parameters.Origin,
                angleAroundOriginInDeg: 180f
            );

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