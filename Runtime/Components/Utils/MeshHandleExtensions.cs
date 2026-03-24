using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    public static class MeshHandleExtensions
    {
        public static void ResizeHandles(this List<MeshHandle> handles, int targetCount)
        {
            for (var i = handles.Count - 1; i >= targetCount; i--)
            {
                handles[i].Dispose();
                handles.RemoveAt(i);
            }

            for (var i = handles.Count; i < targetCount; i++)
            {
                handles.Add(new MeshHandle());
            }
        }
    }
}