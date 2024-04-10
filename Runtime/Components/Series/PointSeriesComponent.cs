using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class PointSeriesComponent : MeshComponent<PointSeriesParameters>
    {
        private List<Guid?> _pointsCachedIds;

        protected override void BuildInternal(ref List<MeshData> meshes, PointSeriesParameters parameters)
        {
            Assert.IsNotNull(parameters.Points, "parameters.Points != null");
            Assert.IsNotNull(parameters.DotParameters, "parameters.DotParameters != null");

            if (parameters.Points.Count == 0)
                return;

            if (_pointsCachedIds == null || _pointsCachedIds.Count != parameters.Points.Count)
            {
                _pointsCachedIds = GetRawCachedIds(parameters.Points.Count);
            }

            for (var i = 0; i < parameters.Points.Count; i++)
            {
                var pointParameters = parameters.DotParameters.CloneAsPoint();

                pointParameters.ForceBuild = parameters.ForceBuild || parameters.DotParameters.ForceBuild;
                pointParameters.Origin = parameters.Points[i];
                pointParameters.Color = parameters.DotParameters.Color;

                var instance = UIMeshFactory
                    .Build(pointParameters, _pointsCachedIds[i])
                    .CacheComponentIdTo(ref _pointsCachedIds, sourceIndex: i);

                meshes.AddRange(instance.Data);
            }
        }

        private static List<Guid?> GetRawCachedIds(int count) => new List<Guid?>(new Guid?[count]);
    }
}