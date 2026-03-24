using System.Collections;
using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    public sealed class VersionedHashSet<T> : IReadOnlyCollection<T>, IVersioned
    {
        private readonly HashSet<T> _set = new();

        public long Version { get; private set; }
        public int Count => _set.Count;

        public bool Add(T item)
        {
            if (!_set.Add(item)) return false;
            Version++;
            return true;
        }

        public bool Remove(T item)
        {
            if (!_set.Remove(item)) return false;
            Version++;
            return true;
        }

        public void Clear()
        {
            _set.Clear();
            Version++;
        }

        public bool Contains(T item) => _set.Contains(item);

        public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
