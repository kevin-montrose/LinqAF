using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IToLookup<TItem>
    {
        LookupEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector);
        LookupEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer);

        LookupEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector);

        LookupEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer);
    }
}
