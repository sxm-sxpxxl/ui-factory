using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        private List<MeshData> _result;

        public override void Init()
        {
            Debug.Log("SolidLineMeshBuilder Init");
            _result = new List<MeshData>(capacity: 1) {MeshData.AllocateQuad()};
        }

        protected override IReadOnlyList<MeshData> Build(SolidLineMeshDescription description)
        {
            MeshUtils.CreateLineMesh(_result[0], description.StartPosition, description.EndPosition, description.Thickness, description.Color);
            return _result;
        }

        public override void Dispose() => _result[0].Dispose();
    }
}