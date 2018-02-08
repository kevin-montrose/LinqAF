using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class ToLookupTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IToLookup<>), out instOverlaps, out extOverlaps);

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
                if (!Helper.Implements(e, typeof(Impl.IToLookup<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IToLookup ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach(var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(3, e.Count);
            Assert.IsTrue(new[] { "1", "1" }.SequenceEqual(res[0]));
            Assert.IsTrue(new[] { "2", "2" }.SequenceEqual(res[1]));
            Assert.IsTrue(new[] { "3", "3" }.SequenceEqual(res[2]));
        }

        [TestMethod]
        public void Grow()
        {
            var e = Enumerable.Range(0, 100).ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(100, res.Count);
            Assert.AreEqual(100, e.Count);
            for(var i = 0; i < 100; i++)
            {
                var set = res[i];
                Assert.AreEqual(1, set.Length);
                Assert.AreEqual(i.ToString(), set[0]);
            }
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0].ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(0, e.Count);
            Assert.AreEqual(0, res.Count);
        }
        
        [TestMethod]
        public void One()
        {
            var e = new[] { 1 }.ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(1, e.Count);
            Assert.AreEqual(1, res.Count);
            Assert.IsTrue(new[] { "1" }.SequenceEqual(res[0]));
        }

        [TestMethod]
        public void Two()
        {
            var e = new[] { 1, 2 }.ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string[]>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.ToArray());
            }

            Assert.AreEqual(2, e.Count);
            Assert.AreEqual(2, res.Count);
            Assert.IsTrue(new[] { "1" }.SequenceEqual(res[0]));
            Assert.IsTrue(new[] { "2" }.SequenceEqual(res[1]));
        }

        [TestMethod]
        public void NullKey()
        {
            var e = new[] { "hello", null }.ToLookup(p => p);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string>();
            foreach (var group in e)
            {
                Assert.IsTrue(group.GetType().IsValueType);

                res.Add(group.Single());
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("hello", res[0]);
            Assert.AreEqual(null, res[1]);

            Assert.IsTrue(e["hello"].SequenceEqual(new[] { "hello" }));
            Assert.IsTrue(e[null].SequenceEqual(new string[] { null }));
        }

        [TestMethod]
        public void Lookup()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);
            
            Assert.AreEqual(3, e.Count);
            Assert.IsTrue(new[] { "1", "1" }.SequenceEqual(e[1]));
            Assert.IsTrue(new[] { "2", "2" }.SequenceEqual(e[2]));
            Assert.IsTrue(new[] { "3", "3" }.SequenceEqual(e[3]));

            Assert.IsFalse(e[4].Any());
        }

        [TestMethod]
        public void Contains()
        {
            var e = new[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, x => x.ToString());

            Assert.IsTrue(e.GetType().IsValueType);

            Assert.AreEqual(3, e.Count);
            Assert.IsTrue(e.Contains(1));
            Assert.IsTrue(e.Contains(2));
            Assert.IsTrue(e.Contains(3));
            Assert.IsFalse(e.Contains(4));
        }

        public class _Comparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Length == y.Length;

            public int GetHashCode(string obj) => obj.Length;
        }

        public class _IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
        }

        [TestMethod]
        public void Chaining()
        {
            // default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "foo", "hello", "fizz", "buzz", "world", "bar" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);

                        var grp1 = (GroupingEnumerable<int, string>)res[0];
                        Assert.AreEqual(3, grp1.Key);
                        var grp1List = new List<string>();
                        foreach (var item in grp1)
                        {
                            grp1List.Add(item);
                        }
                        Assert.AreEqual(2, grp1List.Count);
                        Assert.AreEqual("foo", grp1List[0]);
                        Assert.AreEqual("bar", grp1List[1]);

                        var grp2 = (GroupingEnumerable<int, string>)res[1];
                        Assert.AreEqual(5, grp2.Key);
                        var grp2List = new List<string>();
                        foreach (var item in grp2)
                        {
                            grp2List.Add(item);
                        }
                        Assert.AreEqual(2, grp2List.Count);
                        Assert.AreEqual("hello", grp2List[0]);
                        Assert.AreEqual("world", grp2List[1]);

                        var grp3 = (GroupingEnumerable<int, string>)res[2];
                        Assert.AreEqual(4, grp3.Key);
                        var grp3List = new List<string>();
                        foreach (var item in grp3)
                        {
                            grp3List.Add(item);
                        }
                        Assert.AreEqual(2, grp3List.Count);
                        Assert.AreEqual("fizz", grp3List[0]);
                        Assert.AreEqual("buzz", grp3List[1]);
                    },
                    @"(_, a) => a.ToLookup(s => s.Length)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // specific
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "foo", "hello", "fizz", "buzz", "world", "bar" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);

                        var grp1 = (GroupingEnumerable<string, string>)res[0];
                        Assert.AreEqual("foo", grp1.Key);
                        var grp1List = new List<string>();
                        foreach (var item in grp1)
                        {
                            grp1List.Add(item);
                        }
                        Assert.AreEqual(2, grp1List.Count);
                        Assert.AreEqual("foo", grp1List[0]);
                        Assert.AreEqual("bar", grp1List[1]);

                        var grp2 = (GroupingEnumerable<string, string>)res[1];
                        Assert.AreEqual("hello", grp2.Key);
                        var grp2List = new List<string>();
                        foreach (var item in grp2)
                        {
                            grp2List.Add(item);
                        }
                        Assert.AreEqual(2, grp2List.Count);
                        Assert.AreEqual("hello", grp2List[0]);
                        Assert.AreEqual("world", grp2List[1]);

                        var grp3 = (GroupingEnumerable<string, string>)res[2];
                        Assert.AreEqual("fizz", grp3.Key);
                        var grp3List = new List<string>();
                        foreach (var item in grp3)
                        {
                            grp3List.Add(item);
                        }
                        Assert.AreEqual(2, grp3List.Count);
                        Assert.AreEqual("fizz", grp3List[0]);
                        Assert.AreEqual("buzz", grp3List[1]);
                    },
                    @"(_, a) => a.ToLookup(x => x, new ToLookupTests._Comparer())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // default, element
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "foo", "hello", "wizz", "bang", "world", "bar" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);

                        var grp1 = (GroupingEnumerable<int, char>)res[0];
                        Assert.AreEqual(3, grp1.Key);
                        var grp1List = new List<char>();
                        foreach (var item in grp1)
                        {
                            grp1List.Add(item);
                        }
                        Assert.AreEqual(2, grp1List.Count);
                        Assert.AreEqual('f', grp1List[0]);
                        Assert.AreEqual('b', grp1List[1]);

                        var grp2 = (GroupingEnumerable<int, char>)res[1];
                        Assert.AreEqual(5, grp2.Key);
                        var grp2List = new List<char>();
                        foreach (var item in grp2)
                        {
                            grp2List.Add(item);
                        }
                        Assert.AreEqual(2, grp2List.Count);
                        Assert.AreEqual('h', grp2List[0]);
                        Assert.AreEqual('w', grp2List[1]);

                        var grp3 = (GroupingEnumerable<int, char>)res[2];
                        Assert.AreEqual(4, grp3.Key);
                        var grp3List = new List<char>();
                        foreach (var item in grp3)
                        {
                            grp3List.Add(item);
                        }
                        Assert.AreEqual(2, grp3List.Count);
                        Assert.AreEqual('w', grp3List[0]);
                        Assert.AreEqual('b', grp3List[1]);
                    },
                    @"(_, a) => a.ToLookup(s => s.Length, s => s[0])",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // specific, element
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "foo", "hello", "fizz", "buzz", "world", "bar" },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);

                        var grp1 = (GroupingEnumerable<string, char>)res[0];
                        Assert.AreEqual("foo", grp1.Key);
                        var grp1List = new List<char>();
                        foreach (var item in grp1)
                        {
                            grp1List.Add(item);
                        }
                        Assert.AreEqual(2, grp1List.Count);
                        Assert.AreEqual('f', grp1List[0]);
                        Assert.AreEqual('b', grp1List[1]);

                        var grp2 = (GroupingEnumerable<string, char>)res[1];
                        Assert.AreEqual("hello", grp2.Key);
                        var grp2List = new List<char>();
                        foreach (var item in grp2)
                        {
                            grp2List.Add(item);
                        }
                        Assert.AreEqual(2, grp2List.Count);
                        Assert.AreEqual('h', grp2List[0]);
                        Assert.AreEqual('w', grp2List[1]);

                        var grp3 = (GroupingEnumerable<string, char>)res[2];
                        Assert.AreEqual("fizz", grp3.Key);
                        var grp3List = new List<char>();
                        foreach (var item in grp3)
                        {
                            grp3List.Add(item);
                        }
                        Assert.AreEqual(2, grp3List.Count);
                        Assert.AreEqual('f', grp3List[0]);
                        Assert.AreEqual('b', grp3List[1]);
                    },
                    @"(_, a) => a.ToLookup(x => x, x => x[0], new ToLookupTests._Comparer())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Chaining_Weird()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(4, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        return e.Count == 0;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        return e.Count == 0;
                    };

                Assert.IsTrue(checkDefault(empty.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(empty.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(empty.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(empty.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // emptyOrdered
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        return e.Count == 0;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        return e.Count == 0;
                    };

                Assert.IsTrue(checkDefault(emptyOrdered.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(emptyOrdered.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(emptyOrdered.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(emptyOrdered.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // groupByDefault
            {
                Func<LookupDefaultEnumerable<int, GroupingEnumerable<int, int>>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, GroupingEnumerable<int, int>>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(groupByDefault.ToLookup(x => x.Key)));
                Assert.IsTrue(checkSpecific(groupByDefault.ToLookup(x => x.Key, new _IntComparer())));
                Assert.IsTrue(checkDefault(groupByDefault.ToLookup(x => x.Key, x => x)));
                Assert.IsTrue(checkSpecific(groupByDefault.ToLookup(x => x.Key, x => x, new _IntComparer())));
            }

            // groupBySpecific
            {
                Func<LookupDefaultEnumerable<int, GroupingEnumerable<int, int>>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, GroupingEnumerable<int, int>>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(groupBySpecific.ToLookup(x => x.Key)));
                Assert.IsTrue(checkSpecific(groupBySpecific.ToLookup(x => x.Key, new _IntComparer())));
                Assert.IsTrue(checkDefault(groupBySpecific.ToLookup(x => x.Key, x => x)));
                Assert.IsTrue(checkSpecific(groupBySpecific.ToLookup(x => x.Key, x => x, new _IntComparer())));
            }

            // lookupDefault
            {
                Func<LookupDefaultEnumerable<int, GroupingEnumerable<int, int>>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, GroupingEnumerable<int, int>>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(lookupDefault.ToLookup(x => x.Key)));
                Assert.IsTrue(checkSpecific(lookupDefault.ToLookup(x => x.Key, new _IntComparer())));
                Assert.IsTrue(checkDefault(lookupDefault.ToLookup(x => x.Key, x => x)));
                Assert.IsTrue(checkSpecific(lookupDefault.ToLookup(x => x.Key, x => x, new _IntComparer())));
            }

            // lookupSpecific
            {
                Func<LookupDefaultEnumerable<int, GroupingEnumerable<int, int>>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, GroupingEnumerable<int, int>>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 3) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single().Count() != 2) return false;
                        if (!e[1].Single().All(x => x == 1)) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single().Count() != 2) return false;
                        if (!e[2].Single().All(x => x == 2)) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single().Count() != 2) return false;
                        if (!e[3].Single().All(x => x == 3)) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(lookupSpecific.ToLookup(x => x.Key)));
                Assert.IsTrue(checkSpecific(lookupSpecific.ToLookup(x => x.Key, new _IntComparer())));
                Assert.IsTrue(checkDefault(lookupSpecific.ToLookup(x => x.Key, x => x)));
                Assert.IsTrue(checkSpecific(lookupSpecific.ToLookup(x => x.Key, x => x, new _IntComparer())));
            }

            // range
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 5) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single() != 1) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single() != 2) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single() != 3) return false;
                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;
                        if (e[5].Count() != 1) return false;
                        if (e[5].Single() != 5) return false;

                        return true;
                    };

                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 5) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single() != 1) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single() != 2) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single() != 3) return false;
                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;
                        if (e[5].Count() != 1) return false;
                        if (e[5].Single() != 5) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(range.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(range.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(range.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(range.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // repeat
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[4].Count() != 5) return false;
                        if (!e[4].All(x => x == 4)) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[4].Count() != 5) return false;
                        if (!e[4].All(x => x == 4)) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(repeat.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(repeat.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(repeat.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(repeat.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // reverseRange
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 5) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single() != 1) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single() != 2) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single() != 3) return false;
                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;
                        if (e[5].Count() != 1) return false;
                        if (e[5].Single() != 5) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 5) return false;

                        if (e[1].Count() != 1) return false;
                        if (e[1].Single() != 1) return false;
                        if (e[2].Count() != 1) return false;
                        if (e[2].Single() != 2) return false;
                        if (e[3].Count() != 1) return false;
                        if (e[3].Single() != 3) return false;
                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;
                        if (e[5].Count() != 1) return false;
                        if (e[5].Single() != 5) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(reverseRange.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(reverseRange.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(reverseRange.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(reverseRange.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // oneItemDefault
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[0].Count() != 1) return false;
                        if (e[0].Single() != 0) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[0].Count() != 1) return false;
                        if (e[0].Single() != 0) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(oneItemDefault.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(oneItemDefault.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(oneItemDefault.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(oneItemDefault.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // oneItemSpecific
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(oneItemSpecific.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(oneItemSpecific.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(oneItemSpecific.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(oneItemSpecific.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // oneItemDefaultOrdered
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[0].Count() != 1) return false;
                        if (e[0].Single() != 0) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[0].Count() != 1) return false;
                        if (e[0].Single() != 0) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(oneItemDefaultOrdered.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(oneItemDefaultOrdered.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(oneItemDefaultOrdered.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(oneItemDefaultOrdered.ToLookup(x => x, x => x, new _IntComparer())));
            }

            // oneItemSpecificOrdered
            {
                Func<LookupDefaultEnumerable<int, int>, bool> checkDefault =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;

                        return true;
                    };
                Func<LookupSpecificEnumerable<int, int>, bool> checkSpecific =
                    e =>
                    {
                        if (e.Count != 1) return false;

                        if (e[4].Count() != 1) return false;
                        if (e[4].Single() != 4) return false;

                        return true;
                    };

                Assert.IsTrue(checkDefault(oneItemSpecificOrdered.ToLookup(x => x)));
                Assert.IsTrue(checkSpecific(oneItemSpecificOrdered.ToLookup(x => x, new _IntComparer())));
                Assert.IsTrue(checkDefault(oneItemSpecificOrdered.ToLookup(x => x, x => x)));
                Assert.IsTrue(checkSpecific(oneItemSpecificOrdered.ToLookup(x => x, x => x, new _IntComparer())));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // default
            Helper.ForEachEnumerableNoRetExpression(
                new int[0],
                @"a =>
                  {
                    try { a.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch(ArgumentNullException exc) { Assert.AreEqual(""keySelector"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // specific
            Helper.ForEachEnumerableNoRetExpression(
                new int[0],
                @"a =>
                  {
                    try { a.ToLookup(default(Func<int, int>), new ToLookupTests._IntComparer()); Assert.Fail(); } catch(ArgumentNullException exc) { Assert.AreEqual(""keySelector"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // default, element
            Helper.ForEachEnumerableNoRetExpression(
                new int[0],
                @"a =>
                  {
                    try { a.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch(ArgumentNullException exc) { Assert.AreEqual(""keySelector"", exc.ParamName); }
                    try { a.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch(ArgumentNullException exc) { Assert.AreEqual(""elementSelector"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // specific, element
            Helper.ForEachEnumerableNoRetExpression(
                new int[0],
                @"a =>
                  {
                    try { a.ToLookup(default(Func<int, int>), x => x, new ToLookupTests._IntComparer()); Assert.Fail(); } catch(ArgumentNullException exc) { Assert.AreEqual(""keySelector"", exc.ParamName); }
                    try { a.ToLookup(x => x, default(Func<int, int>), new ToLookupTests._IntComparer()); Assert.Fail(); } catch(ArgumentNullException exc) { Assert.AreEqual(""elementSelector"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
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
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(4, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // errors
            {
                try { empty.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { empty.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { empty.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { emptyOrdered.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { emptyOrdered.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupByDefault.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { groupBySpecific.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupDefault.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.ToLookup(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { lookupSpecific.ToLookup(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // range
            {
                try { range.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { range.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { repeat.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { reverseRange.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefault.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecific.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.ToLookup(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
            }
        }
        [TestMethod]
        public void Malformed()
        {
            // default
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => 
                  {
                    try { a.ToLookup(x => x); Assert.Fail(); } catch(ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // specific
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => 
                  {
                    try { a.ToLookup(x => x, new ToLookupTests._IntComparer()); Assert.Fail(); } catch(ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // default, element
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => 
                  {
                    try { a.ToLookup(x => x, x => x); Assert.Fail(); } catch(ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // specific, element
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => 
                  {
                    try { a.ToLookup(x => x, x => x, new ToLookupTests._IntComparer()); Assert.Fail(); } catch(ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
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
                try { empty.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.ToLookup(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.ToLookup(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.ToLookup(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.ToLookup(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.ToLookup(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.ToLookup(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.ToLookup(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.ToLookup(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.ToLookup(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.ToLookup(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
