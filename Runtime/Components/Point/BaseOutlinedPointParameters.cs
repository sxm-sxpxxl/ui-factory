using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public abstract class BaseOutlinedPointParameters : BasePointParameters
    {
        public float Size;
        public BaseLineParameters LineParameters;

        protected BaseOutlinedPointParameters()
        {
        }

        protected BaseOutlinedPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.Size = GetSimpleParameter<float>(rawParameters, nameof(Size));
            this.LineParameters = GetMeshParameters<BaseLineParameters>(rawParameters, nameof(LineParameters));
        }

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.Size);
            yield return CompareUtils.GetHashCode(this.LineParameters);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<BaseOutlinedPointParameters>();
            return CompareUtils.Equals(this.Size, obj.Size) &&
                   CompareUtils.Equals(this.LineParameters, obj.LineParameters);
        }
    }
}