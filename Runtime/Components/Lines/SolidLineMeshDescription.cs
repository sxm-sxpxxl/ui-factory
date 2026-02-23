using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    public sealed record SolidLineMeshDescription(
        int Thickness,
        Color32 Color,
        Vector2 StartPosition = default,
        Vector2 EndPosition = default,
        bool ForceBuild = default
    ) : LineMeshDescription(Thickness, Color, StartPosition, EndPosition, ForceBuild)
    {
        public SolidLineMeshDescription(IDictionary<string, object> rawData) : this(
            rawData.Get<int>("thickness"),
            rawData.Get<Color32>("color"),
            rawData.GetOrDefault<Vector2>("start_position", default),
            rawData.GetOrDefault<Vector2>("end_position", default),
            rawData.GetOrDefault<bool>("force_build", default)
        )
        {
        }

        internal override IMeshBuilder ConstructBuilder() => new SolidLineMeshBuilder();
    }
}