using SxmTools.UIFactory.Components.Lines;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    public sealed record OutlinedPointMeshDescription(
        SolidLineMeshDescription Outline,
        float Size,
        PointShape Shape,
        Vector2 Origin = default,
        bool ForceBuild = default
    ) : PointMeshDescription(Size, Shape, Origin, ForceBuild)
    {
        public SolidLineMeshDescription Outline { get; set; } = Outline;

        internal override IMeshBuilder ConstructBuilder() => new OutlinedPointMeshBuilder();
    }
}