using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static TakeEnumerable<TItem, TEnumerable, TEnumerator> Take<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int count)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return TakeImpl<TItem, TEnumerable, TEnumerator>(ref source, count);
        }

        internal static TakeEnumerable<TItem, TEnumerable, TEnumerator> TakeImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int count)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if(count < 0)
            {
                count = 0;
            }

            return new TakeEnumerable<TItem, TEnumerable, TEnumerator>(ref source, count);
        }

        public static TakeWhileEnumerable<TItem, TEnumerable, TEnumerator> TakeWhile<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return TakeWhileImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TakeWhileEnumerable<TItem, TEnumerable, TEnumerator> TakeWhileImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new TakeWhileEnumerable<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        public static TakeWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator> TakeWhile<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return TakeWhileImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TakeWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator> TakeWhileImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new TakeWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }
    }
}
