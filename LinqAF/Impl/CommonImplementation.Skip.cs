using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static SkipEnumerable<TItem, TEnumerable, TEnumerator> Skip<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int count)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return SkipImpl<TItem, TEnumerable, TEnumerator>(ref source, count);
        }

        internal static SkipEnumerable<TItem, TEnumerable, TEnumerator> SkipImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int count)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if(count < 0)
            {
                count = 0;
            }

            return new SkipEnumerable<TItem, TEnumerable, TEnumerator>(ref source, count);
        }

        public static SkipWhileEnumerable<TItem, TEnumerable, TEnumerator> SkipWhile<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return SkipWhileImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static SkipWhileEnumerable<TItem, TEnumerable, TEnumerator> SkipWhileImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new SkipWhileEnumerable<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        public static SkipWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator> SkipWhile<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return SkipWhileImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static SkipWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator> SkipWhileImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new SkipWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        public static SkipLastEnumerable<TItem, TEnumerable, TEnumerator> SkipLast<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int count)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return SkipLastImpl<TItem, TEnumerable, TEnumerator>(ref source, count);
        }

        internal static SkipLastEnumerable<TItem, TEnumerable, TEnumerator> SkipLastImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int count)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (count < 0)
            {
                count = 0;
            }

            return new SkipLastEnumerable<TItem, TEnumerable, TEnumerator>(ref source, count);
        }
    }
}
