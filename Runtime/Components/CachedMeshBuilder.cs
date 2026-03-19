using System.Collections.Generic;
using UnityEngine.Pool;

namespace SxmTools.UIFactory.Components
{
    internal sealed class CachedMeshBuilder : MeshBuilder<MeshDescription>
    {
        private readonly IMeshBuilder _meshBuilder;
        private (MeshDescription description, List<MeshData> result) _cached;

        public CachedMeshBuilder(IMeshBuilder meshBuilder)
        {
            _meshBuilder = meshBuilder;
        }

        public override void Init() => _meshBuilder.Init();

        protected override void Build(MeshDescription description, List<MeshData> result)
        {
            if (!IsCachedDescriptionChanged() && !description.ForceBuild)
            {
                result.AddRange(_cached.result);
                return;
            }

            _meshBuilder.Build(description, result);

            _cached.description = description;
            _cached.result ??= ListPool<MeshData>.Get();
            _cached.result.Clear();
            _cached.result.AddRange(result);

            return;

            bool IsCachedDescriptionChanged() => _cached.description == null || !_cached.description.Equals(description);
        }

        public override void Dispose()
        {
            _meshBuilder.Dispose();
            ListPool<MeshData>.Release(_cached.result);
            _cached = (null, null);
        }
    }
}