using LinqAF.Impl;

namespace LinqAF
{
    abstract class AverageExtensionMethodsBase:
        ExtensionMethodsBase
    {
        public double Average(PlaceholderEnumerable<int> source)
        => CommonImplementation.AverageInt<PlaceholderEnumerable<int>, PlaceholderEnumerator<int>>(RefParam(source));

        public double? Average(PlaceholderEnumerable<int?> source)
        => CommonImplementation.AverageNullableInt<PlaceholderEnumerable<int?>, PlaceholderEnumerator<int?>>(RefParam(source));

        public double Average(PlaceholderEnumerable<long> source)
        => CommonImplementation.AverageLong<PlaceholderEnumerable<long>, PlaceholderEnumerator<long>>(RefParam(source));

        public double? Average(PlaceholderEnumerable<long?> source)
        => CommonImplementation.AverageNullableLong<PlaceholderEnumerable<long?>, PlaceholderEnumerator<long?>>(RefParam(source));

        public float Average(PlaceholderEnumerable<float> source)
        => CommonImplementation.AverageFloat<PlaceholderEnumerable<float>, PlaceholderEnumerator<float>>(RefParam(source));

        public float? Average(PlaceholderEnumerable<float?> source)
        => CommonImplementation.AverageNullableFloat<PlaceholderEnumerable<float?>, PlaceholderEnumerator<float?>>(RefParam(source));

        public double Average(PlaceholderEnumerable<double> source)
        => CommonImplementation.AverageDouble<PlaceholderEnumerable<double>, PlaceholderEnumerator<double>>(RefParam(source));

        public double? Average(PlaceholderEnumerable<double?> source)
        => CommonImplementation.AverageNullableDouble<PlaceholderEnumerable<double?>, PlaceholderEnumerator<double?>>(RefParam(source));

        public decimal Average(PlaceholderEnumerable<decimal> source)
        => CommonImplementation.AverageDecimal<PlaceholderEnumerable<decimal>, PlaceholderEnumerator<decimal>>(RefParam(source));

        public decimal? Average(PlaceholderEnumerable<decimal?> source)
        => CommonImplementation.AverageNullableDecimal<PlaceholderEnumerable<decimal?>, PlaceholderEnumerator<decimal?>>(RefParam(source));
    }
}
