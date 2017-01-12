using LinqAF.Impl;
using System.Collections.Generic;
using System;

namespace LinqAF
{
    abstract class ToListBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IToList<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public List<TItem> ToList()
        => CommonImplementation.ToList<TItem, TEnumerable, TEnumerator>(RefThis());
    }
}
