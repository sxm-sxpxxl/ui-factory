using UnityEngine;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        public static MeshData CreateCircleMesh(int resolution, float radius, Vector2 origin = default, Color32 color = default)
        {
            var mesh = new MeshData.TriangleSameVertexAllocationRequest(triangleOrVertexCount: resolution + 1).Allocate();

            FillVerticesOnCircumference(mesh.Vertices, radius, resolution, hasOriginPoint: true, origin);
            FillTintColors(mesh.Vertices, color);

            var lastIndex = (ushort) (mesh.Vertices.Length - 1);
            for (ushort currentIndex = 0; currentIndex < mesh.Vertices.Length; currentIndex++)
            {
                var nextIndex = (ushort) Mathf.Repeat(currentIndex + 1, lastIndex);

                mesh.Indices[3 * currentIndex + 0] = currentIndex;
                mesh.Indices[3 * currentIndex + 1] = nextIndex;
                mesh.Indices[3 * currentIndex + 2] = lastIndex;
            }

            return mesh;
        }

        public static Vector2[] GetVerticesOnCircumference(bool hasOriginPoint, int resolution, float radius, Vector2 origin = default)
        {
            var verticesCount = resolution + (hasOriginPoint ? 1 : 0);
            var vertices = new Vector2[verticesCount];

            FillVerticesOnCircumference(vertices, radius, resolution, hasOriginPoint, origin);
            return vertices;
        }

        private static void FillVerticesOnCircumference(Vertex[] vertices, float radius, int resolution, bool hasOriginPoint = false, Vector2 origin = default)
        {
            var deltaInRad = 2f * Mathf.PI / resolution;

            for (var i = 0; i < resolution; i++)
            {
                var t = i * deltaInRad;
                var x = radius * Mathf.Cos(t);
                var y = radius * Mathf.Sin(t);

                vertices[i].position = origin + new Vector2(x, y);
            }

            if (hasOriginPoint)
            {
                vertices[resolution].position = origin;
            }
        }

        private static void FillVerticesOnCircumference(Vector2[] vertices, float radius, int resolution, bool hasOriginPoint = false, Vector2 origin = default)
        {
            var deltaInRad = 2f * Mathf.PI / resolution;

            for (var i = 0; i < resolution; i++)
            {
                var t = i * deltaInRad;
                var x = radius * Mathf.Cos(t);
                var y = radius * Mathf.Sin(t);

                vertices[i] = origin + new Vector2(x, y);
            }

            if (hasOriginPoint)
            {
                vertices[resolution] = origin;
            }
        }
    }
}