using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class DashLineParameters : BaseLineParameters
    {
        public float DashWidth;
        public float DashGap;

        public DashLineParameters()
        {
        }

        public DashLineParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.DashWidth = GetSimpleParameter<float>(rawParameters, nameof(DashWidth));
            this.DashGap = GetSimpleParameter<float>(rawParameters, nameof(DashGap));
        }

        public override BaseLineSegmentParameters CloneAsSegment() => new DashLineSegmentParameters
        {
            ForceBuild = this.ForceBuild,
            Thickness = this.Thickness,
            DashWidth = this.DashWidth,
            DashGap = this.DashGap
        };

        protected override IEnumerable<int> GetHashCodes()
        {
            foreach (var hashCode in base.GetHashCodes())
                yield return hashCode;

            yield return CompareUtils.GetHashCode(this.DashWidth);
            yield return CompareUtils.GetHashCode(this.DashGap);
        }

        protected override bool Equals(MeshParameters other)
        {
            if (!base.Equals(other))
                return false;

            var obj = other.CastTo<DashLineParameters>();
            return CompareUtils.Equals(this.DashWidth, obj.DashWidth) &&
                   CompareUtils.Equals(this.DashGap, obj.DashGap);
        }
    }
}