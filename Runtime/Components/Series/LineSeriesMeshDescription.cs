using System.Collections.Generic;
using Sxm.UIFactory.Components.Lines;
using UnityEngine;

namespace Sxm.UIFactory.Components.Series
{
    public sealed record LineSeriesMeshDescription(
        LineMeshDescription Line,
        float Padding,
        bool Closed,
        IList<Vector2> Positions,
        bool ForceBuild = default
    ) : SeriesMeshDescription(Positions, ForceBuild)
    {
        public LineSeriesMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<LineMeshDescription>("line"),
            rawData.Get<float>("padding"),
            rawData.Get<bool>("closed"),
            rawData.Get<IList<Vector2>>("positions"),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new LineSeriesMeshBuilder();
    }
}