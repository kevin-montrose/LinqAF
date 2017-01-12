using LinqAF.Impl;

namespace LinqAF
{
    abstract class MinExtensionMethodsBase:
        ExtensionMethodsBase
    {
        public double Min(PlaceholderEnumerable<int> source)
        => CommonImplementation.MinInt<PlaceholderEnumerable<int>, PlaceholderEnumerator<int>>(RefParam(source));

        public double? Min(PlaceholderEnumerable<int?> source)
        => CommonImplementation.MinNullableInt<PlaceholderEnumerable<int?>, PlaceholderEnumerator<int?>>(RefParam(source));

        public double Min(PlaceholderEnumerable<long> source)
        => CommonImplementation.MinLong<PlaceholderEnumerable<long>, PlaceholderEnumerator<long>>(RefParam(source));

        public double? Min(PlaceholderEnumerable<long?> source)
        => CommonImplementation.MinNullableLong<PlaceholderEnumerable<long?>, PlaceholderEnumerator<long?>>(RefParam(source));

        public float Min(PlaceholderEnumerable<float> source)
        => CommonImplementation.MinFloat<PlaceholderEnumerable<float>, PlaceholderEnumerator<float>>(RefParam(source));

        public float? Min(PlaceholderEnumerable<float?> source)
        => CommonImplementation.MinNullableFloat<PlaceholderEnumerable<float?>, PlaceholderEnumerator<float?>>(RefParam(source));

        public double Min(PlaceholderEnumerable<double> source)
        => CommonImplementation.MinDouble<PlaceholderEnumerable<double>, PlaceholderEnumerator<double>>(RefParam(source));

        public double? Min(PlaceholderEnumerable<double?> source)
        => CommonImplementation.MinNullableDouble<PlaceholderEnumerable<double?>, PlaceholderEnumerator<double?>>(RefParam(source));

        public decimal Min(PlaceholderEnumerable<decimal> source)
        => CommonImplementation.MinDecimal<PlaceholderEnumerable<decimal>, PlaceholderEnumerator<decimal>>(RefParam(source));

        public decimal? Min(PlaceholderEnumerable<decimal?> source)
        => CommonImplementation.MinNullableDecimal<PlaceholderEnumerable<decimal?>, PlaceholderEnumerator<decimal?>>(RefParam(source));
    }
}
