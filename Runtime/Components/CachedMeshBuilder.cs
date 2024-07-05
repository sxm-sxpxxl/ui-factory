using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public sealed class CachedMeshBuilder<T> : MeshBuilder<T> where T : MeshDescription
    {
        private readonly IMeshBuilder _meshBuilder;
        private (T description, IEnumerable<MeshData> meshData) _cached;

        public CachedMeshBuilder(IMeshBuilder meshBuilder)
        {
            _meshBuilder = meshBuilder;
        }

        public override IEnumerable<MeshData> Build(T description)
        {
            if (!IsCachedDescriptionChanged(description) && !description.ForceBuild)
                return _cached.meshData;

            var meshData = _meshBuilder.Build(description);
            _cached = (description, meshData);

            return meshData;
        }

        private bool IsCachedDescriptionChanged(T description) => _cached.description == null || !description.Equals(description);
    }
}