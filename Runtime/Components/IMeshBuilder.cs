using System;
using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal interface IMeshBuilder : IDisposable
    {
        IEnumerable<MeshData> Build(object description);
    }
}