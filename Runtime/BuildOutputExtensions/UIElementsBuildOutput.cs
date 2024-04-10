using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sxm.UIFactory
{
    public static class UIElementsBuildOutput
    {
        public class Result
        {
            public Vertex[] Vertices;
            public ushort[] Indices;
        }

        public static IEnumerable<Result> GetDataForUIElements(this UIMeshFactory.CachedMeshInstance input) => input.Data.GetDataForUIElements();

        private static IEnumerable<Result> GetDataForUIElements(this IEnumerable<MeshData> input)
        {
            return input.Select(mesh => new Result
            {
                Vertices = mesh.Vertices.Select((v, vIndex) => new Vertex
                {
                    position = new Vector3(v.x, v.y, Vertex.nearZ),
                    tint = mesh.TintColors[vIndex]
                }).ToArray(),
                Indices = mesh.Indices
            }).ToList();
        }
    }
}