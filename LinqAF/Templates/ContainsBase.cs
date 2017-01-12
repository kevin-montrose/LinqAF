using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class ContainsBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IContains<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public bool Contains(TItem value)
        => CommonImplementation.Contains<TItem, TEnumerable, TEnumerator>(RefThis(), value);

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Contains<TItem, TEnumerable, TEnumerator>(RefThis(), value, comparer);
    }
}
