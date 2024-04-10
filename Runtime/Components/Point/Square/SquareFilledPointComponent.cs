using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class SquareFilledPointComponent : MeshComponent<SquareFilledPointParameters>
    {
        protected override void BuildInternal(ref List<MeshData> meshes, SquareFilledPointParameters parameters)
        {
            Assert.IsTrue(parameters.Size > 0f, "parameters.Size > 0f");

            meshes.Add(new MeshData(new MeshData.QuadRequest {QuadsCount = 1}));
            MeshUtils.FillRectangle(meshes[0], parameters.Size * Vector2.one, parameters.Origin, angleAroundOriginInDeg: 180f, parameters.Color);
        }
    }
}