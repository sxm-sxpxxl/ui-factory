using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class TriangleFilledPointComponent : MeshComponent<TriangleFilledPointParameters>
    {
        protected override void BuildInternal(ref List<MeshData> meshes, TriangleFilledPointParameters parameters)
        {
            Assert.IsTrue(parameters.Size > 0f, "parameters.Size > 0f");

            meshes.Add(new MeshData(new MeshData.TriangleNotSameVertexRequest {TrianglesCount = 1, VerticesCount = 3}));
            MeshUtils.FillEquilateralTriangle(
                meshes[0],
                parameters.Size,
                parameters.Origin,
                angleAroundOriginInDeg: 180f,
                parameters.Color
            );
        }
    }
}