using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Lines;
using SxmTools.UIFactory.Components.Points;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Graphs
{
    public sealed record GraphMeshDescription(
        Snapshot<VersionedList<Vector2>> Positions,
        SolidLineMeshDescription Line,
        [CanBeNull] PointMeshDescription Point = default,
        Snapshot<VersionedHashSet<int>>? IgnoredPointIndices = default,
        [CanBeNull] PointMeshDescription SelectionPoint = default,
        Snapshot<VersionedHashSet<int>>? SelectionPointIndices = default,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public Snapshot<VersionedList<Vector2>> Positions { get; set; } = Positions;
        public SolidLineMeshDescription Line { get; set; } = Line;
        [CanBeNull] public PointMeshDescription Point { get; set; } = Point;
        public Snapshot<VersionedHashSet<int>>? IgnoredPointIndices { get; set; } = IgnoredPointIndices;
        [CanBeNull] public PointMeshDescription SelectionPoint { get; set; } = SelectionPoint;
        public Snapshot<VersionedHashSet<int>>? SelectionPointIndices { get; set; } = SelectionPointIndices;

        internal override IMeshBuilder ConstructBuilder() => new GraphMeshBuilder();
    }
}