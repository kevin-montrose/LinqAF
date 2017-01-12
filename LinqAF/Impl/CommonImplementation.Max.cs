using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // selector
        public static int MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static int MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int> selector)
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

                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static int? MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static int? MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, int?> selector)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static long MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static long MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long> selector)
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

                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static long? MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static long? MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, long?> selector)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static float MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float> selector)
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

                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static float? MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static float? MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, float?> selector)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static double MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double> selector)
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

                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static double? MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static double? MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, double?> selector)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static decimal MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal> selector)
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

                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static decimal? MaxSelector<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal?> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static decimal? MaxSelectorImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, decimal?> selector)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // comparable

        public static TItem MaxComparable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxComparableImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem MaxComparableImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (default(TItem) == null)
            {
                return MaxComparableNullable<TItem, TEnumerable, TEnumerator>(ref source);
            }
            else
            {
                return MaxComparableNonNullable<TItem, TEnumerable, TEnumerator>(ref source);
            }
        }

        static TItem MaxComparableNullable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (best == null || comparer.Compare(cur, best) > 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        static TItem MaxComparableNonNullable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (comparer.Compare(cur, best) > 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // projected comparable

        public static TResult MaxProjectedComparable<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return MaxProjectedComparableImpl<TItem, TResult, TEnumerable, TEnumerator>(ref source, selector);
        }

        internal static TResult MaxProjectedComparableImpl<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (default(TResult) == null)
            {
                return MaxProjectedComparableNullable<TItem, TResult, TEnumerable, TEnumerator>(ref source, selector);
            }
            else
            {
                return MaxProjectedComparableNonNullable<TItem, TResult, TEnumerable, TEnumerator>(ref source, selector);
            }
        }

        static TResult MaxProjectedComparableNullable<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
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

                    if (best == null || comparer.Compare(cur, best) > 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        static TResult MaxProjectedComparableNonNullable<TItem, TResult, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TResult> selector)
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

                    if (comparer.Compare(cur, best) > 0)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // int

        public static int MaxInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static int MaxIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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
                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static int? MaxNullableInt<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<int?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<int?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxNullableIntImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static int? MaxNullableIntImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }



        // long

        public static long MaxLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static long MaxLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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
                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static long? MaxNullableLong<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<long?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<long?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxNullableLongImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static long? MaxNullableLongImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // float

        public static float MaxFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float MaxFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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
                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static float? MaxNullableFloat<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<float?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<float?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxNullableFloatImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static float? MaxNullableFloatImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        // double

        public static double MaxDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double MaxDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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
                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static double? MaxNullableDouble<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<double?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<double?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxNullableDoubleImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static double? MaxNullableDoubleImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }


        // decimal

        public static decimal MaxDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal MaxDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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
                    if (cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }

        public static decimal? MaxNullableDecimal<TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<decimal?, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<decimal?>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return MaxNullableDecimalImpl<TEnumerable, TEnumerator>(ref source);
        }

        internal static decimal? MaxNullableDecimalImpl<TEnumerable, TEnumerator>(ref TEnumerable source)
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

                    if (best == null || cur > best)
                    {
                        best = cur;
                    }
                }

                return best;
            }
        }
    }
}
