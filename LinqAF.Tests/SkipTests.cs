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
    public class SkipTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ISkip<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ISkip ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            // skip, count
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual(2, res[0]);
                        Assert.AreEqual(3, res[1]);
                    },
                    "(_, a) => a.Skip(1)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // skip while, simple
            {
                Helper.ForEachEnumerableExpression(
                   new object[0],
                   new[] { 1, 2, 3 },
                   res =>
                   {
                       Assert.AreEqual(1, res.Count);
                       Assert.AreEqual(3, res[0]);
                   },
                   "(_, a) => a.SkipWhile(f => f < 3)",
                   typeof(EmptyEnumerable<>),
                   typeof(EmptyOrderedEnumerable<>),
                   typeof(GroupByDefaultEnumerable<,,,,>),
                   typeof(GroupBySpecificEnumerable<,,,,>),
                   typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
               );
            }

            // skip while, indexed
            {
                Helper.ForEachEnumerableExpression(
                   new object[0],
                   new[] { 1, 2, 3 },
                   res =>
                   {
                       Assert.AreEqual(2, res.Count);
                       Assert.AreEqual(2, res[0]);
                       Assert.AreEqual(3, res[1]);
                   },
                   "(__, a) => a.SkipWhile((_, ix) => ix < 1)",
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
                Assert.IsTrue(empty.Skip(1).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.SkipWhile(x => false).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.SkipWhile((x, ix) => false).SequenceEqual(new int[0]));
            }

            // emptyOrdered
            {
                Assert.IsTrue(emptyOrdered.Skip(1).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.SkipWhile(x => false).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.SkipWhile((x, ix) => false).SequenceEqual(new int[0]));
            }

            // groupByDefault
            {
                Assert.AreEqual(2, groupByDefault.Skip(1).First().Key);
                Assert.AreEqual(2, groupByDefault.SkipWhile(x => x.Key <= 1).First().Key);
                Assert.AreEqual(2, groupByDefault.SkipWhile((x, ix) => x.Key <= 1).First().Key);
            }

            // groupBySpecific
            {
                Assert.AreEqual("world", groupBySpecific.Skip(1).First().Key);
                Assert.AreEqual("world", groupBySpecific.SkipWhile(x => x.Key == "hello").First().Key);
                Assert.AreEqual("world", groupBySpecific.SkipWhile((x, ix) => ix <= 0).First().Key);
            }

            // lookupDefault
            {
                Assert.AreEqual(2, lookupDefault.Skip(1).First().Key);
                Assert.AreEqual(2, lookupDefault.SkipWhile(x => x.Key <= 1).First().Key);
                Assert.AreEqual(2, lookupDefault.SkipWhile((x, ix) => x.Key <= 1).First().Key);
            }

            // lookupSpecific
            {
                Assert.AreEqual(2, lookupSpecific.Skip(1).First().Key);
                Assert.AreEqual(2, lookupSpecific.SkipWhile(x => x.Key <= 1).First().Key);
                Assert.AreEqual(2, lookupSpecific.SkipWhile((x, ix) => x.Key <= 1).First().Key);
            }

            // range
            {
                Assert.IsTrue(range.Skip(1).SequenceEqual(new[] { 2, 3, 4, 5 }));
                Assert.IsTrue(range.SkipWhile(x => x <= 1).SequenceEqual(new[] { 2, 3, 4, 5 }));
                Assert.IsTrue(range.SkipWhile((x, ix) => x <= 1).SequenceEqual(new[] { 2, 3, 4, 5 }));
            }

            // repeat
            {
                Assert.IsTrue(repeat.Skip(1).SequenceEqual(new[] { "foo", "foo", "foo", "foo", }));
                Assert.IsTrue(repeat.SkipWhile(x => x == "foo").SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.SkipWhile((x, ix) => ix < 1).SequenceEqual(new[] { "foo", "foo", "foo", "foo", }));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.Skip(1).SequenceEqual(new[] { 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.SkipWhile(x => x >= 5).SequenceEqual(new[] { 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.SkipWhile((x, ix) => ix < 1).SequenceEqual(new[] { 4, 3, 2, 1 }));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.Skip(1).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.SkipWhile(x => x >= 5).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.SkipWhile((x, ix) => ix < 1).SequenceEqual(new int[0]));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.Skip(1).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.SkipWhile(x => x >= 5).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.SkipWhile((x, ix) => ix < 1).SequenceEqual(new int[0]));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.Skip(1).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.SkipWhile(x => x >= 5).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SkipWhile((x, ix) => ix < 1).SequenceEqual(new int[0]));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.Skip(1).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.SkipWhile(x => x >= 5).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SkipWhile((x, ix) => ix < 1).SequenceEqual(new int[0]));
            }
        }

        [TestMethod]
        public void Errors()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { "hello" },
                @"a => { try { a.SkipWhile(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""predicate"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { "hello" },
                @"a => { try { a.SkipWhile(default(Func<string, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""predicate"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
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
                try { empty.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { empty.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { emptyOrdered.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.SkipWhile(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.SkipWhile(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.SkipWhile(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.SkipWhile(default(Func<GroupingEnumerable<string, string>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.SkipWhile(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookupDefault.SkipWhile(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.SkipWhile(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookupSpecific.SkipWhile(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.SkipWhile(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.SkipWhile(default(Func<string, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.SkipWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.SkipWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
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

            // groupByDefault
            {
                try { groupByDefault.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Skip(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SkipWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SkipWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3 };
            var asSkip = e.Skip(2);

            Assert.IsTrue(asSkip.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkip)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(3, res[0]);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];
            var asSkip = e.Skip(2);
            Assert.IsTrue(asSkip.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkip)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void UnderSkip()
        {
            var e = new[] { 1, 2, 3 };
            var asSkip = e.Skip(-1);

            Assert.IsTrue(asSkip.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkip)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }



        [TestMethod]
        public void OverSkip()
        {
            var e = new[] { 1, 2, 3 };
            var asSkip = e.Skip(4);

            Assert.IsTrue(asSkip.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkip)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void While()
        {
            var e = new[] { 1, 2, 3 };
            var asSkipWhile = e.SkipWhile(i => i < 3);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(3, res[0]);
        }

        [TestMethod]
        public void WhileEmpty()
        {
            var e = new int[0];
            var asSkipWhile = e.SkipWhile(i => true);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileAll()
        {
            var e = new[] { 1, 2, 3 };
            var asSkipWhile = e.SkipWhile(i => true);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileNone()
        {
            var e = new[] { 1, 2, 3 };
            var asSkipWhile = e.SkipWhile(x => false);
            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }

        [TestMethod]
        public void WhileIndexed()
        {
            var e = new[] { 1, 2, 3 };
            var asSkipWhile = e.SkipWhile((_, ix) => ix < 1);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(2, res[0]);
            Assert.AreEqual(3, res[1]);
        }

        [TestMethod]
        public void WhileIndexedEmpty()
        {
            var e = new int[0];
            var asSkipWhile = e.SkipWhile((_, ix) => ix < 1);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileIndexedAll()
        {
            var e = new[] { 1, 2, 3 };
            var asSkipWhile = e.SkipWhile((_, ix) => true);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileIndexedNone()
        {
            var e = new[] { 1, 2, 3 };
            var asSkipWhile = e.SkipWhile((_, ix) => false);

            Assert.IsTrue(asSkipWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asSkipWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }
    }
}
