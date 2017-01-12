using LinqAF.Impl;

namespace LinqAF
{
    abstract class MaxExtensionMethodsBase:
        ExtensionMethodsBase
    {
        public double Max(PlaceholderEnumerable<int> source)
        => CommonImplementation.MaxInt<PlaceholderEnumerable<int>, PlaceholderEnumerator<int>>(RefParam(source));

        public double? Max(PlaceholderEnumerable<int?> source)
        => CommonImplementation.MaxNullableInt<PlaceholderEnumerable<int?>, PlaceholderEnumerator<int?>>(RefParam(source));

        public double Max(PlaceholderEnumerable<long> source)
        => CommonImplementation.MaxLong<PlaceholderEnumerable<long>, PlaceholderEnumerator<long>>(RefParam(source));

        public double? Max(PlaceholderEnumerable<long?> source)
        => CommonImplementation.MaxNullableLong<PlaceholderEnumerable<long?>, PlaceholderEnumerator<long?>>(RefParam(source));

        public float Max(PlaceholderEnumerable<float> source)
        => CommonImplementation.MaxFloat<PlaceholderEnumerable<float>, PlaceholderEnumerator<float>>(RefParam(source));

        public float? Max(PlaceholderEnumerable<float?> source)
        => CommonImplementation.MaxNullableFloat<PlaceholderEnumerable<float?>, PlaceholderEnumerator<float?>>(RefParam(source));

        public double Max(PlaceholderEnumerable<double> source)
        => CommonImplementation.MaxDouble<PlaceholderEnumerable<double>, PlaceholderEnumerator<double>>(RefParam(source));

        public double? Max(PlaceholderEnumerable<double?> source)
        => CommonImplementation.MaxNullableDouble<PlaceholderEnumerable<double?>, PlaceholderEnumerator<double?>>(RefParam(source));

        public decimal Max(PlaceholderEnumerable<decimal> source)
        => CommonImplementation.MaxDecimal<PlaceholderEnumerable<decimal>, PlaceholderEnumerator<decimal>>(RefParam(source));

        public decimal? Max(PlaceholderEnumerable<decimal?> source)
        => CommonImplementation.MaxNullableDecimal<PlaceholderEnumerable<decimal?>, PlaceholderEnumerator<decimal?>>(RefParam(source));
    }
}
