using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class SumBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ISum<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public long Sum(Func<TItem, long> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float Sum(Func<TItem, float> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double Sum(Func<TItem, double> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal Sum(Func<TItem, decimal> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal? Sum(Func<TItem, decimal?> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double? Sum(Func<TItem, double?> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float? Sum(Func<TItem, float?> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public long? Sum(Func<TItem, long?> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public int? Sum(Func<TItem, int?> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public int Sum(Func<TItem, int> selector)
        => CommonImplementation.SumSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);
    }
}
