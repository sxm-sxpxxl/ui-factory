using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal abstract class MeshBuilder<T> : IMeshBuilder where T : MeshDescription
    {
        protected abstract IEnumerable<MeshData> Build(T description);

        public IEnumerable<MeshData> Build(object description) => Build((T) description);

        public virtual void Dispose()
        {
        }
    }
}