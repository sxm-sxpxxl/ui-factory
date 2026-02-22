using System.Collections.Generic;
using System.Linq;
using SxmTools.UIFactory.Components.Series;

namespace SxmTools.UIFactory.Components.Graphs
{
    internal sealed class GraphMeshBuilder : MeshBuilder<GraphMeshDescription>
    {
        private readonly MeshHandle _lineSeriesHandle = new();
        private readonly MeshHandle _pointSeriesHandle = new();

        protected override IEnumerable<MeshData> Build(GraphMeshDescription description)
        {
            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: description.Line,
                Padding: 0f,
                Closed: false,
                Positions: description.Positions,
                ForceBuild: description.ForceBuild
            );
            var lineSeriesData = UIFactoryManager.Build(lineSeriesDescription, _lineSeriesHandle);

            if (description.Point == null)
                return lineSeriesData;

            var pointSeriesDescription = new PointSeriesMeshDescription(
                Point: description.Point,
                Positions: description.Positions,
                IgnoredPointIndices: description.IgnoredPointIndices,
                ForceBuild: description.ForceBuild
            );
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