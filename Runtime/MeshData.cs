using Unity.Collections;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory
{
    internal struct MeshData
    {
        public const int CircleResolution = 8;

        public NativeArray<Vertex> Vertices;
        public NativeArray<ushort> Indices;
        public bool Inited;

        private MeshData(int vertices, int indices)
        {
            Vertices = new NativeArray<Vertex>(vertices, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            Indices = new NativeArray<ushort>(indices, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            Inited = true;
        }

        public void Dispose()
        {
            if (!Inited)
                return;

            Vertices.Dispose();
            Indices.Dispose();

            Inited = false;
        }

        public static MeshData AllocateQuad() => new(vertices: 4, indices: 6);

        public static MeshData AllocateTriangle() => new(vertices: 3, indices: 3);

        public static MeshData AllocateCircle() => new(vertices: CircleResolution, indices: 3 * (CircleResolution - 1));
    }
}