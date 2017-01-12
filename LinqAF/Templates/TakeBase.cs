using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class TakeBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ITake<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public TakeEnumerable<TItem, TEnumerable, TEnumerator> Take(int count)
        => CommonImplementation.Take<TItem, TEnumerable, TEnumerator>(RefThis(), count);

        public TakeWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator> TakeWhile(Func<TItem, int, bool> predicate)
        => CommonImplementation.TakeWhile<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public TakeWhileEnumerable<TItem, TEnumerable, TEnumerator> TakeWhile(Func<TItem, bool> predicate)
        => CommonImplementation.TakeWhile<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
