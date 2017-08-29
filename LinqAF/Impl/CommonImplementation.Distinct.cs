using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source)
            where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return DistinctImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref source);
        }

        internal static DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DistinctImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            return new DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref source);
        }

        public static DistinctSpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, IEqualityComparer<TItem> comparer)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return DistinctImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, comparer);
        }

        internal static DistinctSpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DistinctImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, IEqualityComparer<TItem> comparer)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? EqualityComparer<TItem>.Default;

            return new DistinctSpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, comparer);
        }
    }
}
