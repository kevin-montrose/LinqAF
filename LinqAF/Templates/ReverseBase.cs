using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class ReverseBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IReverse<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public ReverseEnumerable<TItem, TEnumerable, TEnumerator> Reverse()
        => CommonImplementation.Reverse<TItem, TEnumerable, TEnumerator>(RefThis());
    }
}
