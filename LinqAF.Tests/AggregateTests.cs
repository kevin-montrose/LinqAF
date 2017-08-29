using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqAF;
using System;
using TestHelpers;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class AggregateTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach(var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if(!Helper.Implements(e, typeof(LinqAF.Impl.IAggregate<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IAggregate ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach(var e in Helper.GetEnumerables(new [] { "hello", "world", "foo", "bar" }))
            {
                Func<string, string, string> d1 = (string a, string b) => a + b;
                string r1 = e.Aggregate(d1);
                Assert.AreEqual("helloworldfoobar", r1);

                Func<int, string, int> d2 = (int val, string a) => val + a.Length;
                int r2 = e.Aggregate(0, d2);
                Assert.AreEqual("helloworldfoobar".Length, r2);

                Func<int, string, int> d3_1 = (int val, string a) => val * a.Length;
                Func<int, double> d3_2 = (int val) => Math.Sqrt(val);
                double r3 = e.Aggregate(1, d3_1, d3_2);
                Assert.AreEqual(Math.Sqrt(1 * 5 * 5 * 3 * 3), r3);
            }

            var r4 = Enumerable.Empty<string>().Aggregate("foo", (_, __) => _);
            Assert.AreEqual("foo", r4);

            var r5 = Enumerable.Empty<string>().Aggregate(0, (old, str) => old + str.Length, len => len.ToString());
            Assert.AreEqual("0", r5);
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

            // 1 arg
            {
                try
                {
                    empty.Aggregate((a, b) => a + b);
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("Sequence was empty", exc.Message);
                }

                try
                {
                    emptyOrdered.Aggregate((a, b) => a + b);
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("Sequence was empty", exc.Message);
                }

                {
                    var ret = groupByDefault.Aggregate((a, b) => a.Key >= b.Key ? a : b);
                    Assert.AreEqual(3, ret.Key);
                }

                {
                    var ret = groupBySpecific.Aggregate((a, b) => a.Key.Length >= b.Key.Length ? a : b);
                    Assert.AreEqual("hello", ret.Key);
                }

                {
                    var ret = lookupDefault.Aggregate((a, b) => a.Key >= b.Key ? a : b);
                    Assert.AreEqual(3, ret.Key);
                }

                {
                    var ret = lookupSpecific.Aggregate((a, b) => a.Key >= b.Key ? a : b);
                    Assert.AreEqual(3, ret.Key);
                }

                {
                    var ret = range.Aggregate((a, b) => a + b);
                    Assert.AreEqual(1 + 2 + 3 + 4 + 5, ret);
                }

                {
                    var ret = repeat.Aggregate((a, b) => a + b);
                    Assert.AreEqual("foofoofoofoofoo", ret);
                }

                {
                    var ret = reverseRange.Aggregate((a, b) => a - b);
                    Assert.AreEqual(5 - 4 - 3 - 2 - 1, ret);
                }

                {
                    var ret = oneItemDefault.Aggregate((a, b) => a + b);
                    Assert.AreEqual(0, ret);
                }

                {
                    var ret = oneItemSpecific.Aggregate((a, b) => a + b);
                    Assert.AreEqual(4, ret);
                }

                {
                    var ret = oneItemDefaultOrdered.Aggregate((a, b) => a + b);
                    Assert.AreEqual(0, ret);
                }

                {
                    var ret = oneItemSpecificOrdered.Aggregate((a, b) => a + b);
                    Assert.AreEqual(4, ret);
                }
            }

            // 2 arg
            {
                {
                    var ret = empty.Aggregate(1, (a, b) => a + b);
                    Assert.AreEqual(1, ret);
                }
                
                {
                    var ret = emptyOrdered.Aggregate(2, (a, b) => a + b);
                    Assert.AreEqual(2, ret);
                }

                {
                    var ret = groupByDefault.Aggregate(groupByDefault.First(), (a, b) => a.Key >= b.Key ? a : b);
                    Assert.AreEqual(3, ret.Key);
                }

                {
                    var ret = groupBySpecific.Aggregate(groupBySpecific.First(), (a, b) => a.Key.Length >= b.Key.Length ? a : b);
                    Assert.AreEqual("hello", ret.Key);
                }

                {
                    var ret = lookupDefault.Aggregate(lookupDefault.First(), (a, b) => a.Key >= b.Key ? a : b);
                    Assert.AreEqual(3, ret.Key);
                }

                {
                    var ret = lookupSpecific.Aggregate(lookupDefault.First(), (a, b) => a.Key >= b.Key ? a : b);
                    Assert.AreEqual(3, ret.Key);
                }

                {
                    var ret = range.Aggregate(10, (a, b) => a + b);
                    Assert.AreEqual(10 + 1 + 2 + 3 + 4 + 5, ret);
                }

                {
                    var ret = repeat.Aggregate("bar", (a, b) => a + b);
                    Assert.AreEqual("barfoofoofoofoofoo", ret);
                }

                {
                    var ret = reverseRange.Aggregate(6, (a, b) => a - b);
                    Assert.AreEqual(6 - 5 - 4 - 3 - 2 - 1, ret);
                }

                {
                    var ret = oneItemDefault.Aggregate(5, (a, b) => a + b);
                    Assert.AreEqual(5, ret);
                }

                {
                    var ret = oneItemSpecific.Aggregate(5, (a, b) => a + b);
                    Assert.AreEqual(9, ret);
                }

                {
                    var ret = oneItemDefaultOrdered.Aggregate(5, (a, b) => a + b);
                    Assert.AreEqual(5, ret);
                }

                {
                    var ret = oneItemSpecificOrdered.Aggregate(5, (a, b) => a + b);
                    Assert.AreEqual(9, ret);
                }
            }

            // 3 arg
            {
                {
                    var ret = empty.Aggregate(1, (a, b) => a + b, x => x + 1);
                    Assert.AreEqual(2, ret);
                }

                {
                    var ret = emptyOrdered.Aggregate(2, (a, b) => a + b, x => x + 2);
                    Assert.AreEqual(4, ret);
                }

                {
                    var ret = groupByDefault.Aggregate(groupByDefault.First(), (a, b) => a.Key >= b.Key ? a : b, x => x.First());
                    Assert.AreEqual(3, ret);
                }

                {
                    var ret = groupBySpecific.Aggregate(groupBySpecific.First(), (a, b) => a.Key.Length >= b.Key.Length ? a : b, x => x.First());
                    Assert.AreEqual("hello", ret);
                }

                {
                    var ret = lookupDefault.Aggregate(lookupDefault.First(), (a, b) => a.Key >= b.Key ? a : b, x => x.First());
                    Assert.AreEqual(3, ret);
                }

                {
                    var ret = lookupSpecific.Aggregate(lookupDefault.First(), (a, b) => a.Key >= b.Key ? a : b, x => x.First());
                    Assert.AreEqual(3, ret);
                }

                {
                    var ret = range.Aggregate(10, (a, b) => a + b, x => x / 2);
                    Assert.AreEqual((10 + 1 + 2 + 3 + 4 + 5) / 2, ret);
                }

                {
                    var ret = repeat.Aggregate("bar", (a, b) => a + b, x => x + "bar");
                    Assert.AreEqual("barfoofoofoofoofoobar", ret);
                }

                {
                    var ret = reverseRange.Aggregate(6, (a, b) => a - b, x => x * 2);
                    Assert.AreEqual((6 - 5 - 4 - 3 - 2 - 1) * 2, ret);
                }

                {
                    var ret = oneItemDefault.Aggregate(5, (a, b) => a + b, x => x * 2);
                    Assert.AreEqual(10, ret);
                }

                {
                    var ret = oneItemSpecific.Aggregate(5, (a, b) => a + b, x => x * 2);
                    Assert.AreEqual(18, ret);
                }

                {
                    var ret = oneItemDefaultOrdered.Aggregate(5, (a, b) => a + b, x => x * 2);
                    Assert.AreEqual(10, ret);
                }

                {
                    var ret = oneItemSpecificOrdered.Aggregate(5, (a, b) => a + b, x => x * 2);
                    Assert.AreEqual(18, ret);
                }
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            // 1 arg
            {
                {
                    var res = dict.Aggregate((a, b) => a);
                    Assert.AreEqual(1, res.Key);
                    Assert.AreEqual(2, res.Value);
                }

                {
                    var res = sortedDict.Aggregate((a, b) => a);
                    Assert.AreEqual(1, res.Key);
                    Assert.AreEqual(2, res.Value);
                }
            }

            // 2 arg
            {
                {
                    var res = dict.Aggregate(5, (a, b) => a + b.Key + b.Value);
                    Assert.AreEqual(15, res);
                }

                {
                    var res = sortedDict.Aggregate(5, (a, b) => a + b.Key + b.Value);
                    Assert.AreEqual(15, res);
                }
            }

            // 3 arg
            {
                {
                    var res = dict.Aggregate(5, (a, b) => a + b.Key + b.Value, x => x + .5);
                    Assert.AreEqual(15.5, res);
                }

                {
                    var res = sortedDict.Aggregate(5, (a, b) => a + b.Key + b.Value, x => x + .5);
                    Assert.AreEqual(15.5, res);
                }
            }
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new string[0]))
            {
                try
                {
                    Func<string, string, string> d1 = null;
                    string r1 = e.Aggregate(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    Func<string, string, string> d1 = null;
                    string r1 = e.Aggregate("foo", d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    Func<string, string, string> d1 = null;
                    Func<string, int> d2 = a => a.Length;
                    int r1 = e.Aggregate("foo", d1, d2);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    Func<string, string, string> d1 = (a, b) => a;
                    Func<string, int> d2 = null;
                    int r1 = e.Aggregate("foo", d1, d2);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
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

            // 1 arg
            {
                try
                {
                    empty.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate(default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate(default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate(default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Aggregate(default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    range.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate(default(Func<string, string, string>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Aggregate(default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }
            }

            // 2 arg
            {
                try
                {
                    empty.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate(default(GroupingEnumerable<int, int>), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate(default(GroupingEnumerable<string, string>), default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate(default(GroupingEnumerable<int, int>), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Aggregate(default(GroupingEnumerable<int, int>), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    range.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate("", default(Func<string, string, string>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Aggregate(1, default(Func<int, int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }
            }
            
            // 3 arg
            {
                try
                {
                    empty.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    empty.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate(default(GroupingEnumerable<int, int>), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate(default(GroupingEnumerable<int, int>), (a, b) => a, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate(default(GroupingEnumerable<string, string>), default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate(default(GroupingEnumerable<string, string>), (a, b) => a, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate(default(GroupingEnumerable<int, int>), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate(default(GroupingEnumerable<int, int>), (a, b) => a, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }
                
                try
                {
                    lookupSpecific.Aggregate(default(GroupingEnumerable<int, int>), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Aggregate(default(GroupingEnumerable<int, int>), (a, b) => a, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    range.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    range.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate("", default(Func<string, string, string>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate("", (a, b) => a + b, default(Func<string, string>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }
                try
                {
                    oneItemDefault.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }
                try
                {
                    oneItemSpecific.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }
                try
                {
                    oneItemDefaultOrdered.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Aggregate(1, default(Func<int, int, int>), x => x);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("func", exc.ParamName);
                }
                try
                {
                    oneItemSpecificOrdered.Aggregate(1, (a, b) => a + b, default(Func<int, int>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("resultSelector", exc.ParamName);
                }
            }
        }

        [TestMethod]
        public void Errors_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            // 1 arg
            {
                try { dict.Aggregate(default(Func<System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("func", exc.ParamName); }
                try { sortedDict.Aggregate(default(Func<System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("func", exc.ParamName); }
            }

            // 2 arg
            {
                try { dict.Aggregate(default(System.Collections.Generic.KeyValuePair<int, int>), default(Func<System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("func", exc.ParamName); }
                try { sortedDict.Aggregate(default(System.Collections.Generic.KeyValuePair<int, int>), default(Func<System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("func", exc.ParamName); }
            }

            // 3 arg
            {
                try { dict.Aggregate(default(System.Collections.Generic.KeyValuePair<int, int>), default(Func<System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>>), x => x.Key); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("func", exc.ParamName); }
                try { dict.Aggregate(default(System.Collections.Generic.KeyValuePair<int, int>), (a, b) => a, default(Func<System.Collections.Generic.KeyValuePair<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { sortedDict.Aggregate(default(System.Collections.Generic.KeyValuePair<int, int>), default(Func<System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>, System.Collections.Generic.KeyValuePair<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("func", exc.ParamName); }
                try { sortedDict.Aggregate(default(System.Collections.Generic.KeyValuePair<int, int>), (a, b) => a, default(Func<System.Collections.Generic.KeyValuePair<int, int>, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<string>())
            {
                try
                {
                    Func<string, string, string> d1 = (a, b) => a + b;
                    string r1 = e.Aggregate(d1);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    Func<string, string, string> d1 = (a, b) => a + b;
                    string r1 = e.Aggregate("foo", d1);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    Func<string, string, string> d1 = (a, b) => a + b;
                    Func<string, int> d2 = a => a.Length;
                    int r1 = e.Aggregate("foo", d1, d2);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    Func<string, string, string> d1 = (a, b) => a;
                    Func<string, int> d2 = null;
                    int r1 = e.Aggregate("foo", d1, d2);
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

            // 1 arg
            {
                try
                {
                    empty.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    range.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Aggregate((a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }

            // 2 arg
            {
                try
                {
                    empty.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate("", (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    range.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate("", (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Aggregate(1, (a, b) => a);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }

            // 3 arg
            {
                try
                {
                    empty.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    emptyOrdered.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupByDefault.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    groupBySpecific.Aggregate("", (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupDefault.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    lookupSpecific.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    range.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    repeat.Aggregate("", (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    reverseRange.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefault.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecific.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemDefaultOrdered.Aggregate(1, (a, b) => a, x => x);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    oneItemSpecificOrdered.Aggregate(1, (a, b) => a, x => x);
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
            var e = new[] { 1, 2, 3 };
            var res = e.Aggregate((a, b) => a + b);

            Assert.AreEqual(6, res);
        }

        [TestMethod]
        public void Empty()
        {
            var e = Enumerable.Empty<int>();

            try
            {
                var r1 = e.Aggregate((a, b) => a + b);
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("Sequence was empty", x.Message);
            }

            var r2 = e.Aggregate(3, (a, b) => a + b);
            Assert.AreEqual(3, r2);

            var r3 = e.Aggregate(3, (a, b) => a + b, n => n.ToString());
            Assert.AreEqual("3", r3);
        }

        [TestMethod]
        public void Seed()
        {
            var e = new[] { 1, 2, 3 };
            var res = e.Aggregate(4, (a, b) => a + b);

            Assert.AreEqual(10, res);
        }

        [TestMethod]
        public void Project()
        {
            var e = new[] { 1, 2, 3 };
            var res = e.Aggregate(4, (a, b) => a + b, n => n.ToString());

            Assert.AreEqual("10", res);
        }
    }
}
