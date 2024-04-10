using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class TriangleOutlinedPointParameters : BaseOutlinedPointParameters
    {
        public TriangleOutlinedPointParameters()
        {
        }

        public TriangleOutlinedPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseDotParameters CloneAsDot() => new TriangleOutlinedDotParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            LineParameters = this.LineParameters
        };
    }
}