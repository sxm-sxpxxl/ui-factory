using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public class PointSeriesParameters : MeshParameters
    {
        public IList<Vector2> Points;
        public BaseDotParameters DotParameters;

        public PointSeriesParameters()
        {
        }

        public PointSeriesParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Points = GetListParameter<Vector2>(rawParameters, nameof(Points));
            this.DotParameters = GetMeshParameters<BaseDotParameters>(rawParameters, nameof(DotParameters));
        }

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Points);
            yield return CompareUtils.GetHashCode(this.DotParameters);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<PointSeriesParameters>();
            return CompareUtils.Equals(this.Points, obj.Points) &&
                   CompareUtils.Equals(this.DotParameters, obj.DotParameters);
        }
    }
}