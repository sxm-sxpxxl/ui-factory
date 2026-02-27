using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory.Components
{
    public static partial class MeshUtils
    {
        public static void CreateEquilateralTriangle(MeshData data, float angleAroundOriginInDeg, float size, Vector2 origin = default, Color32 color = default)
        {
            // var directionToV0 = -(Vector2) (Quaternion.Euler(-60f * Vector3.forward) * Vector2.up);
            // var directionToV1 = Vector2.up;
            // var directionToV2 = -(Vector2) (Quaternion.Euler(60f * Vector3.forward) * Vector2.up);
            //
            // var circumscribedCircleRadius = size * Mathf.Sqrt(3f) / 3f;
            // var v0 = circumscribedCircleRadius * directionToV0;
            // var v1 = circumscribedCircleRadius * directionToV1;
            // var v2 = circumscribedCircleRadius * directionToV2;
            //
            // var localToWorldMatrix = Matrix4x4.TRS(origin, Quaternion.Euler(angleAroundOriginInDeg * Vector3.forward), Vector3.one);
            //
            // vertices[0].position = localToWorldMatrix.MultiplyPoint3x4(v0);
            // vertices[1].position = localToWorldMatrix.MultiplyPoint3x4(v1);
            // vertices[2].position = localToWorldMatrix.MultiplyPoint3x4(v2);

            TriangleBurstProcedures.FillEquilateralTriangle(ref data.Vertices, ref data.Indices, size, origin, angleAroundOriginInDeg, color);
        }

        public static Vector2[] RentVerticesOnEquilateralTriangle(float angleAroundOriginInDeg, float size, Vector2 origin = default)
        {
//             var rentedVertices = Pool.Rent(3);
//
//             var nativeVertices = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray(rentedVertices.AsSpan(), Allocator.None);
// #if UNITY_EDITOR
//             NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeVertices, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
// #endif
            var nativeVertices = new NativeArray<Vector2>(3, Allocator.Temp);

            // var directionToV0 = -(Vector2) (Quaternion.Euler(-60f * Vector3.forward) * Vector2.up);
            // var directionToV1 = Vector2.up;
            // var directionToV2 = -(Vector2) (Quaternion.Euler(60f * Vector3.forward) * Vector2.up);
            //
            // var circumscribedCircleRadius = size * Mathf.Sqrt(3f) / 3f;
            // var v0 = circumscribedCircleRadius * directionToV0;
            // var v1 = circumscribedCircleRadius * directionToV1;
            // var v2 = circumscribedCircleRadius * directionToV2;
            //
            // var localToWorldMatrix = Matrix4x4.TRS(origin, Quaternion.Euler(angleAroundOriginInDeg * Vector3.forward), Vector3.one);
            //
            // vertices[0] = localToWorldMatrix.MultiplyPoint3x4(v0);
            // vertices[1] = localToWorldMatrix.MultiplyPoint3x4(v1);
            // vertices[2] = localToWorldMatrix.MultiplyPoint3x4(v2);

            TriangleBurstProcedures.FillEquilateralTriangle(ref nativeVertices, size, origin, angleAroundOriginInDeg);
            var rentedVertices = nativeVertices.ToArray();
            nativeVertices.Dispose();

            return rentedVertices;
        }
    }

    [BurstCompile]
    public static class TriangleBurstProcedures
    {
        [BurstCompile]
        public static void FillEquilateralTriangle(
            ref NativeArray<Vertex> vertices, 
            ref NativeArray<ushort> indices,
            float size, 
            in float2 origin, 
            float angleDeg, 
            in Color32 color
        )
        {
            float rad60 = math.radians(60f);
            math.sincos(rad60, out float sin60, out float cos60);

            float2 dirV0 = new float2(-sin60, -cos60);
            float2 dirV1 = new float2(0f, 1f);
            float2 dirV2 = new float2(sin60, -cos60);

            float radius = size * math.sqrt(3f) / 3f;

            float4x4 matrix = float4x4.TRS(
                new float3(origin.x, origin.y, 0f), 
                quaternion.EulerXYZ(0f, 0f, math.radians(angleDeg)), 
                new float3(1f, 1f, 1f)
            );

            vertices[0] = CreateVertex(matrix, dirV0 * radius, color);
            vertices[1] = CreateVertex(matrix, dirV1 * radius, color);
            vertices[2] = CreateVertex(matrix, dirV2 * radius, color);

            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;
            return;

            static Vertex CreateVertex(float4x4 mat, float2 localPos, Color32 col)
            {
                float4 worldPos = math.mul(mat, new float4(localPos.x, localPos.y, 0f, 1f));
                return new Vertex 
                { 
                    position = new Vector3(worldPos.x, worldPos.y, worldPos.z), 
                    tint = col 
                };
            }
        }

        [BurstCompile]
        public static void FillEquilateralTriangle(
            ref NativeArray<Vector2> vertices, 
            float size, 
            in float2 origin, 
            float angleDeg
        )
        {
            float rad60 = math.radians(60f);
            math.sincos(rad60, out float sin60, out float cos60);

            float2 dirV0 = new float2(-sin60, -cos60);
            float2 dirV1 = new float2(0f, 1f);
            float2 dirV2 = new float2(sin60, -cos60);

            float radius = size * math.sqrt(3f) / 3f;

            float4x4 matrix = float4x4.TRS(
                new float3(origin.x, origin.y, 0f), 
                quaternion.EulerXYZ(0f, 0f, math.radians(angleDeg)), 
                new float3(1f, 1f, 1f)
            );

            vertices[0] = CreateVertex(matrix, dirV0 * radius);
            vertices[1] = CreateVertex(matrix, dirV1 * radius);
            vertices[2] = CreateVertex(matrix, dirV2 * radius);
            return;

            static Vector2 CreateVertex(float4x4 mat, float2 localPos)
            {
                float4 worldPos = math.mul(mat, new float4(localPos.x, localPos.y, 0f, 1f));
                return new Vector2(worldPos.x, worldPos.y);
            }
        }
    }
}