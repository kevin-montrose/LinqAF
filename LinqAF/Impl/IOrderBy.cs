using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IOrderBy<TItem, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultAscending<TItem, TKey>> OrderBy<TKey>(Func<TItem, TKey> keySelector);
        OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerAscending<TItem, TKey>> OrderBy<TKey>(Func<TItem, TKey> keySelector, IComparer<TKey> comparer);

        OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultDescending<TItem, TKey>> OrderByDescending<TKey>(Func<TItem, TKey> keySelector);
        OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerDescending<TItem, TKey>> OrderByDescending<TKey>(Func<TItem, TKey> keySelector, IComparer<TKey> comparer);
    }
}
