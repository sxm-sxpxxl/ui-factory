using Unity.Collections;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory
{
    public struct MeshData
    {
        public const int CircleResolution = 32;

        public NativeArray<Vertex> Vertices;
        public NativeArray<ushort> Indices;

        private MeshData(int vertices, int indices)
        {
            Vertices = new NativeArray<Vertex>(vertices, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            Indices = new NativeArray<ushort>(indices, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
        }

        public static MeshData AllocateQuad() => new(vertices: 4, indices: 6);

        public static MeshData AllocateTriangle() => new(vertices: 3, indices: 3);

        public static MeshData AllocateCircle()
        {
            const int triangleOrVertexCount = CircleResolution + 1;
            return new MeshData(vertices: triangleOrVertexCount, indices: 3 * triangleOrVertexCount);
        }
    }
}