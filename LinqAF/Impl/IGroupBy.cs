using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IGroupBy<TInItem, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<TInItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TInItem>
    {
        GroupByDefaultEnumerable<TInItem, TKey, TInItem, TEnumerable, TEnumerator> GroupBy<TKey>(Func<TInItem, TKey> keySelector);
        GroupBySpecificEnumerable<TInItem, TKey, TInItem, TEnumerable, TEnumerator> GroupBy<TKey>(Func<TInItem, TKey> keySelector, IEqualityComparer<TKey> comparer);

        GroupByDefaultEnumerable<TInItem, TKey, TElement, TEnumerable, TEnumerator> GroupBy<TKey, TElement>(Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector);
        GroupBySpecificEnumerable<TInItem, TKey, TElement, TEnumerable, TEnumerator> GroupBy<TKey, TElement>(Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector, IEqualityComparer<TKey> comparer);

        GroupByCollectionDefaultEnumerable<TInItem, TKey, TInItem, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TResult>(Func<TInItem, TKey> keySelector, Func<TKey, GroupedEnumerable<TKey, TInItem>, TResult> resultSelector);
        GroupByCollectionSpecificEnumerable<TInItem, TKey, TInItem, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TResult>(Func<TInItem, TKey> keySelector, Func<TKey, GroupedEnumerable<TKey, TInItem>, TResult> resultSelector, IEqualityComparer<TKey> comparer);

        GroupByCollectionDefaultEnumerable<TInItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TElement, TResult>(Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector);
        GroupByCollectionSpecificEnumerable<TInItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TElement, TResult>(Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer);
    }
}
