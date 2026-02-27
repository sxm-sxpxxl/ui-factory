using System;
using System.Buffers;
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
        private static readonly ArrayPool<Vector2> Pool = ArrayPool<Vector2>.Shared;

        public static void CreateCircleMesh(MeshData data, float radius, Vector2 origin = default, Color32 color = default)
        {
            // for (var i = 0; i < resolution; i++)
            // {
            //     var t = i * deltaInRad;
            //     var x = radius * Mathf.Cos(t);
            //     var y = radius * Mathf.Sin(t);
            //
            //     vertices[i].position = origin + new Vector2(x, y);
            // }

            CircleBurstProcedures.FillCircumference(ref data.Vertices, ref data.Indices, radius, MeshData.CircleResolution, origin, color);
        }

        public static Vector2[] RentVerticesOnCircumference(bool hasOriginPoint, int resolution, float radius, Vector2 origin = default)
        {
//             var verticesCount = resolution + (hasOriginPoint ? 1 : 0);
//             var rentedVertices = Pool.Rent(verticesCount);
//
//             var nativeVertices = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray(rentedVertices.AsSpan(), Allocator.None);
// #if UNITY_EDITOR
//             NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeVertices, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
// #endif

            // todo@sxm: потрогать вот эти места
            var nativeVertices = new NativeArray<Vector2>(resolution + (hasOriginPoint ? 1 : 0), Allocator.Temp);

            // var deltaInRad = 2f * Mathf.PI / resolution;
            //
            // for (var i = 0; i < resolution; i++)
            // {
            //     var t = i * deltaInRad;
            //     var x = radius * Mathf.Cos(t);
            //     var y = radius * Mathf.Sin(t);
            //
            //     vertices[i] = origin + new Vector2(x, y);
            // }
            //
            // if (hasOriginPoint)
            // {
            //     vertices[resolution] = origin;
            // }

            CircleBurstProcedures.FillCircumference(ref nativeVertices, radius, resolution, origin, hasOriginPoint);
            var rentedVertices = nativeVertices.ToArray();
            nativeVertices.Dispose();
            
            return rentedVertices;
        }

        public static void ReturnVertices(Vector2[] rentedVertices)
        {
            Pool.Return(rentedVertices);
        }
    }

    [BurstCompile]
    public static class CircleBurstProcedures
    {
        [BurstCompile]
        public static void FillCircumference(
            ref NativeArray<Vertex> vertices,
            ref NativeArray<ushort> indices,
            float radius,
            int resolution,
            in float2 origin,
            in Color32 color
        )
        {
            float deltaInRad = (2f * math.PI) / resolution;
            ushort last = (ushort) resolution;

            for (int i = 0; i < resolution; i++)
            {
                float t = i * deltaInRad;
                math.sincos(t, out float sin, out float cos);

                vertices[i] = new Vertex
                {
                    position = new Vector3(origin.x + (cos * radius), origin.y + (sin * radius), 0f),
                    tint = color
                };

                ushort curr = (ushort) i;
                ushort next = (ushort) ((i + 1) % resolution);

                int baseIdx = i * 3;
                indices[baseIdx + 0] = curr;
                indices[baseIdx + 1] = next;
                indices[baseIdx + 2] = last;
            }

            vertices[last] = new Vertex
            {
                position = new Vector3(origin.x, origin.y, 0f),
                tint = color
            };
        }

        [BurstCompile]
        public static void FillCircumference(
            ref NativeArray<Vector2> vertices,
            float radius,
            int resolution,
            in float2 origin,
            bool hasOriginPoint
        )
        {
            float deltaInRad = (2f * math.PI) / resolution;

            for (int i = 0; i < resolution; i++)
            {
                float t = i * deltaInRad;
                math.sincos(t, out float sin, out float cos);

                vertices[i] = new Vector2(origin.x + (cos * radius), origin.y + (sin * radius));
            }

            if (hasOriginPoint)
            {
                vertices[resolution] = origin;
            }
        }
    }
}