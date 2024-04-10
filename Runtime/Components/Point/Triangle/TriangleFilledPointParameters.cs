using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class TriangleFilledPointParameters : BaseFilledPointParameters
    {
        public TriangleFilledPointParameters()
        {
        }

        public TriangleFilledPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseDotParameters CloneAsDot() => new TriangleFilledDotParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size
        };
    }
}