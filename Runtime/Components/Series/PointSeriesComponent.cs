using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class PointSeriesComponent : MeshComponent<PointSeriesParameters>
    {
        private Guid?[] _cachedPointsIds;

        // todo@sxm: maybe a yield generator will be best decision
        protected override void BuildInternal(ref List<MeshData> meshes, PointSeriesParameters parameters)
        {
            Assert.IsNotNull(parameters.Points, "parameters.Points != null");
            Assert.IsNotNull(parameters.DotParameters, "parameters.DotParameters != null");

            if (parameters.Points.Count == 0)
                return;

            if (_cachedPointsIds == null || _cachedPointsIds.Length != parameters.Points.Count)
            {
                // todo@sxm: maybe should use ArrayPool for best performance? But first need to evaluate the effectiveness of the solution with GC (#2)
                _cachedPointsIds = new Guid?[parameters.Points.Count];
            }

            for (var i = 0; i < parameters.Points.Count; i++)
            {
                var pointParameters = parameters.DotParameters.CloneAsPoint();

                pointParameters.ForceBuild = parameters.ForceBuild || parameters.DotParameters.ForceBuild;
                pointParameters.Origin = parameters.Points[i];
                pointParameters.Color = parameters.DotParameters.Color;

                var instance = UIMeshFactory
                    .Build(pointParameters, _cachedPointsIds[i])
                    .CacheComponentIdTo(ref _cachedPointsIds, sourceIndex: i);

                meshes.AddRange(instance.Data);
            }
        }
    }
}