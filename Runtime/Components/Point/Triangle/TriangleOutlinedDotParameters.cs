using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class TriangleOutlinedDotParameters : BaseOutlinedDotParameters
    {
        public TriangleOutlinedDotParameters()
        {
        }

        public TriangleOutlinedDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BasePointParameters CloneAsPoint() => new TriangleOutlinedPointParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            LineParameters = this.LineParameters
        };
    }
}