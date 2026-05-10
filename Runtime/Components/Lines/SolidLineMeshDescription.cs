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
        internal override IMeshBuilder ConstructBuilder() => new SolidLineMeshBuilder();
    }
}