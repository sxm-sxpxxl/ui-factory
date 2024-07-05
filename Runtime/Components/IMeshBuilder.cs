using System.Collections.Generic;

namespace Sxm.UIFactory.Components
{
    public interface IMeshBuilder
    {
        IEnumerable<MeshData> Build(object description);
    }
}