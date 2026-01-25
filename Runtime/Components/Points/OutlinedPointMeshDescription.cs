using System.Collections.Generic;
using Sxm.UIFactory.Components.Lines;
using UnityEngine;

namespace Sxm.UIFactory.Components.Points
{
    public sealed record OutlinedPointMeshDescription(
        SolidLineMeshDescription Outline,
        float Size,
        PointShape Shape,
        Vector2 Origin = default,
        bool ForceBuild = default
    ) : PointMeshDescription(Size, Shape, Origin, ForceBuild)
    {
        public OutlinedPointMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<SolidLineMeshDescription>("outline"),
            rawData.Get<float>("size"),
            rawData.Get<PointShape>("shape"),
            rawData.GetOrDefault<Vector2>("origin", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new OutlinedPointMeshBuilder();
    }
}