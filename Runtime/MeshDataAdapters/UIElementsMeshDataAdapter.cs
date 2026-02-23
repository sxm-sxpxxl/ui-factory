using System.Collections.Generic;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory.MeshDataAdapters
{
    public static class UIElementsMeshDataAdapter
    {
        public static void SetData(this MeshGenerationContext context, IEnumerable<MeshData> data)
        {
            foreach (var meshData in data)
            {
                var meshWriteData = context.Allocate(meshData.Vertices.Length, meshData.Indices.Length);
                meshWriteData.SetAllVertices(meshData.Vertices);
                meshWriteData.SetAllIndices(meshData.Indices);
            }
        }
    }
}