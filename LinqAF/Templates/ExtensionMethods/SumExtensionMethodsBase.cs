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

        // RangeEnumerable

        public int Sum(RangeEnumerable source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (source.InnerCount == 0) return 0;
            
            long first = source.Start;
            long last = first + source.InnerCount - 1;
            var length = source.InnerCount;

            // https://math.stackexchange.com/a/50487/111
            // sum of range [a, b] = (a + b) / 2 * ((b - a) + 1)
            // count = b - a + 1
            // 2 * sum = (a + b) * ((b - a) + 1)

            // 0 1 2 3
            //   a = 0
            //   b = 3
            //   (a + b) = 3
            //   (b - a) + 1 = 4
            //   (a + b) * ((b - a) + 1) = 12
            //   sum = 12 / 2 = 6

            // 1 2 3 4 5
            //   a = 1
            //   b = 5
            //   (a + b) = 6
            //   (b - a) + 1 = 5
            //   (a + b) * ((b - a) + 1) = 30
            //   sum = 30 / 2 = 15
            
            long twiceSum = (first + last);
            twiceSum *= length;
            long ret = twiceSum / 2;
            
            checked
            {
                return (int)ret;
            }
        }

        // ReverseRangeEnumerable

        public int Sum(ReverseRangeEnumerable source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (source.InnerCount == 0) return 0;

            long last = source.Start;
            long first = last - source.InnerCount + 1;
            var length = source.InnerCount;

            // https://math.stackexchange.com/a/50487/111
            // sum of range [a, b] = (a + b) / 2 * ((b - a) + 1)
            // count = b - a + 1
            // 2 * sum = (a + b) * ((b - a) + 1)

            // 0 1 2 3
            //   a = 0
            //   b = 3
            //   (a + b) = 3
            //   (b - a) + 1 = 4
            //   (a + b) * ((b - a) + 1) = 12
            //   sum = 12 / 2 = 6

            // 1 2 3 4 5
            //   a = 1
            //   b = 5
            //   (a + b) = 6
            //   (b - a) + 1 = 5
            //   (a + b) * ((b - a) + 1) = 30
            //   sum = 30 / 2 = 15

            long twiceSum = (first + last);
            twiceSum *= length;
            long ret = twiceSum / 2;

            checked
            {
                return (int)ret;
            }
        }
    }
}
