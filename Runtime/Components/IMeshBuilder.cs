using System;
using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public interface IMeshBuilder : IDisposable
    {
        IEnumerable<MeshData> Build(object description);
    }
}