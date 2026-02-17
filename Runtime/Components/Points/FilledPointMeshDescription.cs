using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    public sealed record FilledPointMeshDescription(
        Color Color,
        float Size,
        PointShape Shape,
        Vector2 Origin = default,
        bool ForceBuild = default
    ) : PointMeshDescription(Size, Shape, Origin, ForceBuild)
    {
        public FilledPointMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<Color>("color"),
            rawData.Get<float>("size"),
            rawData.Get<PointShape>("shape"),
            rawData.GetOrDefault<Vector2>("origin", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new FilledPointMeshBuilder();
    }
}