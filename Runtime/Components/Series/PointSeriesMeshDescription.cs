using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Points;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    public sealed record PointSeriesMeshDescription(
        PointMeshDescription Point,
        Snapshot<VersionedList<Vector2>> Positions,
        [CanBeNull] Snapshot<VersionedHashSet<int>>? IgnoredPointIndices = default,
        [CanBeNull] PointMeshDescription SelectionPoint = default,
        [CanBeNull] Snapshot<VersionedHashSet<int>>? SelectionPointIndices = default,
        bool ForceBuild = default
    ) : SeriesMeshDescription(Positions, ForceBuild)
    {
        internal override IMeshBuilder ConstructBuilder() => new PointSeriesMeshBuilder();
    }
}