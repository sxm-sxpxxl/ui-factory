using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components.Lines
{
    public sealed record DashLineMeshDescription(
        float DashWidth,
        float DashGap,
        int Thickness,
        Color Color,
        Vector2 StartPosition = default,
        Vector2 EndPosition = default,
        bool ForceBuild = default
    ) : LineMeshDescription(Thickness, Color, StartPosition, EndPosition, ForceBuild)
    {
        public DashLineMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<float>("dash_width"),
            rawData.Get<float>("dash_gap"),
            rawData.Get<int>("thickness"),
            rawData.Get<Color>("color"),
            rawData.GetOrDefault<Vector2>("start_position", default),
            rawData.GetOrDefault<Vector2>("end_position", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        public override IMeshBuilder ConstructBuilder() => new DashLineMeshBuilder();
    }
}