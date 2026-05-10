using SxmTools.UIFactory.Components.Lines;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Series
{
    public sealed record LineSeriesMeshDescription(
        SolidLineMeshDescription Line,
        float Padding,
        bool Closed,
        Snapshot<VersionedList<Vector2>> Positions,
        bool ForceBuild = default
    ) : SeriesMeshDescription(Positions, ForceBuild)
    {
        public SolidLineMeshDescription Line { get; set; } = Line;
        public float Padding { get; set; } = Padding;
        public bool Closed { get; set; } = Closed;

        internal override IMeshBuilder ConstructBuilder() => new LineSeriesMeshBuilder();
    }
}