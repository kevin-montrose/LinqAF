using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class SumTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ISum<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ISum ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            // int
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new int[] { 4, 5, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(15, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new int?[] { 4, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "world" },
                    @"a => { var res = a.Sum(s => s.Length); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "bar", "fizz", "buzz" },
                    @"a => { var res = a.Sum(s => (s.Length % 2) == 0 ? null : (int?)s.Length); Assert.AreEqual(6, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new long[] { 4, 5, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(15, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new long?[] { 4, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "world" },
                    @"a => { var res = a.Sum(s => (long)s.Length); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "bar", "fizz", "buzz" },
                    @"a => { var res = a.Sum(s => (s.Length % 2) == 0 ? null : (long?)s.Length); Assert.AreEqual(6, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new float[] { 4, 5, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(15, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new float?[] { 4, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "world" },
                    @"a => { var res = a.Sum(s => (float)s.Length); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "bar", "fizz", "buzz" },
                    @"a => { var res = a.Sum(s => (s.Length % 2) == 0 ? null : (float?)s.Length); Assert.AreEqual(6, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new double[] { 4, 5, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(15, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new double?[] { 4, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "world" },
                    @"a => { var res = a.Sum(s => (double)s.Length); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "bar", "fizz", "buzz" },
                    @"a => { var res = a.Sum(s => (s.Length % 2) == 0 ? null : (double?)s.Length); Assert.AreEqual(6, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new decimal[] { 4, 5, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(15, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new decimal?[] { 4, 6 },
                    @"a => { var res = a.Sum(); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "world" },
                    @"a => { var res = a.Sum(s => (decimal)s.Length); Assert.AreEqual(10, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "bar", "fizz", "buzz" },
                    @"a => { var res = a.Sum(s => (s.Length % 2) == 0 ? null : (decimal?)s.Length); Assert.AreEqual(6, res); }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        class _IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
        }

        [TestMethod]
        public void Chaining_Weird()
        {
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                Assert.AreEqual(0, Enumerable.Empty<int>().Sum());
                Assert.AreEqual(0, Enumerable.Empty<int?>().Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long>().Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long?>().Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float>().Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float?>().Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double>().Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double?>().Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal>().Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal?>().Sum());

                Assert.AreEqual(0, Enumerable.Empty<string>().Sum(x => (int)x.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().Sum(x => (int?)x.Length));
                Assert.AreEqual(0L, Enumerable.Empty<string>().Sum(x => (long)x.Length));
                Assert.AreEqual(0L, Enumerable.Empty<string>().Sum(x => (long?)x.Length));
                Assert.AreEqual(0f, Enumerable.Empty<string>().Sum(x => (float)x.Length));
                Assert.AreEqual(0f, Enumerable.Empty<string>().Sum(x => (float?)x.Length));
                Assert.AreEqual(0.0, Enumerable.Empty<string>().Sum(x => (double)x.Length));
                Assert.AreEqual(0.0, Enumerable.Empty<string>().Sum(x => (double?)x.Length));
                Assert.AreEqual(0m, Enumerable.Empty<string>().Sum(x => (decimal)x.Length));
                Assert.AreEqual(0m, Enumerable.Empty<string>().Sum(x => (decimal?)x.Length));
            }

            // emptyOrdered
            {
                Assert.AreEqual(0, Enumerable.Empty<int>().OrderBy(x => x).Sum());
                Assert.AreEqual(0, Enumerable.Empty<int?>().OrderBy(x => x).Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long>().OrderBy(x => x).Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long?>().OrderBy(x => x).Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float>().OrderBy(x => x).Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float?>().OrderBy(x => x).Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double>().OrderBy(x => x).Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double?>().OrderBy(x => x).Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal>().OrderBy(x => x).Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal?>().OrderBy(x => x).Sum());

                Assert.AreEqual(0, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (int)x.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (int?)x.Length));
                Assert.AreEqual(0L, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (long)x.Length));
                Assert.AreEqual(0L, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (long?)x.Length));
                Assert.AreEqual(0f, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (float)x.Length));
                Assert.AreEqual(0f, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (float?)x.Length));
                Assert.AreEqual(0.0, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (double)x.Length));
                Assert.AreEqual(0.0, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (double?)x.Length));
                Assert.AreEqual(0m, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (decimal)x.Length));
                Assert.AreEqual(0m, Enumerable.Empty<string>().OrderBy(x => x).Sum(x => (decimal?)x.Length));
            }

            // groupByDefault
            {
                // no project sums don't make sense

                Assert.AreEqual(6, groupByDefault.Sum(kv => (int)kv.Key));
                Assert.AreEqual(6, groupByDefault.Sum(kv => (int?)kv.Key));
                Assert.AreEqual(6L, groupByDefault.Sum(kv => (long)kv.Key));
                Assert.AreEqual(6L, groupByDefault.Sum(kv => (long?)kv.Key));
                Assert.AreEqual(6f, groupByDefault.Sum(kv => (float)kv.Key));
                Assert.AreEqual(6f, groupByDefault.Sum(kv => (float?)kv.Key));
                Assert.AreEqual(6.0, groupByDefault.Sum(kv => (double)kv.Key));
                Assert.AreEqual(6.0, groupByDefault.Sum(kv => (double?)kv.Key));
                Assert.AreEqual(6m, groupByDefault.Sum(kv => (decimal)kv.Key));
                Assert.AreEqual(6m, groupByDefault.Sum(kv => (decimal?)kv.Key));
            }

            // groupBySpecific
            {
                // no project sums don't make sense

                Assert.AreEqual(13, groupBySpecific.Sum(kv => (int)kv.Key.Length));
                Assert.AreEqual(13, groupBySpecific.Sum(kv => (int?)kv.Key.Length));
                Assert.AreEqual(13L, groupBySpecific.Sum(kv => (long)kv.Key.Length));
                Assert.AreEqual(13L, groupBySpecific.Sum(kv => (long?)kv.Key.Length));
                Assert.AreEqual(13f, groupBySpecific.Sum(kv => (float)kv.Key.Length));
                Assert.AreEqual(13f, groupBySpecific.Sum(kv => (float?)kv.Key.Length));
                Assert.AreEqual(13.0, groupBySpecific.Sum(kv => (double)kv.Key.Length));
                Assert.AreEqual(13.0, groupBySpecific.Sum(kv => (double?)kv.Key.Length));
                Assert.AreEqual(13m, groupBySpecific.Sum(kv => (decimal)kv.Key.Length));
                Assert.AreEqual(13m, groupBySpecific.Sum(kv => (decimal?)kv.Key.Length));
            }

            // lookupDefault
            {
                // no project sums don't make sense

                Assert.AreEqual(6, lookupDefault.Sum(kv => (int)kv.Key));
                Assert.AreEqual(6, lookupDefault.Sum(kv => (int?)kv.Key));
                Assert.AreEqual(6L, lookupDefault.Sum(kv => (long)kv.Key));
                Assert.AreEqual(6L, lookupDefault.Sum(kv => (long?)kv.Key));
                Assert.AreEqual(6f, lookupDefault.Sum(kv => (float)kv.Key));
                Assert.AreEqual(6f, lookupDefault.Sum(kv => (float?)kv.Key));
                Assert.AreEqual(6.0, lookupDefault.Sum(kv => (double)kv.Key));
                Assert.AreEqual(6.0, lookupDefault.Sum(kv => (double?)kv.Key));
                Assert.AreEqual(6m, lookupDefault.Sum(kv => (decimal)kv.Key));
                Assert.AreEqual(6m, lookupDefault.Sum(kv => (decimal?)kv.Key));
            }

            // lookupSpecific
            {
                // no project sums don't make sense

                Assert.AreEqual(6, lookupSpecific.Sum(kv => (int)kv.Key));
                Assert.AreEqual(6, lookupSpecific.Sum(kv => (int?)kv.Key));
                Assert.AreEqual(6L, lookupSpecific.Sum(kv => (long)kv.Key));
                Assert.AreEqual(6L, lookupSpecific.Sum(kv => (long?)kv.Key));
                Assert.AreEqual(6f, lookupSpecific.Sum(kv => (float)kv.Key));
                Assert.AreEqual(6f, lookupSpecific.Sum(kv => (float?)kv.Key));
                Assert.AreEqual(6.0, lookupSpecific.Sum(kv => (double)kv.Key));
                Assert.AreEqual(6.0, lookupSpecific.Sum(kv => (double?)kv.Key));
                Assert.AreEqual(6m, lookupSpecific.Sum(kv => (decimal)kv.Key));
                Assert.AreEqual(6m, lookupSpecific.Sum(kv => (decimal?)kv.Key));
            }

            // range
            {
                Assert.AreEqual(15, range.Sum());
                // non-int range sums don't make sense

                Assert.AreEqual(15, range.Sum(x => (int)x));
                Assert.AreEqual(15, range.Sum(x => (int?)x));
                Assert.AreEqual(15L, range.Sum(x => (long)x));
                Assert.AreEqual(15L, range.Sum(x => (long?)x));
                Assert.AreEqual(15f, range.Sum(x => (float)x));
                Assert.AreEqual(15f, range.Sum(x => (float?)x));
                Assert.AreEqual(15.0, range.Sum(x => (double)x));
                Assert.AreEqual(15.0, range.Sum(x => (double?)x));
                Assert.AreEqual(15m, range.Sum(x => (decimal)x));
                Assert.AreEqual(15m, range.Sum(x => (decimal?)x));
            }

            // repeat
            {
                Assert.AreEqual(15, Enumerable.Repeat((int)3, 5).Sum());
                Assert.AreEqual(15, Enumerable.Repeat((int?)3, 5).Sum());
                Assert.AreEqual(15L, Enumerable.Repeat((long)3, 5).Sum());
                Assert.AreEqual(15L, Enumerable.Repeat((long?)3, 5).Sum());
                Assert.AreEqual(15f, Enumerable.Repeat((float)3, 5).Sum());
                Assert.AreEqual(15f, Enumerable.Repeat((float?)3, 5).Sum());
                Assert.AreEqual(15.0, Enumerable.Repeat((double)3, 5).Sum());
                Assert.AreEqual(15.0, Enumerable.Repeat((double?)3, 5).Sum());
                Assert.AreEqual(15m, Enumerable.Repeat((decimal)3, 5).Sum());
                Assert.AreEqual(15m, Enumerable.Repeat((decimal?)3, 5).Sum());

                Assert.AreEqual(15, repeat.Sum(x => (int)x.Length));
                Assert.AreEqual(15, repeat.Sum(x => (int?)x.Length));
                Assert.AreEqual(15L, repeat.Sum(x => (long)x.Length));
                Assert.AreEqual(15L, repeat.Sum(x => (long?)x.Length));
                Assert.AreEqual(15f, repeat.Sum(x => (float)x.Length));
                Assert.AreEqual(15f, repeat.Sum(x => (float?)x.Length));
                Assert.AreEqual(15.0, repeat.Sum(x => (double)x.Length));
                Assert.AreEqual(15.0, repeat.Sum(x => (double?)x.Length));
                Assert.AreEqual(15m, repeat.Sum(x => (decimal)x.Length));
                Assert.AreEqual(15m, repeat.Sum(x => (decimal?)x.Length));
            }

            // reverseRange
            {
                Assert.AreEqual(15, reverseRange.Sum());
                // non-int range sums don't make sense

                Assert.AreEqual(15, reverseRange.Sum(x => (int)x));
                Assert.AreEqual(15, reverseRange.Sum(x => (int?)x));
                Assert.AreEqual(15L, reverseRange.Sum(x => (long)x));
                Assert.AreEqual(15L, reverseRange.Sum(x => (long?)x));
                Assert.AreEqual(15f, reverseRange.Sum(x => (float)x));
                Assert.AreEqual(15f, reverseRange.Sum(x => (float?)x));
                Assert.AreEqual(15.0, reverseRange.Sum(x => (double)x));
                Assert.AreEqual(15.0, reverseRange.Sum(x => (double?)x));
                Assert.AreEqual(15m, reverseRange.Sum(x => (decimal)x));
                Assert.AreEqual(15m, reverseRange.Sum(x => (decimal?)x));
            }

            // oneItemDefault
            {
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0, Enumerable.Empty<int?>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long?>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float?>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double?>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal>().DefaultIfEmpty().Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal?>().DefaultIfEmpty().Sum());

                Assert.AreEqual(0, oneItemDefault.Sum(x => (int)x));
                Assert.AreEqual(0, oneItemDefault.Sum(x => (int?)x));
                Assert.AreEqual(0L, oneItemDefault.Sum(x => (long)x));
                Assert.AreEqual(0L, oneItemDefault.Sum(x => (long?)x));
                Assert.AreEqual(0f, oneItemDefault.Sum(x => (float)x));
                Assert.AreEqual(0f, oneItemDefault.Sum(x => (float?)x));
                Assert.AreEqual(0.0, oneItemDefault.Sum(x => (double)x));
                Assert.AreEqual(0.0, oneItemDefault.Sum(x => (double?)x));
                Assert.AreEqual(0m, oneItemDefault.Sum(x => (decimal)x));
                Assert.AreEqual(0m, oneItemDefault.Sum(x => (decimal?)x));
            }

            // oneItemSpecific
            {
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4L, Enumerable.Empty<long>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4L, Enumerable.Empty<long?>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4f, Enumerable.Empty<float>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4f, Enumerable.Empty<float?>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4.0, Enumerable.Empty<double>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4.0, Enumerable.Empty<double?>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4m, Enumerable.Empty<decimal>().DefaultIfEmpty(4).Sum());
                Assert.AreEqual(4m, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).Sum());

                Assert.AreEqual(4, oneItemSpecific.Sum(x => (int)x));
                Assert.AreEqual(4, oneItemSpecific.Sum(x => (int?)x));
                Assert.AreEqual(4L, oneItemSpecific.Sum(x => (long)x));
                Assert.AreEqual(4L, oneItemSpecific.Sum(x => (long?)x));
                Assert.AreEqual(4f, oneItemSpecific.Sum(x => (float)x));
                Assert.AreEqual(4f, oneItemSpecific.Sum(x => (float?)x));
                Assert.AreEqual(4.0, oneItemSpecific.Sum(x => (double)x));
                Assert.AreEqual(4.0, oneItemSpecific.Sum(x => (double?)x));
                Assert.AreEqual(4m, oneItemSpecific.Sum(x => (decimal)x));
                Assert.AreEqual(4m, oneItemSpecific.Sum(x => (decimal?)x));
            }

            // oneItemDefaultOrdered
            {
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0, Enumerable.Empty<int?>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0L, Enumerable.Empty<long?>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0f, Enumerable.Empty<float?>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0.0, Enumerable.Empty<double?>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal>().DefaultIfEmpty().OrderBy(x => x).Sum());
                Assert.AreEqual(0m, Enumerable.Empty<decimal?>().DefaultIfEmpty().OrderBy(x => x).Sum());

                Assert.AreEqual(0, oneItemDefaultOrdered.Sum(x => (int)x));
                Assert.AreEqual(0, oneItemDefaultOrdered.Sum(x => (int?)x));
                Assert.AreEqual(0L, oneItemDefaultOrdered.Sum(x => (long)x));
                Assert.AreEqual(0L, oneItemDefaultOrdered.Sum(x => (long?)x));
                Assert.AreEqual(0f, oneItemDefaultOrdered.Sum(x => (float)x));
                Assert.AreEqual(0f, oneItemDefaultOrdered.Sum(x => (float?)x));
                Assert.AreEqual(0.0, oneItemDefaultOrdered.Sum(x => (double)x));
                Assert.AreEqual(0.0, oneItemDefaultOrdered.Sum(x => (double?)x));
                Assert.AreEqual(0m, oneItemDefaultOrdered.Sum(x => (decimal)x));
                Assert.AreEqual(0m, oneItemDefaultOrdered.Sum(x => (decimal?)x));
            }

            // oneItemSpecificOrdered
            {
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4L, Enumerable.Empty<long>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4L, Enumerable.Empty<long?>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4f, Enumerable.Empty<float>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4f, Enumerable.Empty<float?>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4.0, Enumerable.Empty<double>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4.0, Enumerable.Empty<double?>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4m, Enumerable.Empty<decimal>().DefaultIfEmpty(4).OrderBy(x => x).Sum());
                Assert.AreEqual(4m, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).OrderBy(x => x).Sum());

                Assert.AreEqual(4, oneItemSpecificOrdered.Sum(x => (int)x));
                Assert.AreEqual(4, oneItemSpecificOrdered.Sum(x => (int?)x));
                Assert.AreEqual(4L, oneItemSpecificOrdered.Sum(x => (long)x));
                Assert.AreEqual(4L, oneItemSpecificOrdered.Sum(x => (long?)x));
                Assert.AreEqual(4f, oneItemSpecificOrdered.Sum(x => (float)x));
                Assert.AreEqual(4f, oneItemSpecificOrdered.Sum(x => (float?)x));
                Assert.AreEqual(4.0, oneItemSpecificOrdered.Sum(x => (double)x));
                Assert.AreEqual(4.0, oneItemSpecificOrdered.Sum(x => (double?)x));
                Assert.AreEqual(4m, oneItemSpecificOrdered.Sum(x => (decimal)x));
                Assert.AreEqual(4m, oneItemSpecificOrdered.Sum(x => (decimal?)x));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // int
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo" },
                    @"a => { try { a.Sum(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                try { empty.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Sum(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Sum(default(Func<GroupingEnumerable<string, string>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Sum(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Sum(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // range
            {
                try { range.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Sum(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Sum(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Sum(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            // int
            {
                Helper.ForEachMalformedEnumerableExpression<int>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<int?>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<int>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<int?>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long
            {
                Helper.ForEachMalformedEnumerableExpression<long>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<long?>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<long>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<long?>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachMalformedEnumerableExpression<float>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<float?>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<float>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<float?>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double
            {
                Helper.ForEachMalformedEnumerableExpression<double>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<double?>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<double>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<double?>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal
            {
                Helper.ForEachMalformedEnumerableExpression<decimal>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<decimal?>(
                    @"a => { try { a.Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<decimal>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachMalformedEnumerableExpression<decimal?>(
                    @"a => { try { a.Sum(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Weird()
        {
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookupDefault = new LookupDefaultEnumerable<int, int>();
            var lookupSpecific = new LookupSpecificEnumerable<int, int>();
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { (new EmptyEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { (new EmptyEnumerable<string>()).Sum(x => (int)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (int?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (long)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (long?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (float)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (float?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (double)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (double?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (decimal)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<string>()).Sum(x => (decimal?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { (new EmptyOrderedEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (int)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (int?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (long)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (long?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (float)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (float?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (double)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (double?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (decimal)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<string>()).Sum(x => (decimal?)x.Length); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                // no projection sums make no sense

                try { groupByDefault.Sum(x => (int)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (int?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (long)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (long?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (float)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (float?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (double)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (double?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (decimal)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Sum(x => (decimal?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                // no projection sums make no sense

                try { groupBySpecific.Sum(x => (int)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (int?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (long)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (long?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (float)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (float?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (double)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (double?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (decimal)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Sum(x => (decimal?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                // no projection sums make no sense

                try { lookupDefault.Sum(x => (int)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (int?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (long)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (long?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (float)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (float?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (double)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (double?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (decimal)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Sum(x => (decimal?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                // no projection sums make no sense

                try { lookupSpecific.Sum(x => (int)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (int?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (long)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (long?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (float)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (float?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (double)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (double?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (decimal)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Sum(x => (decimal?)x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { (new RangeEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { range.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { (new RepeatEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { repeat.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { (new ReverseRangeEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { reverseRange.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { (new OneItemDefaultEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefault.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { (new OneItemSpecificEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecific.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { (new OneItemDefaultOrderedEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefaultOrdered.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { (new OneItemSpecificOrderedEnumerable<int>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<int?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal?>()).Sum(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecificOrdered.Sum(x => (int)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (int?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (long)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (long?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (float)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (float?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (double)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (double?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (decimal)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Sum(x => (decimal?)x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            // int
            {
                var e1 = new int[] { 1, 2, 3 };
                var res1 = e1.Sum();

                Assert.AreEqual(6, res1);

                var e2 = new int?[] { 1, 2, null };
                var res2 = e2.Sum();

                Assert.AreEqual(3, res2);
            }

            // long
            {
                var e1 = new long[] { 1, 2, 3 };
                var res1 = e1.Sum();

                Assert.AreEqual(6, res1);

                var e2 = new long?[] { 1, 2, null };
                var res2 = e2.Sum();

                Assert.AreEqual(3, res2);
            }

            // float
            {
                var e1 = new float[] { 1, 2, 3 };
                var res1 = e1.Sum();

                Assert.AreEqual(6, res1);

                var e2 = new float?[] { 1, 2, null };
                var res2 = e2.Sum();

                Assert.AreEqual(3, res2);
            }

            // double
            {
                var e1 = new double[] { 1, 2, 3 };
                var res1 = e1.Sum();

                Assert.AreEqual(6, res1);

                var e2 = new double?[] { 1, 2, null };
                var res2 = e2.Sum();

                Assert.AreEqual(3, res2);
            }

            // decimal
            {
                var e1 = new decimal[] { 1, 2, 3 };
                var res1 = e1.Sum();

                Assert.AreEqual(6, res1);

                var e2 = new decimal?[] { 1, 2, null };
                var res2 = e2.Sum();

                Assert.AreEqual(3, res2);
            }
        }

        [TestMethod]
        public void Empty()
        {
            // int
            {
                var e1 = new int[0];
                var res1 = e1.Sum();

                Assert.AreEqual(0, res1);

                var e2 = new int?[0];
                var res2 = e2.Sum();

                Assert.AreEqual(0, res2);
            }

            // long
            {
                var e1 = new long[0];
                var res1 = e1.Sum();

                Assert.AreEqual(0, res1);

                var e2 = new long?[0];
                var res2 = e2.Sum();

                Assert.AreEqual(0, res2);
            }

            // float
            {
                var e1 = new float[0];
                var res1 = e1.Sum();

                Assert.AreEqual(0, res1);

                var e2 = new float?[0];
                var res2 = e2.Sum();

                Assert.AreEqual(0, res2);
            }

            // double
            {
                var e1 = new double[0];
                var res1 = e1.Sum();

                Assert.AreEqual(0, res1);

                var e2 = new double?[0];
                var res2 = e2.Sum();

                Assert.AreEqual(0, res2);
            }

            // decimal
            {
                var e1 = new decimal[0];
                var res1 = e1.Sum();

                Assert.AreEqual(0, res1);

                var e2 = new decimal?[0];
                var res2 = e2.Sum();

                Assert.AreEqual(0, res2);
            }
        }

        [TestMethod]
        public void Projected()
        {
            // int
            {
                var e1 = new[] { "hello", "world" };
                var res1 = e1.Sum(s => s.Length);

                Assert.AreEqual(10, res1);

                var e2 = new[] { "hello", "fizz", "world" };
                var res2 = e2.Sum(s => s.Length !=5 ? (int?)s.Length: null);

                Assert.AreEqual(4, res2);
            }

            // long
            {
                var e1 = new[] { "hello", "world" };
                var res1 = e1.Sum(s => (long)s.Length);

                Assert.AreEqual(10, res1);

                var e2 = new[] { "hello", "fizz", "world" };
                var res2 = e2.Sum(s => s.Length != 5 ? (long?)s.Length : null);

                Assert.AreEqual(4, res2);
            }

            // float
            {
                var e1 = new[] { "hello", "world" };
                var res1 = e1.Sum(s => (float)s.Length);

                Assert.AreEqual(10, res1);

                var e2 = new[] { "hello", "fizz", "world" };
                var res2 = e2.Sum(s => s.Length != 5 ? (float?)s.Length : null);

                Assert.AreEqual(4, res2);
            }

            // double
            {
                var e1 = new[] { "hello", "world" };
                var res1 = e1.Sum(s => (double)s.Length);

                Assert.AreEqual(10, res1);

                var e2 = new[] { "hello", "fizz", "world" };
                var res2 = e2.Sum(s => s.Length != 5 ? (double?)s.Length : null);

                Assert.AreEqual(4, res2);
            }

            // decimal
            {
                var e1 = new[] { "hello", "world" };
                var res1 = e1.Sum(s => (decimal)s.Length);

                Assert.AreEqual(10, res1);

                var e2 = new[] { "hello", "fizz", "world" };
                var res2 = e2.Sum(s => s.Length != 5 ? (decimal?)s.Length : null);

                Assert.AreEqual(4, res2);
            }
        }

        [TestMethod]
        public void NaNs()
        {
            // float
            {
                var e1 = new[] { 1, float.NaN, 2 };
                var res1 = e1.Sum();

                Assert.IsTrue(float.IsNaN(res1));

                var e2 = new[] { 1, float.NaN, default(float?) };
                var res2 = e2.Sum();

                Assert.IsTrue(float.IsNaN(res2.Value));

                var e3 = new[] { "hello", null, "world" };
                var res3 = e3.Sum(f => f != null ? f.Length : float.NaN);

                Assert.IsTrue(float.IsNaN(res3));
            }
        }
    }
}
