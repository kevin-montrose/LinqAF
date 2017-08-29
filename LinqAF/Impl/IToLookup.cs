using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IToLookup<TItem>
    {
        LookupDefaultEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector);
        LookupSpecificEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer);

        LookupDefaultEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector);

        LookupSpecificEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer);
    }
}
