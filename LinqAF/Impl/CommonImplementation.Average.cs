using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // selector
        public static double AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                checked
                {
                    count++;
                    total += selector(item);
                }
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return ((double)total) / (double)count;
        }

        public static double? AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double? AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                var val = selector(item);

                if (val == null) continue;

                checked
                {
                    count++;
                    total += val.Value;
                }
            }

            if (count == 0) return null;

            return ((double)total) / (double)count;
        }

        public static double AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                checked
                {
                    count++;
                    total += selector(item);
                }
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return ((double)total) / (double)count;
        }

        public static double? AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double? AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                var val = selector(item);

                if (val == null) continue;

                checked
                {
                    count++;
                    total += val.Value;
                }
            }

            if (count == 0) return null;

            return ((double)total) / (double)count;
        }

        public static float AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                count++;
                total += selector(item);
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return (float)(total / count);
        }

        public static float? AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float? AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                var val = selector(item);

                if (val == null) continue;

                count++;
                total += val.Value;
            }

            if (count == 0) return null;

            return (float)(total / count);
        }

        public static double AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                count++;
                total += selector(item);
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return (total / count);
        }

        public static double? AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double? AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                var val = selector(item);

                if (val == null) continue;

                count++;
                total += val.Value;
            }

            if (count == 0) return null;

            return (total / count);
        }

        public static decimal AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            decimal count = 0;
            decimal total = 0;

            foreach (var item in source)
            {
                checked
                {
                    count++;
                    total += selector(item);
                }
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return (total / count);
        }

        public static decimal? AverageSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal? AverageSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            decimal count = 0;
            decimal total = 0;

            foreach (var item in source)
            {
                var val = selector(item);

                if (val == null) continue;

                checked
                {
                    count++;
                    total += val.Value;
                }
            }

            if (count == 0) return null;

            return (total / count);
        }

        // int
        public static double AverageInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double AverageIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                checked
                {
                    count++;
                    total += item;
                }
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return ((double)total) / (double)count;
        }

        // int?
        public static double? AverageNullableInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageNullableIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double? AverageNullableIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                if (item == null) continue;

                checked
                {
                    count++;
                    total += item.Value;
                }
            }

            if (count == 0) return null;

            return ((double)total) / (double)count;
        }
        
        // long
        public static double AverageLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double AverageLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                checked
                {
                    count++;
                    total += item;
                }
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return ((double)total) / (double)count;
        }

        // long?
        public static double? AverageNullableLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageNullableLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double? AverageNullableLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            long count = 0;
            long total = 0;

            foreach (var item in source)
            {
                if (item == null) continue;

                checked
                {
                    count++;
                    total += item.Value;
                }
            }

            if (count == 0) return null;

            return ((double)total) / (double)count;
        }

        // float
        public static float AverageFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float AverageFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                count++;
                total += item;
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return (float)(total / count);
        }

        // float?
        public static float? AverageNullableFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageNullableFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float? AverageNullableFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                if (item == null) continue;

                count++;
                total += item.Value;
            }

            if (count == 0) return null;

            return (float)(total / count);
        }
        
        // double
        public static double AverageDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double AverageDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                count++;
                total += item;
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return (total / count);
        }

        // double?
        public static double? AverageNullableDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageNullableDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double? AverageNullableDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            double count = 0;
            double total = 0;

            foreach (var item in source)
            {
                if (item == null) continue;

                count++;
                total += item.Value;
            }

            if (count == 0) return null;

            return (total / count);
        }
        
        // decimal
        public static decimal AverageDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal AverageDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            decimal count = 0;
            decimal total = 0;

            foreach (var item in source)
            {
                checked
                {
                    count++;
                    total += item;
                }
            }

            if (count == 0) throw new InvalidOperationException("Sequence was empty");

            return (total / count);
        }

        // decimal?
        public static decimal? AverageNullableDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return AverageNullableDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal? AverageNullableDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            decimal count = 0;
            decimal total = 0;

            foreach (var item in source)
            {
                if (item == null) continue;

                checked
                {
                    count++;
                    total += item.Value;
                }
            }

            if (count == 0) return null;

            return (total / count);
        }
    }
}
