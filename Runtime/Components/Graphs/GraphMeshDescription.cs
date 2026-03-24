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
        [CanBeNull] Snapshot<VersionedHashSet<int>>? IgnoredPointIndices = default,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        internal override IMeshBuilder ConstructBuilder() => new GraphMeshBuilder();
    }
}