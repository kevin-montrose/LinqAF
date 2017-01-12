using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class AggregateBase<TItem, TEnumerable, TEnumerator>:
        TemplateBase,
        IAggregate<TItem>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Aggregate(Func<TItem, TItem, TItem> func)
        => CommonImplementation.Aggregate<TItem, TEnumerable, TEnumerator>(RefThis(), func);

        public TItemOut Aggregate<TItemOut>(TItemOut seed, Func<TItemOut, TItem, TItemOut> func)
        => CommonImplementation.Aggregate<TItem, TItemOut, TEnumerable, TEnumerator>(RefThis(), seed, func);
        
        public TItemOut Aggregate<TItemMid, TItemOut>(TItemMid seed, Func<TItemMid, TItem, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        => CommonImplementation.Aggregate<TItem, TItemMid, TItemOut, TEnumerable, TEnumerator>(RefThis(), seed, func, resultSelector);
    }
}
