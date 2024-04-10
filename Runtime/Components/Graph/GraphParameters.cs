using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public sealed class GraphParameters : MeshParameters
    {
        public IList<Vector2> Points;
        public BaseDotParameters DotParameters;
        public BaseLineParameters LineParameters;

        public GraphParameters()
        {
        }

        public GraphParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Points = GetListParameter<Vector2>(rawParameters, nameof(Points));
            this.DotParameters = GetMeshParameters<BaseDotParameters>(rawParameters, nameof(DotParameters));
            this.LineParameters = GetMeshParameters<BaseLineParameters>(rawParameters, nameof(LineParameters));
        }

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Points);
            yield return CompareUtils.GetHashCode(this.DotParameters);
            yield return CompareUtils.GetHashCode(this.LineParameters);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<GraphParameters>();
            return CompareUtils.Equals(this.Points, obj.Points) &&
                   CompareUtils.Equals(this.DotParameters, obj.DotParameters) &&
                   CompareUtils.Equals(this.LineParameters, obj.LineParameters);
        }
    }
}