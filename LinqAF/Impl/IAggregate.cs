using System;

namespace LinqAF.Impl
{
    interface IAggregate<TAggregateItemIn>
    {
        TAggregateItemIn Aggregate(Func<TAggregateItemIn, TAggregateItemIn, TAggregateItemIn> func);
        TAggregateItemOut Aggregate<TAggregateItemOut>(TAggregateItemOut seed, Func<TAggregateItemOut, TAggregateItemIn, TAggregateItemOut> func);

        TAggregateItemOut Aggregate<TAggregateItemMid, TAggregateItemOut>(TAggregateItemMid seed, Func<TAggregateItemMid, TAggregateItemIn, TAggregateItemMid> func, Func<TAggregateItemMid, TAggregateItemOut> resultSelector);
    }
}
