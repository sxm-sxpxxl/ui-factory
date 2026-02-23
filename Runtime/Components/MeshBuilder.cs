using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal abstract class MeshBuilder<T> : IMeshBuilder where T : MeshDescription
    {
        protected abstract IReadOnlyList<MeshData> Build(T description);

        public IReadOnlyList<MeshData> Build(object description) => Build((T) description);

        public virtual void Dispose()
        {
        }
    }
}