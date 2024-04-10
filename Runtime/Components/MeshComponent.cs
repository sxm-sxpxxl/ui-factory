using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Sxm.UIFactory.Components
{
    internal abstract class MeshComponent<TParameters> : IMeshComponent
        where TParameters : MeshParameters
    {
        private List<MeshData> _cachedMeshes;
        private TParameters _cachedParameters;

        IReadOnlyList<MeshData> IMeshComponent.Build<T>(T parameters)
        {
            var castedParameters = parameters.CastTo<TParameters>();
            return Build(castedParameters);
        }

        private IReadOnlyList<MeshData> Build(TParameters parameters)
        {
            Assert.IsNotNull(parameters);
            if (!IsCachedParametersChanged(parameters) && !parameters.ForceBuild)
                return _cachedMeshes;

            _cachedMeshes ??= new List<MeshData>();
            _cachedMeshes.Clear();

            BuildInternal(ref _cachedMeshes, _cachedParameters);
            return _cachedMeshes.AsReadOnly();
        }

        protected abstract void BuildInternal(ref List<MeshData> meshes, TParameters parameters);

        private bool IsCachedParametersChanged(TParameters parameters)
        {
            if (_cachedParameters == null || !_cachedParameters.Equals(parameters))
            {
                _cachedParameters = parameters;
                return true;
            }

            return false;
        }
    }
}