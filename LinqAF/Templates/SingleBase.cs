using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class SingleBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ISingle<TItem>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Single()
        => CommonImplementation.Single<TItem, TEnumerable, TEnumerator>(RefThis());

        public TItem Single(Func<TItem, bool> predicate)
        => CommonImplementation.Single<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public TItem SingleOrDefault()
        => CommonImplementation.SingleOrDefault<TItem, TEnumerable, TEnumerator>(RefThis());

        public TItem SingleOrDefault(Func<TItem, bool> predicate)
        => CommonImplementation.SingleOrDefault<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
