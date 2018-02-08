using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinqAF;
using TestHelpers;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class CountTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.ICount<>), out instOverlaps, out extOverlaps);

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
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ICount<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ICount ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 2, 4, 6 }))
            {
                Assert.AreEqual(3, e.Count());

                Func<int, bool> f = i => i < 5;
                Assert.AreEqual(2, e.Count(f));

                Assert.AreEqual(3L, e.LongCount());
                Assert.AreEqual(2L, e.LongCount(f));
            }

            foreach (var e in Helper.GetEnumerables(new int[0]))
            {
                var expectedOnEmpty = 0;

                var eType = Helper.GetGenericTypeDefinition(((object)e).GetType());
                if(eType == typeof(DefaultIfEmptyDefaultEnumerable<,,>) || eType == typeof(DefaultIfEmptySpecificEnumerable<,,>))
                {
                    expectedOnEmpty = 1;
                }

                Assert.AreEqual(expectedOnEmpty, e.Count());

                Func<int, bool> f = i => i < 5;
                Assert.AreEqual(expectedOnEmpty, e.Count(f));

                Assert.AreEqual((long)expectedOnEmpty, e.LongCount());
                Assert.AreEqual((long)expectedOnEmpty, e.LongCount(f));
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
                Assert.AreEqual(0, empty.Count());
                Assert.AreEqual(0, empty.Count(x => true));
                Assert.AreEqual(0, empty.LongCount());
                Assert.AreEqual(0, empty.LongCount(x => true));
            }

            // emptyOrdered
            {
                Assert.AreEqual(0, emptyOrdered.Count());
                Assert.AreEqual(0, emptyOrdered.Count(x => true));
                Assert.AreEqual(0, emptyOrdered.LongCount());
                Assert.AreEqual(0, emptyOrdered.LongCount(x => true));
            }

            // groupByDefault
            {
                Assert.AreEqual(3, groupByDefault.Count());
                Assert.AreEqual(3, groupByDefault.Count(x => true));
                Assert.AreEqual(3, groupByDefault.LongCount());
                Assert.AreEqual(3, groupByDefault.LongCount(x => true));
            }

            // groupBySpecific
            {
                Assert.AreEqual(3, groupBySpecific.Count());
                Assert.AreEqual(3, groupBySpecific.Count(x => true));
                Assert.AreEqual(3, groupBySpecific.LongCount());
                Assert.AreEqual(3, groupBySpecific.LongCount(x => true));
            }

            // lookupDefault
            {
                Assert.AreEqual(3, lookupDefault.Count());
                Assert.AreEqual(3, lookupDefault.Count(x => true));
                Assert.AreEqual(3, lookupDefault.LongCount());
                Assert.AreEqual(3, lookupDefault.LongCount(x => true));
            }

            // lookupSpecific
            {
                Assert.AreEqual(3, lookupSpecific.Count());
                Assert.AreEqual(3, lookupSpecific.Count(x => true));
                Assert.AreEqual(3, lookupSpecific.LongCount());
                Assert.AreEqual(3, lookupSpecific.LongCount(x => true));
            }

            // range
            {
                Assert.AreEqual(5, range.Count());
                Assert.AreEqual(5, range.Count(x => true));
                Assert.AreEqual(5, range.LongCount());
                Assert.AreEqual(5, range.LongCount(x => true));
            }

            // repeat
            {
                Assert.AreEqual(5, repeat.Count());
                Assert.AreEqual(5, repeat.Count(x => true));
                Assert.AreEqual(5, repeat.LongCount());
                Assert.AreEqual(5, repeat.LongCount(x => true));
            }

            // reverseRange
            {
                Assert.AreEqual(5, reverseRange.Count());
                Assert.AreEqual(5, reverseRange.Count(x => true));
                Assert.AreEqual(5, reverseRange.LongCount());
                Assert.AreEqual(5, reverseRange.LongCount(x => true));
            }

            // oneItemDefault
            {
                Assert.AreEqual(1, oneItemDefault.Count());
                Assert.AreEqual(1, oneItemDefault.Count(x => true));
                Assert.AreEqual(1, oneItemDefault.LongCount());
                Assert.AreEqual(1, oneItemDefault.LongCount(x => true));
            }

            // oneItemSpecific
            {
                Assert.AreEqual(1, oneItemSpecific.Count());
                Assert.AreEqual(1, oneItemSpecific.Count(x => true));
                Assert.AreEqual(1, oneItemSpecific.LongCount());
                Assert.AreEqual(1, oneItemSpecific.LongCount(x => true));
            }

            // oneItemDefaultOrdered
            {
                Assert.AreEqual(1, oneItemDefaultOrdered.Count());
                Assert.AreEqual(1, oneItemDefaultOrdered.Count(x => true));
                Assert.AreEqual(1, oneItemDefaultOrdered.LongCount());
                Assert.AreEqual(1, oneItemDefaultOrdered.LongCount(x => true));
            }

            // oneItemSpecificOrdered
            {
                Assert.AreEqual(1, oneItemSpecificOrdered.Count());
                Assert.AreEqual(1, oneItemSpecificOrdered.Count(x => true));
                Assert.AreEqual(1, oneItemSpecificOrdered.LongCount());
                Assert.AreEqual(1, oneItemSpecificOrdered.LongCount(x => true));
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            Assert.AreEqual(2, dict.Count());
            Assert.AreEqual(2, dict.Count(x => x.Key < 4));
            Assert.AreEqual(2, dict.LongCount());
            Assert.AreEqual(2, dict.LongCount(x => x.Key < 4));
            Assert.AreEqual(2, sortedDict.Count());
            Assert.AreEqual(2, sortedDict.Count(x => x.Key < 4));
            Assert.AreEqual(2, sortedDict.LongCount());
            Assert.AreEqual(2, sortedDict.LongCount(x => x.Key < 4));
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new object[] { new object() }))
            {
                Func<object, bool> d1 = null;
                try
                {
                    e.Count(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    e.LongCount(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
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
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                try { empty.Count(default(Func<int, bool>)); Assert.Fail(); }catch(ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { empty.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // empty
            {
                try { emptyOrdered.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { emptyOrdered.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Count(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.LongCount(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Count(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.LongCount(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Count(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookup.LongCount(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Count(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.LongCount(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Count(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.LongCount(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Errors_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            try { dict.Count(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { dict.LongCount(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.Count(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.LongCount(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<int>())
            {
                Func<int, bool> nope = x => x == 0;

                try
                {
                    e.Count(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.Count();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.LongCount(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.LongCount();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
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
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { empty.Count(); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            { 
                try { lookupDefault.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Count(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Count(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.LongCount(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.LongCount(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3 };
            Assert.AreEqual(3, e.Count());
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];
            Assert.AreEqual(0, e.Count());
        }
    }
}
