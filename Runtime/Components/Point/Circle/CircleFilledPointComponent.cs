using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal class CircleFilledPointComponent : MeshComponent<CircleFilledPointParameters>
    {
        private const int Resolution = 32;

        protected override void BuildInternal(ref List<MeshData> meshes, CircleFilledPointParameters parameters)
        {
            Assert.IsTrue(parameters.Size > 0f, "parameters.Size > 0f");

            meshes.Add(new MeshData(new MeshData.TriangleSameVertexRequest {TriangleOrVertexCount = Resolution + 1}));

            MeshUtils.FillCircle(
                meshes[0],
                0.5f * parameters.Size,
                Resolution,
                parameters.Origin,
                parameters.Color
            );
        }
    }
}