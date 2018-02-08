using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class MaxTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IMax<>), out instOverlaps, out extOverlaps);

            if (instOverlaps.Count > 0)
            {
                var failure = new StringBuilder();
                foreach (var kv in instOverlaps)
                {
                    failure.AppendLine("For " + kv.Key);
                    failure.AppendLine(
                        LinqAFString.Join("\t -", kv.Value.Select(x => x.ToString() + "\n"))
                    );

                    Assert.Fail(failure.ToString());
                }
            }

            if (extOverlaps.Count > 0)
            {
                var failure = new StringBuilder();
                foreach (var kv in extOverlaps)
                {
                    failure.AppendLine("For " + kv.Key);
                    failure.AppendLine(
                        LinqAFString.Join("\t -", kv.Value.Select(x => x.ToString() + "\n"))
                    );

                    Assert.Fail(failure.ToString());
                }
            }
        }

        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IMax<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IMax ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            // int
            {
                const string ANSWER_STRING = "123";

                Helper.ForEachEnumerableNoRetExpression(
                    new int[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
                
                Helper.ForEachEnumerableNoRetExpression(
                    new int?[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)

               );
            }

            // long
            {
                const string ANSWER_STRING = "123L";

                Helper.ForEachEnumerableNoRetExpression(
                    new long[] { 1, 123, 44 },
                    "a => Assert.AreEqual("+ANSWER_STRING+", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new long?[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
               );
            }

            // float
            {
                const string ANSWER_STRING = "(float)123";

                Helper.ForEachEnumerableNoRetExpression(
                    new float[] { 1, 123, 44 },
                    "a => Assert.AreEqual("+ANSWER_STRING+", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new float?[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
               );
            }

            // double
            {
                const string ANSWER_STRING = "(double)123";

                Helper.ForEachEnumerableNoRetExpression(
                    new double[] { 1, 123, 44 },
                    "a => Assert.AreEqual("+ANSWER_STRING+", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new double?[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
               );
            }

            // decimal
            {
                const string ANSWER_STRING = "123m";

                Helper.ForEachEnumerableNoRetExpression(
                    new decimal[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new decimal?[] { 1, 123, 44 },
                    "a => Assert.AreEqual(" + ANSWER_STRING + ", a.Max())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
               );
            }

            // projections
            {
                var dt = new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);

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
                Func<string, DateTime> dtProj = str => dt;
                Func<string, DateTime?> nDtProj = str => (DateTime?)dt;

                foreach (var e in Helper.GetEnumerables(new[] { "foo" }))
                {
                    Assert.AreEqual(3, e.Max(intProj));
                    Assert.AreEqual(3, e.Max(nIntProj));
                    Assert.AreEqual(3L, e.Max(longProj));
                    Assert.AreEqual(3L, e.Max(nLongProj));
                    Assert.AreEqual(3f, e.Max(floatProj));
                    Assert.AreEqual(3f, e.Max(nFloatProj));
                    Assert.AreEqual(3.0, e.Max(doubleProj));
                    Assert.AreEqual(3.0, e.Max(nDoubleProj));
                    Assert.AreEqual(3m, e.Max(decimalProj));
                    Assert.AreEqual(3m, e.Max(nDecimalProj));
                    Assert.AreEqual(dt, e.Max(dtProj));
                    Assert.AreEqual((DateTime?)dt, e.Max(nDtProj));
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
            var dt = new DateTime(2017, 08, 01, 0, 0, 0, DateTimeKind.Utc);
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
            Func<string, DateTime> dtProj = str => dt;
            Func<string, DateTime?> nDtProj = str => (DateTime?)dt;

            // empty
            {
                try { Enumerable.Empty<int>().Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().Max());
                try { Enumerable.Empty<long>().Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().Max());
                try { Enumerable.Empty<float>().Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().Max());
                try { Enumerable.Empty<double>().Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().Max());
                try { Enumerable.Empty<decimal>().Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().Max());
                try { Enumerable.Empty<DateTime>().Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<DateTime?>().Max());

                try { Enumerable.Empty<string>().Max(intProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().Max(nIntProj));
                try { Enumerable.Empty<string>().Max(longProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().Max(nLongProj));
                try { Enumerable.Empty<string>().Max(floatProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().Max(nFloatProj));
                try { Enumerable.Empty<string>().Max(doubleProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().Max(nDoubleProj));
                try { Enumerable.Empty<string>().Max(decimalProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().Max(nDecimalProj));
                try { Enumerable.Empty<string>().Max(dtProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<string>().Max(nDtProj));
            }

            // emptyOrdered
            {
                try { Enumerable.Empty<int>().OrderBy(x => x).Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().OrderBy(x => x).Max());
                try { Enumerable.Empty<long>().OrderBy(x => x).Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().OrderBy(x => x).Max());
                try { Enumerable.Empty<float>().OrderBy(x => x).Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().OrderBy(x => x).Max());
                try { Enumerable.Empty<double>().OrderBy(x => x).Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().OrderBy(x => x).Max());
                try { Enumerable.Empty<decimal>().OrderBy(x => x).Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().OrderBy(x => x).Max());
                try { Enumerable.Empty<DateTime>().OrderBy(x => x).Max(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<DateTime?>().OrderBy(x => x).Max());

                try { Enumerable.Empty<string>().OrderBy(x => x).Max(intProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().OrderBy(x => x).Max(nIntProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Max(longProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().OrderBy(x => x).Max(nLongProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Max(floatProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().OrderBy(x => x).Max(nFloatProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Max(doubleProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().OrderBy(x => x).Max(nDoubleProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Max(decimalProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().OrderBy(x => x).Max(nDecimalProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Max(dtProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<string>().OrderBy(x => x).Max(nDtProj));
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
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => dt;
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)dt;

                // non-projection Max makes no sense
                var groupByDefault = new[] { 1, 2, 3 }.GroupBy(x => x);

                Assert.AreEqual((int)3, groupByDefault.Max(g_intProj));
                Assert.AreEqual((int?)3, groupByDefault.Max(g_nIntProj));
                Assert.AreEqual((long)3, groupByDefault.Max(g_longProj));
                Assert.AreEqual((long?)3, groupByDefault.Max(g_nLongProj));
                Assert.AreEqual((float)3, groupByDefault.Max(g_floatProj));
                Assert.AreEqual((float?)3, groupByDefault.Max(g_nFloatProj));
                Assert.AreEqual((double)3, groupByDefault.Max(g_doubleProj));
                Assert.AreEqual((double?)3, groupByDefault.Max(g_nDoubleProj));
                Assert.AreEqual((decimal)3, groupByDefault.Max(g_decimalProj));
                Assert.AreEqual((decimal?)3, groupByDefault.Max(g_nDecimalProj));
                Assert.AreEqual(dt, groupByDefault.Max(g_dtProj));
                Assert.AreEqual((DateTime?)dt, groupByDefault.Max(g_nDtProj));
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
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => dt;
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)dt;

                // non-projection Max makes no sense

                var groupBySpecific = new[] { 1, 2, 3 }.GroupBy(x => x, new _IntComparer());

                Assert.AreEqual((int)3, groupBySpecific.Max(g_intProj));
                Assert.AreEqual((int?)3, groupBySpecific.Max(g_nIntProj));
                Assert.AreEqual((long)3, groupBySpecific.Max(g_longProj));
                Assert.AreEqual((long?)3, groupBySpecific.Max(g_nLongProj));
                Assert.AreEqual((float)3, groupBySpecific.Max(g_floatProj));
                Assert.AreEqual((float?)3, groupBySpecific.Max(g_nFloatProj));
                Assert.AreEqual((double)3, groupBySpecific.Max(g_doubleProj));
                Assert.AreEqual((double?)3, groupBySpecific.Max(g_nDoubleProj));
                Assert.AreEqual((decimal)3, groupBySpecific.Max(g_decimalProj));
                Assert.AreEqual((decimal?)3, groupBySpecific.Max(g_nDecimalProj));
                Assert.AreEqual(dt, groupBySpecific.Max(g_dtProj));
                Assert.AreEqual((DateTime?)dt, groupBySpecific.Max(g_nDtProj));
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
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => dt;
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)dt;

                // non-projection Max makes no sense
                var lookupDefault = new[] { 1, 2, 3 }.ToLookup(x => x);

                Assert.AreEqual((int)3, lookupDefault.Max(g_intProj));
                Assert.AreEqual((int?)3, lookupDefault.Max(g_nIntProj));
                Assert.AreEqual((long)3, lookupDefault.Max(g_longProj));
                Assert.AreEqual((long?)3, lookupDefault.Max(g_nLongProj));
                Assert.AreEqual((float)3, lookupDefault.Max(g_floatProj));
                Assert.AreEqual((float?)3, lookupDefault.Max(g_nFloatProj));
                Assert.AreEqual((double)3, lookupDefault.Max(g_doubleProj));
                Assert.AreEqual((double?)3, lookupDefault.Max(g_nDoubleProj));
                Assert.AreEqual((decimal)3, lookupDefault.Max(g_decimalProj));
                Assert.AreEqual((decimal?)3, lookupDefault.Max(g_nDecimalProj));
                Assert.AreEqual(dt, lookupDefault.Max(g_dtProj));
                Assert.AreEqual((DateTime?)dt, lookupDefault.Max(g_nDtProj));
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
                Func<GroupingEnumerable<int, int>, DateTime> g_dtProj = x => dt;
                Func<GroupingEnumerable<int, int>, DateTime?> g_nDtProj = x => (DateTime?)dt;

                // non-projection Max makes no sense
                var lookupSpecific = new[] { 1, 2, 3 }.ToLookup(x => x, new _IntComparer());

                Assert.AreEqual((int)3, lookupSpecific.Max(g_intProj));
                Assert.AreEqual((int?)3, lookupSpecific.Max(g_nIntProj));
                Assert.AreEqual((long)3, lookupSpecific.Max(g_longProj));
                Assert.AreEqual((long?)3, lookupSpecific.Max(g_nLongProj));
                Assert.AreEqual((float)3, lookupSpecific.Max(g_floatProj));
                Assert.AreEqual((float?)3, lookupSpecific.Max(g_nFloatProj));
                Assert.AreEqual((double)3, lookupSpecific.Max(g_doubleProj));
                Assert.AreEqual((double?)3, lookupSpecific.Max(g_nDoubleProj));
                Assert.AreEqual((decimal)3, lookupSpecific.Max(g_decimalProj));
                Assert.AreEqual((decimal?)3, lookupSpecific.Max(g_nDecimalProj));
                Assert.AreEqual(dt, lookupSpecific.Max(g_dtProj));
                Assert.AreEqual((DateTime?)dt, lookupSpecific.Max(g_nDtProj));
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
                Func<int, DateTime> i_dtProj = x => dt;
                Func<int, DateTime?> i_nDtProj = x => (DateTime?)dt;

                Assert.AreEqual(5, Enumerable.Range(1, 5).Max());
                // non-int no projection make no sense

                Assert.AreEqual(5, Enumerable.Range(1, 5).Max(i_intProj));
                Assert.AreEqual((int?)5, Enumerable.Range(1, 5).Max(i_nIntProj));
                Assert.AreEqual(5L, Enumerable.Range(1, 5).Max(i_longProj));
                Assert.AreEqual((long?)5, Enumerable.Range(1, 5).Max(i_nLongProj));
                Assert.AreEqual(5f, Enumerable.Range(1, 5).Max(i_floatProj));
                Assert.AreEqual((float?)5, Enumerable.Range(1, 5).Max(i_nFloatProj));
                Assert.AreEqual(5.0, Enumerable.Range(1, 5).Max(i_doubleProj));
                Assert.AreEqual((double?)5, Enumerable.Range(1, 5).Max(i_nDoubleProj));
                Assert.AreEqual(5m, Enumerable.Range(1, 5).Max(i_decimalProj));
                Assert.AreEqual((decimal?)5, Enumerable.Range(1, 5).Max(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Range(1, 5).Max(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Range(1, 5).Max(i_nDtProj));
            }

            // repeat
            {
                Assert.AreEqual((int)3, Enumerable.Repeat((int)3, 3).Max());
                Assert.AreEqual((int?)3, Enumerable.Repeat((int?)3, 3).Max());
                Assert.AreEqual((long)3, Enumerable.Repeat((long)3, 3).Max());
                Assert.AreEqual((long?)3, Enumerable.Repeat((long?)3, 3).Max());
                Assert.AreEqual((float)3, Enumerable.Repeat((float)3, 3).Max());
                Assert.AreEqual((float?)3, Enumerable.Repeat((float?)3, 3).Max());
                Assert.AreEqual((double)3, Enumerable.Repeat((double)3, 3).Max());
                Assert.AreEqual((double?)3, Enumerable.Repeat((double?)3, 3).Max());
                Assert.AreEqual((decimal)3, Enumerable.Repeat((decimal)3, 3).Max());
                Assert.AreEqual((decimal?)3, Enumerable.Repeat((decimal?)3, 3).Max());
                Assert.AreEqual(dt, Enumerable.Repeat(dt, 3).Max());
                Assert.AreEqual((DateTime?)dt, Enumerable.Repeat((DateTime?)dt, 3).Max());

                Assert.AreEqual((int)3, Enumerable.Repeat("foo", 3).Max(intProj));
                Assert.AreEqual((int?)3, Enumerable.Repeat("foo", 3).Max(nIntProj));
                Assert.AreEqual((long)3, Enumerable.Repeat("foo", 3).Max(longProj));
                Assert.AreEqual((long?)3, Enumerable.Repeat("foo", 3).Max(nLongProj));
                Assert.AreEqual((float)3, Enumerable.Repeat("foo", 3).Max(floatProj));
                Assert.AreEqual((float?)3, Enumerable.Repeat("foo", 3).Max(nFloatProj));
                Assert.AreEqual((double)3, Enumerable.Repeat("foo", 3).Max(doubleProj));
                Assert.AreEqual((double?)3, Enumerable.Repeat("foo", 3).Max(nDoubleProj));
                Assert.AreEqual((decimal)3, Enumerable.Repeat("foo", 3).Max(decimalProj));
                Assert.AreEqual((decimal?)3, Enumerable.Repeat("foo", 3).Max(nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Repeat("foo", 3).Max(dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Repeat("foo", 3).Max(nDtProj));
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
                Func<int, DateTime> i_dtProj = x => dt;
                Func<int, DateTime?> i_nDtProj = x => (DateTime?)dt;

                Assert.AreEqual(5, Enumerable.Range(1, 5).Reverse().Max());
                // non-int no projection make no sense

                Assert.AreEqual(5, Enumerable.Range(1, 5).Reverse().Max(i_intProj));
                Assert.AreEqual((int?)5, Enumerable.Range(1, 5).Reverse().Max(i_nIntProj));
                Assert.AreEqual(5L, Enumerable.Range(1, 5).Reverse().Max(i_longProj));
                Assert.AreEqual((long?)5, Enumerable.Range(1, 5).Reverse().Max(i_nLongProj));
                Assert.AreEqual(5f, Enumerable.Range(1, 5).Reverse().Max(i_floatProj));
                Assert.AreEqual((float?)5, Enumerable.Range(1, 5).Reverse().Max(i_nFloatProj));
                Assert.AreEqual(5.0, Enumerable.Range(1, 5).Reverse().Max(i_doubleProj));
                Assert.AreEqual((double?)5, Enumerable.Range(1, 5).Reverse().Max(i_nDoubleProj));
                Assert.AreEqual(5m, Enumerable.Range(1, 5).Reverse().Max(i_decimalProj));
                Assert.AreEqual((decimal?)5, Enumerable.Range(1, 5).Reverse().Max(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Range(1, 5).Reverse().Max(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Range(1, 5).Reverse().Max(i_nDtProj));
            }

            // oneItemDefault
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
                Func<int, DateTime> i_dtProj = x => dt;
                Func<int, DateTime?> i_nDtProj = x => (DateTime?)dt;

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max());
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().DefaultIfEmpty().Max());
                Assert.AreEqual(0, Enumerable.Empty<long>().DefaultIfEmpty().Max());
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().DefaultIfEmpty().Max());
                Assert.AreEqual(0, Enumerable.Empty<float>().DefaultIfEmpty().Max());
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().DefaultIfEmpty().Max());
                Assert.AreEqual(0, Enumerable.Empty<double>().DefaultIfEmpty().Max());
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().DefaultIfEmpty().Max());
                Assert.AreEqual(0, Enumerable.Empty<decimal>().DefaultIfEmpty().Max());
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().DefaultIfEmpty().Max());

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_intProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_nIntProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_longProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_nLongProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_floatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_nFloatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_doubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_nDoubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_decimalProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty().Max(i_nDtProj));
            }
            
            // oneItemSpecific
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
                Func<int, DateTime> i_dtProj = x => dt;
                Func<int, DateTime?> i_nDtProj = x => (DateTime?)dt;

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<long>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<long?>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<float>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<float?>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<double>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<double?>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<decimal>().DefaultIfEmpty(4).Max());
                Assert.AreEqual(4, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).Max());

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_intProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_nIntProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_longProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_nLongProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_floatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_nFloatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_doubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_nDoubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_decimalProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty(4).Max(i_nDtProj));
            }

            // oneItemDefaultOrdered
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
                Func<int, DateTime> i_dtProj = x => dt;
                Func<int, DateTime?> i_nDtProj = x => (DateTime?)dt;

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(0, Enumerable.Empty<long>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(0, Enumerable.Empty<float>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(0, Enumerable.Empty<double>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(0, Enumerable.Empty<decimal>().DefaultIfEmpty().OrderBy(x => x).Max());
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().DefaultIfEmpty().OrderBy(x => x).Max());

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_intProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_nIntProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_longProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_nLongProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_floatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_nFloatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_doubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_nDoubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_decimalProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Max(i_nDtProj));
            }

            // oneItemSpecificOrdered
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
                Func<int, DateTime> i_dtProj = x => dt;
                Func<int, DateTime?> i_nDtProj = x => (DateTime?)dt;

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<long>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<long?>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<float>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<float?>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<double>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<double?>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<decimal>().DefaultIfEmpty(4).OrderBy(x => x).Max());
                Assert.AreEqual(4, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).OrderBy(x => x).Max());

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_intProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_nIntProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_longProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_nLongProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_floatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_nFloatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_doubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_nDoubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_decimalProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Max(i_nDtProj));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // projections
            {
                // int
                {
                    Func<string, int> foo = null;
                    Func<string, int?> bar = null;
                    foreach (var e in Helper.GetEnumerables(new string[0]))
                    {
                        try
                        {
                            e.Max(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }

                        try
                        {
                            e.Max(bar);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }
                    }
                }

                // long
                {
                    Func<string, long> foo = null;
                    Func<string, long?> bar = null;
                    foreach (var e in Helper.GetEnumerables(new string[0]))
                    {
                        try
                        {
                            e.Max(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }

                        try
                        {
                            e.Max(bar);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }
                    }
                }

                // float
                {
                    Func<string, float> foo = null;
                    Func<string, float?> bar = null;
                    foreach (var e in Helper.GetEnumerables(new string[0]))
                    {
                        try
                        {
                            e.Max(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }

                        try
                        {
                            e.Max(bar);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }
                    }
                }

                // double
                {
                    Func<string, double> foo = null;
                    Func<string, double?> bar = null;
                    foreach (var e in Helper.GetEnumerables(new string[0]))
                    {
                        try
                        {
                            e.Max(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }

                        try
                        {
                            e.Max(bar);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }
                    }
                }

                // decimal
                {
                    Func<string, decimal> foo = null;
                    Func<string, decimal?> bar = null;
                    foreach (var e in Helper.GetEnumerables(new string[0]))
                    {
                        try
                        {
                            e.Max(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }

                        try
                        {
                            e.Max(bar);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }
                    }
                }

                // comparable
                {
                    Func<string, Guid> foo = null;
                    Func<string, Guid?> bar = null;
                    foreach (var e in Helper.GetEnumerables(new string[0]))
                    {
                        try
                        {
                            e.Max(foo);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }

                        try
                        {
                            e.Max(bar);
                            Assert.Fail();
                        }
                        catch (ArgumentNullException exc)
                        {
                            Assert.AreEqual("selector", exc.ParamName);
                        }
                    }
                }
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
                try { empty.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Max(default(Func<GroupingEnumerable<int, int>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Max(default(Func<GroupingEnumerable<string, string>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Max(default(Func<GroupingEnumerable<int, int>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Max(default(Func<GroupingEnumerable<int, int>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // range
            {
                try { range.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Max(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Max(default(Func<string, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Max(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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

            // float?
            {
                Helper.ForEachMalformedEnumerableExpression<float?>(
                    @"a =>
                      {
                        try
                        {
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                            a.Max();
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
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
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, int> f1 = str => str.Length;
                        Func<string, int?> f2 = str => str.Length;
                        Func<string, long> f3 = str => str.Length;
                        Func<string, long?> f4 = str => str.Length;
                        Func<string, float> f5 = str => str.Length;
                        Func<string, float?> f6 = str => str.Length;
                        Func<string, double> f7 = str => str.Length;
                        Func<string, double?> f8 = str => str.Length;
                        Func<string, decimal> f9 = str => str.Length;
                        Func<string, decimal?> f10 = str => str.Length;
                        Func<string, Guid> f11 = str => Guid.NewGuid();
                        try{ a.Max(f1); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f2); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f3); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f4); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f5); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f6); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f7); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f8); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f9); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f10); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Max(f11); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                      }",
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
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookupDefault = new LookupDefaultEnumerable<int, int>();
            var lookupSpecific = new LookupSpecificEnumerable<int, int>();
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
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
                try { (new EmptyEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { empty.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { (new EmptyOrderedEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { emptyOrdered.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                // non-projection Max makes no sense

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

                try { groupByDefault.Max(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Max(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                // non-projection Max makes no sense

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

                try { groupBySpecific.Max(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Max(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                // non-projection Max makes no sense

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

                try { lookupDefault.Max(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Max(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                // non-projection Max makes no sense

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

                try { lookupSpecific.Max(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Max(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { (new RangeEnumerable()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { range.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { (new RepeatEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { repeat.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { (new ReverseRangeEnumerable()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { reverseRange.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { (new OneItemDefaultEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefault.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { (new OneItemSpecificEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecific.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { (new OneItemDefaultOrderedEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefaultOrdered.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { (new OneItemSpecificOrderedEnumerable<int>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<int?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<DateTime>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<DateTime?>()).Max(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecificOrdered.Max(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Max(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            // int
            {
                var e1 = new int[] { 1, 3, 2 };
                var res1 = e1.Max();
                Assert.AreEqual(3, res1);

                var e2 = new int?[] { 1, null, 3, 2 };
                var res2 = e2.Max();
                Assert.AreEqual(3, res2);

                var e3 = new string[] { "a", "bb", "ccc", "dd", "e" };
                var res3 = e3.Max(s => s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "a", "bb", null, "ccc", null, "dd", "e" };
                var res4 = e4.Max(s => s?.Length);
                Assert.AreEqual(3, res4);
            }

            // long
            {
                var e1 = new long[] { 1, 3, 2 };
                var res1 = e1.Max();
                Assert.AreEqual(3, res1);

                var e2 = new long?[] { 1, null, 3, 2 };
                var res2 = e2.Max();
                Assert.AreEqual(3, res2);

                var e3 = new string[] { "a", "bb", "ccc", "dd", "e" };
                var res3 = e3.Max(s => (long)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "a", "bb", null, "ccc", null, "dd", "e" };
                var res4 = e4.Max(s => (long?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // float
            {
                var e1 = new float[] { 1, 3, 2 };
                var res1 = e1.Max();
                Assert.AreEqual(3, res1);

                var e2 = new float?[] { 1, null, 3, 2 };
                var res2 = e2.Max();
                Assert.AreEqual(3, res2);

                var e3 = new string[] { "a", "bb", "ccc", "dd", "e" };
                var res3 = e3.Max(s => (float)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "a", "bb", null, "ccc", null, "dd", "e" };
                var res4 = e4.Max(s => (float?)s?.Length);
                Assert.AreEqual(3, res4);
            }
            
            // double
            {
                var e1 = new double[] { 1, 3, 2 };
                var res1 = e1.Max();
                Assert.AreEqual(3, res1);

                var e2 = new double?[] { 1, null, 3, 2 };
                var res2 = e2.Max();
                Assert.AreEqual(3, res2);

                var e3 = new string[] { "a", "bb", "ccc", "dd", "e" };
                var res3 = e3.Max(s => (double)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "a", "bb", null, "ccc", null, "dd", "e" };
                var res4 = e4.Max(s => (double?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // decimal
            {
                var e1 = new decimal[] { 1, 3, 2 };
                var res1 = e1.Max();
                Assert.AreEqual(3, res1);

                var e2 = new decimal?[] { 1, null, 3, 2 };
                var res2 = e2.Max();
                Assert.AreEqual(3, res2);

                var e3 = new string[] { "a", "bb", "ccc", "dd", "e" };
                var res3 = e3.Max(s => (decimal)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "a", "bb", null, "ccc", null, "dd", "e" };
                var res4 = e4.Max(s => (decimal?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // comparable
            {
                var e1 = new string[] { "easy", "as", "123" };
                var res1 = e1.Max();
                Assert.AreEqual("easy", res1);
            }

            // projected comparable
            {
                var e1 = new string[] { "C82232F4-2C7D-4528-931A-327B266DD1D7", "98930954-F362-430F-ACBE-3FDDA082F3B9" };
                var res1 = e1.Max(s => Guid.Parse(s));
                Assert.AreEqual(Guid.Parse("C82232F4-2C7D-4528-931A-327B266DD1D7"), res1);
            }
        }
    }
}
