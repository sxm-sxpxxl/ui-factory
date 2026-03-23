using System.Collections.Generic;

namespace SxmTools.UIFactory.Components
{
    internal static class CollectionEquality
    {
        public static bool SequenceEqual<T>(IList<T> a, IList<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            var comparer = EqualityComparer<T>.Default;
            for (var i = 0; i < a.Count; i++)
            {
                if (!comparer.Equals(a[i], b[i])) return false;
            }

            return true;
        }

        public static bool SetEqual<T>(HashSet<T> a, HashSet<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.SetEquals(b);
        }

        public static int GetSequenceHashCode<T>(IList<T> list)
        {
            if (list is null) return 0;

            var hash = list.Count;
            var comparer = EqualityComparer<T>.Default;
            for (var i = 0; i < list.Count; i++)
            {
                hash = hash * 397 ^ comparer.GetHashCode(list[i]);
            }

            return hash;
        }

        public static int GetSetHashCode<T>(HashSet<T> set)
        {
            if (set is null) return 0;

            var hash = set.Count;
            foreach (var item in set)
            {
                hash ^= EqualityComparer<T>.Default.GetHashCode(item);
            }

            return hash;
        }
    }
}
