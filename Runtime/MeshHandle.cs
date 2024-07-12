using System;
using Sxm.UIFactory.Components;

namespace Sxm.UIFactory
{
    public sealed class MeshHandle : IDisposable
    {
        private MeshDescription _description;
        private IMeshBuilder _builder;

        public IMeshBuilder GetMeshBuilder(MeshDescription description)
        {
            if (_description == null || _description.GetType() != description.GetType())
            {
                _builder = new CachedMeshBuilder(description.ConstructBuilder());
            }

            _description = description;
            return _builder;
        }

        public void Dispose()
        {
            _builder?.Dispose();
        }
    }
}