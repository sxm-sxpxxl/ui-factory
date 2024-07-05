using System;
using System.Collections.Generic;
using System.Linq;
using Sxm.UIFactory.Components.Series;

namespace Sxm.UIFactory.Components
{
    public sealed class LineGraphMeshBuilder : MeshBuilder<LineGraphMeshDescription>
    {
        private Guid? _cachedLineSeriesId;
        private Guid? _cachedPointSeriesId;

        public override IEnumerable<MeshData> Build(LineGraphMeshDescription description)
        {
            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: description.Line,
                Padding: 0f,
                Closed: false,
                Positions: description.Positions,
                ForceBuild: description.ForceBuild
            );

            var pointSeriesDescription = new PointSeriesMeshDescription(
                Point: description.Point,
                Positions: description.Positions,
                ForceBuild: description.ForceBuild
            );

            var lineSeriesInstance = UIFactoryManager.Build(lineSeriesDescription, _cachedLineSeriesId);
            var pointSeriesInstance = UIFactoryManager.Build(pointSeriesDescription, _cachedPointSeriesId);

            return lineSeriesInstance.Result.Concat(pointSeriesInstance.Result);
        }
    }
}