using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    public abstract record PointMeshDescription(float Size, PointShape Shape, Vector2 Origin = default, bool ForceBuild = default) : MeshDescription(ForceBuild);
}