using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class MaxBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IMax<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Max()
        => CommonImplementation.MaxComparable<TItem, TEnumerable, TEnumerator>(RefThis());

        public int Max(Func<TItem, int> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public long Max(Func<TItem, long> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float Max(Func<TItem, float> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double Max(Func<TItem, double> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal Max(Func<TItem, decimal> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public decimal? Max(Func<TItem, decimal?> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public double? Max(Func<TItem, double?> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public float? Max(Func<TItem, float?> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public long? Max(Func<TItem, long?> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public int? Max(Func<TItem, int?> selector)
        => CommonImplementation.MaxSelector<TItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public TResult Max<TResult>(Func<TItem, TResult> selector)
        => CommonImplementation.MaxProjectedComparable<TItem, TResult, TEnumerable, TEnumerator>(RefThis(), selector);
    }
}
