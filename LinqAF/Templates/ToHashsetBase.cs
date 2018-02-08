using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ToHashSetBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IToHashSet<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public HashSet<TItem> ToHashSet()
        => CommonImplementation.ToHashSet<TItem, TEnumerable, TEnumerator>(RefThis());

        public HashSet<TItem> ToHashSet(IEqualityComparer<TItem> comparer)
        => CommonImplementation.ToHashSet<TItem, TEnumerable, TEnumerator>(RefThis(), comparer);
    }
}
