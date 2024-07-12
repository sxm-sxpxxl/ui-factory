using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public sealed class CachedMeshBuilder : MeshBuilder<MeshDescription>
    {
        private readonly IMeshBuilder _meshBuilder;
        private (MeshDescription description, IEnumerable<MeshData> meshData) _cached;

        public CachedMeshBuilder(IMeshBuilder meshBuilder)
        {
            _meshBuilder = meshBuilder;
        }

        protected override IEnumerable<MeshData> Build(MeshDescription description)
        {
            if (!IsCachedDescriptionChanged(description) && !description.ForceBuild)
                return _cached.meshData;

            var meshData = _meshBuilder.Build(description);
            _cached = (description, meshData);

            return meshData;
        }

        private bool IsCachedDescriptionChanged(MeshDescription description) => _cached.description == null || !description.Equals(description);

        public override void Dispose() => _meshBuilder.Dispose();
    }
}