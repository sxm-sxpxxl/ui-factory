using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class SolidLineMeshBuilder : MeshBuilder<SolidLineMeshDescription>
    {
        private readonly MeshData[] _result = new MeshData[1];

        public override void Init()
        {
            // Debug.Log("SolidLineMeshBuilder Init");
            _result[0] = MeshData.AllocateQuad();
        }

        protected override IReadOnlyList<MeshData> Build(SolidLineMeshDescription description)
        {
            MeshUtils.CreateLineMesh(_result[0], description.StartPosition, description.EndPosition, description.Thickness, description.Color);
            return _result;
        }

        public override void Dispose() => _result[0].Dispose();
    }
}