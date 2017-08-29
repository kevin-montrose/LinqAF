using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class MinTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IMin<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IMin ({string.Join(", ", missing)})");
                }
            }
        }

        public static string _Chaining_Rev(string str)
        {
            var cs = str.ToCharArray();
            Array.Reverse(cs);

            return new string(cs);
        }

        [TestMethod]
        public void Chaining()
        {
            // int
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 3, 1, 2 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new int?[] { 3, 1 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo" },
                    "a => Assert.AreEqual(3, a.Min(s => (int)s.Length))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    "a => Assert.AreEqual(4, a.Min(s => s.Length != 3 ? (int?)s.Length : null))",
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
                Helper.ForEachEnumerableNoRetExpression(
                    new long[] { 3, 1, 2 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new long?[] { 3, 1 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo" },
                    "a => Assert.AreEqual(3, a.Min(s => (long)s.Length))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    "a => Assert.AreEqual(4, a.Min(s => s.Length != 3 ? (long?)s.Length : null))",
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
                Helper.ForEachEnumerableNoRetExpression(
                    new float[] { 3, 1, 2 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new float?[] { 3, 1 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo" },
                    "a => Assert.AreEqual(3, a.Min(s => (float)s.Length))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    "a => Assert.AreEqual(4, a.Min(s => s.Length != 3 ? (float?)s.Length : null))",
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
                Helper.ForEachEnumerableNoRetExpression(
                    new double[] { 3, 1, 2 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new double?[] { 3, 1 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo" },
                    "a => Assert.AreEqual(3, a.Min(s => (double)s.Length))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    "a => Assert.AreEqual(4, a.Min(s => s.Length != 3 ? (double?)s.Length : null))",
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
                Helper.ForEachEnumerableNoRetExpression(
                    new decimal[] { 3, 1, 2 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new decimal?[] { 3, 1 },
                    "a => Assert.AreEqual(1, a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo" },
                    "a => Assert.AreEqual(3, a.Min(s => (decimal)s.Length))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    "a => Assert.AreEqual(4, a.Min(s => s.Length != 3 ? (decimal?)s.Length : null))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // basic comparer
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "def", "abc", "123" },
                    @"a => Assert.AreEqual(""123"", a.Min())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // projected comparer
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "def", "abc", "123" },
                    @"a => Assert.AreEqual(""321"", a.Min((Func<string, string>)MinTests._Chaining_Rev))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
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
                try { Enumerable.Empty<int>().Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().Min());
                try { Enumerable.Empty<long>().Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().Min());
                try { Enumerable.Empty<float>().Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().Min());
                try { Enumerable.Empty<double>().Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().Min());
                try { Enumerable.Empty<decimal>().Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().Min());
                try { Enumerable.Empty<DateTime>().Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<DateTime?>().Min());

                try { Enumerable.Empty<string>().Min(intProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().Min(nIntProj));
                try { Enumerable.Empty<string>().Min(longProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().Min(nLongProj));
                try { Enumerable.Empty<string>().Min(floatProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().Min(nFloatProj));
                try { Enumerable.Empty<string>().Min(doubleProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().Min(nDoubleProj));
                try { Enumerable.Empty<string>().Min(decimalProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().Min(nDecimalProj));
                try { Enumerable.Empty<string>().Min(dtProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<string>().Min(nDtProj));
            }

            // emptyOrdered
            {
                try { Enumerable.Empty<int>().OrderBy(x => x).Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().OrderBy(x => x).Min());
                try { Enumerable.Empty<long>().OrderBy(x => x).Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().OrderBy(x => x).Min());
                try { Enumerable.Empty<float>().OrderBy(x => x).Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().OrderBy(x => x).Min());
                try { Enumerable.Empty<double>().OrderBy(x => x).Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().OrderBy(x => x).Min());
                try { Enumerable.Empty<decimal>().OrderBy(x => x).Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().OrderBy(x => x).Min());
                try { Enumerable.Empty<DateTime>().OrderBy(x => x).Min(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<DateTime?>().OrderBy(x => x).Min());

                try { Enumerable.Empty<string>().OrderBy(x => x).Min(intProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(int?), Enumerable.Empty<string>().OrderBy(x => x).Min(nIntProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Min(longProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(long?), Enumerable.Empty<string>().OrderBy(x => x).Min(nLongProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Min(floatProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(float?), Enumerable.Empty<string>().OrderBy(x => x).Min(nFloatProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Min(doubleProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(double?), Enumerable.Empty<string>().OrderBy(x => x).Min(nDoubleProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Min(decimalProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(decimal?), Enumerable.Empty<string>().OrderBy(x => x).Min(nDecimalProj));
                try { Enumerable.Empty<string>().OrderBy(x => x).Min(dtProj); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(default(DateTime?), Enumerable.Empty<string>().OrderBy(x => x).Min(nDtProj));
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

                Assert.AreEqual((int)1, groupByDefault.Min(g_intProj));
                Assert.AreEqual((int?)1, groupByDefault.Min(g_nIntProj));
                Assert.AreEqual((long)1, groupByDefault.Min(g_longProj));
                Assert.AreEqual((long?)1, groupByDefault.Min(g_nLongProj));
                Assert.AreEqual((float)1, groupByDefault.Min(g_floatProj));
                Assert.AreEqual((float?)1, groupByDefault.Min(g_nFloatProj));
                Assert.AreEqual((double)1, groupByDefault.Min(g_doubleProj));
                Assert.AreEqual((double?)1, groupByDefault.Min(g_nDoubleProj));
                Assert.AreEqual((decimal)1, groupByDefault.Min(g_decimalProj));
                Assert.AreEqual((decimal?)1, groupByDefault.Min(g_nDecimalProj));
                Assert.AreEqual(dt, groupByDefault.Min(g_dtProj));
                Assert.AreEqual((DateTime?)dt, groupByDefault.Min(g_nDtProj));
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

                Assert.AreEqual((int)1, groupBySpecific.Min(g_intProj));
                Assert.AreEqual((int?)1, groupBySpecific.Min(g_nIntProj));
                Assert.AreEqual((long)1, groupBySpecific.Min(g_longProj));
                Assert.AreEqual((long?)1, groupBySpecific.Min(g_nLongProj));
                Assert.AreEqual((float)1, groupBySpecific.Min(g_floatProj));
                Assert.AreEqual((float?)1, groupBySpecific.Min(g_nFloatProj));
                Assert.AreEqual((double)1, groupBySpecific.Min(g_doubleProj));
                Assert.AreEqual((double?)1, groupBySpecific.Min(g_nDoubleProj));
                Assert.AreEqual((decimal)1, groupBySpecific.Min(g_decimalProj));
                Assert.AreEqual((decimal?)1, groupBySpecific.Min(g_nDecimalProj));
                Assert.AreEqual(dt, groupBySpecific.Min(g_dtProj));
                Assert.AreEqual((DateTime?)dt, groupBySpecific.Min(g_nDtProj));
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

                Assert.AreEqual((int)1, lookupDefault.Min(g_intProj));
                Assert.AreEqual((int?)1, lookupDefault.Min(g_nIntProj));
                Assert.AreEqual((long)1, lookupDefault.Min(g_longProj));
                Assert.AreEqual((long?)1, lookupDefault.Min(g_nLongProj));
                Assert.AreEqual((float)1, lookupDefault.Min(g_floatProj));
                Assert.AreEqual((float?)1, lookupDefault.Min(g_nFloatProj));
                Assert.AreEqual((double)1, lookupDefault.Min(g_doubleProj));
                Assert.AreEqual((double?)1, lookupDefault.Min(g_nDoubleProj));
                Assert.AreEqual((decimal)1, lookupDefault.Min(g_decimalProj));
                Assert.AreEqual((decimal?)1, lookupDefault.Min(g_nDecimalProj));
                Assert.AreEqual(dt, lookupDefault.Min(g_dtProj));
                Assert.AreEqual((DateTime?)dt, lookupDefault.Min(g_nDtProj));
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

                Assert.AreEqual((int)1, lookupSpecific.Min(g_intProj));
                Assert.AreEqual((int?)1, lookupSpecific.Min(g_nIntProj));
                Assert.AreEqual((long)1, lookupSpecific.Min(g_longProj));
                Assert.AreEqual((long?)1, lookupSpecific.Min(g_nLongProj));
                Assert.AreEqual((float)1, lookupSpecific.Min(g_floatProj));
                Assert.AreEqual((float?)1, lookupSpecific.Min(g_nFloatProj));
                Assert.AreEqual((double)1, lookupSpecific.Min(g_doubleProj));
                Assert.AreEqual((double?)1, lookupSpecific.Min(g_nDoubleProj));
                Assert.AreEqual((decimal)1, lookupSpecific.Min(g_decimalProj));
                Assert.AreEqual((decimal?)1, lookupSpecific.Min(g_nDecimalProj));
                Assert.AreEqual(dt, lookupSpecific.Min(g_dtProj));
                Assert.AreEqual((DateTime?)dt, lookupSpecific.Min(g_nDtProj));
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

                Assert.AreEqual(1, Enumerable.Range(1, 5).Min());
                // non-int no projection make no sense

                Assert.AreEqual(1, Enumerable.Range(1, 5).Min(i_intProj));
                Assert.AreEqual((int?)1, Enumerable.Range(1, 5).Min(i_nIntProj));
                Assert.AreEqual(1L, Enumerable.Range(1, 5).Min(i_longProj));
                Assert.AreEqual((long?)1, Enumerable.Range(1, 5).Min(i_nLongProj));
                Assert.AreEqual(1f, Enumerable.Range(1, 5).Min(i_floatProj));
                Assert.AreEqual((float?)1, Enumerable.Range(1, 5).Min(i_nFloatProj));
                Assert.AreEqual(1.0, Enumerable.Range(1, 5).Min(i_doubleProj));
                Assert.AreEqual((double?)1, Enumerable.Range(1, 5).Min(i_nDoubleProj));
                Assert.AreEqual(1m, Enumerable.Range(1, 5).Min(i_decimalProj));
                Assert.AreEqual((decimal?)1, Enumerable.Range(1, 5).Min(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Range(1, 5).Min(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Range(1, 5).Min(i_nDtProj));
            }

            // repeat
            {
                Assert.AreEqual((int)3, Enumerable.Repeat((int)3, 3).Min());
                Assert.AreEqual((int?)3, Enumerable.Repeat((int?)3, 3).Min());
                Assert.AreEqual((long)3, Enumerable.Repeat((long)3, 3).Min());
                Assert.AreEqual((long?)3, Enumerable.Repeat((long?)3, 3).Min());
                Assert.AreEqual((float)3, Enumerable.Repeat((float)3, 3).Min());
                Assert.AreEqual((float?)3, Enumerable.Repeat((float?)3, 3).Min());
                Assert.AreEqual((double)3, Enumerable.Repeat((double)3, 3).Min());
                Assert.AreEqual((double?)3, Enumerable.Repeat((double?)3, 3).Min());
                Assert.AreEqual((decimal)3, Enumerable.Repeat((decimal)3, 3).Min());
                Assert.AreEqual((decimal?)3, Enumerable.Repeat((decimal?)3, 3).Min());
                Assert.AreEqual(dt, Enumerable.Repeat(dt, 3).Min());
                Assert.AreEqual((DateTime?)dt, Enumerable.Repeat((DateTime?)dt, 3).Min());

                Assert.AreEqual((int)3, Enumerable.Repeat("foo", 3).Min(intProj));
                Assert.AreEqual((int?)3, Enumerable.Repeat("foo", 3).Min(nIntProj));
                Assert.AreEqual((long)3, Enumerable.Repeat("foo", 3).Min(longProj));
                Assert.AreEqual((long?)3, Enumerable.Repeat("foo", 3).Min(nLongProj));
                Assert.AreEqual((float)3, Enumerable.Repeat("foo", 3).Min(floatProj));
                Assert.AreEqual((float?)3, Enumerable.Repeat("foo", 3).Min(nFloatProj));
                Assert.AreEqual((double)3, Enumerable.Repeat("foo", 3).Min(doubleProj));
                Assert.AreEqual((double?)3, Enumerable.Repeat("foo", 3).Min(nDoubleProj));
                Assert.AreEqual((decimal)3, Enumerable.Repeat("foo", 3).Min(decimalProj));
                Assert.AreEqual((decimal?)3, Enumerable.Repeat("foo", 3).Min(nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Repeat("foo", 3).Min(dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Repeat("foo", 3).Min(nDtProj));
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

                Assert.AreEqual(1, Enumerable.Range(1, 5).Reverse().Min());
                // non-int no projection make no sense

                Assert.AreEqual(1, Enumerable.Range(1, 5).Reverse().Min(i_intProj));
                Assert.AreEqual((int?)1, Enumerable.Range(1, 5).Reverse().Min(i_nIntProj));
                Assert.AreEqual(1L, Enumerable.Range(1, 5).Reverse().Min(i_longProj));
                Assert.AreEqual((long?)1, Enumerable.Range(1, 5).Reverse().Min(i_nLongProj));
                Assert.AreEqual(1f, Enumerable.Range(1, 5).Reverse().Min(i_floatProj));
                Assert.AreEqual((float?)1, Enumerable.Range(1, 5).Reverse().Min(i_nFloatProj));
                Assert.AreEqual(1.0, Enumerable.Range(1, 5).Reverse().Min(i_doubleProj));
                Assert.AreEqual((double?)1, Enumerable.Range(1, 5).Reverse().Min(i_nDoubleProj));
                Assert.AreEqual(1m, Enumerable.Range(1, 5).Reverse().Min(i_decimalProj));
                Assert.AreEqual((decimal?)1, Enumerable.Range(1, 5).Reverse().Min(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Range(1, 5).Reverse().Min(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Range(1, 5).Reverse().Min(i_nDtProj));
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

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min());
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().DefaultIfEmpty().Min());
                Assert.AreEqual(0, Enumerable.Empty<long>().DefaultIfEmpty().Min());
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().DefaultIfEmpty().Min());
                Assert.AreEqual(0, Enumerable.Empty<float>().DefaultIfEmpty().Min());
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().DefaultIfEmpty().Min());
                Assert.AreEqual(0, Enumerable.Empty<double>().DefaultIfEmpty().Min());
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().DefaultIfEmpty().Min());
                Assert.AreEqual(0, Enumerable.Empty<decimal>().DefaultIfEmpty().Min());
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().DefaultIfEmpty().Min());

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_intProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_nIntProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_longProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_nLongProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_floatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_nFloatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_doubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_nDoubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_decimalProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty().Min(i_nDtProj));
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

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<long>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<long?>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<float>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<float?>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<double>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<double?>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<decimal>().DefaultIfEmpty(4).Min());
                Assert.AreEqual(4, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).Min());

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_intProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_nIntProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_longProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_nLongProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_floatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_nFloatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_doubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_nDoubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_decimalProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty(4).Min(i_nDtProj));
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

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(default(int?), Enumerable.Empty<int?>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(0, Enumerable.Empty<long>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(default(long?), Enumerable.Empty<long?>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(0, Enumerable.Empty<float>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(default(float?), Enumerable.Empty<float?>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(0, Enumerable.Empty<double>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(default(double?), Enumerable.Empty<double?>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(0, Enumerable.Empty<decimal>().DefaultIfEmpty().OrderBy(x => x).Min());
                Assert.AreEqual(default(decimal?), Enumerable.Empty<decimal?>().DefaultIfEmpty().OrderBy(x => x).Min());

                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_intProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_nIntProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_longProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_nLongProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_floatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_nFloatProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_doubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_nDoubleProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_decimalProj));
                Assert.AreEqual(0, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Min(i_nDtProj));
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

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<int?>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<long>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<long?>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<float>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<float?>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<double>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<double?>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<decimal>().DefaultIfEmpty(4).OrderBy(x => x).Min());
                Assert.AreEqual(4, Enumerable.Empty<decimal?>().DefaultIfEmpty(4).OrderBy(x => x).Min());

                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_intProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_nIntProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_longProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_nLongProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_floatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_nFloatProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_doubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_nDoubleProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_decimalProj));
                Assert.AreEqual(4, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_nDecimalProj));
                Assert.AreEqual(dt, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_dtProj));
                Assert.AreEqual((DateTime?)dt, Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Min(i_nDtProj));
            }
        }


        [TestMethod]
        public void Errors()
        {
            // int
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // long
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // float
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // double
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // decimal
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // projected
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, Guid>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.Min(default(Func<string, Guid?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
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
                try { empty.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Min(default(Func<GroupingEnumerable<int, int>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Min(default(Func<GroupingEnumerable<string, string>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupDefault.Min(default(Func<GroupingEnumerable<int, int>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookupSpecific.Min(default(Func<GroupingEnumerable<int, int>, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // range
            {
                try { range.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Min(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Min(default(Func<string, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Min(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, int?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, long>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, long?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, float>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, float?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, double?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, decimal>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, decimal?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, DateTime>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(default(Func<int, DateTime?>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                            a.Min();
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
                        try{ a.Min(f1); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f2); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f3); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f4); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f5); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f6); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f7); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f8); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f9); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f10); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
                        try{ a.Min(f11); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""source"", exc.ParamName); }
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
                try { (new EmptyEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { empty.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { (new EmptyOrderedEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new EmptyOrderedEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { emptyOrdered.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
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

                try { groupByDefault.Min(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Min(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
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

                try { groupBySpecific.Min(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Min(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
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

                try { lookupDefault.Min(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Min(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
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

                try { lookupSpecific.Min(g_intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Min(g_nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { (new RangeEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RangeEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { range.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { (new RepeatEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new RepeatEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { repeat.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { (new ReverseRangeEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new ReverseRangeEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { reverseRange.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { (new OneItemDefaultEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefault.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { (new OneItemSpecificEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecific.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { (new OneItemDefaultOrderedEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemDefaultOrderedEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemDefaultOrdered.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { (new OneItemSpecificOrderedEnumerable<int>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<int?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<long?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<float?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<double?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<decimal?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<DateTime>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { (new OneItemSpecificOrderedEnumerable<DateTime?>()).Min(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }

                try { oneItemSpecificOrdered.Min(intProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(nIntProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(longProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(nLongProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(floatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(nFloatProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(doubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(nDoubleProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(decimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(nDecimalProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(dtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Min(nDtProj); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            // int
            {
                var e1 = new int[] { 3, 2, 1 };
                var res1 = e1.Min();
                Assert.AreEqual(1, res1);

                var e2 = new int?[] { 5, 4, null };
                var res2 = e2.Min();
                Assert.AreEqual(4, res2);

                var e3 = new string[] { "hello", "foo" };
                var res3 = e3.Min(s => (int)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "hello", null, "foo" };
                var res4 = e4.Min(s => (int?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // long
            {
                var e1 = new long[] { 3, 2, 1 };
                var res1 = e1.Min();
                Assert.AreEqual(1, res1);

                var e2 = new long?[] { 5, 4, null };
                var res2 = e2.Min();
                Assert.AreEqual(4, res2);

                var e3 = new string[] { "hello", "foo" };
                var res3 = e3.Min(s => (long)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "hello", null, "foo" };
                var res4 = e4.Min(s => (long?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // float
            {
                var e1 = new float[] { 3, 2, 1 };
                var res1 = e1.Min();
                Assert.AreEqual(1, res1);

                var e2 = new float?[] { 5, 4, null };
                var res2 = e2.Min();
                Assert.AreEqual(4, res2);

                var e3 = new string[] { "hello", "foo" };
                var res3 = e3.Min(s => (float)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "hello", null, "foo" };
                var res4 = e4.Min(s => (float?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // double
            {
                var e1 = new double[] { 3, 2, 1 };
                var res1 = e1.Min();
                Assert.AreEqual(1, res1);

                var e2 = new double?[] { 5, 4, null };
                var res2 = e2.Min();
                Assert.AreEqual(4, res2);

                var e3 = new string[] { "hello", "foo" };
                var res3 = e3.Min(s => (double)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "hello", null, "foo" };
                var res4 = e4.Min(s => (double?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // decimal
            {
                var e1 = new decimal[] { 3, 2, 1 };
                var res1 = e1.Min();
                Assert.AreEqual(1, res1);

                var e2 = new decimal?[] { 5, 4, null };
                var res2 = e2.Min();
                Assert.AreEqual(4, res2);

                var e3 = new string[] { "hello", "foo" };
                var res3 = e3.Min(s => (decimal)s.Length);
                Assert.AreEqual(3, res3);

                var e4 = new string[] { "hello", null, "foo" };
                var res4 = e4.Min(s => (decimal?)s?.Length);
                Assert.AreEqual(3, res4);
            }

            // comparable
            {
                var e1 = new string[] { "foo", "bar" };
                var res1 = e1.Min();
                Assert.AreEqual("bar", res1);
            }

            // projected comparable
            {
                var e1 = new string[] { "C82232F4-2C7D-4528-931A-327B266DD1D7", "98930954-F362-430F-ACBE-3FDDA082F3B9" };
                var res1 = e1.Min(s => Guid.Parse(s));
                Assert.AreEqual(Guid.Parse("98930954-F362-430F-ACBE-3FDDA082F3B9"), res1);
            }
        }
    }
}
