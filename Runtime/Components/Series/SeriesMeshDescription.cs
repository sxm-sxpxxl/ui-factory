using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components.Series
{
    public abstract record SeriesMeshDescription(IList<Vector2> Positions, bool ForceBuild = default) : MeshDescription(ForceBuild);
}