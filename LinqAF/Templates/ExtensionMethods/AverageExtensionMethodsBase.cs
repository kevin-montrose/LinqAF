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

        // RangeEnumerable

        public double Average(RangeEnumerable source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (source.InnerCount == 0) throw CommonImplementation.SequenceEmpty();

            // 4 5 6
            //   ^

            // 1 2 3 4
            //    ^

            var leftIx = source.InnerCount / 2;
            double left = source.Start + leftIx;

            if (source.InnerCount % 2 == 0)
            {
                // even
                double right = left + 1;

                return (left + right) / 2.0;
            }
            else
            {
                // odd
                return left;
            }
        }

        // ReverseRangeEnumerable

        public double Average(ReverseRangeEnumerable source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (source.InnerCount == 0) throw CommonImplementation.SequenceEmpty();

            // 6 5 4
            //   ^

            // 4 3 2 1
            //    ^

            var leftIx = source.InnerCount / 2;
            double left = source.Start - leftIx;

            if (source.InnerCount % 2 == 0)
            {
                // even
                double right = left - 1;

                return (left + right) / 2.0;
            }
            else
            {
                // odd
                return left;
            }
        }
    }
}
