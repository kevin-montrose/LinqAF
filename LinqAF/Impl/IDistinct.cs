using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IDistinct<TItem, TInnerEnumerable, TInnerEnumerator>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct();
        DistinctSpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct(IEqualityComparer<TItem> comparer);
    }
}
