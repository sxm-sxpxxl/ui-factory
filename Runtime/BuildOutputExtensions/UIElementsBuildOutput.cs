using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sxm.UIFactory
{
    public static class UIElementsBuildOutput
    {
        public readonly struct Result
        {
            public readonly Vertex[] Vertices;
            public readonly ushort[] Indices;

            public Result(Vertex[] vertices, ushort[] indices)
            {
                Vertices = vertices;
                Indices = indices;
            }
        }

        public static IEnumerable<Result> GetDataForUIElements(this UIFactoryManager.CookedMesh input) => input.Result.GetDataForUIElements();

        private static IEnumerable<Result> GetDataForUIElements(this IEnumerable<MeshData> input) => input.Select(mesh => new Result(
            vertices: mesh.Vertices.Select((v, vIndex) => new Vertex
            {
                position = new Vector3(v.x, v.y, Vertex.nearZ),
                tint = mesh.TintColors[vIndex]
            }).ToArray(),
            indices: mesh.Indices
        ));
    }
}