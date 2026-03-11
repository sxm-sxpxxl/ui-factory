using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal abstract class MeshBuilder<T> : IMeshBuilder where T : MeshDescription
    {
        protected abstract void Build(T description, List<MeshData> result);

        public void Build(object description, List<MeshData> result) => Build((T) description, result);

        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}