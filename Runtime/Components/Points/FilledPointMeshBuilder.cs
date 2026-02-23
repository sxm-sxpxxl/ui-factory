using System;
using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    internal sealed class FilledPointMeshBuilder : MeshBuilder<FilledPointMeshDescription>
    {
        private readonly List<MeshData> _result = new(capacity: 1) {default};

        protected override IReadOnlyList<MeshData> Build(FilledPointMeshDescription description)
        {
            _result[0] = description.Shape switch
            {
                PointShape.Circle => MeshUtils.CreateCircleMesh(resolution: 32, 0.5f * description.Size, description.Origin, description.Color),
                PointShape.Square => MeshUtils.CreateRectangleMesh(angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin, description.Color),
                PointShape.Triangle => MeshUtils.CreateEquilateralTriangle(angleAroundOriginInDeg: 180f, description.Size, description.Origin, description.Color),
                _ => throw new ArgumentOutOfRangeException()
            };
            return _result;
        }
    }
}