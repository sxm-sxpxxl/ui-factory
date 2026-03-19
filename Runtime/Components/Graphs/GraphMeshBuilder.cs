using System.Collections.Generic;
using JetBrains.Annotations;
using SxmTools.UIFactory.Components.Series;

namespace SxmTools.UIFactory.Components.Graphs
{
    internal sealed class GraphMeshBuilder : MeshBuilder<GraphMeshDescription>
    {
        private MeshHandle _lineSeriesHandle;
        [CanBeNull] private MeshHandle _pointSeriesHandle;

        protected override void Build(GraphMeshDescription description, List<MeshData> result)
        {
            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: description.Line,
                Padding: 0f,
                Closed: false,
                Positions: description.Positions,
                ForceBuild: description.ForceBuild
            );
            _lineSeriesHandle = UIFactoryManager.BuildMesh(lineSeriesDescription, result, _lineSeriesHandle);

            if (description.Point == null)
                return;

            var pointSeriesDescription = new PointSeriesMeshDescription(
                Point: description.Point,
                Positions: description.Positions,
                IgnoredPointIndices: description.IgnoredPointIndices,
                ForceBuild: description.ForceBuild
            );
            _pointSeriesHandle = UIFactoryManager.BuildMesh(pointSeriesDescription, result, _pointSeriesHandle);
        }

        public override void Dispose()
        {
            _lineSeriesHandle.Dispose();
            _pointSeriesHandle?.Dispose();
        }
    }
}