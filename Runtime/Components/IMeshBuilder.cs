using System;
using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal interface IMeshBuilder : IDisposable
    {
        void Init();
        IReadOnlyList<MeshData> Build(object description);
    }
}