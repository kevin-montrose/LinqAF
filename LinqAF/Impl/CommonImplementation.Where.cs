using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Where<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TItem, bool> predicate)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return WhereImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, predicate);
        }

        internal static WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> WhereImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TItem, bool> predicate)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            return new WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, predicate);
        }

        public static WhereIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Where<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TItem, int, bool> predicate)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return WhereImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, predicate);
        }

        internal static WhereIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> WhereImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TItem, int, bool> predicate)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            return new WhereIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, predicate);
        }
    }
}
