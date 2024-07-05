using System;
using System.Collections.Generic;
using Sxm.UIFactory.Components.Series;
using UnityEngine;

namespace Sxm.UIFactory.Components.Lines
{
    public sealed class DashLineMeshBuilder : MeshBuilder<DashLineMeshDescription>
    {
        private Guid? _cachedLineSeriesId;

        public override IEnumerable<MeshData> Build(DashLineMeshDescription description)
        {
            var dashWidth = description.DashWidth;
            var dashGap = description.DashGap;

            var dashesCount = Mathf.CeilToInt((description.LineLength + dashGap) / (dashWidth + dashGap));
            var positionsCount = dashesCount + 1;

            var positions = new Vector2[positionsCount];
            for (var i = 0; i < positionsCount; i++)
            {
                positions[i] = Vector2.Lerp(description.StartPosition, description.EndPosition, (float) i / dashesCount);
            }

            var lineSeriesDescription = new LineSeriesMeshDescription(
                Line: new SolidLineMeshDescription(
                    Thickness: description.Thickness,
                    Color: description.Color,
                    ForceBuild: description.ForceBuild
                ),
                Padding: 0.5f * description.DashGap,
                Closed: false,
                Positions: positions,
                ForceBuild: description.ForceBuild
            );

            var instance = UIFactoryManager.Build(lineSeriesDescription, _cachedLineSeriesId).CacheAssignedMeshBuilderId(ref _cachedLineSeriesId);
            return instance.Result;
        }
    }
}