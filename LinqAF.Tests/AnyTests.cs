using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class AnyTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IAny<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IAny ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 2, 4, 6 }))
            {
                Assert.IsTrue(e.Any());

                Func<int, bool> tFunc = a => a == 4;
                Func<int, bool> fFunc = a => a > 6;
                Assert.IsTrue(e.Any(tFunc));
                Assert.IsFalse(e.Any(fFunc));
            }

            foreach(var e in Helper.GetEnumerables(new int[0]))
            {
                object o = (object)e;
                var eType = Helper.GetGenericTypeDefinition(o.GetType());
                if (eType == typeof(DefaultIfEmptyDefaultEnumerable<,,>) || eType == typeof(DefaultIfEmptySpecificEnumerable<,,>))
                {
                    Assert.IsTrue(e.Any());
                }
                else
                {
                    Assert.IsFalse(e.Any());
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

            // simple
            {
                Assert.IsFalse(empty.Any());
                Assert.IsFalse(emptyOrdered.Any());
                Assert.IsTrue(groupByDefault.Any());
                Assert.IsTrue(groupBySpecific.Any());
                Assert.IsTrue(lookupDefault.Any());
                Assert.IsTrue(lookupSpecific.Any());
                Assert.IsTrue(range.Any());
                Assert.IsTrue(repeat.Any());
                Assert.IsTrue(reverseRange.Any());
                Assert.IsTrue(oneItemDefault.Any());
                Assert.IsTrue(oneItemSpecific.Any());
                Assert.IsTrue(oneItemDefaultOrdered.Any());
                Assert.IsTrue(oneItemSpecificOrdered.Any());
            }

            // predicate
            {
                Assert.IsFalse(empty.Any(x => true));
                Assert.IsFalse(emptyOrdered.Any(x => true));
                Assert.IsTrue(groupByDefault.Any(x => true));
                Assert.IsTrue(groupBySpecific.Any(x => true));
                Assert.IsTrue(lookupDefault.Any(x => true));
                Assert.IsTrue(lookupSpecific.Any(x => true));
                Assert.IsTrue(range.Any(x => true));
                Assert.IsTrue(repeat.Any(x => true));
                Assert.IsTrue(reverseRange.Any(x => true));
                Assert.IsTrue(oneItemDefault.Any(x => true));
                Assert.IsTrue(oneItemSpecific.Any(x => true));
                Assert.IsTrue(oneItemDefaultOrdered.Any(x => true));
                Assert.IsTrue(oneItemSpecificOrdered.Any(x => true));
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            Assert.IsTrue(dict.Any());
            Assert.IsTrue(dict.Any(kv => kv.Key == 3));
            Assert.IsTrue(sortedDict.Any());
            Assert.IsTrue(sortedDict.Any(kv => kv.Key == 3));
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new object[] { new object() }))
            {
                Func<object, bool> d1 = null;
                try
                {
                    e.Any(d1);
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
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // predicate
            {
                try
                {
                    empty.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    groupByDefault.Any(default(Func<GroupingEnumerable<int, int>, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Any(default(Func<GroupingEnumerable<string, string>, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    lookupDefault.Any(default(Func<GroupingEnumerable<int, int>, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Any(default(Func<GroupingEnumerable<int, int>, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    range.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    repeat.Any(default(Func<string, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    reverseRange.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Any(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }
            }
        }

        [TestMethod]
        public void Errors_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            try { dict.Any(null); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.Any(null); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("predicate", exc.ParamName); }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<int>())
            {
                Func<int, bool> nope = x => x == 0;

                try
                {
                    e.Any(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.Any();
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

            // simple
            {
                try
                {
                    empty.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupByDefault.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupDefault.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    range.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    repeat.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    reverseRange.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Any();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }

            // predicate
            {
                try
                {
                    empty.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupByDefault.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupDefault.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    range.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    repeat.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    reverseRange.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Any(x => true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }
        }


        [TestMethod]
        public void Simple()
        {
            var e = new[] { 2, 4 };

            Assert.IsTrue(e.All(i => i % 2 == 0));
            Assert.IsFalse(e.All(i => i % 2 == 1));
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];

            Assert.IsTrue(e.All(i => i == 0));
        }
    }
}
