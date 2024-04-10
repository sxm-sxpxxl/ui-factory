using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class SquareOutlinedDotParameters : BaseOutlinedDotParameters
    {
        public SquareOutlinedDotParameters()
        {
        }

        public SquareOutlinedDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BasePointParameters CloneAsPoint() => new SquareOutlinedPointParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            LineParameters = this.LineParameters,
            Color = this.Color
        };
    }
}