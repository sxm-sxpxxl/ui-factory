using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Points;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    public sealed record PointSeriesMeshDescription(
        PointMeshDescription Point,
        Snapshot<VersionedList<Vector2>> Positions,
        Snapshot<VersionedHashSet<int>>? IgnoredPointIndices = default,
        [CanBeNull] PointMeshDescription SelectionPoint = default,
        Snapshot<VersionedHashSet<int>>? SelectionPointIndices = default,
        bool ForceBuild = default
    ) : SeriesMeshDescription(Positions, ForceBuild)
    {
        public PointMeshDescription Point { get; set; } = Point;
        public Snapshot<VersionedHashSet<int>>? IgnoredPointIndices { get; set; } = IgnoredPointIndices;
        [CanBeNull] public PointMeshDescription SelectionPoint { get; set; } = SelectionPoint;
        public Snapshot<VersionedHashSet<int>>? SelectionPointIndices { get; set; } = SelectionPointIndices;

        internal override IMeshBuilder ConstructBuilder() => new PointSeriesMeshBuilder();
    }
}