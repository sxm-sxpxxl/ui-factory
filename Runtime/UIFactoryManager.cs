using System.Collections.Generic;
using Sxm.UIFactory.Components;

namespace Sxm.UIFactory
{
    public static class UIFactoryManager
    {
        public static IEnumerable<MeshData> Build(MeshDescription description)
        {
            using var handle = new MeshHandle();
            return Build(description, handle);
        }

        public static IEnumerable<MeshData> Build(MeshDescription description, MeshHandle handle)
        {
            return handle.GetMeshBuilder(description).Build(description);
        }
    }
}