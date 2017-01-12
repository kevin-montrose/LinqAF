using System;

namespace LinqAF.Impl
{
    interface IMax<TItem>
    {
        TItem Max();
        TResult Max<TResult>(Func<TItem, TResult> selector);

        int Max(Func<TItem, int> selector);
        int? Max(Func<TItem, int?> selector);

        long Max(Func<TItem, long> selector);
        long? Max(Func<TItem, long?> selector);

        float Max(Func<TItem, float> selector);
        float? Max(Func<TItem, float?> selector);

        double Max(Func<TItem, double> selector);
        double? Max(Func<TItem, double?> selector);

        decimal Max(Func<TItem, decimal> selector);
        decimal? Max(Func<TItem, decimal?> selector);
    }
}
