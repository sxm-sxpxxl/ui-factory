using System;
using System.Collections.Generic;
using SxmTools.UIFactory.Components;

namespace SxmTools.UIFactory
{
    public static class UIFactoryManager
    {
        private static readonly Dictionary<Type, MeshBuilderPool> Pools = new();

        /// <summary>
        /// Build a mesh described in the description.
        /// </summary>
        /// <param name="description">Description of a mesh</param>
        /// <returns>A sequence of mesh data representing its different parts</returns>
        public static IEnumerable<MeshData> Build(MeshDescription description)
        {
            using var handle = new MeshHandle();
            return Build(description, handle);
        }

        /// <summary>
        /// Build a mesh described in the description with caching of a previous result in the handle.
        /// </summary>
        /// <param name="description">Description of a mesh</param>
        /// <param name="handle">Handle for caching a previous result</param>
        /// <returns>A sequence of mesh data representing its different parts</returns>
        public static IEnumerable<MeshData> Build(MeshDescription description, MeshHandle handle)
        {
            var descriptionType = description.GetType();

            if (!Pools.ContainsKey(descriptionType))
            {
                Pools[descriptionType] = new MeshBuilderPool(description);
            }

            return handle.GetMeshBuilder(Pools[descriptionType], descriptionType).Build(description);
        }
    }
}