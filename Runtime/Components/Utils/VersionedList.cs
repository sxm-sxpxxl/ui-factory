using System.Collections;
using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    public sealed class VersionedList<T> : IReadOnlyList<T>, IVersioned
    {
        private readonly List<T> _list;

        public long Version { get; private set; }
        public int Count => _list.Count;

        public VersionedList()
        {
            _list = new List<T>();
        }

        public VersionedList(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public T this[int index] => _list[index];

        public void Add(T item)
        {
            _list.Add(item);
            Version++;
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            Version++;
        }

        public void Clear()
        {
            _list.Clear();
            Version++;
        }

        public void Set(int index, T value)
        {
            _list[index] = value;
            Version++;
        }

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
