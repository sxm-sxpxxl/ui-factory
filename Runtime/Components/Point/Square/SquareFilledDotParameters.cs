using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public class SquareFilledDotParameters : BaseFilledDotParameters
    {
        public SquareFilledDotParameters()
        {
        }

        public SquareFilledDotParameters(Dictionary<string, object> rawParameters) : base(rawParameters)
        {
        }

        public override BasePointParameters CloneAsPoint() => new SquareFilledPointParameters
        {
            ForceBuild = this.ForceBuild,
            Size = this.Size,
            Color = this.Color
        };
    }
}