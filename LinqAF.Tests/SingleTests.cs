using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class SingleTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ISingle<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ISingle ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 2 }))
            {
                Assert.AreEqual(2, e.Single());
                Assert.AreEqual(2, e.SingleOrDefault());
            }

            foreach (var e in Helper.GetEnumerables(new[] { 2, 3, 4 }))
            {
                Func<int, bool> d = v => v == 3;

                Assert.AreEqual(3, e.Single(d));
                Assert.AreEqual(3, e.SingleOrDefault(d));
            }

            foreach (var e in Helper.GetEnumerables(new int[0]))
            {
                Func<int, bool> d = v => v == 3;

                Assert.AreEqual(0, e.SingleOrDefault());
                Assert.AreEqual(0, e.SingleOrDefault(d));
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
                try { empty.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                try { empty.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(0, empty.SingleOrDefault());
                Assert.AreEqual(0, empty.SingleOrDefault(x => true));
            }

            // emptyOrdered
            {
                try { emptyOrdered.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                try { emptyOrdered.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(0, emptyOrdered.SingleOrDefault());
                Assert.AreEqual(0, emptyOrdered.SingleOrDefault(x => true));
            }

            // groupByDefault
            {
                try { groupByDefault.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { groupByDefault.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { groupByDefault.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { groupByDefault.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { groupBySpecific.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { groupBySpecific.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { groupBySpecific.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // lookupDefault
            {
                try { lookupDefault.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { lookupDefault.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { lookupDefault.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { lookupDefault.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { lookupSpecific.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { lookupSpecific.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { lookupSpecific.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // lookup
            {
                try { lookupDefault.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { lookupDefault.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { lookupDefault.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { lookupDefault.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // range
            {
                try { range.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { range.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { range.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { range.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // repeat
            {
                try { repeat.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { repeat.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { repeat.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { repeat.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // reverseRange
            {
                try { reverseRange.Single(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { reverseRange.Single(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
                try { reverseRange.SingleOrDefault(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple elements", exc.Message); }
                try { reverseRange.SingleOrDefault(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence contained multiple matching elements", exc.Message); }
            }

            // oneItemDefault
            {
                Assert.AreEqual(0, oneItemDefault.Single());
                Assert.AreEqual(0, oneItemDefault.Single(x => true));
                Assert.AreEqual(0, oneItemDefault.SingleOrDefault());
                Assert.AreEqual(0, oneItemDefault.SingleOrDefault(x => true));
            }

            // oneItemSpecific
            {
                Assert.AreEqual(4, oneItemSpecific.Single());
                Assert.AreEqual(4, oneItemSpecific.Single(x => true));
                Assert.AreEqual(4, oneItemSpecific.SingleOrDefault());
                Assert.AreEqual(4, oneItemSpecific.SingleOrDefault(x => true));
            }

            // oneItemDefaultOrdered
            {
                Assert.AreEqual(0, oneItemDefaultOrdered.Single());
                Assert.AreEqual(0, oneItemDefaultOrdered.Single(x => true));
                Assert.AreEqual(0, oneItemDefaultOrdered.SingleOrDefault());
                Assert.AreEqual(0, oneItemDefaultOrdered.SingleOrDefault(x => true));
            }

            // oneItemSpecificOrdered
            {
                Assert.AreEqual(4, oneItemSpecificOrdered.Single());
                Assert.AreEqual(4, oneItemSpecificOrdered.Single(x => true));
                Assert.AreEqual(4, oneItemSpecificOrdered.SingleOrDefault());
                Assert.AreEqual(4, oneItemSpecificOrdered.SingleOrDefault(x => true));
            }
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new object[] { new object() }))
            {
                Func<object, bool> d1 = null;
                try
                {
                    e.Single(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    e.SingleOrDefault(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }
            }

            foreach (var e in Helper.GetEnumerables(new int[0]))
            {
                var eGenType = Helper.GetGenericTypeDefinition(((object)e).GetType());
                if (eGenType == typeof(DefaultIfEmptySpecificEnumerable<,,>) || eGenType == typeof(DefaultIfEmptyDefaultEnumerable<,,>))
                {
                    continue;
                }

                Func<int, bool> d1 = _ => true;
                try
                {
                    e.Single(d1);
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("No items matched predicate", exc.Message);
                }

                try
                {
                    e.Single();
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("Sequence was empty", exc.Message);
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
                try { empty.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { empty.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { emptyOrdered.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Single(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.SingleOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Single(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.SingleOrDefault(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Single(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookupDefault.SingleOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Single(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookupSpecific.SingleOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Single(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.SingleOrDefault(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Single(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.SingleOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }
        
        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<int>())
            {
                Func<int, bool> nope = x => x == 0;

                try
                {
                    e.Single(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.Single();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.SingleOrDefault(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.SingleOrDefault();
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
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { empty.Single(); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Single(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Single(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SingleOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SingleOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e1 = new[] { 1 };
            Assert.AreEqual(1, e1.Single());

            try
            {
                var e2 = new[] { 1, 2 };
                e2.Single();
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("Sequence contained multiple elements", x.Message);
            }
        }

        [TestMethod]
        public void Filtered()
        {
            var e = Enumerable.Range(5, 5);
            var ix = e.Single(i => i == 6);

            Assert.AreEqual(6, ix);

            try
            {
                e.Single(i => i % 2 == 0);
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("Sequence contained multiple matching elements", x.Message);
            }
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];
            try
            {
                var ix = e.Single();
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("Sequence was empty", x.Message);
            }

            try
            {
                var ix = e.Single(i => 5 % 2 == 0);
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("No items matched predicate", x.Message);
            }
        }

        [TestMethod]
        public void Default()
        {
            var e = Enumerable.Range(0, 5);
            var a = e.SingleOrDefault(ix => ix == 4);
            var b = e.SingleOrDefault(ix => ix == 10);

            Assert.AreEqual(4, a);
            Assert.AreEqual(default(int), b);

            try
            {
                new[] { 2, 2 }.SingleOrDefault(i => i == 2);
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("Sequence contained multiple matching elements", x.Message);
            }
        }
    }
}