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
        public LookupEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector)
        => ToLookup(keySelector, _ => _, null);

        public LookupEnumerable<TKey, TItem> ToLookup<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        => ToLookup(keySelector, _ => _, comparer);

        public LookupEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector)
        => ToLookup(keySelector, elementSelector, null);

        public LookupEnumerable<TKey, TElement> ToLookup<TKey, TElement>(Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.ToLookup<TItem, TKey, TElement, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, comparer);
    }
}
