using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ThenByBase<TItem, TSelfEnumerable, TSelfEnumerator, TInnerEnumerable, TInnerEnumerator, TKey, TComparer> :
        TemplateBase,
        IThenBy<TItem, TKey, TComparer, TInnerEnumerable, TInnerEnumerator>
        where TSelfEnumerable : struct, IStructEnumerable<TItem, TSelfEnumerator>, IHasComparer<TItem, TKey, TComparer, TInnerEnumerable, TInnerEnumerator>
        where TSelfEnumerator : struct, IStructEnumerator<TItem>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
        where TComparer : struct, IStructComparer<TItem, TKey>
    {
        public OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, DefaultAscending<TItem, TSecondKey>>> ThenBy<TSecondKey>(Func<TItem, TSecondKey> keySelector)
        => CommonImplementation.ThenBy<TItem, TKey, TComparer, TSelfEnumerable, TSelfEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(RefThis(), keySelector);

        public OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, SingleComparerAscending<TItem, TSecondKey>>> ThenBy<TSecondKey>(Func<TItem, TSecondKey> keySelector, IComparer<TSecondKey> comparer)
        => CommonImplementation.ThenBy<TItem, TKey, TComparer, TSelfEnumerable, TSelfEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(RefThis(), keySelector, comparer);

        public OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, DefaultDescending<TItem, TSecondKey>>> ThenByDescending<TSecondKey>(Func<TItem, TSecondKey> keySelector)
        => CommonImplementation.ThenByDescending<TItem, TKey, TComparer, TSelfEnumerable, TSelfEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(RefThis(), keySelector);

        public OrderByEnumerable<TItem, CompoundKey<TKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TKey, TComparer, TSecondKey, SingleComparerDescending<TItem, TSecondKey>>> ThenByDescending<TSecondKey>(Func<TItem, TSecondKey> keySelector, IComparer<TSecondKey> comparer)
        => CommonImplementation.ThenByDescending<TItem, TKey, TComparer, TSelfEnumerable, TSelfEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(RefThis(), keySelector, comparer);
    }
}