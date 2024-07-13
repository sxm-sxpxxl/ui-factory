using System;
using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    internal interface IMeshBuilder : IDisposable
    {
        IEnumerable<MeshData> Build(object description);
    }
}