using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class OrderByBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IOrderBy<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultAscending<TItem, TKey>> OrderBy<TKey>(Func<TItem, TKey> keySelector)
        => CommonImplementation.OrderBy<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector);

        public OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerAscending<TItem, TKey>> OrderBy<TKey>(Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
        => CommonImplementation.OrderBy<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector, comparer);

        public OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultDescending<TItem, TKey>> OrderByDescending<TKey>(Func<TItem, TKey> keySelector)
        => CommonImplementation.OrderByDescending<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector);

        public OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerDescending<TItem, TKey>> OrderByDescending<TKey>(Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
        => CommonImplementation.OrderByDescending<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector, comparer);
    }
}
