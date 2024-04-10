using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public abstract class BasePointParameters : MeshParameters
    {
        public Vector2 Origin;
        public Color Color;

        protected BasePointParameters()
        {
        }

        protected BasePointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Origin = GetSimpleParameter<Vector2>(rawParameters, nameof(Origin));
            this.Color = GetSimpleParameter<Color>(rawParameters, nameof(Color));
        }

        public abstract BaseDotParameters CloneAsDot();

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Origin);
            yield return CompareUtils.GetHashCode(this.Color);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<BasePointParameters>();
            return CompareUtils.Equals(this.Origin, obj.Origin) && CompareUtils.Equals(this.Color, obj.Color);
        }
    }
}