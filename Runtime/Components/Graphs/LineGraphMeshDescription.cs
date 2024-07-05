using System.Collections.Generic;
using Sxm.UIFactory.Components.Lines;
using Sxm.UIFactory.Components.Points;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public sealed record LineGraphMeshDescription(
        IList<Vector2> Positions,
        LineMeshDescription Line,
        PointMeshDescription Point,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public LineGraphMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<IList<Vector2>>("positions"),
            rawData.Get<LineMeshDescription>("line"),
            rawData.Get<PointMeshDescription>("point"),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        public override IMeshBuilder ConstructBuilder() => new LineGraphMeshBuilder();
    }
}