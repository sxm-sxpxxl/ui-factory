﻿using System.Collections.Generic;
using System.Linq;
using Sxm.UIFactory.Components.Series;

namespace Sxm.UIFactory.Components.Graphs
{
    internal sealed class LineGraphMeshBuilder : MeshBuilder<LineGraphMeshDescription>
    {
        private readonly MeshHandle _lineSeriesHandle = new();
        private readonly MeshHandle _pointSeriesHandle = new();

        protected override IEnumerable<MeshData> Build(LineGraphMeshDescription description)
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

            var lineSeriesData = UIFactoryManager.Build(lineSeriesDescription, _lineSeriesHandle);
            var pointSeriesData = UIFactoryManager.Build(pointSeriesDescription, _pointSeriesHandle);

            return lineSeriesData.Concat(pointSeriesData);
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();
            _pointSeriesHandle.Dispose();
        }
    }
}