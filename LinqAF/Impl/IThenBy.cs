using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IThenBy<TItem, TKey, TComparer, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
        where TComparer: struct, IStructComparer<TItem, TKey>
    {
        OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TEnumerable, TEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, DefaultAscending<TItem, TSecondKey>>> ThenBy<TSecondKey>(Func<TItem, TSecondKey> keySelector);
        OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TEnumerable, TEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, SingleComparerAscending<TItem, TSecondKey>>> ThenBy<TSecondKey>(Func<TItem, TSecondKey> keySelector, IComparer<TSecondKey> comparer);

        OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TEnumerable, TEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, DefaultDescending<TItem, TSecondKey>>> ThenByDescending<TSecondKey>(Func<TItem, TSecondKey> keySelector);
        OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TEnumerable, TEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, SingleComparerDescending<TItem, TSecondKey>>> ThenByDescending<TSecondKey>(Func<TItem, TSecondKey> keySelector, IComparer<TSecondKey> comparer);
    }
}