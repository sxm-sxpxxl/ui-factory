using Sxm.UIFactory.Components;
using UnityEngine.Pool;

namespace Sxm.UIFactory
{
    internal sealed class MeshBuilderPool : IObjectPool<IMeshBuilder>
    {
        public int CountInactive => _pool.CountInactive;

        private readonly MeshDescription _description;
        private readonly ObjectPool<IMeshBuilder> _pool;

        public MeshBuilderPool(MeshDescription description)
        {
            _description = description;
            _pool = new ObjectPool<IMeshBuilder>(ConstructMeshBuilder);
        }

        private IMeshBuilder ConstructMeshBuilder() => new CachedMeshBuilder(_description.ConstructBuilder());

        public IMeshBuilder Get() => _pool.Get();

        public PooledObject<IMeshBuilder> Get(out IMeshBuilder v) => _pool.Get(out v);

        public void Release(IMeshBuilder element) => _pool.Release(element);

        public void Clear() => _pool.Clear();
    }
}