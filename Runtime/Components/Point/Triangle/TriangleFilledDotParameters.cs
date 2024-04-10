using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class TriangleFilledDotParameters : BaseFilledDotParameters
    {
        public TriangleFilledDotParameters()
        {
        }

        public TriangleFilledDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BasePointParameters CloneAsPoint() => new TriangleFilledPointParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size
        };
    }
}