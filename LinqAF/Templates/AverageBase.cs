using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class AverageBase<TItem, TEnumerable, TEnumerator> :
         TemplateBase,
         IAverage<TItem>
         where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
         where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public double Average(Func<TItem, long> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float Average(Func<TItem, float> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double Average(Func<TItem, double> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal Average(Func<TItem, decimal> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal? Average(Func<TItem, decimal?> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double? Average(Func<TItem, double?> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float? Average(Func<TItem, float?> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double? Average(Func<TItem, long?> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double? Average(Func<TItem, int?> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double Average(Func<TItem, int> selector)
        => CommonImplementation.AverageSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);
    }
}
