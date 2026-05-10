using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    public sealed record DashLineMeshDescription(
        float DashWidth,
        float DashGap,
        int Thickness,
        Color32 Color,
        Vector2 StartPosition = default,
        Vector2 EndPosition = default,
        bool ForceBuild = default
    ) : LineMeshDescription(Thickness, Color, StartPosition, EndPosition, ForceBuild)
    {
        public float DashWidth { get; set; } = DashWidth;
        public float DashGap { get; set; } = DashGap;

        internal override IMeshBuilder ConstructBuilder() => new DashLineMeshBuilder();
    }
}