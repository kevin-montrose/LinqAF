using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ToLookupBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IToLookup<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public LookupDefaultEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector)
        => CommonImplementation.ToLookup<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector);

        public LookupSpecificEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.ToLookup<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector, comparer);

        public LookupDefaultEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector)
        => CommonImplementation.ToLookup<TItem, TKey, TElement, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector);

        public LookupSpecificEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.ToLookup<TItem, TKey, TElement, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, comparer);
    }
}
