using System.Collections.Generic;

namespace Sxm.UIFactory.Components.Lines
{
    public sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        // todo@sxm: add description validators through attributes?
        public override IEnumerable<MeshData> Build(SolidLineMeshDescription description)
        {
            // todo@sxm: use nullable value types? and validation before use?
            yield return MeshUtils.CreateLineMesh(description.StartPosition, description.EndPosition, description.Thickness, description.Color);
        }
    }
}