using System.Collections.Generic;
using Sxm.UIFactory.Components.Points;
using UnityEngine;

namespace Sxm.UIFactory.Components.Series
{
    public sealed record PointSeriesMeshDescription(
        PointMeshDescription Point,
        IList<Vector2> Positions,
        bool ForceBuild = default
    ) : SeriesMeshDescription(Positions, ForceBuild)
    {
        public PointSeriesMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<PointMeshDescription>("point"),
            rawData.Get<IList<Vector2>>("positions"),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new PointSeriesMeshBuilder();
    }
}