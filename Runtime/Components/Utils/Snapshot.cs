using System;
using System.Runtime.CompilerServices;

namespace SxmTools.UIFactory.Components
{
    public readonly struct Snapshot<T> : IEquatable<Snapshot<T>> where T : class, IVersioned
    {
        public readonly T Collection;
        public readonly long Version;

        public Snapshot(T collection)
        {
            Collection = collection;
            Version = collection.Version;
        }

        public bool Equals(Snapshot<T> other) =>
            ReferenceEquals(Collection, other.Collection) && Version == other.Version;

        public override bool Equals(object obj) =>
            obj is Snapshot<T> other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(RuntimeHelpers.GetHashCode(Collection), Version);

        public static bool operator ==(Snapshot<T> left, Snapshot<T> right) => left.Equals(right);
        public static bool operator !=(Snapshot<T> left, Snapshot<T> right) => !left.Equals(right);
    }
}
