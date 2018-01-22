using System;

namespace LinqAF.Impl
{
    interface ISum<TInItem>
    {
        int Sum(Func<TInItem, int> selector);
        int? Sum(Func<TInItem, int?> selector);

        long Sum(Func<TInItem, long> selector);
        long? Sum(Func<TInItem, long?> selector);

        float Sum(Func<TInItem, float> selector);
        float? Sum(Func<TInItem, float?> selector);

        double Sum(Func<TInItem, double> selector);
        double? Sum(Func<TInItem, double?> selector);

        decimal Sum(Func<TInItem, decimal> selector);
        decimal? Sum(Func<TInItem, decimal?> selector);
    }
}
