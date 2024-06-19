using UnityEngine;
using UnityEngine.Assertions;

namespace Sxm.UIFactory
{
    // todo@sxm: refactoring (indices get method, one single MeshData constructor, readonly struct)
    public class MeshData
    {
        private interface IVerifiable
        {
            void Verify();
        }

        public struct QuadRequest : IVerifiable
        {
            public int QuadsCount;
            public void Verify() => Assert.IsTrue(QuadsCount > 0);
        }

        public struct TriangleSameVertexRequest : IVerifiable
        {
            public int TriangleOrVertexCount;
            public void Verify() => Assert.IsTrue(TriangleOrVertexCount > 0);
        }

        public struct TriangleNotSameVertexRequest : IVerifiable
        {
            public int TrianglesCount;
            public int VerticesCount;

            public void Verify()
            {
                Assert.IsTrue(TrianglesCount > 0);
                Assert.IsTrue(VerticesCount > 0);
                Assert.IsTrue(TrianglesCount < VerticesCount);
            }
        }

        public Vector2[] Vertices;
        public ushort[] Indices;
        public Color[] TintColors;

        public MeshData(QuadRequest request)
        {
            request.Verify();
            CreateData(vertices: 4 * request.QuadsCount, indices: 6 * request.QuadsCount);
        }

        public MeshData(TriangleSameVertexRequest request)
        {
            request.Verify();
            CreateData(vertices: request.TriangleOrVertexCount, indices: 3 * request.TriangleOrVertexCount);
        }

        public MeshData(TriangleNotSameVertexRequest request)
        {
            request.Verify();
            CreateData(vertices: request.VerticesCount, indices: 3 * request.TrianglesCount);
        }

        private void CreateData(int vertices, int indices)
        {
            Vertices = new Vector2[vertices];
            Indices = new ushort[indices];
            TintColors = new Color[vertices];
        }
    }
}