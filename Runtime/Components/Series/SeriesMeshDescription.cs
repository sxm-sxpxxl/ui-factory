using System;
using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    public abstract record SeriesMeshDescription(IList<Vector2> Positions, bool ForceBuild = default) : MeshDescription(ForceBuild)
    {
        public virtual bool Equals(SeriesMeshDescription other) =>
            base.Equals(other) && CollectionEquality.SequenceEqual(Positions, other?.Positions);

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), CollectionEquality.GetSequenceHashCode(Positions));
    }
}
