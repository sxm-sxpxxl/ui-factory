using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace SxmTools.UIFactory.Components
{
    internal static partial class MeshUtils
    {
        private static NativeArray<Vector2> WrapAsNativeArray(this Vector2[] vectors)
        {
            var nativeVectors = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray(vectors.AsSpan(), Allocator.None);
#if UNITY_EDITOR
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeVectors, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
#endif
            return nativeVectors;
        }

        [BurstCompile]
        private static partial class BurstProcedures
        {
        }
    }
}