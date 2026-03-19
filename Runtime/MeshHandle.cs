using System;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components;

namespace SxmTools.UIFactory
{
    // todo@sxm: подумать над тем чтобы сделать его struct
    public sealed class MeshHandle : IDisposable
    {
        [CanBeNull] private Type _descriptionType;
        [CanBeNull] private MeshBuilderPool _pool;
        [CanBeNull] private IMeshBuilder _builder;

        internal MeshHandle()
        {
        }

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
            if (_builder != null)
            {
                _builder.Dispose();
                _pool?.Release(_builder);
                _builder = null;
            }

            _pool = null;
            _descriptionType = null;
        }

        public void Dispose()
        {
            ReleaseBuilder();
        }
    }
}