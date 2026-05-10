using System.Collections.Generic;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        private MeshData _result;

        public override void Init()
        {
            _result = MeshData.AllocateQuads(count: 1);
        }

        protected override void Build(SolidLineMeshDescription description, List<MeshData> result)
        {
            MeshUtils.CreateLineMesh(ref _result, description.StartPosition, description.EndPosition, description.Thickness, description.Color);
            result.Add(_result);
        }

        public override void Dispose() => _result.Dispose();
    }
}