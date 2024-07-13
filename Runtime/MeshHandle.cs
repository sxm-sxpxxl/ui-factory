﻿using System;
using Sxm.UIFactory.Components;

namespace Sxm.UIFactory
{
    public sealed class MeshHandle : IDisposable
    {
        private Type _descriptionType;
        private MeshBuilderPool _pool;
        private IMeshBuilder _builder;

        internal IMeshBuilder GetMeshBuilder(MeshBuilderPool pool, Type descriptionType)
        {
            if (_descriptionType == null || _descriptionType != descriptionType)
            {
                ReleaseBuilder();
                _builder = pool.Get();
            }

            _pool = pool;
            _descriptionType = descriptionType;

            return _builder;
        }

        private void ReleaseBuilder()
        {
            _builder?.Dispose();
            _pool?.Release(_builder);
        }

        public void Dispose()
        {
            ReleaseBuilder();
        }
    }
}