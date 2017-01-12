using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class WhereBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IWhere<TItem, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public WhereIndexedEnumerable<TItem, TEnumerable, TEnumerator> Where(Func<TItem, int, bool> predicate)
        => CommonImplementation.Where<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public WhereEnumerable<TItem, TEnumerable, TEnumerator> Where(Func<TItem, bool> predicate)
        => CommonImplementation.Where<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
