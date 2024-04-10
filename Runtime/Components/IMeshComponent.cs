using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    internal interface IMeshComponent
    {
        IReadOnlyList<MeshData> Build<T>(T parameters) where T : MeshParameters;
    }
}