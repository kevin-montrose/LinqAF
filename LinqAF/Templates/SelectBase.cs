using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class SelectBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ISelect<TItem, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public SelectIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator> Select<TOutItem>(Func<TItem, int, TOutItem> selector)
        => CommonImplementation.Select<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public SelectEnumerable<TItem, TOutItem, TEnumerable, TEnumerator> Select<TOutItem>(Func<TItem, TOutItem> selector)
        => CommonImplementation.Select<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), selector);
    }
}
