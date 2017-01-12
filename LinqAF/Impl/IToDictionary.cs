using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IToDictionary<TItem>
    {
        Dictionary<TKey, TItem> ToDictionary<TKey>(Func<TItem, TKey> keySelector);
        Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Func<TItem, TKey> keySelector, Func<TItem, TValue> elementSelector);
        Dictionary<TKey, TItem> ToDictionary<TKey>(Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer);
        Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Func<TItem, TKey> keySelector, Func<TItem, TValue> elementSelector, IEqualityComparer<TKey> comparer);
    }
}
