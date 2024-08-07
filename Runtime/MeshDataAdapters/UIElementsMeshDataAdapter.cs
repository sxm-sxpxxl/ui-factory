using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sxm.UIFactory.MeshDataAdapters
{
    public static class UIElementsMeshDataAdapter
    {
        public readonly struct MeshResult : IDisposable
        {
            public readonly Vertex[] Vertices;
            public readonly ushort[] Indices;

            private readonly ArrayPool<Vertex> _verticesPool;

            public MeshResult(Vector2[] vertices, Color[] tintColors, ushort[] indices)
            {
                (Vertices, _verticesPool) = GetVertices(vertices, tintColors);
                Indices = indices;
            }

            private static (Vertex[] result, ArrayPool<Vertex> pool) GetVertices(Vector2[] vertices, Color[] tintColors)
            {
                var pool = ArrayPool<Vertex>.Shared;
                var result = pool.Rent(vertices.Length);

                for (var i = 0; i < vertices.Length; i++)
                {
                    result[i] = new Vertex
                    {
                        position = new Vector3(vertices[i].x, vertices[i].y, Vertex.nearZ),
                        tint = tintColors[i]
                    };
                }

                return (result, pool);
            }

            public void Dispose()
            {
                _verticesPool.Return(Vertices);
            }
        }

        public static IEnumerable<MeshResult> GetResults(this IEnumerable<MeshData> input)
        {
            return input.Select(mesh => new MeshResult(mesh.Vertices, mesh.TintColors, mesh.Indices));
        }
    }
}