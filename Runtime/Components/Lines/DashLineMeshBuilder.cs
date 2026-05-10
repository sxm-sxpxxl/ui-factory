using System.Collections.Generic;
using UnityEngine;

namespace SxmTools.UIFactory.Components.Lines
{
    internal sealed class DashLineMeshBuilder : MeshBuilder<DashLineMeshDescription>
    {
        private MeshData _result;
        private int _dashesCount;

        protected override void Build(DashLineMeshDescription description, List<MeshData> result)
        {
            var dashWidth = description.DashWidth;
            var dashGap = description.DashGap;
            var lineLength = description.LineLength;

            var dashesCount = Mathf.Max(0, Mathf.CeilToInt((lineLength + dashGap) / (dashWidth + dashGap)));
            if (dashesCount == 0)
                return;

            if (_dashesCount != dashesCount)
            {
                if (_result.Inited)
                {
                    _result.Dispose();
                }

                _result = MeshData.AllocateQuads(dashesCount);
                _dashesCount = dashesCount;
            }

            MeshUtils.CreateDashLineMesh(
                ref _result,
                dashesCount,
                description.StartPosition,
                description.EndPosition,
                description.DashGap,
                description.Thickness,
                description.Color
            );

            result.Add(_result);
        }

        public override void Dispose()
        {
            _result.Dispose();
            _dashesCount = 0;
        }
    }
}