using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class CircleOutlinedDotParameters : BaseOutlinedDotParameters
    {
        public CircleOutlinedDotParameters()
        {
        }

        public CircleOutlinedDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BasePointParameters CloneAsPoint() => new CircleOutlinedPointParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            LineParameters = this.LineParameters,
            Color = this.Color
        };
    }
}