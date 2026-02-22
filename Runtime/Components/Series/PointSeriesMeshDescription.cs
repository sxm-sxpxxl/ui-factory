using System.Collections.Generic;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Points;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    public sealed record PointSeriesMeshDescription(
        PointMeshDescription Point,
        IList<Vector2> Positions,
        [CanBeNull] HashSet<int> IgnoredPointIndices = default,
        bool ForceBuild = default
    ) : SeriesMeshDescription(Positions, ForceBuild)
    {
        public PointSeriesMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<PointMeshDescription>("point"),
            rawData.Get<IList<Vector2>>("positions"),
            rawData.GetOrDefault<HashSet<int>>("ignored_point_indices", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new PointSeriesMeshBuilder();
    }
}