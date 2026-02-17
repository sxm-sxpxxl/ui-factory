using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    public abstract record LineMeshDescription(int Thickness, Color Color, Vector2 StartPosition = default, Vector2 EndPosition = default, bool ForceBuild = default) : MeshDescription(ForceBuild)
    {
        public float LineLength => (EndPosition - StartPosition).magnitude;
        public Vector2 LineDirection => (EndPosition - StartPosition).normalized;
    }
}