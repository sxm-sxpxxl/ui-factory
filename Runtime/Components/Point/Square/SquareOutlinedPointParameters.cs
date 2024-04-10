using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class SquareOutlinedPointParameters : BaseOutlinedPointParameters
    {
        public SquareOutlinedPointParameters()
        {
        }

        public SquareOutlinedPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseDotParameters CloneAsDot() => new SquareOutlinedDotParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            LineParameters = this.LineParameters,
            Color = this.Color
        };
    }
}