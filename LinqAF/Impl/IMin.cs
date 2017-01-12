using System;

namespace LinqAF.Impl
{
    interface IMin<TItem>
    {
        TItem Min();
        TResult Min<TResult>(Func<TItem, TResult> selector);

        int Min(Func<TItem, int> selector);
        int? Min(Func<TItem, int?> selector);

        long Min(Func<TItem, long> selector);
        long? Min(Func<TItem, long?> selector);

        float Min(Func<TItem, float> selector);
        float? Min(Func<TItem, float?> selector);

        double Min(Func<TItem, double> selector);
        double? Min(Func<TItem, double?> selector);

        decimal Min(Func<TItem, decimal> selector);
        decimal? Min(Func<TItem, decimal?> selector);
    }
}
