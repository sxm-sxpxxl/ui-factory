using System.Collections.Generic;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        private readonly List<MeshData> _result = new(capacity: 1) {default};

        protected override IReadOnlyList<MeshData> Build(SolidLineMeshDescription description)
        {
            _result[0] = MeshUtils.CreateLineMesh(description.StartPosition, description.EndPosition, description.Thickness, description.Color);
            return _result;
        }
    }
}