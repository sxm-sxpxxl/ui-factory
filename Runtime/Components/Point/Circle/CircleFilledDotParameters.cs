using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class CircleFilledDotParameters : BaseFilledDotParameters
    {
        public CircleFilledDotParameters()
        {
        }

        public CircleFilledDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BasePointParameters CloneAsPoint() => new CircleFilledPointParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            Color = this.Color
        };
    }
}