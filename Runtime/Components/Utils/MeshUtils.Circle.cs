using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        private static readonly Vector2[] VerticesOnCircumference = new Vector2[MeshData.CircleResolution];

        public static void CreateCircleMesh(ref MeshData data, float radius, Vector2 origin = default, Color32 color = default)
        {
            BurstProcedures.FillCircumference(ref data.Vertices, ref data.Indices, radius, origin, color);
        }

        public static Vector2[] GetVerticesOnCircumference(float radius, Vector2 origin = default)
        {
            var nativeVertices = VerticesOnCircumference.WrapAsNativeArray();
            BurstProcedures.FillCircumference(ref nativeVertices, radius, origin);

            return VerticesOnCircumference;
        }

        private static partial class BurstProcedures
        {
            [BurstCompile]
            public static void FillCircumference(
                ref NativeArray<Vertex> vertices,
                ref NativeArray<ushort> indices,
                float radius,
                in float2 origin,
                in Color32 color
            )
            {
                ushort last = (ushort) (vertices.Length - 1);
                float deltaInRad = 2f * math.PI / last;

                for (ushort i = 0; i < last; i++)
                {
                    float t = i * deltaInRad;
                    math.sincos(t, out float sin, out float cos);

                    vertices[i] = new Vertex
                    {
                        position = new Vector3(origin.x + (cos * radius), origin.y + (sin * radius), 0f),
                        tint = color
                    };

                    ushort curr = i;
                    ushort next = (ushort) ((i + 1) % last);

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
                in float2 origin
            )
            {
                int resolution = vertices.Length;
                float deltaInRad = (2f * math.PI) / resolution;

                for (int i = 0; i < resolution; i++)
                {
                    float t = i * deltaInRad;
                    math.sincos(t, out float sin, out float cos);

                    vertices[i] = new Vector2(origin.x + (cos * radius), origin.y + (sin * radius));
                }
            }
        }
    }
}