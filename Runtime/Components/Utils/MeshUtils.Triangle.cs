using UnityEngine;

namespace Sxm.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        public static void FillEquilateralTriangle(MeshData mesh, float size, Vector2 origin = default, float angleAroundOriginInDeg = 0f, Color color = default)
        {
            mesh.Vertices = GetVerticesOnEquilateralTriangle(size, origin, angleAroundOriginInDeg);
            mesh.TintColors = CreateColors(mesh.Vertices.Length, color);

            mesh.Indices[0] = 0;
            mesh.Indices[1] = 2;
            mesh.Indices[2] = 1;
        }

        public static Vector2[] GetVerticesOnEquilateralTriangle(float size, Vector2 origin = default, float angleAroundOriginInDeg = 0f)
        {
            var vertices = new Vector2[3];

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

            return vertices;
        }
    }
}