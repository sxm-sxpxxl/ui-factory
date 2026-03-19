using System;
using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal interface IMeshBuilder : IDisposable
    {
        void Init();
        void Build(object description, List<MeshData> result);
    }
}