using System;
using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public abstract class MeshParameters
    {
        public bool ForceBuild = false;

        protected MeshParameters()
        {
        }

        protected MeshParameters(Dictionary<string, object> rawParameters)
        {
            this.ForceBuild = GetSimpleParameter<bool>(rawParameters, nameof(ForceBuild));
        }

        protected static T GetSimpleParameter<T>(Dictionary<string, object> rawParameters, string key) where T : struct => UIMeshFactory.GetSimpleParameter<T>(rawParameters, key);

        protected static IList<T> GetListParameter<T>(Dictionary<string, object> rawParameters, string key) where T : struct => UIMeshFactory.GetListParameter<T>(rawParameters, key);

        protected static T GetMeshParameters<T>(Dictionary<string, object> rawParameters, string key) where T : MeshParameters => UIMeshFactory.GetMeshParameters<T>(rawParameters, key);

        public T CastTo<T>() where T : MeshParameters
        {
            if (this is not T obj)
                throw new ArgumentException($"Failed cast to '{typeof(T).Name}'.");

            return obj;
        }

        public override int GetHashCode() => CompareUtils.GetHashCode(GetHashCodes());

        protected virtual IEnumerable<int> GetHashCodes()
        {
            yield return CompareUtils.GetHashCode(this.ForceBuild);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;

            return Equals((MeshParameters) obj);
        }

        protected virtual bool Equals(MeshParameters other) => CompareUtils.Equals(this.ForceBuild, other.ForceBuild);
    }
}