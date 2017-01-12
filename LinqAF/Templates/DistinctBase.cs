using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class DistinctBase<TItem, TInnerEnumerable, TInnerEnumerator> :
        TemplateBase,
        IDistinct<TItem, TInnerEnumerable, TInnerEnumerator>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct()
        => CommonImplementation.Distinct<TItem, TInnerEnumerable, TInnerEnumerator>(RefThis());

        public DistinctSpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct(IEqualityComparer<TItem> comparer)
        => CommonImplementation.Distinct<TItem, TInnerEnumerable, TInnerEnumerator>(RefThis(), comparer);
    }
}
