using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class AnyBase<TItem, TEnumerable, TEnumerator>: 
        TemplateBase, 
        IAny<TItem>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public bool Any()
        => CommonImplementation.Any<TItem, TEnumerable, TEnumerator>(RefThis());

        public bool Any(Func<TItem, bool> predicate)
        => CommonImplementation.Any<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
