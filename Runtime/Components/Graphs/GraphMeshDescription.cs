using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Lines;
using SxmTools.UIFactory.Components.Points;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Graphs
{
    public sealed record GraphMeshDescription(
        IList<Vector2> Positions,
        SolidLineMeshDescription Line,
        [CanBeNull] PointMeshDescription Point = default,
        [CanBeNull] HashSet<int> IgnoredPointIndices = default,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public GraphMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<IList<Vector2>>("positions"),
            rawData.Get<SolidLineMeshDescription>("line"),
            rawData.GetOrDefault<PointMeshDescription>("point", default),
            rawData.GetOrDefault<HashSet<int>>("ignored_point_indices", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        public bool Equals(GraphMeshDescription other) =>
            base.Equals((MeshDescription)other)
            && CollectionEquality.SequenceEqual(Positions, other?.Positions)
            && Equals(Line, other?.Line)
            && Equals(Point, other?.Point)
            && CollectionEquality.SetEqual(IgnoredPointIndices, other?.IgnoredPointIndices);

        public override int GetHashCode() =>
            HashCode.Combine(
                base.GetHashCode(),
                CollectionEquality.GetSequenceHashCode(Positions),
                Line,
                Point,
                CollectionEquality.GetSetHashCode(IgnoredPointIndices));

        internal override IMeshBuilder ConstructBuilder() => new GraphMeshBuilder();
    }
}