using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class SolidLineSegmentParameters : BaseLineSegmentParameters
    {
        public SolidLineSegmentParameters()
        {
        }

        public SolidLineSegmentParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseLineParameters CloneAsLine() => new SolidLineParameters
        {
            ForceBuild = this.ForceBuild,
            Thickness = this.Thickness,
            Color = this.Color
        };
    }
}