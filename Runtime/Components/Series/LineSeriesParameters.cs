using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public class LineSeriesParameters : MeshParameters
    {
        public IList<Vector2> Points;
        public BaseLineParameters LineParameters;
        public float PointOffset;
        public bool Closed;

        public LineSeriesParameters()
        {
        }

        public LineSeriesParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Points = GetListParameter<Vector2>(rawParameters, nameof(Points));
            this.LineParameters = GetMeshParameters<BaseLineParameters>(rawParameters, nameof(LineParameters));
            this.PointOffset = GetSimpleParameter<float>(rawParameters, nameof(PointOffset));
            this.Closed = GetSimpleParameter<bool>(rawParameters, nameof(Closed));
        }

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Points);
            yield return CompareUtils.GetHashCode(this.LineParameters);
            yield return CompareUtils.GetHashCode(this.PointOffset);
            yield return CompareUtils.GetHashCode(this.Closed);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<LineSeriesParameters>();
            return CompareUtils.Equals(this.Points, obj.Points) &&
                   CompareUtils.Equals(this.LineParameters, obj.LineParameters) &&
                   CompareUtils.Equals(this.PointOffset, obj.PointOffset) &&
                   CompareUtils.Equals(this.Closed, obj.Closed);
        }
    }
}