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
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public GraphMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<IList<Vector2>>("positions"),
            rawData.Get<SolidLineMeshDescription>("line"),
            rawData.GetOrDefault<PointMeshDescription>("point", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new GraphMeshBuilder();
    }
}