using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class SolidLineParameters : BaseLineParameters
    {
        public SolidLineParameters()
        {
        }

        public SolidLineParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseLineSegmentParameters CloneAsSegment() => new SolidLineSegmentParameters
        {
            ForceBuild = this.ForceBuild,
            Thickness = this.Thickness,
            Color = this.Color
        };
    }
}