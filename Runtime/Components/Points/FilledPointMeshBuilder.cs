using System;
using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    internal sealed class FilledPointMeshBuilder : MeshBuilder<FilledPointMeshDescription>
    {
        private List<MeshData> _result;

        public override void Init()
        {
            Debug.Log("FilledPointMeshBuilder Init");
            _result = new List<MeshData>(capacity: 3)
            {
                MeshData.AllocateCircle(),
                MeshData.AllocateQuad(),
                MeshData.AllocateTriangle()
            };
        }

        protected override IReadOnlyList<MeshData> Build(FilledPointMeshDescription description)
        {
            switch (description.Shape)
            {
                case PointShape.Circle:
                {
                    MeshUtils.CreateCircleMesh(_result[0], radius: 0.5f * description.Size, description.Origin, description.Color);
                    break;
                }
                case PointShape.Square:
                {
                    MeshUtils.CreateRectangleMesh(_result[1], angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin, description.Color);
                    break;
                }
                case PointShape.Triangle:
                {
                    MeshUtils.CreateEquilateralTriangle(_result[2], angleAroundOriginInDeg: 180f, description.Size, description.Origin, description.Color);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return _result;
        }

        public override void Dispose()
        {
            _result[0].Dispose();
            _result[1].Dispose();
            _result[2].Dispose();
        }
    }
}