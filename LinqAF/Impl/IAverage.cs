using System;

namespace LinqAF.Impl
{
    interface IAverage<TItem>
    {
        // int methods
        double Average(Func<TItem, int> selector);
        double? Average(Func<TItem, int?> selector);

        // long methods
        double Average(Func<TItem, long> selector);
        double? Average(Func<TItem, long?> selector);

        // float methods
        float Average(Func<TItem, float> selector);
        float? Average(Func<TItem, float?> selector);

        // double methods
        double Average(Func<TItem, double> selector);
        double? Average(Func<TItem, double?> selector);

        // decimal methods
        decimal Average(Func<TItem, decimal> selector);
        decimal? Average(Func<TItem, decimal?> selector);
    }
}
