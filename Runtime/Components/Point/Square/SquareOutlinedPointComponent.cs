using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class SquareOutlinedPointComponent : MeshComponent<SquareOutlinedPointParameters>
    {
        private Guid? _lineSeriesCachedId;

        protected override void BuildInternal(ref List<MeshData> meshes, SquareOutlinedPointParameters parameters)
        {
            Assert.IsTrue(parameters.Size > 0f, "parameters.Size > 0f");
            Assert.IsNotNull(parameters.LineParameters, "parameters.LineParameters != null");

            var points = MeshUtils.GetVerticesOnRectangle(
                MeshUtils.RectangleVerticesBuildOrder.Cyclic,
                parameters.Size * Vector2.one,
                parameters.Origin,
                angleAroundOriginInDeg: 180f
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