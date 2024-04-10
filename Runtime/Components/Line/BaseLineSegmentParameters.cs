using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    public abstract class BaseLineSegmentParameters : MeshParameters
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        public int Thickness;
        public Color Color;

        public float LineLength => (EndPoint - StartPoint).magnitude;
        public Vector2 Direction => (EndPoint - StartPoint).normalized;

        protected BaseLineSegmentParameters()
        {
        }

        protected BaseLineSegmentParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.StartPoint = GetSimpleParameter<Vector2>(rawParameters, nameof(StartPoint));
            this.EndPoint = GetSimpleParameter<Vector2>(rawParameters, nameof(EndPoint));
            this.Thickness = GetSimpleParameter<int>(rawParameters, nameof(Thickness));
            this.Color = GetSimpleParameter<Color>(rawParameters, nameof(Color));
        }

        public abstract BaseLineParameters CloneAsLine();

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.StartPoint);
            yield return CompareUtils.GetHashCode(this.EndPoint);
            yield return CompareUtils.GetHashCode(this.Thickness);
            yield return CompareUtils.GetHashCode(this.Color);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<BaseLineSegmentParameters>();
            return CompareUtils.Equals(this.StartPoint, obj.StartPoint) &&
                   CompareUtils.Equals(this.EndPoint, obj.EndPoint) &&
                   CompareUtils.Equals(this.Thickness, obj.Thickness) &&
                   CompareUtils.Equals(this.Color, obj.Color);
        }
    }
}