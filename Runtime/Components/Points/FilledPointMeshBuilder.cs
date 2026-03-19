using System;
using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Points
{
    internal sealed class FilledPointMeshBuilder : MeshBuilder<FilledPointMeshDescription>
    {
        private MeshData _resultCircle;
        private MeshData _resultQuad;
        private MeshData _resultTriangle;

        public override void Init()
        {
        }

        protected override void Build(FilledPointMeshDescription description, List<MeshData> result)
        {
            switch (description.Shape)
            {
                case PointShape.Circle:
                {
                    if (!_resultCircle.Inited) _resultCircle = MeshData.AllocateCircle();
                    MeshUtils.CreateCircleMesh(ref _resultCircle, radius: 0.5f * description.Size, description.Origin, description.Color);
                    result.Add(_resultCircle);
                    break;
                }
                case PointShape.Square:
                {
                    if (!_resultQuad.Inited) _resultQuad = MeshData.AllocateQuad();
                    MeshUtils.CreateRectangleMesh(ref _resultQuad, angleAroundOriginInDeg: 180f, Vector2.one * description.Size, description.Origin, description.Color);
                    result.Add(_resultQuad);
                    break;
                }
                case PointShape.Triangle:
                {
                    if (!_resultTriangle.Inited) _resultTriangle = MeshData.AllocateTriangle();
                    MeshUtils.CreateEquilateralTriangle(ref _resultTriangle, angleAroundOriginInDeg: 180f, description.Size, description.Origin, description.Color);
                    result.Add(_resultTriangle);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Dispose()
        {
            _resultCircle.Dispose();
            _resultQuad.Dispose();
            _resultTriangle.Dispose();
        }
    }
}