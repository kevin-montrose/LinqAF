using System;
using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ToDictionaryBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IToDictionary<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public Dictionary<TKey, TItem> ToDictionary<TKey>(Func<TItem, TKey> keySelector)
        => CommonImplementation.ToDictionary<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector);

        public Dictionary<TKey, TItem> ToDictionary<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.ToDictionary<TItem, TKey, TEnumerable, TEnumerator>(RefThis(), keySelector, comparer);

        public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Func<TItem, TKey> keySelector, Func<TItem, TValue> elementSelector)
        => CommonImplementation.ToDictionary<TItem, TKey, TValue, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector);

        public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Func<TItem, TKey> keySelector, Func<TItem, TValue> elementSelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.ToDictionary<TItem, TKey, TValue, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, comparer);
    }
}
