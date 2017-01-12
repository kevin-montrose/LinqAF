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
        => ToDictionary<TKey, TItem>(keySelector, _ => _, EqualityComparer<TKey>.Default);

        public Dictionary<TKey, TItem> ToDictionary<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        => ToDictionary<TKey, TItem>(keySelector, _ => _, comparer);

        public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Func<TItem, TKey> keySelector, Func<TItem, TValue> elementSelector)
        => ToDictionary<TKey, TValue>(keySelector, elementSelector, EqualityComparer<TKey>.Default);

        public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Func<TItem, TKey> keySelector, Func<TItem, TValue> elementSelector, IEqualityComparer<TKey> comparer)
        => CommonImplementation.ToDictionary<TItem, TKey, TValue, TEnumerable, TEnumerator>(RefThis(), keySelector, elementSelector, comparer);
    }
}
