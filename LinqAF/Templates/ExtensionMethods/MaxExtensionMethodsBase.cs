using LinqAF.Impl;

namespace LinqAF
{
    abstract class MaxExtensionMethodsBase:
        ExtensionMethodsBase
    {
        public int Max(PlaceholderEnumerable<int> source)
        => CommonImplementation.MaxInt<PlaceholderEnumerable<int>, PlaceholderEnumerator<int>>(RefParam(source));

        public int? Max(PlaceholderEnumerable<int?> source)
        => CommonImplementation.MaxNullableInt<PlaceholderEnumerable<int?>, PlaceholderEnumerator<int?>>(RefParam(source));

        public long Max(PlaceholderEnumerable<long> source)
        => CommonImplementation.MaxLong<PlaceholderEnumerable<long>, PlaceholderEnumerator<long>>(RefParam(source));

        public long? Max(PlaceholderEnumerable<long?> source)
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

        //// RangeEnumerable

        //public int Max(RangeEnumerable source)
        //{
        //    if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
        //    if (source.InnerCount == 0) throw CommonImplementation.SequenceEmpty();

        //    var ret = source.Start + source.InnerCount - 1;

        //    return ret;
        //}

        //// ReverseRangeEnumerable

        //public int Max(ReverseRangeEnumerable source)
        //{
        //    if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
        //    if (source.InnerCount == 0) throw CommonImplementation.SequenceEmpty();

        //    return source.Start;
        //}
    }
}
