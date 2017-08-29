using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class GroupByTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(Impl.IGroupBy<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IToLookup ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach(var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(3, res.Count);
            Assert.IsTrue(new[] { "1", "1" }.SequenceEqual(res[0]));
            Assert.IsTrue(new[] { "2", "2" }.SequenceEqual(res[1]));
            Assert.IsTrue(new[] { "3", "3" }.SequenceEqual(res[2]));
        }

        [TestMethod]
        public void Grow()
        {
            var e = new[] { "hello", "world", "foo", "bar", "fizz", "buzz", "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz" }.GroupBy(x => x, x => x);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(15, res.Count);
            Assert.IsTrue(new[] { "hello" }.SequenceEqual(res[0]));
            Assert.IsTrue(new[] { "world" }.SequenceEqual(res[1]));
            Assert.IsTrue(new[] { "foo" }.SequenceEqual(res[2]));
            Assert.IsTrue(new[] { "bar" }.SequenceEqual(res[3]));
            Assert.IsTrue(new[] { "fizz" }.SequenceEqual(res[4]));
            Assert.IsTrue(new[] { "buzz" }.SequenceEqual(res[5]));
            Assert.IsTrue(new[] { "abc" }.SequenceEqual(res[6]));
            Assert.IsTrue(new[] { "def" }.SequenceEqual(res[7]));
            Assert.IsTrue(new[] { "ghi" }.SequenceEqual(res[8]));
            Assert.IsTrue(new[] { "jkl" }.SequenceEqual(res[9]));
            Assert.IsTrue(new[] { "mno" }.SequenceEqual(res[10]));
            Assert.IsTrue(new[] { "pqr" }.SequenceEqual(res[11]));
            Assert.IsTrue(new[] { "stu" }.SequenceEqual(res[12]));
            Assert.IsTrue(new[] { "vwx" }.SequenceEqual(res[13]));
            Assert.IsTrue(new[] { "yz" }.SequenceEqual(res[14]));
        }

        [TestMethod]
        public void Comparer()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x.ToString(), x => x.ToString(), StringComparer.InvariantCultureIgnoreCase);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(3, res.Count);
            Assert.IsTrue(new[] { "1", "1" }.SequenceEqual(res[0]));
            Assert.IsTrue(new[] { "2", "2" }.SequenceEqual(res[1]));
            Assert.IsTrue(new[] { "3", "3" }.SequenceEqual(res[2]));
        }

        [TestMethod]
        public void Collection()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, (x, grp) => string.Join("-", grp.AsEnumerable()));

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual("1-1", res[0]);
            Assert.AreEqual("2-2", res[1]);
            Assert.AreEqual("3-3", res[2]);
        }

        [TestMethod]
        public void CollectionComparer()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x.ToString(), (x, grp) => string.Join("-", grp.AsEnumerable()), StringComparer.InvariantCultureIgnoreCase);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual("1-1", res[0]);
            Assert.AreEqual("2-2", res[1]);
            Assert.AreEqual("3-3", res[2]);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0].GroupBy(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void Chaining()
        {
            // key, default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "world", "foo", "bar", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        var grp1 = (GroupingEnumerable<int, string>)res[0];
                        Assert.AreEqual(5, grp1.Key);
                        Assert.AreEqual(2, grp1.Count());
                        Assert.AreEqual("hello", grp1.ElementAt(0));
                        Assert.AreEqual("world", grp1.ElementAt(1));
                        var grp2 = (GroupingEnumerable<int, string>)res[1];
                        Assert.AreEqual(3, grp2.Key);
                        Assert.AreEqual(2, grp2.Count());
                        Assert.AreEqual("foo", grp2.ElementAt(0));
                        Assert.AreEqual("bar", grp2.ElementAt(1));
                        var grp3 = (GroupingEnumerable<int, string>)res[2];
                        Assert.AreEqual(4, grp3.Key);
                        Assert.AreEqual(1, grp3.Count());
                        Assert.AreEqual("fizz", grp3.ElementAt(0));
                    },
                    "(_, a) => a.GroupBy(k => k.Length)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, custom
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "HeLLO", "FOO", "foo", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        var grp1 = (GroupingEnumerable<string, string>)res[0];
                        Assert.AreEqual("hello", grp1.Key);
                        Assert.AreEqual(2, grp1.Count());
                        Assert.AreEqual("hello", grp1.ElementAt(0));
                        Assert.AreEqual("HeLLO", grp1.ElementAt(1));
                        var grp2 = (GroupingEnumerable<string, string>)res[1];
                        Assert.AreEqual("FOO", grp2.Key);
                        Assert.AreEqual(2, grp2.Count());
                        Assert.AreEqual("FOO", grp2.ElementAt(0));
                        Assert.AreEqual("foo", grp2.ElementAt(1));
                        var grp3 = (GroupingEnumerable<string, string>)res[2];
                        Assert.AreEqual("fizz", grp3.Key);
                        Assert.AreEqual(1, grp3.Count());
                        Assert.AreEqual("fizz", grp3.ElementAt(0));
                    },
                    "(_, a) => a.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "world", "foo", "bar", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        var grp1 = (GroupingEnumerable<int, char>)res[0];
                        Assert.AreEqual(5, grp1.Key);
                        Assert.AreEqual(2, grp1.Count());
                        Assert.AreEqual('h', grp1.ElementAt(0));
                        Assert.AreEqual('w', grp1.ElementAt(1));
                        var grp2 = (GroupingEnumerable<int, char>)res[1];
                        Assert.AreEqual(3, grp2.Key);
                        Assert.AreEqual(2, grp2.Count());
                        Assert.AreEqual('f', grp2.ElementAt(0));
                        Assert.AreEqual('b', grp2.ElementAt(1));
                        var grp3 = (GroupingEnumerable<int, char>)res[2];
                        Assert.AreEqual(4, grp3.Key);
                        Assert.AreEqual(1, grp3.Count());
                        Assert.AreEqual('f', grp3.ElementAt(0));
                    },
                    "(_, a) => a.GroupBy(k => k.Length, x => x[0])",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, custom
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "HeLLO", "FOO", "foo", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        var grp1 = (GroupingEnumerable<string, char>)res[0];
                        Assert.AreEqual("hello", grp1.Key);
                        Assert.AreEqual(2, grp1.Count());
                        Assert.AreEqual('e', grp1.ElementAt(0));
                        Assert.AreEqual('e', grp1.ElementAt(1));
                        var grp2 = (GroupingEnumerable<string, char>)res[1];
                        Assert.AreEqual("FOO", grp2.Key);
                        Assert.AreEqual(2, grp2.Count());
                        Assert.AreEqual('O', grp2.ElementAt(0));
                        Assert.AreEqual('o', grp2.ElementAt(1));
                        var grp3 = (GroupingEnumerable<string, char>)res[2];
                        Assert.AreEqual("fizz", grp3.Key);
                        Assert.AreEqual(1, grp3.Count());
                        Assert.AreEqual('i', grp3.ElementAt(0));
                    },
                    "(_, a) => a.GroupBy(x => x, f => f[1], StringComparer.InvariantCultureIgnoreCase)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, result, default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "HeLLO", "FOO", "foo", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual("hello,HeLLO", res[0]);
                        Assert.AreEqual("FOO,foo", res[1]);
                        Assert.AreEqual("fizz", res[2]);
                    },
                    @"(_, a) => a.GroupBy(h => h.Length, (key, grp) => string.Join("","", grp.AsEnumerable()))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, result, custom
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "HeLLO", "FOO", "foo", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual("hello;HeLLO", res[0]);
                        Assert.AreEqual("FOO;foo", res[1]);
                        Assert.AreEqual("fizz", res[2]);
                    },
                    @"(_, a) => a.GroupBy(g => g, (key, grp) => string.Join("";"", grp.AsEnumerable()), StringComparer.InvariantCultureIgnoreCase)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, result, default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "fizz", "buzz", "foo", "bar", "nope", "a" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual("f.b.n", res[0]);
                        Assert.AreEqual("f.b", res[1]);
                        Assert.AreEqual("a", res[2]);
                    },
                    @"(_, a) => a.GroupBy(g => g.Length, e => e[0], (key, grp) => string.Join(""."", grp.AsEnumerable()))",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, result, custom
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "HeLLO", "FOO", "foo", "fizz" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual("hello-e,e", res[0]);
                        Assert.AreEqual("FOO-O,o", res[1]);
                        Assert.AreEqual("fizz-i", res[2]);
                    },
                    @"(_, a) => a.GroupBy(k => k, e => e[1], (key, grp) => key + ""-"" + string.Join("","", grp.AsEnumerable()), StringComparer.InvariantCultureIgnoreCase)",
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
                Assert.IsFalse(empty.GroupBy(x => x).Any());
                Assert.IsFalse(empty.GroupBy(x => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupBy(x => x, x => x).Any());
                Assert.IsFalse(empty.GroupBy(x => x, x => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupBy(x => x, (a, b) => a + b.First()).Any());
                Assert.IsFalse(empty.GroupBy(x => x, (a, b) => a + b.First(), new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupBy(x => x, x => x, (a, b) => a + b.First()).Any());
                Assert.IsFalse(empty.GroupBy(x => x, x => x, (a, b) => a + b.First(), new _IntComparer()).Any());
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.GroupBy(x => x).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, x => x).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, x => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, (a, b) => a + b.First()).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, (a, b) => a + b.First(), new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, x => x, (a, b) => a + b.First()).Any());
                Assert.IsFalse(emptyOrdered.GroupBy(x => x, x => x, (a, b) => a + b.First(), new _IntComparer()).Any());
            }

            // groupByDefault
            {
                Action<IEnumerable<GroupingEnumerable<int, GroupingEnumerable<int, int>>>> check1 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(g => g.Key == 1 && g.All(h => h == 1)));

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(g => g.Key == 2 && g.All(h => h == 2)));

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(g => g.Key == 3 && g.All(h => h == 3)));
                    };

                Action<IEnumerable<GroupingEnumerable<int, int>>> check2 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(i => i == 1));

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(i => i == 2));

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(i => i == 3));
                    };

                check1(groupByDefault.GroupBy(x => x.Key).AsEnumerable());
                check1(groupByDefault.GroupBy(x => x.Key, new _IntComparer()).AsEnumerable());
                check1(groupByDefault.GroupBy(x => x.Key, x => x).AsEnumerable());
                check1(groupByDefault.GroupBy(x => x.Key, x => x, new _IntComparer()).AsEnumerable());
                check2(groupByDefault.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(groupByDefault.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check2(groupByDefault.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(groupByDefault.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // groupBySpecific
            {
                Action<IEnumerable<GroupingEnumerable<string, GroupingEnumerable<string, string>>>> check1 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual("hello", e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(g => g.Key.ToLowerInvariant() == "hello" && g.All(h => h.ToLowerInvariant() == "hello")));

                        Assert.AreEqual("world", e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(g => g.Key.ToLowerInvariant() == "world" && g.All(h => h.ToLowerInvariant() == "world")));

                        Assert.AreEqual("foo", e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(g => g.Key.ToLowerInvariant() == "foo" && g.All(h => h.ToLowerInvariant() == "foo")));
                    };

                Action<IEnumerable<GroupingEnumerable<string, string>>> check2 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual("hello", e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(i => i.ToLowerInvariant() == "hello"));

                        Assert.AreEqual("world", e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(i => i.ToLowerInvariant() == "world"));

                        Assert.AreEqual("foo", e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(i => i.ToLowerInvariant() == "foo"));
                    };

                check1(groupBySpecific.GroupBy(x => x.Key).AsEnumerable());
                check1(groupBySpecific.GroupBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).AsEnumerable());
                check1(groupBySpecific.GroupBy(x => x.Key, x => x).AsEnumerable());
                check1(groupBySpecific.GroupBy(x => x.Key, x => x, StringComparer.InvariantCultureIgnoreCase).AsEnumerable());
                check2(groupBySpecific.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(groupBySpecific.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First(), StringComparer.InvariantCultureIgnoreCase).AsEnumerable());
                check2(groupBySpecific.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(groupBySpecific.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), StringComparer.InvariantCultureIgnoreCase).AsEnumerable());
            }
            
            // lookupDefault
            {
                Action<IEnumerable<GroupingEnumerable<int, GroupingEnumerable<int, int>>>> check1 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(g => g.Key == 1 && g.All(h => h == 1)));

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(g => g.Key == 2 && g.All(h => h == 2)));

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(g => g.Key == 3 && g.All(h => h == 3)));
                    };

                Action<IEnumerable<GroupingEnumerable<int, int>>> check2 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(i => i == 1));

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(i => i == 2));

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(i => i == 3));
                    };

                check1(lookupDefault.GroupBy(x => x.Key).AsEnumerable());
                check1(lookupDefault.GroupBy(x => x.Key, new _IntComparer()).AsEnumerable());
                check1(lookupDefault.GroupBy(x => x.Key, x => x).AsEnumerable());
                check1(lookupDefault.GroupBy(x => x.Key, x => x, new _IntComparer()).AsEnumerable());
                check2(lookupDefault.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(lookupDefault.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check2(lookupDefault.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(lookupDefault.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // lookupSpecific
            {
                Action<IEnumerable<GroupingEnumerable<int, GroupingEnumerable<int, int>>>> check1 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(g => g.Key == 1 && g.All(h => h == 1)));

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(g => g.Key == 2 && g.All(h => h == 2)));

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(g => g.Key == 3 && g.All(h => h == 3)));
                    };

                Action<IEnumerable<GroupingEnumerable<int, int>>> check2 =
                    e =>
                    {
                        Assert.AreEqual(3, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.IsTrue(e.ElementAt(0).All(i => i == 1));

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.IsTrue(e.ElementAt(1).All(i => i == 2));

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.IsTrue(e.ElementAt(2).All(i => i == 3));
                    };

                check1(lookupSpecific.GroupBy(x => x.Key).AsEnumerable());
                check1(lookupSpecific.GroupBy(x => x.Key, new _IntComparer()).AsEnumerable());
                check1(lookupSpecific.GroupBy(x => x.Key, x => x).AsEnumerable());
                check1(lookupSpecific.GroupBy(x => x.Key, x => x, new _IntComparer()).AsEnumerable());
                check2(lookupSpecific.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(lookupSpecific.GroupBy(x => x.Key, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check2(lookupSpecific.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check2(lookupSpecific.GroupBy(x => x.Key, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // range
            {
                Action<IEnumerable<GroupingEnumerable<int, int>>> check =
                    e =>
                    {
                        Assert.AreEqual(5, e.Count());

                        Assert.AreEqual(1, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.AreEqual(1, e.ElementAt(0).Single());

                        Assert.AreEqual(2, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.AreEqual(2, e.ElementAt(1).Single());

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.AreEqual(3, e.ElementAt(2).Single());

                        Assert.AreEqual(4, e.ElementAt(3).Key);
                        Assert.AreEqual(1, e.ElementAt(3).Count());
                        Assert.AreEqual(4, e.ElementAt(3).Single());

                        Assert.AreEqual(5, e.ElementAt(4).Key);
                        Assert.AreEqual(1, e.ElementAt(4).Count());
                        Assert.AreEqual(5, e.ElementAt(4).Single());
                    };
                
                check(range.GroupBy(x => x).AsEnumerable());
                check(range.GroupBy(x => x, new _IntComparer()).AsEnumerable());
                check(range.GroupBy(x => x, x => x).AsEnumerable());
                check(range.GroupBy(x => x, x => x, new _IntComparer()).AsEnumerable());
                check(range.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(range.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check(range.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(range.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // repeat
            {
                Action<IEnumerable<GroupingEnumerable<string, string>>, int> check =
                    (e, len) =>
                    {
                        Assert.AreEqual(1, e.Count());

                        Assert.AreEqual("foo", e.Single().Key);
                        Assert.AreEqual(len, e.Single().Count());
                        Assert.IsTrue(e.Single().All(x => x == "foo"));
                    };

                check(repeat.GroupBy(x => x).AsEnumerable(), 5);
                check(repeat.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase).AsEnumerable(), 5);
                check(repeat.GroupBy(x => x, x => x).AsEnumerable(), 5);
                check(repeat.GroupBy(x => x, x => x, StringComparer.InvariantCultureIgnoreCase).AsEnumerable(), 5);
                check(repeat.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable(), 1);
                check(repeat.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), StringComparer.InvariantCultureIgnoreCase).AsEnumerable(), 1);
                check(repeat.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable(), 1);
                check(repeat.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), StringComparer.InvariantCultureIgnoreCase).AsEnumerable(), 1);
            }
            
            // reverseRange
            {
                Action<IEnumerable<GroupingEnumerable<int, int>>> check =
                    e =>
                    {
                        Assert.AreEqual(5, e.Count());

                        Assert.AreEqual(1, e.ElementAt(4).Key);
                        Assert.AreEqual(1, e.ElementAt(4).Count());
                        Assert.AreEqual(1, e.ElementAt(4).Single());

                        Assert.AreEqual(2, e.ElementAt(3).Key);
                        Assert.AreEqual(1, e.ElementAt(3).Count());
                        Assert.AreEqual(2, e.ElementAt(3).Single());

                        Assert.AreEqual(3, e.ElementAt(2).Key);
                        Assert.AreEqual(1, e.ElementAt(2).Count());
                        Assert.AreEqual(3, e.ElementAt(2).Single());

                        Assert.AreEqual(4, e.ElementAt(1).Key);
                        Assert.AreEqual(1, e.ElementAt(1).Count());
                        Assert.AreEqual(4, e.ElementAt(1).Single());

                        Assert.AreEqual(5, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.AreEqual(5, e.ElementAt(0).Single());
                    };

                check(reverseRange.GroupBy(x => x).AsEnumerable());
                check(reverseRange.GroupBy(x => x, new _IntComparer()).AsEnumerable());
                check(reverseRange.GroupBy(x => x, x => x).AsEnumerable());
                check(reverseRange.GroupBy(x => x, x => x, new _IntComparer()).AsEnumerable());
                check(reverseRange.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(reverseRange.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check(reverseRange.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(reverseRange.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // oneItemDefault
            {
                Action<IEnumerable<GroupingEnumerable<int, int>>> check =
                    e =>
                    {
                        Assert.AreEqual(1, e.Count());

                        Assert.AreEqual(0, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.AreEqual(0, e.ElementAt(0).Single());
                    };

                check(oneItemDefault.GroupBy(x => x).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, new _IntComparer()).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, x => x).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, x => x, new _IntComparer()).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemDefault.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // oneItemSpecific
            {
                Action<IEnumerable<GroupingEnumerable<int, int>>> check =
                    e =>
                    {
                        Assert.AreEqual(1, e.Count());

                        Assert.AreEqual(4, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.AreEqual(4, e.ElementAt(0).Single());
                    };

                check(oneItemSpecific.GroupBy(x => x).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, new _IntComparer()).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, x => x).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, x => x, new _IntComparer()).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemSpecific.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // oneItemDefaultOrdered
            {
                Action<IEnumerable<GroupingEnumerable<int, int>>> check =
                    e =>
                    {
                        Assert.AreEqual(1, e.Count());

                        Assert.AreEqual(0, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.AreEqual(0, e.ElementAt(0).Single());
                    };

                check(oneItemDefaultOrdered.GroupBy(x => x).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, new _IntComparer()).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, x => x).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, x => x, new _IntComparer()).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemDefaultOrdered.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }

            // oneItemSpecificOrdered
            {
                Action<IEnumerable<GroupingEnumerable<int, int>>> check =
                    e =>
                    {
                        Assert.AreEqual(1, e.Count());

                        Assert.AreEqual(4, e.ElementAt(0).Key);
                        Assert.AreEqual(1, e.ElementAt(0).Count());
                        Assert.AreEqual(4, e.ElementAt(0).Single());
                    };

                check(oneItemSpecificOrdered.GroupBy(x => x).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, new _IntComparer()).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, x => x).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, x => x, new _IntComparer()).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First()).AsEnumerable());
                check(oneItemSpecificOrdered.GroupBy(x => x, x => x, (a, b) => new[] { a }.GroupBy(y => y).First(), new _IntComparer()).AsEnumerable());
            }
        }

        [TestMethod]
        public void Errors()
        {
            // key, default
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        try
                        {
                            a.GroupBy(default(Func<string, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, specific
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        try
                        {
                            a.GroupBy(default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, default
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        Func<string, int> key = str => str.Length;
                        Func<string, string> element = str => str;

                        try
                        {
                            a.GroupBy(default(Func<string, int>), element);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, default(Func<string, string>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""elementSelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, specific
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;

                        try
                        {
                            a.GroupBy(default(Func<string, string>), element, StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""elementSelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, result, default
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(default(Func<string, string>), result);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, default(Func<string, GroupedEnumerable<string, string>, string>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, result, specific
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(default(Func<string, string>), result, StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, default(Func<string, GroupedEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, result, default
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(default(Func<string, string>), element, result);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, default(Func<string, string>), result);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""elementSelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, element, default(Func<string, GroupedEnumerable<string, string>, string>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // key, element, result, specific
            {
                Helper.ForEachEnumerableNoRetExpression<string>(
                    new string[0],
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(default(Func<string, string>), element, result, StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, default(Func<string, string>), result, StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""elementSelector"", exc.ParamName);
                        }

                        try
                        {
                            a.GroupBy(key, element, default(Func<string, GroupedEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }
                      }",
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
                try { empty.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { empty.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { empty.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, x => x, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, default(Func<GroupingEnumerable<string, string>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>), x => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, default(Func<GroupingEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>), (a, b) => a, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, default(Func<string, GroupedEnumerable<string, GroupingEnumerable<string, string>>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, default(Func<GroupingEnumerable<string, string>, string>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(default(Func<GroupingEnumerable<string, string>, string>), x => x, (a, b) => a, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, default(Func<GroupingEnumerable<string, string>, string>), (a, b) => a, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, x => x, default(Func<string, GroupedEnumerable<string, GroupingEnumerable<string, string>>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, x => x, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(default(Func<GroupingEnumerable<int, int>, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, x => x, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // range
            {
                try { range.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { range.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { range.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.GroupBy(default(Func<string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, default(Func<string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), x => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, default(Func<string, GroupedEnumerable<string, string>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), (a, b) => a, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, default(Func<string, GroupedEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, default(Func<string, string>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, x => x, default(Func<string, GroupedEnumerable<string, string>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupBy(default(Func<string, string>), x => x, (a, b) => a, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, default(Func<string, string>), (a, b) => a, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { repeat.GroupBy(x => x, x => x, default(Func<string, GroupedEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { range.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { reverseRange.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, default(Func<int, int>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(default(Func<int, int>), x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, default(Func<int, int>), (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
            }
        }
        
        [TestMethod]
        public void Malformed()
        {
            // key, default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        
                        try
                        {
                            a.GroupBy(key);
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

            // key, specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        
                        try
                        {
                            a.GroupBy(key, StringComparer.InvariantCultureIgnoreCase);
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

            // key, element, default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;
                        
                        try
                        {
                            a.GroupBy(key, element);
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

            // key, element, specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;
                        
                        try
                        {
                            a.GroupBy(key, element, StringComparer.InvariantCultureIgnoreCase);
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

            // key, result, default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(key, result);
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

            // key, result, specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(key, result, StringComparer.InvariantCultureIgnoreCase);
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

            // key, element, result, default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(key, element, result);
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

            // key, element, result, specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;
                        Func<string, string> element = str => str;
                        Func<string, GroupedEnumerable<string, string>, string> result = (k, grp) => """";
                        
                        try
                        {
                            a.GroupBy(key, element, result, StringComparer.InvariantCultureIgnoreCase);
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
                try { empty.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.GroupBy(x => x.Key, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.GroupBy(x => x.Key, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.GroupBy(x => x.Key, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.GroupBy(x => x.Key, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.GroupBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, x => x, (a, b) => b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupBy(x => x, x => x, (a, b) => b, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
