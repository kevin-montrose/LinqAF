using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class AverageTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IAverage<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IAverage ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            // int
            {
                const string ANSWER_STRING = "(1 + 3 + 7) / 3.0";

                Helper.ForEachEnumerableNoRetExpression(
                    new int[] { 1, 3, 7 },
                    "a => Assert.AreEqual("+ ANSWER_STRING + ", a.Average())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new int?[] { 1, 3, 7 },
                    "a => Assert.AreEqual("+ANSWER_STRING+", a.Average())",
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
                const string ANSWER_STRING = "(1 + 3 + 7) / 3.0";

                Helper.ForEachEnumerableNoRetExpression(
                    new long[] { 1, 3, 7 },
                    "a => Assert.AreEqual("+ANSWER_STRING+", a.Average())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new long?[] { 1, 3, 7 },
                    "a => Assert.AreEqual("+ ANSWER_STRING+", a.Average())",
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
                const string ANSWER_STRING = "(float)((((double)1.2f) + ((double)3.3f) + ((double)7.9f)) / ((double)3f))";

                Helper.ForEachEnumerableNoRetExpression(
                    new float[] { 1.2f, 3.3f, 7.9f },
                    "a => Assert.AreEqual("+ ANSWER_STRING + ", a.Average())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new float?[] { 1.2f, 3.3f, 7.9f },
                     "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Average())",
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
                const string ANSWER_STRING = "(1.222 + 3.345 + 7.9876) / 3.0";

                Helper.ForEachEnumerableNoRetExpression(
                    new double[] { 1.222, 3.345, 7.9876 },
                    "a => Assert.AreEqual("+ ANSWER_STRING + ", a.Average())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new double?[] { 1.222, 3.345, 7.9876 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Average())",
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
                const string ANSWER_STRING = "(1.222m + 3.345m + 7.9876m) / 3.0m";

                Helper.ForEachEnumerableNoRetExpression(
                    new decimal[] { 1.222m, 3.345m, 7.9876m },
                    "a => Assert.AreEqual("+ ANSWER_STRING + ", a.Average())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new decimal?[] { 1.222m, 3.345m, 7.9876m },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Average())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // projections
            {
                Func<string, int> intProj = str => (int)str.Length;
                Func<string, int?> nIntProj = str => (int?)str.Length;
                Func<string, long> longProj = str => (long)str.Length;
                Func<string, long?> nLongProj = str => (long?)str.Length;
                Func<string, float> floatProj = str => (float)str.Length;
                Func<string, float?> nFloatProj = str => (float?)str.Length;
                Func<string, double> doubleProj = str => (double)str.Length;
                Func<string, double?> nDoubleProj = str => (double?)str.Length;
                Func<string, decimal> decimalProj = str => (decimal)str.Length;
                Func<string, decimal?> nDecimalProj = str => (decimal?)str.Length;

                var doubleAns = ((double)3);
                var floatAns = ((float)3);
                var decimalAns = ((decimal)3);

                foreach (var e in Helper.GetEnumerables(new[] { "foo" }))
                {
                    Assert.AreEqual(doubleAns, e.Average(intProj));
                    Assert.AreEqual((double?)doubleAns, e.Average(nIntProj));
                    Assert.AreEqual(doubleAns, e.Average(longProj));
                    Assert.AreEqual((double?)doubleAns, e.Average(nLongProj));
                    Assert.AreEqual(floatAns, e.Average(floatProj));
                    Assert.AreEqual((float?)floatAns, e.Average(nFloatProj));
                    Assert.AreEqual(doubleAns, e.Average(doubleProj));
                    Assert.AreEqual((double?)doubleAns, e.Average(nDoubleProj));
                    Assert.AreEqual(decimalAns, e.Average(decimalProj));
                    Assert.AreEqual((decimal?)decimalAns, e.Average(nDecimalProj));
                }
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
            Func<string, int> intProj = str => (int)str.Length;
            Func<string, int?> nIntProj = str => (int?)str.Length;
            Func<string, long> longProj = str => (long)str.Length;
            Func<string, long?> nLongProj = str => (long?)str.Length;
            Func<string, float> floatProj = str => (float)str.Length;
            Func<string, float?> nFloatProj = str => (float?)str.Length;
            Func<string, double> doubleProj = str => (double)str.Length;
            Func<string, double?> nDoubleProj = str => (double?)str.Length;
            Func<string, decimal> decimalProj = str => (decimal)str.Length;
            Func<string, decimal?> nDecimalProj = str => (decimal?)str.Length;

            // empty
            {
                try { Enumerable.Empty<int>().Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().Average());
                try { Enumerable.Empty<long>().Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().Average());
                try { Enumerable.Empty<float>().Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().Average());
                try { Enumerable.Empty<double>().Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().Average());
                try { Enumerable.Empty<decimal>().Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().Average());

                try { Enumerable.Empty<string>().Average(intProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().Average(nIntProj));
                try { Enumerable.Empty<string>().Average(longProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().Average(nLongProj));
                try { Enumerable.Empty<string>().Average(floatProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().Average(nFloatProj));
                try { Enumerable.Empty<string>().Average(doubleProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().Average(nDoubleProj));
                try { Enumerable.Empty<string>().Average(decimalProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().Average(nDecimalProj));
            }

            // emptyOrdered
            {
                try { Enumerable.Empty<int>().OrderBy(x => x).Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().OrderBy(x => x).Average());
                try { Enumerable.Empty<long>().OrderBy(x => x).Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().OrderBy(x => x).Average());
                try { Enumerable.Empty<float>().OrderBy(x => x).Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().OrderBy(x => x).Average());
                try { Enumerable.Empty<double>().OrderBy(x => x).Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().OrderBy(x => x).Average());
                try { Enumerable.Empty<decimal>().OrderBy(x => x).Average(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().OrderBy(x => x).Average());

                try { Enumerable.Empty<string>().OrderBy(x => x).Average(intProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().OrderBy(x => x).Average(nIntProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Average(longProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().OrderBy(x => x).Average(nLongProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Average(floatProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().OrderBy(x => x).Average(nFloatProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Average(doubleProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().OrderBy(x => x).Average(nDoubleProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Average(decimalProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().OrderBy(x => x).Average(nDecimalProj));
            }

            // groupByDefault
            {
                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;

                // non-projection Average makes no sense
                var groupByDefault = new[] { 1, 2, 3, 4, 5 }.GroupBy(x => x);

                double doubleAns = (1.0 + 2.0 + 3.0 + 4.0 + 5.0) / 5.0;
                double floatAns = (1f + 2f + 3f + 4f + 5f) / 5f;
                decimal decimalAns = (1m + 2m + 3m + 4m + 5m) / 5m;

                Assert.AreEqual(doubleAns, groupByDefault.Average(g_intProj));
                Assert.AreEqual((double?)doubleAns, groupByDefault.Average(g_nIntProj));
                Assert.AreEqual(doubleAns, groupByDefault.Average(g_longProj));
                Assert.AreEqual((double?)doubleAns, groupByDefault.Average(g_nLongProj));
                Assert.AreEqual(floatAns, groupByDefault.Average(g_floatProj));
                Assert.AreEqual((float?)floatAns, groupByDefault.Average(g_nFloatProj));
                Assert.AreEqual(doubleAns, groupByDefault.Average(g_doubleProj));
                Assert.AreEqual((double?)doubleAns, groupByDefault.Average(g_doubleProj));
                Assert.AreEqual(decimalAns, groupByDefault.Average(g_nDecimalProj));
                Assert.AreEqual((decimal?)decimalAns, groupByDefault.Average(g_nDecimalProj));
            }

            // groupBySpecific
            {
                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;

                // non-projection Average makes no sense
                var groupBySpecific = new[] { 1, 2, 3, 4, 5 }.GroupBy(x => x, new _IntComparer());

                double doubleAns = (1.0 + 2.0 + 3.0 + 4.0 + 5.0) / 5.0;
                double floatAns = (1f + 2f + 3f + 4f + 5f) / 5f;
                decimal decimalAns = (1m + 2m + 3m + 4m + 5m) / 5m;

                Assert.AreEqual(doubleAns, groupBySpecific.Average(g_intProj));
                Assert.AreEqual((double?)doubleAns, groupBySpecific.Average(g_nIntProj));
                Assert.AreEqual(doubleAns, groupBySpecific.Average(g_longProj));
                Assert.AreEqual((double?)doubleAns, groupBySpecific.Average(g_nLongProj));
                Assert.AreEqual(floatAns, groupBySpecific.Average(g_floatProj));
                Assert.AreEqual((float?)floatAns, groupBySpecific.Average(g_nFloatProj));
                Assert.AreEqual(doubleAns, groupBySpecific.Average(g_doubleProj));
                Assert.AreEqual((double?)doubleAns, groupBySpecific.Average(g_doubleProj));
                Assert.AreEqual(decimalAns, groupBySpecific.Average(g_nDecimalProj));
                Assert.AreEqual((decimal?)decimalAns, groupBySpecific.Average(g_nDecimalProj));
            }

            // lookupDefault
            {
                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;

                // non-projection Average makes no sense
                var lookupDefault = new[] { 1, 2, 3, 4, 5 }.ToLookup(x => x);

                double doubleAns = (1.0 + 2.0 + 3.0 + 4.0 + 5.0) / 5.0;
                double floatAns = (1f + 2f + 3f + 4f + 5f) / 5f;
                decimal decimalAns = (1m + 2m + 3m + 4m + 5m) / 5m;

                Assert.AreEqual(doubleAns, lookupDefault.Average(g_intProj));
                Assert.AreEqual((double?)doubleAns, lookupDefault.Average(g_nIntProj));
                Assert.AreEqual(doubleAns, lookupDefault.Average(g_longProj));
                Assert.AreEqual((double?)doubleAns, lookupDefault.Average(g_nLongProj));
                Assert.AreEqual(floatAns, lookupDefault.Average(g_floatProj));
                Assert.AreEqual((float?)floatAns, lookupDefault.Average(g_nFloatProj));
                Assert.AreEqual(doubleAns, lookupDefault.Average(g_doubleProj));
                Assert.AreEqual((double?)doubleAns, lookupDefault.Average(g_doubleProj));
                Assert.AreEqual(decimalAns, lookupDefault.Average(g_nDecimalProj));
                Assert.AreEqual((decimal?)decimalAns, lookupDefault.Average(g_nDecimalProj));
            }

            // lookupSpecific
            {
                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;

                // non-projection Average makes no sense
                var lookupSpecific = new[] { 1, 2, 3, 4, 5 }.ToLookup(x => x, new _IntComparer());

                double doubleAns = (1.0 + 2.0 + 3.0 + 4.0 + 5.0) / 5.0;
                double floatAns = (1f + 2f + 3f + 4f + 5f) / 5f;
                decimal decimalAns = (1m + 2m + 3m + 4m + 5m) / 5m;

                Assert.AreEqual(doubleAns, lookupSpecific.Average(g_intProj));
                Assert.AreEqual((double?)doubleAns, lookupSpecific.Average(g_nIntProj));
                Assert.AreEqual(doubleAns, lookupSpecific.Average(g_longProj));
                Assert.AreEqual((double?)doubleAns, lookupSpecific.Average(g_nLongProj));
                Assert.AreEqual(floatAns, lookupSpecific.Average(g_floatProj));
                Assert.AreEqual((float?)floatAns, lookupSpecific.Average(g_nFloatProj));
                Assert.AreEqual(doubleAns, lookupSpecific.Average(g_doubleProj));
                Assert.AreEqual((double?)doubleAns, lookupSpecific.Average(g_doubleProj));
                Assert.AreEqual(decimalAns, lookupSpecific.Average(g_nDecimalProj));
                Assert.AreEqual((decimal?)decimalAns, lookupSpecific.Average(g_nDecimalProj));
            }

            // range
            {
                Func<int, int> i_intProj = x => (int)x;
                Func<int, int?> i_nIntProj = x => (int?)x;
                Func<int, long> i_longProj = x => (long)x;
                Func<int, long?> i_nLongProj = x => (long?)x;
                Func<int, float> i_floatProj = x => (float)x;
                Func<int, float?> i_nFloatProj = x => (float?)x;
                Func<int, double> i_doubleProj = x => (double)x;
                Func<int, double?> i_nDoubleProj = x => (double?)x;
                Func<int, decimal> i_decimalProj = x => (decimal)x;
                Func<int, decimal?> i_nDecimalProj = x => (decimal?)x;

                Assert.AreEqual((1 + 2 + 3 + 4 + 5) / 5, Enumerable.Range(1, 5).Average());
                // non-int non-projection Average makes no sense

                double doubleAns = (1.0 + 2.0 + 3.0 + 4.0 + 5.0) / 5.0;
                double floatAns = (1f + 2f + 3f + 4f + 5f) / 5f;
                decimal decimalAns = (1m + 2m + 3m + 4m + 5m) / 5m;

                Assert.AreEqual(doubleAns, Enumerable.Range(1, 5).Average(i_intProj));
                Assert.AreEqual((double?)doubleAns, Enumerable.Range(1, 5).Average(i_nIntProj));
                Assert.AreEqual(doubleAns, Enumerable.Range(1, 5).Average(i_longProj));
                Assert.AreEqual((double?)doubleAns, Enumerable.Range(1, 5).Average(i_nLongProj));
                Assert.AreEqual(floatAns, Enumerable.Range(1, 5).Average(i_floatProj));
                Assert.AreEqual((float?)floatAns, Enumerable.Range(1, 5).Average(i_nFloatProj));
                Assert.AreEqual(doubleAns, Enumerable.Range(1, 5).Average(i_doubleProj));
                Assert.AreEqual((double?)doubleAns, Enumerable.Range(1, 5).Average(i_doubleProj));
                Assert.AreEqual(decimalAns, Enumerable.Range(1, 5).Average(i_nDecimalProj));
                Assert.AreEqual((decimal?)decimalAns, Enumerable.Range(1, 5).Average(i_nDecimalProj));
            }

            // repeat
            {
                double doubleAns = (3.0 + 3.0 + 3.0) / 3.0;
                float floatAns = (3f + 3f + 3f) / 3f;
                decimal decimalAns = (3m + 3m + 3m) / 3m;

                Assert.AreEqual(doubleAns, Enumerable.Repeat(3, 3).Average());
                Assert.AreEqual(doubleAns, Enumerable.Repeat((int?)3, 3).Average());
                Assert.AreEqual(doubleAns, Enumerable.Repeat(3L, 3).Average());
                Assert.AreEqual(doubleAns, Enumerable.Repeat((long?)3, 3).Average());
                Assert.AreEqual(floatAns, Enumerable.Repeat(3f, 3).Average());
                Assert.AreEqual(floatAns, Enumerable.Repeat((float?)3, 3).Average());
                Assert.AreEqual(doubleAns, Enumerable.Repeat(3.0, 3).Average());
                Assert.AreEqual(doubleAns, Enumerable.Repeat((double?)3, 3).Average());
                Assert.AreEqual(decimalAns, Enumerable.Repeat(3m, 3).Average());
                Assert.AreEqual(decimalAns, Enumerable.Repeat((decimal?)3, 3).Average());

                Assert.AreEqual(doubleAns, Enumerable.Repeat("foo", 3).Average(intProj));
                Assert.AreEqual(doubleAns, Enumerable.Repeat("foo", 3).Average(nIntProj));
                Assert.AreEqual(doubleAns, Enumerable.Repeat("foo", 3).Average(longProj));
                Assert.AreEqual(doubleAns, Enumerable.Repeat("foo", 3).Average(nLongProj));
                Assert.AreEqual(floatAns, Enumerable.Repeat("foo", 3).Average(floatProj));
                Assert.AreEqual(floatAns, Enumerable.Repeat("foo", 3).Average(nFloatProj));
                Assert.AreEqual(doubleAns, Enumerable.Repeat("foo", 3).Average(doubleProj));
                Assert.AreEqual(doubleAns, Enumerable.Repeat("foo", 3).Average(nDoubleProj));
                Assert.AreEqual(decimalAns, Enumerable.Repeat("foo", 3).Average(decimalProj));
                Assert.AreEqual(decimalAns, Enumerable.Repeat("foo", 3).Average(nDecimalProj));
            }

            // reverseRange
            {
                Func<int, int> i_intProj = x => (int)x;
                Func<int, int?> i_nIntProj = x => (int?)x;
                Func<int, long> i_longProj = x => (long)x;
                Func<int, long?> i_nLongProj = x => (long?)x;
                Func<int, float> i_floatProj = x => (float)x;
                Func<int, float?> i_nFloatProj = x => (float?)x;
                Func<int, double> i_doubleProj = x => (double)x;
                Func<int, double?> i_nDoubleProj = x => (double?)x;
                Func<int, decimal> i_decimalProj = x => (decimal)x;
                Func<int, decimal?> i_nDecimalProj = x => (decimal?)x;

                Assert.AreEqual((1 + 2 + 3 + 4 + 5) / 5, Enumerable.Range(1, 5).Reverse().Average());
                // non-int non-projection Average makes no sense

                double doubleAns = (1.0 + 2.0 + 3.0 + 4.0 + 5.0) / 5.0;
                double floatAns = (1f + 2f + 3f + 4f + 5f) / 5f;
                decimal decimalAns = (1m + 2m + 3m + 4m + 5m) / 5m;

                Assert.AreEqual(doubleAns, Enumerable.Range(1, 5).Reverse().Average(i_intProj));
                Assert.AreEqual((double?)doubleAns, Enumerable.Range(1, 5).Reverse().Average(i_nIntProj));
                Assert.AreEqual(doubleAns, Enumerable.Range(1, 5).Reverse().Average(i_longProj));
                Assert.AreEqual((double?)doubleAns, Enumerable.Range(1, 5).Reverse().Average(i_nLongProj));
                Assert.AreEqual(floatAns, Enumerable.Range(1, 5).Reverse().Average(i_floatProj));
                Assert.AreEqual((float?)floatAns, Enumerable.Range(1, 5).Reverse().Average(i_nFloatProj));
                Assert.AreEqual(doubleAns, Enumerable.Range(1, 5).Reverse().Average(i_doubleProj));
                Assert.AreEqual((double?)doubleAns, Enumerable.Range(1, 5).Reverse().Average(i_doubleProj));
                Assert.AreEqual(decimalAns, Enumerable.Range(1, 5).Reverse().Average(i_nDecimalProj));
                Assert.AreEqual((decimal?)decimalAns, Enumerable.Range(1, 5).Reverse().Average(i_nDecimalProj));
            }

            // oneItemDefault
            {
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Average());
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().DefaultIfEmpty().Average());
                Assert.AreEqual(0, Enumerable.Empty<long>().DefaultIfEmpty().Average());
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().DefaultIfEmpty().Average());
                Assert.AreEqual(0, Enumerable.Empty<float>().DefaultIfEmpty().Average());
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().DefaultIfEmpty().Average());
                Assert.AreEqual(0, Enumerable.Empty<double>().DefaultIfEmpty().Average());
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().DefaultIfEmpty().Average());
                Assert.AreEqual(0, Enumerable.Empty<decimal>().DefaultIfEmpty().Average());
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().DefaultIfEmpty().Average());

                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (int)(s?.Length ?? 0)));
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (int?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (long)(s?.Length ?? 0)));
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (long?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (float)(s?.Length ?? 0)));
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (float?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (double)(s?.Length ?? 0)));
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (double?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (decimal)(s?.Length ?? 0)));
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().DefaultIfEmpty().Average(s => (decimal?)s?.Length));
            }

            // oneItemSpecific
            {
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<long>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<long?>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<float>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<float?>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<double>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<double?>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<decimal>().DefaultIfEmpty(4).Average());
                Assert.AreEqual(4, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).Average());

                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (int)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (int?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (long)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (long?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (float)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (float?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (double)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (double?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (decimal)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").Average(s => (decimal?)s?.Length));
            }

            // oneItemDefaultOrdered
            {
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(0, Enumerable.Empty<long>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(0, Enumerable.Empty<float>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(0, Enumerable.Empty<double>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(0, Enumerable.Empty<decimal>().DefaultIfEmpty().OrderBy(x => x).Average());
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().DefaultIfEmpty().OrderBy(x => x).Average());

                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (int)(s?.Length ?? 0)));
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (int?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (long)(s?.Length ?? 0)));
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (long?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (float)(s?.Length ?? 0)));
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (float?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (double)(s?.Length ?? 0)));
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (double?)s?.Length));
                Assert.AreEqual(0, Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (decimal)(s?.Length ?? 0)));
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x).Average(s => (decimal?)s?.Length));
            }

            // oneItemSpecificOrdered
            {
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<long>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<long?>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<float>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<float?>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<double>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<double?>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<decimal>().DefaultIfEmpty(4).OrderBy(x => x).Average());
                Assert.AreEqual(4, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).OrderBy(x => x).Average());

                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (int)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (int?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (long)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (long?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (float)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (float?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (double)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (double?)s?.Length));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (decimal)(s?.Length ?? 0)));
                Assert.AreEqual(4, Enumerable.Empty<string>().DefaultIfEmpty("fizz").OrderBy(x => x).Average(s => (decimal?)s?.Length));
            }
        }

        [TestMethod]
        public void Simple()
        {
            // int
            {
                const double EXPECTED1 = (double)(5 + 8 + 13) / 3.0;
                const double EXPECTED2 = (double)(5 + 3 + 3) / 3.0;

                var e1 = new int[] { 5, 8, 13 };
                var res1 = e1.Average();
                Assert.AreEqual(EXPECTED1, res1);

                var e2 = new int?[] { 5, null, 8, 13};
                var res2 = e2.Average();
                Assert.AreEqual(EXPECTED1, res2);

                var e3 = new string[] { "hello", "foo", "bar" };
                var res3 = e3.Average(s => s.Length);
                Assert.AreEqual(EXPECTED2, res3);

                var e4 = new string[] { "hello", "foo", null, "bar" };
                var res4 = e3.Average(s => s?.Length);
                Assert.AreEqual(EXPECTED2, res3);
            }

            // long
            {
                const double EXPECTED1 = (double)(5 + 8 + 13) / 3.0;
                const double EXPECTED2 = (double)(5 + 3 + 3) / 3.0;

                var e1 = new long[] { 5, 8, 13 };
                var res1 = e1.Average();
                Assert.AreEqual(EXPECTED1, res1);

                var e2 = new long?[] { 5, null, 8, 13 };
                var res2 = e2.Average();
                Assert.AreEqual(EXPECTED1, res2);

                var e3 = new string[] { "hello", "foo", "bar" };
                var res3 = e3.Average(s => (long)s.Length);
                Assert.AreEqual(EXPECTED2, res3);

                var e4 = new string[] { "hello", "foo", null, "bar" };
                var res4 = e3.Average(s => (long?)s?.Length);
                Assert.AreEqual(EXPECTED2, res3);
            }

            // float
            {
                const float EXPECTED1 = (float)((double)(5 + 8 + 13) / 3.0);
                const float EXPECTED2 = (float)((double)(5 + 3 + 3) / 3.0);

                var e1 = new float[] { 5, 8, 13 };
                var res1 = e1.Average();
                Assert.AreEqual(EXPECTED1, res1);

                var e2 = new float?[] { 5, null, 8, 13 };
                var res2 = e2.Average();
                Assert.AreEqual(EXPECTED1, res2);

                var e3 = new string[] { "hello", "foo", "bar" };
                var res3 = e3.Average(s => (float)s.Length);
                Assert.AreEqual(EXPECTED2, res3);

                var e4 = new string[] { "hello", "foo", null, "bar" };
                var res4 = e3.Average(s => (float?)s?.Length);
                Assert.AreEqual(EXPECTED2, res3);
            }

            // double
            {
                const double EXPECTED1 = (double)(5 + 8 + 13) / 3.0;
                const double EXPECTED2 = (double)(5 + 3 + 3) / 3.0;

                var e1 = new double[] { 5, 8, 13 };
                var res1 = e1.Average();
                Assert.AreEqual(EXPECTED1, res1);

                var e2 = new double?[] { 5, null, 8, 13 };
                var res2 = e2.Average();
                Assert.AreEqual(EXPECTED1, res2);

                var e3 = new string[] { "hello", "foo", "bar" };
                var res3 = e3.Average(s => (double)s.Length);
                Assert.AreEqual(EXPECTED2, res3);

                var e4 = new string[] { "hello", "foo", null, "bar" };
                var res4 = e3.Average(s => (double?)s?.Length);
                Assert.AreEqual(EXPECTED2, res3);
            }

            // decimal
            {
                const decimal EXPECTED1 = ((5 + 8 + 13) / 3.0m);
                const decimal EXPECTED2 = ((5 + 3 + 3) / 3.0m);

                var e1 = new decimal[] { 5, 8, 13 };
                var res1 = e1.Average();
                Assert.AreEqual(EXPECTED1, res1);

                var e2 = new decimal?[] { 5, null, 8, 13 };
                var res2 = e2.Average();
                Assert.AreEqual(EXPECTED1, res2);

                var e3 = new string[] { "hello", "foo", "bar" };
                var res3 = e3.Average(s => (decimal)s.Length);
                Assert.AreEqual(EXPECTED2, res3);

                var e4 = new string[] { "hello", "foo", null, "bar" };
                var res4 = e3.Average(s => (decimal?)s?.Length);
                Assert.AreEqual(EXPECTED2, res3);
            }
        }

        [TestMethod]
        public void Errors()
        {
            // int
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                   typeof(GroupByDefaultEnumerable<,,,,>),
                   typeof(GroupBySpecificEnumerable<,,,,>),
                   typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // int?
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long?
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double?
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal?
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[] { "hello" },
                    @"a => { try { a.Average(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException e) { Assert.AreEqual(""selector"", e.ParamName); } }",
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
                try { empty.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Average(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Average(default(Func<GroupingEnumerable<string, string>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Average(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Average(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // range
            {
                try { range.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Average(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Average(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Average(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            // int
            {
                Helper.ForEachMalformedEnumerableExpression<int>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // int?
            {
                Helper.ForEachMalformedEnumerableExpression<int?>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long
            {
                Helper.ForEachMalformedEnumerableExpression<long>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // long?
            {
                Helper.ForEachMalformedEnumerableExpression<long?>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachMalformedEnumerableExpression<float>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // float
            {
                Helper.ForEachMalformedEnumerableExpression<float?>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double
            {
                Helper.ForEachMalformedEnumerableExpression<double>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // double?
            {
                Helper.ForEachMalformedEnumerableExpression<double?>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal
            {
                Helper.ForEachMalformedEnumerableExpression<decimal>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // decimal?
            {
                Helper.ForEachMalformedEnumerableExpression<decimal?>(
                    @"a => 
                      {
                        try
                        {
                            a.Average();
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                   typeof(LookupSpecificEnumerable<,>)
                );
            }

            // projections
            {
                foreach (var e in Helper.GetMalformedEnumerables<string>())
                {
                    // int
                    {
                        try
                        {
                            Func<string, int> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }

                        try
                        {
                            Func<string, int?> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }
                    }

                    // long
                    {
                        try
                        {
                            Func<string, long> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }

                        try
                        {
                            Func<string, long?> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }
                    }

                    // float
                    {
                        try
                        {
                            Func<string, float> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }

                        try
                        {
                            Func<string, float?> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }
                    }

                    // double
                    {
                        try
                        {
                            Func<string, double> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }

                        try
                        {
                            Func<string, double?> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }
                    }

                    // decimal
                    {
                        try
                        {
                            Func<string, decimal> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }

                        try
                        {
                            Func<string, decimal?> foo = s => s.Length;
                            e.Average(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentException exc)
                        {
                            Assert.AreEqual("source", exc.ParamName);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Malformed_Weird()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
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

            Func<int, int> intProj = x => (int)x;
            Func<int, int?> nIntProj = x => (int?)x;
            Func<int, long> longProj = x => (long)x;
            Func<int, long?> nLongProj = x => (long?)x;
            Func<int, float> floatProj = x => (float)x;
            Func<int, float?> nFloatProj = x => (float?)x;
            Func<int, double> doubleProj = x => (double)x;
            Func<int, double?> nDoubleProj = x => (double?)x;
            Func<int, decimal> decimalProj = x => (decimal)x;
            Func<int, decimal?> nDecimalProj = x => (decimal?)x;
            Func<int, DateTime> dtProj = x => new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);
            Func<int, DateTime?> nDtProj = x => (DateTime?)new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);

            // empty
            {
                try { (new EmptyEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { empty.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { (new EmptyOrderedEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { emptyOrdered.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                // non-projection Average makes no sense

                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);

                try { groupByDefault.Average(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Average(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                // non-projection Average makes no sense

                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);

                try { groupBySpecific.Average(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Average(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                // non-projection Average makes no sense

                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);

                try { lookupDefault.Average(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Average(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                // non-projection Average makes no sense

                Func<GroupingEnumerable<int, int>, int> g_intProj = x => (int)x.Key;
                Func<GroupingEnumerable<int, int>, int?> g_nIntProj = x => (int?)x.Key;
                Func<GroupingEnumerable<int, int>, long> g_longProj = x => (long)x.Key;
                Func<GroupingEnumerable<int, int>, long?> g_nLongProj = x => (long?)x.Key;
                Func<GroupingEnumerable<int, int>, float> g_floatProj = x => (float)x.Key;
                Func<GroupingEnumerable<int, int>, float?> g_nFloatProj = x => (float?)x.Key;
                Func<GroupingEnumerable<int, int>, double> g_doubleProj = x => (double)x.Key;
                Func<GroupingEnumerable<int, int>, double?> g_nDoubleProj = x => (double?)x.Key;
                Func<GroupingEnumerable<int, int>, decimal> g_decimalProj = x => (decimal)x.Key;
                Func<GroupingEnumerable<int, int>, decimal?> g_nDecimalProj = x => (decimal?)x.Key;
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);

                try { lookupSpecific.Average(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Average(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { (new RangeEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { range.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { (new RepeatEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { repeat.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { (new ReverseRangeEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { reverseRange.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { (new OneItemDefaultEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefault.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { (new OneItemSpecificEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecific.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { (new OneItemDefaultOrderedEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefaultOrdered.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { (new OneItemSpecificOrderedEnumerable<int>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<int?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal?>()).Average(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecificOrdered.Average(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Average(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
