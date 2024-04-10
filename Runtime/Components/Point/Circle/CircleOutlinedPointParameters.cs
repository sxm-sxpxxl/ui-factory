using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class CircleOutlinedPointParameters : BaseOutlinedPointParameters
    {
        public CircleOutlinedPointParameters()
        {
        }

        public CircleOutlinedPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseDotParameters CloneAsDot() => new CircleOutlinedDotParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            LineParameters = this.LineParameters,
            Color = this.Color
        };
    }
}