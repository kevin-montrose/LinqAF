using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class AsEnumerableBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IAsEnumerable<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public IEnumerable<TItem> AsEnumerable()
        => CommonImplementation.AsEnumerable<TItem, TEnumerable, TEnumerator>(RefThis());
    }
}
