using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public abstract class BaseDotParameters : MeshParameters
    {
        public Color Color;

        protected BaseDotParameters()
        {
        }

        protected BaseDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Color = GetSimpleParameter<Color>(rawParameters, nameof(Color));
        }

        public abstract BasePointParameters CloneAsPoint();

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Color);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<BaseDotParameters>();
            return CompareUtils.Equals(this.Color, obj.Color);
        }
    }
}