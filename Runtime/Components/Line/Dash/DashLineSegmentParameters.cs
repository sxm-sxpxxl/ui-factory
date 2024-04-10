using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class DashLineSegmentParameters : BaseLineSegmentParameters
    {
        public float DashWidth;
        public float DashGap;

        public DashLineSegmentParameters()
        {
        }

        public DashLineSegmentParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
            this.DashWidth = GetSimpleParameter<float>(rawParameters, nameof(DashWidth));
            this.DashGap = GetSimpleParameter<float>(rawParameters, nameof(DashGap));
        }

        public override BaseLineParameters CloneAsLine() => new DashLineParameters
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

            DashLineSegmentParameters obj = other.CastTo<DashLineSegmentParameters>();
            return CompareUtils.Equals(this.DashWidth, obj.DashWidth) &&
                   CompareUtils.Equals(this.DashGap, obj.DashGap);
        }
    }
}