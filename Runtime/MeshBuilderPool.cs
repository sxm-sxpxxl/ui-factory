using System.Collections.Generic;
using Sxm.UIFactory.Components;
using UnityEngine.Pool;

namespace Sxm.UIFactory
{
    internal sealed class MeshBuilderPool : IObjectPool<IMeshBuilder>
    {
        public int CountInactive => _pool.CountInactive;

        private readonly MeshDescription _description;
        private readonly ObjectPool<IMeshBuilder> _pool;
        private readonly HashSet<IMeshBuilder> _usedElements;

        public MeshBuilderPool(MeshDescription description)
        {
            _description = description;
            _pool = new ObjectPool<IMeshBuilder>(ConstructMeshBuilder);
            _usedElements = new HashSet<IMeshBuilder>();
        }

        private IMeshBuilder ConstructMeshBuilder() => new CachedMeshBuilder(_description.ConstructBuilder());

        public IMeshBuilder Get()
        {
            var element = _pool.Get();
            _usedElements.Add(element);
            return element;
        }

        public PooledObject<IMeshBuilder> Get(out IMeshBuilder element)
        {
            var pooledObject = _pool.Get(out element);
            _usedElements.Add(element);
            return pooledObject;
        }

        public void Release(IMeshBuilder element)
        {
            if (_usedElements.Remove(element))
            {
                _pool.Release(element);
            }
        }

        public void Clear() => _pool.Clear();
    }
}