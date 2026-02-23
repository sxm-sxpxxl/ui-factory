using System.Collections.Generic;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory.MeshDataAdapters
{
    public static class UIElementsMeshDataAdapter
    {
        public static void SetData(this MeshGenerationContext context, IReadOnlyList<MeshData> data)
        {
            for (var index = 0; index < data.Count; index++)
            {
                var meshData = data[index];
                var meshWriteData = context.Allocate(meshData.Vertices.Length, meshData.Indices.Length);

                meshWriteData.SetAllVertices(meshData.Vertices);
                meshWriteData.SetAllIndices(meshData.Indices);
            }
        }
    }
}