using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    public abstract record SeriesMeshDescription(
        Snapshot<VersionedList<Vector2>> Positions,
        bool ForceBuild = default
    ) : MeshDescription(ForceBuild)
    {
        public Snapshot<VersionedList<Vector2>> Positions { get; set; } = Positions;
    }
}