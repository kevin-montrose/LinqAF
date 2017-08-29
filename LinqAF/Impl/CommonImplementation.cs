using LinqAF.Config;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool AreEqual<T>(T a, T b, IEqualityComparer<T> comparer)
        {
            return comparer.Equals(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool AreEqual<T>(T a, T b)
        {
            if (a == null && b == null) return true;

            var aEquatable = a as IEquatable<T>;
            if (aEquatable != null)
            {
                return aEquatable.Equals(b);
            }

            var bEquatable = b as IEquatable<T>;
            if (bEquatable != null)
            {
                return bEquatable.Equals(a);
            }

            if (a != null)
            {
                return a.Equals(b);
            }

            if (b != null)
            {
                return b.Equals(a);
            }

            throw UnexpectedPath(nameof(AreEqual));
        }
        
        internal static TItem[] Buffer<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, out int count)
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            const int BUFFER_INITIAL_SIZE = 4;

            var ret = Allocator.Current.GetArray<TItem>(BUFFER_INITIAL_SIZE);
            var ix = 0;
            foreach (var item in source)
            {
                if (ix == ret.Length)
                {
                    Allocator.Current.ResizeArray(ref ret, NextSize(ret.Length));
                }

                ret[ix] = item;
                ix++;
            }

            count = ix;
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetHashCode<T>(T val, IEqualityComparer<T> comparer)
        {
            if (val == null) return 0;

            return comparer.GetHashCode(val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetHashCode<T>(T val)
        {
            if (val == null) return 0;

            return val.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int NextSize(int curSize)
        {
            const int MAX_DOUBLE_SIZE = 4096;

            if (curSize < MAX_DOUBLE_SIZE)
            {
                return curSize * 2;
            }

            return curSize + MAX_DOUBLE_SIZE;
        }
    }
}