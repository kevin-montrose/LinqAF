using LinqAF.Impl;

namespace LinqAF
{
    abstract class SumExtensionMethodsBase:
        ExtensionMethodsBase
    {
        public int Sum(PlaceholderEnumerable<int> source)
        => CommonImplementation.SumInt<PlaceholderEnumerable<int>, PlaceholderEnumerator<int>>(RefParam(source));

        public int? Sum(PlaceholderEnumerable<int?> source)
        => CommonImplementation.SumNullableInt<PlaceholderEnumerable<int?>, PlaceholderEnumerator<int?>>(RefParam(source));

        public long Sum(PlaceholderEnumerable<long> source)
        => CommonImplementation.SumLong<PlaceholderEnumerable<long>, PlaceholderEnumerator<long>>(RefParam(source));

        public long? Sum(PlaceholderEnumerable<long?> source)
        => CommonImplementation.SumNullableLong<PlaceholderEnumerable<long?>, PlaceholderEnumerator<long?>>(RefParam(source));

        public float Sum(PlaceholderEnumerable<float> source)
        => CommonImplementation.SumFloat<PlaceholderEnumerable<float>, PlaceholderEnumerator<float>>(RefParam(source));

        public float? Sum(PlaceholderEnumerable<float?> source)
        => CommonImplementation.SumNullableFloat<PlaceholderEnumerable<float?>, PlaceholderEnumerator<float?>>(RefParam(source));

        public double Sum(PlaceholderEnumerable<double> source)
        => CommonImplementation.SumDouble<PlaceholderEnumerable<double>, PlaceholderEnumerator<double>>(RefParam(source));

        public double? Sum(PlaceholderEnumerable<double?> source)
        => CommonImplementation.SumNullableDouble<PlaceholderEnumerable<double?>, PlaceholderEnumerator<double?>>(RefParam(source));

        public decimal Sum(PlaceholderEnumerable<decimal> source)
        => CommonImplementation.SumDecimal<PlaceholderEnumerable<decimal>, PlaceholderEnumerator<decimal>>(RefParam(source));

        public decimal? Sum(PlaceholderEnumerable<decimal?> source)
        => CommonImplementation.SumNullableDecimal<PlaceholderEnumerable<decimal?>, PlaceholderEnumerator<decimal?>>(RefParam(source));
    }
}
