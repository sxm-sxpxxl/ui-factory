using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public abstract class BaseLineParameters : MeshParameters
    {
        public int Thickness;
        public Color Color;

        protected BaseLineParameters()
        {
        }

        protected BaseLineParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Thickness = GetSimpleParameter<int>(rawParameters, nameof(Thickness));
            this.Color = GetSimpleParameter<Color>(rawParameters, nameof(Color));
        }

        public abstract BaseLineSegmentParameters CloneAsSegment();

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Thickness);
            yield return CompareUtils.GetHashCode(this.Color);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<BaseLineParameters>();
            return CompareUtils.Equals(this.Thickness, obj.Thickness) && CompareUtils.Equals(this.Color, obj.Color);
        }
    }
}