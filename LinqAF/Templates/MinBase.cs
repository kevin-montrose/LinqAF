using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class MinBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IMin<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Min()
        => CommonImplementation.MinComparable<TItem, TEnumerable, TEnumerator>(RefThis());

        public long Min(Func<TItem, long> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float Min(Func<TItem, float> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double Min(Func<TItem, double> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal Min(Func<TItem, decimal> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal? Min(Func<TItem, decimal?> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double? Min(Func<TItem, double?> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float? Min(Func<TItem, float?> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public long? Min(Func<TItem, long?> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public int? Min(Func<TItem, int?> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public int Min(Func<TItem, int> selector)
        => CommonImplementation.MinSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public TResult Min<TResult>(Func<TItem, TResult> selector)
        => CommonImplementation.MinProjectedComparable<TItem, TResult, TEnumerable, TEnumerator>(RefThis(), selector);
    }
}
