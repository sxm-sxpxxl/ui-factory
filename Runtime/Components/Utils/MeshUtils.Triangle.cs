using UnityEngine;

namespace SxmTools.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        public static MeshData CreateEquilateralTriangle(float angleAroundOriginInDeg, float size, Vector2 origin = default, Color color = default)
        {
            var mesh = new MeshData.TriangleNotSameVertexAllocationRequest(trianglesCount: 1, verticesCount: 3).Allocate();

            FillVerticesOnEquilateralTriangle(mesh.Vertices, angleAroundOriginInDeg, size, origin);
            FillTintColors(mesh.TintColors, color);

            mesh.Indices[0] = 0;
            mesh.Indices[1] = 2;
            mesh.Indices[2] = 1;

            return mesh;
        }

        public static Vector2[] GetVerticesOnEquilateralTriangle(float angleAroundOriginInDeg, float size, Vector2 origin = default)
        {
            var vertices = new Vector2[3];
            FillVerticesOnEquilateralTriangle(vertices, angleAroundOriginInDeg, size, origin);
            return vertices;
        }

        private static void FillVerticesOnEquilateralTriangle(Vector2[] vertices, float angleAroundOriginInDeg, float size, Vector2 origin = default)
        {
            var directionToV0 = -(Vector2) (Quaternion.Euler(-60f * Vector3.forward) * Vector2.up);
            var directionToV1 = Vector2.up;
            var directionToV2 = -(Vector2) (Quaternion.Euler(60f * Vector3.forward) * Vector2.up);

            var circumscribedCircleRadius = size * Mathf.Sqrt(3f) / 3f;
            var v0 = circumscribedCircleRadius * directionToV0;
            var v1 = circumscribedCircleRadius * directionToV1;
            var v2 = circumscribedCircleRadius * directionToV2;

            var localToWorldMatrix = Matrix4x4.TRS(origin, Quaternion.Euler(angleAroundOriginInDeg * Vector3.forward), Vector3.one);

            vertices[0] = localToWorldMatrix.MultiplyPoint3x4(v0);
            vertices[1] = localToWorldMatrix.MultiplyPoint3x4(v1);
            vertices[2] = localToWorldMatrix.MultiplyPoint3x4(v2);
        }
    }
}