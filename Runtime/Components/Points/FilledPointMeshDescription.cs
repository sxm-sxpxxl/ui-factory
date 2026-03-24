using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    public sealed record FilledPointMeshDescription(
        Color32 Color,
        float Size,
        PointShape Shape,
        Vector2 Origin = default,
        bool ForceBuild = default
    ) : PointMeshDescription(Size, Shape, Origin, ForceBuild)
    {
        internal override IMeshBuilder ConstructBuilder() => new FilledPointMeshBuilder();
    }
}