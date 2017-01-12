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
    public class DefaultIfEmptyTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IDefaultIfEmpty<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IDefaultIfEmpty ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableExpression(
                new object[0],
                new[] { 1, 2, 3 },
                res =>
                {
                    Assert.AreEqual(3, res.Count);
                    Assert.AreEqual(1, res[0]);
                    Assert.AreEqual(2, res[1]);
                    Assert.AreEqual(3, res[2]);
                },
                "(_, a) => a.DefaultIfEmpty()",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );

            Helper.ForEachEnumerableExpression(
                new object[0],
                new[] { 1, 2, 3 },
                res =>
                {
                    Assert.AreEqual(3, res.Count);
                    Assert.AreEqual(1, res[0]);
                    Assert.AreEqual(2, res[1]);
                    Assert.AreEqual(3, res[2]);
                },
                "(_, a) => a.DefaultIfEmpty(4)",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );

            Helper.ForEachEnumerableExpression(
                new object[0],
                new string[0],
                res =>
                {
                    Assert.AreEqual(1, res.Count);
                    Assert.IsNull(res[0]);
                },
                "(_, a) => a.DefaultIfEmpty()",
                typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                typeof(DefaultIfEmptySpecificEnumerable<,,>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );

            Helper.ForEachEnumerableExpression(
                new object[0],
                new string[0],
                res =>
                {
                    Assert.AreEqual(1, res.Count);
                    Assert.AreEqual("foo", res[0]);
                },
                @"(_, a) => a.DefaultIfEmpty(""foo"")",
                typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                typeof(DefaultIfEmptySpecificEnumerable<,,>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );
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
            var defaultIfEmptyDefault = new[] { 1, 2, 3 }.DefaultIfEmpty();
            var defaultIfEmptySpecific = new[] { 1, 2, 3 }.DefaultIfEmpty(4);
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
                Assert.IsTrue(empty.DefaultIfEmpty().SequenceEqual(new[] { 0 }));
                Assert.IsTrue(empty.DefaultIfEmpty(4).SequenceEqual(new[] { 4 }));
            }

            // emptyOrdered
            {
                Assert.IsTrue(emptyOrdered.DefaultIfEmpty().SequenceEqual(new[] { 0 }));
                Assert.IsTrue(emptyOrdered.DefaultIfEmpty(4).SequenceEqual(new[] { 4 }));
            }

            // defaultIfEmptyDefault
            {
                Assert.IsTrue(defaultIfEmptyDefault.DefaultIfEmpty().SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(defaultIfEmptyDefault.DefaultIfEmpty(4).SequenceEqual(new[] { 1, 2, 3 }));
            }

            // defaultIfEmptySpecific
            {
                Assert.IsTrue(defaultIfEmptySpecific.DefaultIfEmpty().SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(defaultIfEmptySpecific.DefaultIfEmpty(4).SequenceEqual(new[] { 1, 2, 3 }));
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.DefaultIfEmpty().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.DefaultIfEmpty(groupByDefault.First()).SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.DefaultIfEmpty().SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.DefaultIfEmpty(groupBySpecific.First()).SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
            }

            // lookup
            {
                Assert.IsTrue(lookup.DefaultIfEmpty().SequenceEqual(new[] { lookup.ElementAt(0), lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.DefaultIfEmpty(lookup.First()).SequenceEqual(new[] { lookup.ElementAt(0), lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.DefaultIfEmpty().SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.DefaultIfEmpty(4).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
            }

            // repeat
            {
                Assert.IsTrue(repeat.DefaultIfEmpty().SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo"}));
                Assert.IsTrue(repeat.DefaultIfEmpty("bar").SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.DefaultIfEmpty().SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.DefaultIfEmpty(4).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.DefaultIfEmpty().SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.DefaultIfEmpty(5).SequenceEqual(new[] { 0 }));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.DefaultIfEmpty().SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.DefaultIfEmpty(5).SequenceEqual(new[] { 4 }));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.DefaultIfEmpty().SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.DefaultIfEmpty(5).SequenceEqual(new[] { 0 }));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.DefaultIfEmpty().SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.DefaultIfEmpty(5).SequenceEqual(new[] { 4 }));
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            Assert.IsTrue(dict.SequenceEqual(dict.DefaultIfEmpty()));
            Assert.IsTrue(dict.SequenceEqual(dict.DefaultIfEmpty(dict.First())));
            Assert.IsTrue(sortedDict.SequenceEqual(sortedDict.DefaultIfEmpty()));
            Assert.IsTrue(sortedDict.SequenceEqual(sortedDict.DefaultIfEmpty(sortedDict.First())));
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.DefaultIfEmpty(1); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed_Weird()
        {
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

            // groupByDefault
            {
                try { groupByDefault.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.DefaultIfEmpty(groupByDefault.First()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.DefaultIfEmpty(groupBySpecific.First()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.DefaultIfEmpty(lookup.First()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.DefaultIfEmpty(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.DefaultIfEmpty(4); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            {
                var e = new[] { 1, 2, 3 };
                var asDefaultIfEmpty = e.DefaultIfEmpty();

                Assert.IsTrue(asDefaultIfEmpty.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asDefaultIfEmpty)
                {
                    res.Add(item);
                }

                Assert.AreEqual(3, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
            }

            {
                var e = new int[0];
                var asDefaultIfEmpty = e.DefaultIfEmpty();

                Assert.IsTrue(asDefaultIfEmpty.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asDefaultIfEmpty)
                {
                    res.Add(item);
                }

                Assert.AreEqual(1, res.Count);
                Assert.AreEqual(0, res[0]);
            }
        }


        [TestMethod]
        public void Specific()
        {
            {
                var e = new[] { 1, 2, 3 };
                var asDefaultIfEmpty = e.DefaultIfEmpty(4);

                Assert.IsTrue(asDefaultIfEmpty.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asDefaultIfEmpty)
                {
                    res.Add(item);
                }

                Assert.AreEqual(3, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
            }

            {
                var e = new int[0];
                var asDefaultIfEmpty = e.DefaultIfEmpty(4);

                Assert.IsTrue(asDefaultIfEmpty.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asDefaultIfEmpty)
                {
                    res.Add(item);
                }

                Assert.AreEqual(1, res.Count);
                Assert.AreEqual(4, res[0]);
            }
        }
    }
}
