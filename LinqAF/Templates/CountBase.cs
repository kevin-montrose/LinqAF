using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class CountBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ICount<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public int Count()
        => CommonImplementation.Count<TItem, TEnumerable, TEnumerator>(RefThis());

        public int Count(Func<TItem, bool> predicate)
        => CommonImplementation.Count<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public long LongCount()
        => CommonImplementation.LongCount<TItem, TEnumerable, TEnumerator>(RefThis());

        public long LongCount(Func<TItem, bool> predicate)
        => CommonImplementation.LongCount<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
