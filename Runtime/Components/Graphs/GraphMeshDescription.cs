using System.Collections.Generic;
using Sxm.UIFactory.Components.Lines;
using Sxm.UIFactory.Components.Points;
using UnityEngine;

namespace Sxm.UIFactory.Components.Graphs
{
    public sealed record GraphMeshDescription(
        IList<Vector2> Positions,
        SolidLineMeshDescription Line,
        PointMeshDescription Point,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public GraphMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<IList<Vector2>>("positions"),
            rawData.Get<SolidLineMeshDescription>("line"),
            rawData.Get<PointMeshDescription>("point"),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new GraphMeshBuilder();
    }
}