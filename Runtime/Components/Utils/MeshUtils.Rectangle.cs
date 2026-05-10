using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        public enum RectangleVerticesBuildOrder
        {
            CrissCross,
            Cyclic
        }

        private static readonly Vector2[] VerticesOnRectangle = new Vector2[4];

        public static void CreateLineMesh(ref MeshData data, Vector2 startPoint, Vector2 endPoint, float thickness, Color32 color = default)
        {
            BurstProcedures.FillLine(ref data.Vertices, ref data.Indices, startPoint, endPoint, thickness, color);
        }

        public static void CreateDashLineMesh(ref MeshData data, int dashesCount, Vector2 startPoint, Vector2 endPoint, float dashGap, float thickness, Color32 color)
        {
            BurstProcedures.FillDashes(ref data.Vertices, ref data.Indices, dashesCount, startPoint, endPoint, dashGap, thickness, color);
        }

        public static void CreateRectangleMesh(ref MeshData data, float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default, Color32 color = default)
        {
            BurstProcedures.FillRectangle(ref data.Vertices, ref data.Indices, size, origin, angleAroundOriginInDeg, color);
        }

        public static Vector2[] GetVerticesOnRectangle(RectangleVerticesBuildOrder buildOrder, float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default)
        {
            var nativeVertices = VerticesOnRectangle.WrapAsNativeArray();
            BurstProcedures.FillRectangle(ref nativeVertices, size, origin, angleAroundOriginInDeg, buildOrder is RectangleVerticesBuildOrder.CrissCross);

            return VerticesOnRectangle;
        }

        private static partial class BurstProcedures
        {
            [BurstCompile]
            public static void FillLine(
                ref NativeArray<Vertex> vertices,
                ref NativeArray<ushort> indices,
                in float2 start,
                in float2 end,
                float thickness,
                in Color32 color)
            {
                float2 dir = end - start;
                float length = math.length(dir);

                float2 origin = (start + end) * 0.5f;
                float2 size = new float2(length, thickness);

                float angleRad = math.atan2(dir.y, dir.x);
                float angleDeg = math.degrees(angleRad);

                FillRectangle(ref vertices, ref indices, size, origin, angleDeg, color);
            }

            [BurstCompile]
            public static void FillDashes(
                ref NativeArray<Vertex> vertices,
                ref NativeArray<ushort> indices,
                int dashesCount,
                in float2 lineStart,
                in float2 lineEnd,
                float dashGap,
                float thickness,
                in Color32 color)
            {
                if (dashesCount <= 0)
                    return;

                float2 lineDir = lineEnd - lineStart;
                float lineLength = math.length(lineDir);
                if (lineLength <= 0f)
                    return;

                float2 unitDir = lineDir / lineLength;
                float2 paddingOffset = unitDir * (0.5f * dashGap);
                float2 halfPerp = new float2(-unitDir.y, unitDir.x) * (0.5f * thickness);

                for (int i = 0; i < dashesCount; i++)
                {
                    float t0 = (float) i / dashesCount;
                    float t1 = (float) (i + 1) / dashesCount;

                    float2 p0 = math.lerp(lineStart, lineEnd, t0) + paddingOffset;
                    float2 p1 = math.lerp(lineStart, lineEnd, t1) - paddingOffset;

                    float2 c0 = p0 - halfPerp;
                    float2 c1 = p0 + halfPerp;
                    float2 c2 = p1 - halfPerp;
                    float2 c3 = p1 + halfPerp;

                    int vIdx = i * 4;
                    int iIdx = i * 6;

                    vertices[vIdx + 0] = new Vertex { position = new Vector3(c0.x, c0.y, 0f), tint = color };
                    vertices[vIdx + 1] = new Vertex { position = new Vector3(c1.x, c1.y, 0f), tint = color };
                    vertices[vIdx + 2] = new Vertex { position = new Vector3(c2.x, c2.y, 0f), tint = color };
                    vertices[vIdx + 3] = new Vertex { position = new Vector3(c3.x, c3.y, 0f), tint = color };

                    indices[iIdx + 0] = (ushort) (vIdx + 0);
                    indices[iIdx + 1] = (ushort) (vIdx + 2);
                    indices[iIdx + 2] = (ushort) (vIdx + 1);
                    indices[iIdx + 3] = (ushort) (vIdx + 1);
                    indices[iIdx + 4] = (ushort) (vIdx + 2);
                    indices[iIdx + 5] = (ushort) (vIdx + 3);
                }
            }

            [BurstCompile]
            public static void FillRectangle(
                ref NativeArray<Vertex> vertices,
                ref NativeArray<ushort> indices,
                in float2 size,
                in float2 origin,
                float angleDeg,
                in Color32 color
            )
            {
                float2 extents = 0.5f * size;

                float2 v0 = new float2(-extents.x, -extents.y);
                float2 v1 = new float2(-extents.x, extents.y);
                float2 v2 = new float2(extents.x, -extents.y);
                float2 v3 = new float2(extents.x, extents.y);

                float4x4 matrix = float4x4.TRS(
                    new float3(origin.x, origin.y, 0f),
                    quaternion.EulerXYZ(0f, 0f, math.radians(angleDeg)),
                    new float3(1f, 1f, 1f)
                );

                vertices[0] = CreateVertex(matrix, v0, color);
                vertices[1] = CreateVertex(matrix, v1, color);

                vertices[2] = CreateVertex(matrix, v2, color);
                vertices[3] = CreateVertex(matrix, v3, color);

                indices[0] = 0;
                indices[1] = 2;
                indices[2] = 1;

                indices[3] = 1;
                indices[4] = 2;
                indices[5] = 3;
                return;

                static Vertex CreateVertex(float4x4 mat, float2 localPos, Color32 color)
                {
                    float4 worldPos = math.mul(mat, new float4(localPos.x, localPos.y, 0f, 1f));
                    return new Vertex
                    {
                        position = new Vector3(worldPos.x, worldPos.y, worldPos.z),
                        tint = color
                    };
                }
            }

            [BurstCompile]
            public static void FillRectangle(
                ref NativeArray<Vector2> vertices,
                in float2 size,
                in float2 origin,
                float angleDeg,
                bool isCrissCross
            )
            {
                float2 extents = 0.5f * size;

                float2 v0 = new float2(-extents.x, -extents.y);
                float2 v1 = new float2(-extents.x, extents.y);
                float2 v2 = new float2(extents.x, -extents.y);
                float2 v3 = new float2(extents.x, extents.y);

                float4x4 matrix = float4x4.TRS(
                    new float3(origin.x, origin.y, 0f),
                    quaternion.EulerXYZ(0f, 0f, math.radians(angleDeg)),
                    new float3(1f, 1f, 1f)
                );

                vertices[0] = CreateVertex(matrix, v0);
                vertices[1] = CreateVertex(matrix, v1);

                vertices[2] = CreateVertex(matrix, isCrissCross ? v2 : v3);
                vertices[3] = CreateVertex(matrix, isCrissCross ? v3 : v2);
                return;

                static Vector2 CreateVertex(float4x4 mat, float2 localPos)
                {
                    float4 worldPos = math.mul(mat, new float4(localPos.x, localPos.y, 0f, 1f));
                    return new Vector2(worldPos.x, worldPos.y);
                }
            }
        }
    }
}