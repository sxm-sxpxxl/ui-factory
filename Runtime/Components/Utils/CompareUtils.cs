using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    internal static class CompareUtils
    {
        public static bool Equals(bool lhs, bool rhs) => lhs == rhs;

        public static bool Equals(int lhs, int rhs) => lhs == rhs;

        public static bool Equals(float lhs, float rhs) => Mathf.Abs(lhs - rhs) < float.Epsilon;

        public static bool Equals(Vector2 lhs, Vector2 rhs) => Equals(lhs.x, rhs.x) && Equals(lhs.y, rhs.y);

        public static bool Equals(Color lhs, Color rhs) => Equals(lhs.r, rhs.r) && Equals(lhs.g, rhs.g) && Equals(lhs.b, rhs.b) && Equals(lhs.a, rhs.a);

        public static bool Equals<T>(T lhs, T rhs) where T : MeshParameters => lhs.Equals(rhs);

        public static bool Equals<T>(IList<T> lhs, IList<T> rhs)
        {
            if (lhs.Count != rhs.Count)
                return false;

            for (var i = 0; i < lhs.Count; i++)
            {
                if (!lhs[i].Equals(rhs[i]))
                    return false;
            }

            return true;
        }

        public static int GetHashCode(bool value) => value.GetHashCode();

        public static int GetHashCode(int value) => value.GetHashCode();

        public static int GetHashCode(float value) => value.GetHashCode();

        public static int GetHashCode<T>(T value) where T : MeshParameters => value.GetHashCode();

        public static int GetHashCode(Vector2 value) => value.GetHashCode();

        public static int GetHashCode(Color value) => value.GetHashCode();

        public static int GetHashCode<T>(IEnumerable<T> array)
        {
            var hashCodes = array.Select(x => x.GetHashCode());
            return GetHashCode(hashCodes);
        }

        public static int GetHashCode(IEnumerable<int> hashCodes)
        {
            var resultHash = 17;

            foreach (var hashCode in hashCodes)
            {
                resultHash = resultHash * 31 + hashCode;
            }

            return resultHash;
        }
    }
}