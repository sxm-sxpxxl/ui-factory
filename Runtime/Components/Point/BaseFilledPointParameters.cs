using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public abstract class BaseFilledPointParameters : BasePointParameters
    {
        public float Size;

        protected BaseFilledPointParameters()
        {
        }

        protected BaseFilledPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Size = GetSimpleParameter<float>(rawParameters, nameof(Size));
        }

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Size);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<BaseFilledPointParameters>();
            return CompareUtils.Equals(this.Size, obj.Size);
        }
    }
}