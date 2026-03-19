using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components;
using Unity.Collections;
using UnityEngine.UIElements;

namespace SxmTools.UIFactory
{
    public static class UIFactoryManager
    {
        private static readonly Dictionary<Type, MeshBuilderPool> Pools = new();
        private static readonly List<MeshData> Result = new();

        /// <summary>
        /// Build a mesh described in the description with caching of a previous result in the handle.
        /// </summary>
        /// <param name="context">Context for UIElements mesh generation</param>
        /// <param name="description">Description of a mesh</param>
        /// <param name="handle">Handle for caching a previous result</param>
        public static MeshHandle BuildMesh(this MeshGenerationContext context, MeshDescription description, [CanBeNull] MeshHandle handle)
        {
            Result.Clear();

            handle = BuildMesh(description, Result, handle);
            context.SetData(Result);

            return handle;
        }

        internal static MeshHandle BuildMesh(MeshDescription description, List<MeshData> result, [CanBeNull] MeshHandle handle)
        {
            var descriptionType = description.GetType();
            handle ??= new MeshHandle();

            if (!Pools.TryGetValue(descriptionType, out var pool))
            {
                pool = new MeshBuilderPool(description);
                Pools[descriptionType] = pool;
            }

            var meshBuilder = handle.GetMeshBuilder(pool, descriptionType);
            meshBuilder.Build(description, result);

            return handle;
        }

        private static void SetData(this MeshGenerationContext context, IReadOnlyList<MeshData> data)
        {
            for (var index = 0; index < data.Count; index++)
            {
                var meshData = data[index];
                var meshWriteData = context.Allocate(meshData.Vertices.Length, meshData.Indices.Length);

                meshWriteData.SetAllVertices(meshData.Vertices.Slice());
                meshWriteData.SetAllIndices(meshData.Indices.Slice());
            }
        }
    }
}