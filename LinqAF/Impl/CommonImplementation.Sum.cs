using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // selector

        public static int SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static int SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            var sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                checked
                {
                    sum += val;
                }
            }

            return sum;
        }

        public static int? SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static int? SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            var sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                checked
                {
                    sum += val.GetValueOrDefault();
                }
            }

            return sum;
        }

        public static long SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static long SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            long sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                checked
                {
                    sum += val;
                }
            }

            return sum;
        }

        public static long? SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static long? SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            long sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                checked
                {
                    sum += val.GetValueOrDefault();
                }
            }

            return sum;
        }

        public static float SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            double sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                if (float.IsNaN(val)) return float.NaN;

                sum += val;
            }

            return (float)sum;
        }

        public static float? SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float? SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            double sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                var effectiveVal = val.GetValueOrDefault();
                if (float.IsNaN(effectiveVal)) return float.NaN;

                sum += val.GetValueOrDefault();
            }

            return (float)sum;
        }

        public static double SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            double sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                if (double.IsNaN(val)) return double.NaN;

                sum += val;
            }

            return sum;
        }

        public static double? SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double? SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            double sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                var effectiveVal = val.GetValueOrDefault();
                if (double.IsNaN(effectiveVal)) return double.NaN;

                sum += val.GetValueOrDefault();
            }

            return (double)sum;
        }

        public static decimal SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);

                sum += val;
            }

            return sum;
        }

        public static decimal? SumSelector<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal? SumSelectorImpl<TInItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TInItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                var val = selector(item);
                var effectiveVal = val.GetValueOrDefault();

                sum += val.GetValueOrDefault();
            }

            return (decimal)sum;
        }

        // int
        public static int SumInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static int SumIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            var sum = 0;
            foreach (var item in source)
            {
                checked
                {
                    sum += item;
                }
            }

            return sum;
        }
        
        public static int? SumNullableInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumNullableIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static int? SumNullableIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            var sum = 0;
            foreach (var item in source)
            {
                checked
                {
                    sum += item.GetValueOrDefault();
                }
            }

            return sum;
        }

        // long
        
        public static long SumLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static long SumLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            long sum = 0;
            foreach (var item in source)
            {
                checked
                {
                    sum += item;
                }
            }

            return sum;
        }
        
        public static long? SumNullableLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumNullableLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static long? SumNullableLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            long sum = 0;
            foreach (var item in source)
            {
                checked
                {
                    sum += item.GetValueOrDefault();
                }
            }

            return sum;
        }

        // float
        
        public static float SumFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float SumFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            double sum = 0;
            foreach (var item in source)
            {
                if (float.IsNaN(item))
                {
                    // can return early here
                    return float.NaN;
                }

                sum += item;
            }

            return (float)sum;
        }
        
        public static float? SumNullableFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumNullableFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float? SumNullableFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            double sum = 0;
            foreach (var item in source)
            {
                var effectiveItem = item.GetValueOrDefault();

                if (float.IsNaN(effectiveItem))
                {
                    return float.NaN;
                }

                sum += effectiveItem;
            }

            return (float)sum;
        }

        // double
        
        public static double SumDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double SumDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            double sum = 0;
            foreach (var item in source)
            {
                if (double.IsNaN(item))
                {
                    // can return early here
                    return double.NaN;
                }

                sum += item;
            }

            return sum;
        }
        
        public static double? SumNullableDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumNullableDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double? SumNullableDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            double sum = 0;
            foreach (var item in source)
            {
                var effectiveItem = item.GetValueOrDefault();

                if (double.IsNaN(effectiveItem))
                {
                    return double.NaN;
                }

                sum += effectiveItem;
            }

            return sum;
        }

        // decimal
        
        public static decimal SumDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal SumDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                sum += item;
            }

            return sum;
        }
        
        public static decimal? SumNullableDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return SumNullableDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal? SumNullableDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                var effectiveItem = item.GetValueOrDefault();

                sum += effectiveItem;
            }

            return sum;
        }
    }
}
