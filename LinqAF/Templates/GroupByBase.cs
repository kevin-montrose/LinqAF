using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class GroupByBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IGroupBy<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public GroupByDefaultEnumerable<TItem, TKey, TItem, TEnumerable, TEnumerator> GroupBy<TKey>(Func<TItem, TKey> keySelector)
        {
            Func<TItem, TItem> elementSelector = _ => _;
            return CommonImplementation.GroupBy<TItem, TKey, TItem, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector);
        }

        public GroupBySpecificEnumerable<TItem, TKey, TItem, TEnumerable, TEnumerator> GroupBy<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            Func<TItem, TItem> elementSelector = _ => _;
            return CommonImplementation.GroupBy<TItem, TKey, TItem, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, comparer);
        }

        public GroupByDefaultEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator> GroupBy<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector)
        => CommonImplementation.GroupBy<TItem, TKey, TElement, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector);

        public GroupBySpecificEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator> GroupBy<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.GroupBy<TItem, TKey, TElement, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, comparer);

        public GroupByCollectionDefaultEnumerable<TItem, TKey, TItem, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TResult>(Func<TItem, TKey> keySelector, Func<TKey, GroupedEnumerable<TKey, TItem>, TResult> resultSelector)
        {
            Func<TItem, TItem> elementSelector = _ => _;
            return CommonImplementation.GroupBy<TItem, TKey, TItem, TResult, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, resultSelector);
        }

        public GroupByCollectionSpecificEnumerable<TItem, TKey, TItem, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TResult>(Func<TItem, TKey> keySelector, Func<TKey, GroupedEnumerable<TKey, TItem>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Func<TItem, TItem> elementSelector = _ => _;
            return CommonImplementation.GroupBy<TItem, TKey, TItem, TResult, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, resultSelector, comparer);
        }

        public GroupByCollectionDefaultEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TElement, TResult>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector)
        => CommonImplementation.GroupBy<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, resultSelector);

        public GroupByCollectionSpecificEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupBy<TKey, TElement, TResult>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.GroupBy<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, resultSelector, comparer);
    }
}
