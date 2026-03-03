using System;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components;

namespace SxmTools.UIFactory
{
    public sealed class MeshHandle : IDisposable
    {
        [CanBeNull] private Type _descriptionType;
        [CanBeNull] private MeshBuilderPool _pool;
        [CanBeNull] private IMeshBuilder _builder;

        internal IMeshBuilder GetMeshBuilder(MeshBuilderPool pool, Type descriptionType)
        {
            if (_descriptionType == null || _descriptionType != descriptionType)
            {
                ReleaseBuilder();
                _builder = pool.Get();
                _builder!.Init();
            }

            _pool = pool;
            _descriptionType = descriptionType;

            return _builder;
        }

        private void ReleaseBuilder()
        {
            _builder?.Dispose();
            _builder = null;
            _pool?.Release(_builder);
            _pool = null;
            _descriptionType = null;
        }

        public void Dispose()
        {
            ReleaseBuilder();
        }
    }
}