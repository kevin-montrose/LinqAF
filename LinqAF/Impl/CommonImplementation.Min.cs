using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // selector
        public static int MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static int MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static int? MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static int? MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static long MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static long MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static long? MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static long? MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static float MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static float? MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float? MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static double MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static double? MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double? MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static decimal MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static decimal? MinSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal? MinSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // comparable
        public static TItem MinComparable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinComparableImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem MinComparableImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (default(TItem) == null)
            {
                return MinComparableNullable<TItem, TEnumerable, TEnumerator>(ref source);
            }
            else
            {
                return MinComparableNonNullable<TItem, TEnumerable, TEnumerator>(ref source);
            }
        }

        static TItem MinComparableNullable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var comparer = Comparer<TItem>.Default;

            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return default(TItem);   // return null, basically

                var best = i.Current;

                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur == null) continue;

                    if (best == null || comparer.Compare(cur, best) < 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        static TItem MinComparableNonNullable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var comparer = Comparer<TItem>.Default;

            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = i.Current;

                while (i.MoveNext())
                {
                    var cur = i.Current;

                    if (comparer.Compare(cur, best) < 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static TResult MinProjectedComparable<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MinProjectedComparableImpl<TItem, TResult, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static TResult MinProjectedComparableImpl<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (default(TResult) == null)
            {
                return MinProjectedComparableNullable<TItem, TResult, TEnumerable, TEnumerator>(ref source, selector);
            }
            else
            {
                return MinProjectedComparableNonNullable<TItem, TResult, TEnumerable, TEnumerator>(ref source, selector);
            }
        }

        static TResult MinProjectedComparableNullable<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var comparer = Comparer<TResult>.Default;

            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return default(TResult);   // return null, basically

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);
                    if (cur == null) continue;

                    if (best == null || comparer.Compare(cur, best) < 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        static TResult MinProjectedComparableNonNullable<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var comparer = Comparer<TResult>.Default;

            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = selector(i.Current);

                while (i.MoveNext())
                {
                    var cur = selector(i.Current);

                    if (comparer.Compare(cur, best) < 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // int
        public static int MinInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinIntimpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static int MinIntimpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static int? MinNullableInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinNullableIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static int? MinNullableIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur == null) continue;

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // long

        public static long MinLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static long MinLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static long? MinNullableLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinNullableLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static long? MinNullableLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur == null) continue;

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // float

        public static float MinFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float MinFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static float? MinNullableFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinNullableFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float? MinNullableFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur == null) continue;

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // double

        public static double MinDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double MinDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static double? MinNullableDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinNullableDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double? MinNullableDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur == null) continue;

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // decimal

        public static decimal MinDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal MinDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static decimal? MinNullableDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MinNullableDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal? MinNullableDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return null;

                var best = i.Current;
                while (i.MoveNext())
                {
                    var cur = i.Current;
                    if (cur == null) continue;

                    if (best == null || cur < best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }
    }
}
