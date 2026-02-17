using System.Collections.Generic;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        protected override IEnumerable<MeshData> Build(SolidLineMeshDescription description)
        {
            yield return MeshUtils.CreateLineMesh(description.StartPosition, description.EndPosition, description.Thickness, description.Color);
        }
    }
}