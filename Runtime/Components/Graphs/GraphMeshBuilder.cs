using System.Collections.Generic;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Series;

namespace SxmTools.UIFactory.Components.Graphs
{
    internal sealed class GraphMeshBuilder : MeshBuilder<GraphMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;
        [CanBeNull] private MeshHandle _pointSeriesHandle;

        private readonly LineSeriesMeshDescription _reusableLineSeries = new(
            Line: null,
            Padding: 0f,
            Closed: false,
            Positions: default,
            ForceBuild: true
        );

        private readonly PointSeriesMeshDescription _reusablePointSeries = new(
            Point: null,
            Positions: default,
            ForceBuild: true
        );

        protected override void Build(GraphMeshDescription description, List<MeshData> result)
        {
            _reusableLineSeries.Line = description.Line;
            _reusableLineSeries.Positions = description.Positions;
            _lineSeriesHandle = UIFactoryManager.BuildMesh(_reusableLineSeries, result, _lineSeriesHandle);

            if (description.Point == null)
                return;

            _reusablePointSeries.Point = description.Point;
            _reusablePointSeries.Positions = description.Positions;
            _reusablePointSeries.IgnoredPointIndices = description.IgnoredPointIndices;
            _reusablePointSeries.SelectionPoint = description.SelectionPoint;
            _reusablePointSeries.SelectionPointIndices = description.SelectionPointIndices;
            _pointSeriesHandle = UIFactoryManager.BuildMesh(_reusablePointSeries, result, _pointSeriesHandle);
        }

        public override void Dispose()
        {
            _lineSeriesHandle?.Dispose();
            _pointSeriesHandle?.Dispose();
        }
    }
}