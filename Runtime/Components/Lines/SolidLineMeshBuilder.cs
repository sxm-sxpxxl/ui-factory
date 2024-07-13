using System.Collections.Generic;

namespace Sxm.UIFactory.Components.Lines
{
    internal sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        // todo@sxm: add description validators through attributes?
        protected override IEnumerable<MeshData> Build(SolidLineMeshDescription description)
        {
            // todo@sxm: use nullable value types? and validation before use?
            yield return MeshUtils.CreateLineMesh(description.StartPosition, description.EndPosition, description.Thickness, description.Color);
        }
    }
}