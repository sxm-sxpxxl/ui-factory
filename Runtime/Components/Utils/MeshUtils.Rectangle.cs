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
        public enum RectangleVerticesBuildOrder
        {
            CrissCross,
            Cyclic
        }

        private static readonly Quaternion OrthoRotation = Quaternion.Euler(90f * Vector3.forward);

        // todo@sxm: тоже перевести на burst-compile
        public static void CreateLineMesh(MeshData data, Vector2 startPoint, Vector2 endPoint, float thickness, Color32 color = default)
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

            CreateRectangleMesh(data, angleAroundOriginInDeg, localSize, origin, color);
        }

        public static void CreateRectangleMesh(MeshData data, float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default, Color32 color = default)
        {
            // var extents = 0.5f * size;
            //
            // var v0 = new Vector2(-extents.x, -extents.y);
            // var v1 = new Vector2(-extents.x, +extents.y);
            // var v2 = new Vector2(+extents.x, -extents.y);
            // var v3 = new Vector2(+extents.x, +extents.y);
            //
            // var localToWorldMatrix = Matrix4x4.TRS(origin, Quaternion.Euler(angleAroundOriginInDeg * Vector3.forward), Vector3.one);
            //
            // v0 = localToWorldMatrix.MultiplyPoint3x4(v0);
            // v1 = localToWorldMatrix.MultiplyPoint3x4(v1);
            // v2 = localToWorldMatrix.MultiplyPoint3x4(v2);
            // v3 = localToWorldMatrix.MultiplyPoint3x4(v3);
            //
            // vertices[0].position = v0;
            // vertices[1].position = v1;
            // vertices[2].position = buildOrder == RectangleVerticesBuildOrder.CrissCross ? v2 : v3;
            // vertices[3].position = buildOrder == RectangleVerticesBuildOrder.CrissCross ? v3 : v2;

            RectangleBurstProcedures.FillRectangle(ref data.Vertices, ref data.Indices, size, origin, angleAroundOriginInDeg, color, true);
        }

        public static Vector2[] RentVerticesOnRectangle(RectangleVerticesBuildOrder buildOrder, float angleAroundOriginInDeg, Vector2 size, Vector2 origin = default)
        {
//             var rentedVertices = Pool.Rent(4);
//
//             var nativeVertices = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray(rentedVertices.AsSpan(), Allocator.None);
// #if UNITY_EDITOR
//             NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeVertices, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
// #endif
            var nativeVertices = new NativeArray<Vector2>(4, Allocator.Temp);

            // var extents = 0.5f * size;
            //
            // var v0 = new Vector2(-extents.x, -extents.y);
            // var v1 = new Vector2(-extents.x, +extents.y);
            // var v2 = new Vector2(+extents.x, -extents.y);
            // var v3 = new Vector2(+extents.x, +extents.y);
            //
            // var localToWorldMatrix = Matrix4x4.TRS(origin, Quaternion.Euler(angleAroundOriginInDeg * Vector3.forward), Vector3.one);
            //
            // v0 = localToWorldMatrix.MultiplyPoint3x4(v0);
            // v1 = localToWorldMatrix.MultiplyPoint3x4(v1);
            // v2 = localToWorldMatrix.MultiplyPoint3x4(v2);
            // v3 = localToWorldMatrix.MultiplyPoint3x4(v3);
            //
            // vertices[0] = v0;
            // vertices[1] = v1;
            // vertices[2] = buildOrder == RectangleVerticesBuildOrder.CrissCross ? v2 : v3;
            // vertices[3] = buildOrder == RectangleVerticesBuildOrder.CrissCross ? v3 : v2;

            RectangleBurstProcedures.FillRectangle(ref nativeVertices, size, origin, angleAroundOriginInDeg, buildOrder is RectangleVerticesBuildOrder.CrissCross);
            var rentedVertices = nativeVertices.ToArray();
            nativeVertices.Dispose();

            return rentedVertices;
        }
    }

    [BurstCompile]
    public static class RectangleBurstProcedures
    {
        [BurstCompile]
        public static void FillRectangle(
            ref NativeArray<Vertex> vertices, 
            ref NativeArray<ushort> indices,
            in float2 size, 
            in float2 origin, 
            float angleDeg, 
            in Color32 color,
            bool isCrissCross
        )
        {
            float2 extents = 0.5f * size;

            float2 v0 = new float2(-extents.x, -extents.y);
            float2 v1 = new float2(-extents.x,  extents.y);
            float2 v2 = new float2( extents.x, -extents.y);
            float2 v3 = new float2( extents.x,  extents.y);

            float4x4 matrix = float4x4.TRS(
                new float3(origin.x, origin.y, 0f),
                quaternion.EulerXYZ(0f, 0f, math.radians(angleDeg)),
                new float3(1f, 1f, 1f)
            );

            vertices[0] = CreateVertex(matrix, v0, color);
            vertices[1] = CreateVertex(matrix, v1, color);
        
            vertices[2] = CreateVertex(matrix, isCrissCross ? v2 : v3, color);
            vertices[3] = CreateVertex(matrix, isCrissCross ? v3 : v2, color);

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
            float2 v1 = new float2(-extents.x,  extents.y);
            float2 v2 = new float2( extents.x, -extents.y);
            float2 v3 = new float2( extents.x,  extents.y);

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