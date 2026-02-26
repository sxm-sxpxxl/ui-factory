using Unity.Collections;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory
{
    public struct MeshData
    {
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

        public static MeshData AllocateQuad(int quadsCount)
        {
            Assert.IsTrue(quadsCount > 0);
            return new MeshData(vertices: 4 * quadsCount, indices: 6 * quadsCount);
        }

        public static MeshData AllocateTriangleNotSameVertex(int trianglesCount, int verticesCount)
        {
            Assert.IsTrue(trianglesCount > 0);
            Assert.IsTrue(verticesCount > 0);
            Assert.IsTrue(trianglesCount < verticesCount);
            return new MeshData(vertices: verticesCount, indices: 3 * trianglesCount);
        }

        public static MeshData AllocateTriangleSameVertex(int triangleOrVertexCount)
        {
            Assert.IsTrue(triangleOrVertexCount > 0);
            return new MeshData(vertices: triangleOrVertexCount, indices: 3 * triangleOrVertexCount);
        }
    }
}