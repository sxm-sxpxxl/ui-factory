using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class DashLineSegmentComponent : BaseLineSegmentComponent<DashLineSegmentParameters>
    {
        private Guid? _lineSeriesCachedId;

        protected override void BuildInternal(ref List<MeshData> meshes, DashLineSegmentParameters parameters)
        {
            Assert.IsTrue(parameters.DashWidth >= 0f, "parameters.DashWidth >= 0f");
            Assert.IsTrue(parameters.DashGap >= 0f, "parameters.DashGap >= 0f");
            Assert.IsTrue(parameters.DashWidth >= parameters.DashGap, "parameters.DashWidth >= parameters.DashGap");

            var dashWidth = parameters.DashWidth;
            var dashGap = parameters.DashGap;

            var dashCount = Mathf.CeilToInt((parameters.LineLength + dashGap) / (dashWidth + dashGap));
            var pointsCount = dashCount + 1;

            var points = new Vector2[pointsCount];
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = Vector2.Lerp(parameters.StartPoint, parameters.EndPoint, (float) i / dashCount);
            }

            var instance = UIMeshFactory
                .Build(new LineSeriesParameters
                {
                    ForceBuild = parameters.ForceBuild,
                    Points = points,
                    PointOffset = 0.5f * parameters.DashGap,
                    LineParameters = new SolidLineParameters
                    {
                        ForceBuild = parameters.ForceBuild,
                        Thickness = parameters.Thickness,
                        Color = parameters.Color
                    }
                }, _lineSeriesCachedId)
                .CacheComponentIdTo(ref _lineSeriesCachedId);

            meshes.AddRange(instance.Data);
        }
    }
}