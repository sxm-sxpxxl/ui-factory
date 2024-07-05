using UnityEngine;

namespace Sxm.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        public enum RectangleVerticesBuildOrder
        {
            CrissCross,
            Cyclic
        }

        private static readonly Quaternion OrthoRotation = Quaternion.Euler(90f * Vector3.forward);

        public static MeshData CreateLineMesh(Vector2 startPoint, Vector2 endPoint, float thickness, Color color = default)
        {
            var lengthDirection = endPoint - startPoint;
            var widthDirection = ((Vector2) (OrthoRotation * lengthDirection)).normalized;
            var halfWidthOffset = 0.5f * thickness * widthDirection;

            var minPoint = startPoint - halfWidthOffset;
            var maxPoint = endPoint + halfWidthOffset;

            var origin = 0.5f * (startPoint + endPoint);
            var angleAroundOriginInDeg = Vector2.SignedAngle(Vector2.up, widthDirection);
            var worldSize = maxPoint - minPoint;
            var localSize = (Vector2) (Quaternion.Euler(-angleAroundOriginInDeg * Vector3.forward) * worldSize);

            return CreateRectangleMesh(angleAroundOriginInDeg, localSize, origin, color);
        }

        public static MeshData CreateRectangleMesh(float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default, Color color = default)
        {
            var mesh = new MeshData.QuadAllocationRequest(quadsCount: 1).Allocate();

            FillVerticesOnRectangle(mesh.Vertices, RectangleVerticesBuildOrder.CrissCross, angleAroundOriginInDeg, size, origin);
            FillTintColors(mesh.TintColors, color);

            mesh.Indices[0] = 0;
            mesh.Indices[1] = 2;
            mesh.Indices[2] = 1;

            mesh.Indices[3] = 1;
            mesh.Indices[4] = 2;
            mesh.Indices[5] = 3;

            return mesh;
        }

        public static Vector2[] GetVerticesOnRectangle(RectangleVerticesBuildOrder buildOrder, float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default)
        {
            var vertices = new Vector2[4];
            FillVerticesOnRectangle(vertices, buildOrder, angleAroundOriginInDeg, size, origin);
            return vertices;
        }

        private static void FillVerticesOnRectangle(Vector2[] vertices, RectangleVerticesBuildOrder buildOrder, float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default)
        {
            var extents = 0.5f * size;

            var v0 = new Vector2(-extents.x, -extents.y);
            var v1 = new Vector2(-extents.x, +extents.y);
            var v2 = new Vector2(+extents.x, -extents.y);
            var v3 = new Vector2(+extents.x, +extents.y);

            var localToWorldMatrix = Matrix4x4.TRS(origin, Quaternion.Euler(angleAroundOriginInDeg * Vector3.forward), Vector3.one);

            v0 = localToWorldMatrix.MultiplyPoint3x4(v0);
            v1 = localToWorldMatrix.MultiplyPoint3x4(v1);
            v2 = localToWorldMatrix.MultiplyPoint3x4(v2);
            v3 = localToWorldMatrix.MultiplyPoint3x4(v3);

            vertices[0] = v0;
            vertices[1] = v1;
            vertices[2] = buildOrder == RectangleVerticesBuildOrder.CrissCross ? v2 : v3;
            vertices[3] = buildOrder == RectangleVerticesBuildOrder.CrissCross ? v3 : v2;
        }

        private static Color[] FillTintColors(Color[] colors, Color defaultValue)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = defaultValue;
            }

            return colors;
        }
    }
}