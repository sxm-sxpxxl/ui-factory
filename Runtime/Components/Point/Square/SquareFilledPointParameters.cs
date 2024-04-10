using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class SquareFilledPointParameters : BaseFilledPointParameters
    {
        public SquareFilledPointParameters()
        {
        }

        public SquareFilledPointParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BaseDotParameters CloneAsDot() => new SquareFilledDotParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            Color = this.Color
        };
    }
}