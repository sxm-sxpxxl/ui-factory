using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal sealed class SolidLineSegmentComponent : BaseLineSegmentComponent<SolidLineSegmentParameters>
    {
        protected override void BuildInternal(ref List<MeshData> meshes, SolidLineSegmentParameters parameters)
        {
            Assert.IsTrue(parameters.Thickness > 0f, "parameters.Thickness > 0f");

            meshes.Add(new MeshData(new MeshData.QuadRequest {QuadsCount = 1}));
            MeshUtils.FillLine(meshes[0], parameters.StartPoint, parameters.EndPoint, parameters.Thickness, parameters.Color);
        }
    }
}