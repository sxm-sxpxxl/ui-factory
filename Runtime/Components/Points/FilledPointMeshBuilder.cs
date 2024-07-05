using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components.Points
{
    public sealed class FilledPointMeshBuilder : MeshBuilder<FilledPointMeshDescription>
    {
        public override IEnumerable<MeshData> Build(FilledPointMeshDescription description)
        {
            yield return description.Shape switch
            {
                PointShape.Circle => MeshUtils.CreateCircleMesh(resolution: 32, 0.5f * description.Size, description.Origin, description.Color),
                PointShape.Square => MeshUtils.CreateRectangleMesh(angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin, description.Color),
                PointShape.Triangle => MeshUtils.CreateEquilateralTriangle(angleAroundOriginInDeg: 180f, description.Size, description.Origin, description.Color),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}