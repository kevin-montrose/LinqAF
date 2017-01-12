using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class LastBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ILast<TItem>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Last()
        => CommonImplementation.Last<TItem, TEnumerable, TEnumerator>(RefThis());

        public TItem Last(Func<TItem, bool> predicate)
        => CommonImplementation.Last<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public TItem LastOrDefault()
        => CommonImplementation.LastOrDefault<TItem, TEnumerable, TEnumerator>(RefThis());

        public TItem LastOrDefault(Func<TItem, bool> predicate)
        => CommonImplementation.LastOrDefault<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
