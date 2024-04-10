using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class CircleFilledPointParameters : BaseFilledPointParameters
    {
        public CircleFilledPointParameters()
        {
        }

        public CircleFilledPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseDotParameters CloneAsDot() => new CircleFilledDotParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            Color = this.Color
        };
    }
}