using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqAF;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class WhereTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IWhere<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IWhere ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            // default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "foo", "bar", "fizz", "buzz", },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual("foo", res[0]);
                        Assert.AreEqual("fizz", res[1]);
                    },
                    "(_, a) => a.Where(s => s[0] == 'f')",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
            }

            // indexed
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "foo", "bar", "fizz", "buzz", },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual("fizz", res[0]);
                        Assert.AreEqual("buzz", res[1]);
                    },
                    @"(_, a) => a.Where((s, ix) => s[ix] == 'z')",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
            }
        }

        class _GroupingComparer<T> : IEqualityComparer<GroupingEnumerable<T, T>>
        {
            public bool Equals(GroupingEnumerable<T, T> x, GroupingEnumerable<T, T> y)
            {
                if (!x.Key.Equals(y.Key)) return false;

                var xList = x.ToList();
                var yList = y.ToList();

                if (xList.Count != yList.Count) return false;

                for (var i = 0; i < xList.Count; i++)
                {
                    if (!xList[i].Equals(yList[i])) return false;
                }

                return true;
            }

            public int GetHashCode(GroupingEnumerable<T, T> obj)
            {
                var ret = obj.Key.GetHashCode();
                foreach (var item in obj)
                {
                    ret *= 17;
                    ret += item.GetHashCode();
                }

                return ret;
            }
        }

        [TestMethod]
        public void Chaining_Weird()
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
                Assert.IsFalse(empty.Where(x => true).Any());
                Assert.IsFalse(empty.Where((x, ix) => true).Any());
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.Where(x => true).Any());
                Assert.IsFalse(emptyOrdered.Where((x, ix) => true).Any());
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.Where(x => true).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Where((x, ix) => true).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.Where(x => true).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Where((x, ix) => true).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
            }

            // lookup
            {
                Assert.IsTrue(lookup.Where(x => true).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Where((x, ix) => true).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.Where(x => true).SequenceEqual(range));
                Assert.IsTrue(range.Where((x, ix) => true).SequenceEqual(range));
            }

            // repeat
            {
                Assert.IsTrue(repeat.Where(x => true).SequenceEqual(repeat));
                Assert.IsTrue(repeat.Where((x, ix) => true).SequenceEqual(repeat));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.Where(x => true).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Where((x, ix) => true).SequenceEqual(reverseRange));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.Where(x => true).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Where((x, ix) => true).SequenceEqual(oneItemDefault));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.Where(x => true).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Where((x, ix) => true).SequenceEqual(oneItemSpecific));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.Where(x => true).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Where((x, ix) => true).SequenceEqual(oneItemDefaultOrdered));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.Where(x => true).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Where((x, ix) => true).SequenceEqual(oneItemSpecificOrdered));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a => { try { a.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""predicate"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
            }

            // indexed
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a => { try { a.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""predicate"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
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
                try { empty.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { empty.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { emptyOrdered.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Where(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.Where(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Where(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.Where(default(Func<GroupingEnumerable<string, string>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Where(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookup.Where(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Where(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.Where(default(Func<string, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Where(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.Where(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed_Weird()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookup = new LookupEnumerable<int, int>();
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { empty.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Where(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Where((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var foo = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var asWhere = foo.Where(i => i % 2 == 0);

            Assert.IsTrue(asWhere.GetType().IsValueType);

            var ret = new List<int>();
            foreach(var item in asWhere)
            {
                ret.Add(item);
            }

            Assert.AreEqual(5, ret.Count);
            Assert.AreEqual(2, ret[0]);
            Assert.AreEqual(4, ret[1]);
            Assert.AreEqual(6, ret[2]);
            Assert.AreEqual(8, ret[3]);
            Assert.AreEqual(10, ret[4]);
        }

        [TestMethod]
        public void Empty()
        {
            var foo = new int[0];
            var asWhere = foo.Where(i => i == 1);

            Assert.IsTrue(asWhere.GetType().IsValueType);

            var e = asWhere.GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        static IEnumerable<int> _Infinite()
        {
            var prev = 0;

            while (true)
            {
                yield return prev;
                prev++;
            }
        }

        [TestMethod]
        public void Infinite()
        {
            var foo = _Infinite();
            var asWhere = foo.Where(i => i % 4 == 0);

            Assert.IsTrue(asWhere.GetType().IsValueType);

            var ret = new List<int>();

            foreach(var item in asWhere)
            {
                ret.Add(item);
                if (ret.Count == 1000) break;
            }
            
            Assert.AreEqual(1000, ret.Count);
            Assert.IsTrue(System.Linq.Enumerable.All(ret, i => i >= 0));
            Assert.IsTrue(System.Linq.Enumerable.All(ret, i => i % 4 == 0));
            Assert.AreEqual(1000, System.Linq.Enumerable.Count(System.Linq.Enumerable.Distinct(ret)));
        }
        
        [TestMethod]
        public void Indexed()
        {
            var foo = new[] { 1, 2, 3, 4 };
            var asWhere = foo.Where((_, ix) => ix % 2 == 0);

            Assert.IsTrue(asWhere.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asWhere)
            {
                ret.Add(item);
            }

            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(1, ret[0]);
            Assert.AreEqual(3, ret[1]);
        }
    }
}
