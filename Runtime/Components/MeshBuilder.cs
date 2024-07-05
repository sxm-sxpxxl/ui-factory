using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public abstract class MeshBuilder<T> : IMeshBuilder
    {
        IEnumerable<MeshData> IMeshBuilder.Build(object description) => Build((T) description);
        public abstract IEnumerable<MeshData> Build(T description);
    }
}