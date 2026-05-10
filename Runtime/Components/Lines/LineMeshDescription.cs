using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    public abstract record LineMeshDescription(
        int Thickness,
        Color32 Color,
        Vector2 StartPosition = default,
        Vector2 EndPosition = default,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public int Thickness { get; set; } = Thickness;
        public Color32 Color { get; set; } = Color;
        public Vector2 StartPosition { get; set; } = StartPosition;
        public Vector2 EndPosition { get; set; } = EndPosition;

        public float LineLength => (EndPosition - StartPosition).magnitude;
        public Vector2 LineDirection => (EndPosition - StartPosition).normalized;
    }
}