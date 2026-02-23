using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory
{
    public readonly struct MeshData
    {
        private interface IMeshDataAllocationRequest
        {
            MeshData Allocate();
        }

        internal readonly struct QuadAllocationRequest : IMeshDataAllocationRequest
        {
            private readonly int _quadsCount;

            public QuadAllocationRequest(int quadsCount)
            {
                Assert.IsTrue(quadsCount > 0);
                _quadsCount = quadsCount;
            }

            public MeshData Allocate() => new(vertices: 4 * _quadsCount, indices: 6 * _quadsCount);
        }

        internal readonly struct TriangleSameVertexAllocationRequest : IMeshDataAllocationRequest
        {
            private readonly int _triangleOrVertexCount;

            public TriangleSameVertexAllocationRequest(int triangleOrVertexCount)
            {
                Assert.IsTrue(triangleOrVertexCount > 0);
                _triangleOrVertexCount = triangleOrVertexCount;
            }

            public MeshData Allocate() => new(vertices: _triangleOrVertexCount, indices: 3 * _triangleOrVertexCount);
        }

        internal readonly struct TriangleNotSameVertexAllocationRequest : IMeshDataAllocationRequest
        {
            private readonly int _trianglesCount;
            private readonly int _verticesCount;

            public TriangleNotSameVertexAllocationRequest(int trianglesCount, int verticesCount)
            {
                Assert.IsTrue(trianglesCount > 0);
                Assert.IsTrue(verticesCount > 0);
                Assert.IsTrue(trianglesCount < verticesCount);
                _trianglesCount = trianglesCount;
                _verticesCount = verticesCount;
            }

            public MeshData Allocate() => new(vertices: _verticesCount, indices: 3 * _trianglesCount);
        }

        public readonly Vertex[] Vertices;
        public readonly ushort[] Indices;

        private MeshData(int vertices, int indices)
        {
            Vertices = new Vertex[vertices];
            Indices = new ushort[indices];
        }
    }
}