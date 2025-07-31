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
        private readonly struct MeshResult : IDisposable
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

        public static void SetData(this MeshGenerationContext context, IEnumerable<MeshData> data)
        {
            foreach (var result in data.Select(mesh => new MeshResult(mesh.Vertices, mesh.TintColors, mesh.Indices)))
            {
                using (result)
                {
                    var meshWriteData = context.Allocate(result.Vertices.Length, result.Indices.Length);
                    meshWriteData.SetAllVertices(result.Vertices);
                    meshWriteData.SetAllIndices(result.Indices);
                }
            }
        }
    }
}