using LinqAF.Impl;

namespace LinqAF
{
    abstract class MinExtensionMethodsBase:
        ExtensionMethodsBase
    {
        public int Min(PlaceholderEnumerable<int> source)
        => CommonImplementation.MinInt<PlaceholderEnumerable<int>, PlaceholderEnumerator<int>>(RefParam(source));

        public int? Min(PlaceholderEnumerable<int?> source)
        => CommonImplementation.MinNullableInt<PlaceholderEnumerable<int?>, PlaceholderEnumerator<int?>>(RefParam(source));

        public long Min(PlaceholderEnumerable<long> source)
        => CommonImplementation.MinLong<PlaceholderEnumerable<long>, PlaceholderEnumerator<long>>(RefParam(source));

        public long? Min(PlaceholderEnumerable<long?> source)
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

        //// RangeEnumerable

        //public int Min(RangeEnumerable source)
        //{
        //    if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
        //    if (source.InnerCount == 0) throw CommonImplementation.SequenceEmpty();

        //    return source.Start;
        //}

        //// ReverseRangeEnumerable

        //public int Min(ReverseRangeEnumerable source)
        //{
        //    if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
        //    if (source.InnerCount == 0) throw CommonImplementation.SequenceEmpty();

        //    var ret = source.Start - source.InnerCount + 1;

        //    return ret;
        //}
    }
}
