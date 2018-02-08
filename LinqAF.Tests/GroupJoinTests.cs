using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestHelpers;
using System;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class GroupJoinTests
    {
        static void _InstanceExtensionNoOverlapImpl(int spread, int take)
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IGroupJoin<,,>), out instOverlaps, out extOverlaps, spread, take);

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
        public void InstanceExtensionNoOverlap1()
        => _InstanceExtensionNoOverlapImpl(5, 0);

        [TestMethod]
        public void InstanceExtensionNoOverlap2()
        => _InstanceExtensionNoOverlapImpl(5, 1);

        [TestMethod]
        public void InstanceExtensionNoOverlap3()
        => _InstanceExtensionNoOverlapImpl(5, 2);

        [TestMethod]
        public void InstanceExtensionNoOverlap4()
        => _InstanceExtensionNoOverlapImpl(5, 3);

        [TestMethod]
        public void InstanceExtensionNoOverlap5()
        => _InstanceExtensionNoOverlapImpl(5, 4);

        static void _Universal(IEnumerable<Type> enums)
        {
            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(Impl.IGroupJoin<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IGroupJoin ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Universal1()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 4 == 0).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void Universal2()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 4 == 1).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void Universal3()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 4 == 2).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void Universal4()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 4 == 3).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingSimple = new List<string>();
            var missingComparer = new List<string>();

            var ijoin = typeof(Impl.IGroupJoin<,,>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: false);
            foreach (var e in enums)
            {
                var simple =
                    ijoin
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 4) return false;

                                var p = ps[0].ParameterType;

                                if (p.IsGenericType && !p.IsGenericTypeDefinition)
                                {
                                    p = Helper.GetGenericTypeDefinition(p);
                                }
                                return p == e;
                            }
                        )
                        .SingleOrDefault();
                if (simple == null) missingSimple.Add(e.Name);

                var comparer =
                    ijoin
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 5) return false;

                                var p = ps[0].ParameterType;

                                if (p.IsGenericType && !p.IsGenericTypeDefinition)
                                {
                                    p = Helper.GetGenericTypeDefinition(p);
                                }

                                var c = ps[4].ParameterType;
                                if (Helper.GetGenericTypeDefinition(c) != typeof(IEqualityComparer<>)) return false;

                                return p == e;
                            }
                        )
                        .SingleOrDefault();
                if (comparer == null) missingComparer.Add(e.Name);
            }

            if (missingSimple.Any())
            {
                Assert.Fail("Missing simple for: \r\n" + string.Join("\r\n", missingSimple));
            }

            if (missingComparer.Any())
            {
                Assert.Fail("Missing comparer for: \r\n" + string.Join("\r\n", missingComparer));
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e1 = new[] { 1, 2, 3, 4 }.Select(i => i);
            var e2 = new[] { "a", "b", "cc", "dd", "eee", "fff", "ggg", "hhhhh" }.Select(x => x);
            var asGroupJoin =
                e1.GroupJoin(
                    e2,
                    a => a,
                    b => b.Length,
                    (item, g) => string.Join(item.ToString(), g.ToArray())
                );

            Assert.IsTrue(asGroupJoin.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in asGroupJoin)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual("a1b", res[0]);
            Assert.AreEqual("cc2dd", res[1]);
            Assert.AreEqual("eee3fff3ggg", res[2]);
            Assert.AreEqual("", res[3]);
        }

        public class _Comparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return (x % 2) == (y % 2);
            }

            public int GetHashCode(int obj)
            {
                return obj % 2;
            }
        }

        [TestMethod]
        public void Comparer()
        {
            var e1 = new[] { 1, 2, 3, 4 }.Select(i => i);
            var e2 = new[] { "a", "b", "cc", "dd", "eee", "fff", "ggg", "hhhhh" }.Select(x => x);
            var asGroupJoin =
                e1.GroupJoin(
                    e2,
                    a => a,
                    b => b.Length,
                    (item, g) => string.Join(item.ToString(), g.ToArray()),
                    new _Comparer()
                );

            Assert.IsTrue(asGroupJoin.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in asGroupJoin)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual("a1b1eee1fff1ggg1hhhhh", res[0]);
            Assert.AreEqual("cc2dd", res[1]);
            Assert.AreEqual("a3b3eee3fff3ggg3hhhhh", res[2]);
            Assert.AreEqual("cc4dd", res[3]);
        }

        [TestMethod]
        public void Chaining_Default1()
        {
            var defaultSkip =
                new List<Type>
                {
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                };

            // default
            {
                var skip = new List<Type>();
                skip.AddRange(defaultSkip);
                skip.AddRange(Helper.AllEnumerables().Where((i, ix) => ix % 2 == 0).AsEnumerable());

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "fizz", "bar", "bazz" },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { 3, 4, 5 },
                            res => 
                            {
                                Assert.AreEqual(4, res.Count);
                                Assert.AreEqual(""3"", res[0]);
                                Assert.AreEqual(""4"", res[1]);
                                Assert.AreEqual(""3"", res[2]);
                                Assert.AreEqual(""4"", res[3]);
                            },
                            ""(a, b) => a.GroupJoin(b, s => s.Length, t => t, (o, i) => string.Join(o.ToString(), i.AsEnumerable()))"",        
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
                    skip.ToArray()
                );
            }
        }

        [TestMethod]
        public void Chaining_Default2()
        {
            var defaultSkip =
                new List<Type>
                {
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                };

            // default
            {
                var skip = new List<Type>();
                skip.AddRange(defaultSkip);
                skip.AddRange(Helper.AllEnumerables().Where((i, ix) => ix % 2 == 1).AsEnumerable());

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "fizz", "bar", "bazz" },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { 3, 4, 5 },
                            res => 
                            {
                                Assert.AreEqual(4, res.Count);
                                Assert.AreEqual(""3"", res[0]);
                                Assert.AreEqual(""4"", res[1]);
                                Assert.AreEqual(""3"", res[2]);
                                Assert.AreEqual(""4"", res[3]);
                            },
                            ""(a, b) => a.GroupJoin(b, s => s.Length, t => t, (o, i) => string.Join(o.ToString(), i.AsEnumerable()))"",        
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
                    skip.ToArray()
                );
            }
        }

        [TestMethod]
        public void Chaining_Specific1()
        {
            var defaultSkip =
               new List<Type>
               {
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
               };

            // specific
            {
                var skip = new List<Type>();
                skip.AddRange(defaultSkip);
                skip.AddRange(Helper.AllEnumerables().Where((i, ix) => ix % 2 == 0).AsEnumerable());

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3, 5 },
                    @"a => 
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { ""foo"", ""fizz"", ""world"" },
                            res => 
                            {
                                Assert.AreEqual(4, res.Count); 
                                Assert.AreEqual(""foo1world"", res[0]); 
                                Assert.AreEqual(""fizz"", res[1]); 
                                Assert.AreEqual(""foo3world"", res[2]); 
                                Assert.AreEqual(""foo5world"", res[3]); 
                            },
                            ""(a, b) => a.GroupJoin(b, f => f, s => s.Length, (k, g) => string.Join(k.ToString(), g.AsEnumerable()), new GroupJoinTests._Comparer())"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )
                    ",
                    skip.ToArray()
                );
            }
        }

        [TestMethod]
        public void Chaining_Specific2()
        {
            var defaultSkip =
               new List<Type>
               {
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
               };

            // specific
            {
                var skip = new List<Type>();
                skip.AddRange(defaultSkip);
                skip.AddRange(Helper.AllEnumerables().Where((i, ix) => ix % 2 == 1).AsEnumerable());

                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3, 5 },
                    @"a => 
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { ""foo"", ""fizz"", ""world"" },
                            res => 
                            {
                                Assert.AreEqual(4, res.Count); 
                                Assert.AreEqual(""foo1world"", res[0]); 
                                Assert.AreEqual(""fizz"", res[1]); 
                                Assert.AreEqual(""foo3world"", res[2]); 
                                Assert.AreEqual(""foo5world"", res[3]); 
                            },
                            ""(a, b) => a.GroupJoin(b, f => f, s => s.Length, (k, g) => string.Join(k.ToString(), g.AsEnumerable()), new GroupJoinTests._Comparer())"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )
                    ",
                    skip.ToArray()
                );
            }
        }

        public class _IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
        }

        public class _GroupingComparer<T> : IEqualityComparer<GroupingEnumerable<T, T>>
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
                Assert.IsFalse(empty.GroupJoin(empty, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).Any());
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsFalse(empty.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(range, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(empty.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(empty.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).Any());

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.GroupJoin(b, x => x, x => x, (x, y) => x).Any());
                        Assert.IsFalse(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).Any());        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.GroupJoin(empty, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).Any());
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsFalse(emptyOrdered.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(range, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(emptyOrdered.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).Any());
                Assert.IsFalse(emptyOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).Any());

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.GroupJoin(b, x => x, x => x, (x, y) => x).Any());
                        Assert.IsFalse(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).Any());        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.GroupJoin(empty, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(empty, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var groupBySpecificInt = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(groupByDefault.GroupJoin(groupBySpecificInt, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(groupBySpecificInt, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(range, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(range, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(repeat, x => x.Key, x => x.Length, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(repeat, x => x.Key, x => x.Length, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { int.MaxValue },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x.Key, x => x, (x, y) => x).SequenceEqual(a, new GroupJoinTests._GroupingComparer<int>()));
                        Assert.IsTrue(a.GroupJoin(b, x => x.Key, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a, new GroupJoinTests._GroupingComparer<int>()));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.GroupJoin(empty, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(empty, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(emptyOrdered, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(emptyOrdered, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(groupByDefault, x => x.Key.Length, x => x.Key, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(groupByDefault, x => x.Key.Length, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(lookup, x => x.Key.Length, x => x.Key, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(lookup, x => x.Key.Length, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(range, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(range, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(repeat, x => x.Key.Length, x => x.Length, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(repeat, x => x.Key.Length, x => x.Length, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(reverseRange, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(reverseRange, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemDefault, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemDefault, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemSpecific, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemSpecific, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemDefaultOrdered, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemDefaultOrdered, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemSpecificOrdered, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.GroupJoin(oneItemSpecificOrdered, x => x.Key.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { int.MaxValue },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x.Key.Length, x => x, (x, y) => x).SequenceEqual(a, new GroupJoinTests._GroupingComparer<string>()));
                        Assert.IsTrue(a.GroupJoin(b, x => x.Key.Length, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a, new GroupJoinTests._GroupingComparer<string>()));      

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // lookup
            {
                Assert.IsTrue(lookup.GroupJoin(empty, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(empty, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                var groupBySpecificInt = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(lookup.GroupJoin(groupBySpecificInt, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(groupBySpecificInt, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(range, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(range, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(repeat, x => x.Key, x => x.Length, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(repeat, x => x.Key, x => x.Length, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(lookup, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { int.MaxValue },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x.Key, x => x, (x, y) => x).SequenceEqual(a, new GroupJoinTests._GroupingComparer<int>()));
                        Assert.IsTrue(a.GroupJoin(b, x => x.Key, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a, new GroupJoinTests._GroupingComparer<int>()));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // range
            {
                Assert.IsTrue(range.GroupJoin(empty, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(range.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(range, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(range.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).SequenceEqual(range));
                Assert.IsTrue(range.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(range));

                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                Assert.IsTrue(repeat.GroupJoin(empty, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(empty, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(emptyOrdered, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(emptyOrdered, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(groupByDefault, x => x.Length, x => x.Key, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(groupByDefault, x => x.Length, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(repeat.GroupJoin(groupBySpecificInt, x => x.Length, x => x.Key, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(groupBySpecificInt, x => x.Length, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(lookup, x => x.Length, x => x.Key, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(lookup, x => x.Length, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(range, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(range, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(repeat.GroupJoin(repeatInt, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(repeatInt, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(reverseRange, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(reverseRange, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemDefault, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemDefault, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemSpecific, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemSpecific, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemDefaultOrdered, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemDefaultOrdered, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemSpecificOrdered, x => x.Length, x => x, (x, y) => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.GroupJoin(oneItemSpecificOrdered, x => x.Length, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(repeat));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x.Length, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x.Length, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.GroupJoin(empty, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(reverseRange.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(range, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(reverseRange.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(reverseRange));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.GroupJoin(empty, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(oneItemDefault.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(range, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemDefault.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefault));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.GroupJoin(empty, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(oneItemSpecific.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(range, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemSpecific.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecific));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(empty, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(range, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));        

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(empty, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(groupBySpecificInt, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(range, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(repeatInt, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(repeatInt, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(reverseRange, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x).SequenceEqual(a));
                        Assert.IsTrue(a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()).SequenceEqual(a));        

                        return Helper.NoCallValue;
                      }",
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
        public void Errors_Default()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[0],
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new string[0],
                            res => { },
                            @""(a, b) =>
                               {
                                Func<string, string> outerKey = str => str;
                                Func<string, string> innerKey = str => str;
                                Func<string, GroupedEnumerable<string, string>, string> result = (k, g) => k;

                                try
                                {
                                    a.GroupJoin(b, default(Func<string, string>), innerKey, result);
                                    Assert.Fail();
                                }
                                catch(ArgumentNullException exc)
                                {
                                    Assert.AreEqual(""""outerKeySelector"""", exc.ParamName);
                                }

                                try
                                {
                                    a.GroupJoin(b, outerKey, default(Func<string, string>), result);
                                    Assert.Fail();
                                }
                                catch(ArgumentNullException exc)
                                {
                                    Assert.AreEqual(""""innerKeySelector"""", exc.ParamName);
                                }

                                try
                                {
                                    a.GroupJoin(b, outerKey, innerKey, default(Func<string, GroupedEnumerable<string, string>, string>));
                                    Assert.Fail();
                                }
                                catch(ArgumentNullException exc)
                                {
                                    Assert.AreEqual(""""resultSelector"""", exc.ParamName);
                                }

                                return Helper.NoCallValue;
                               }"",
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Specific()
        {
            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[0],
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new string[0],
                            res => { },
                            @""(a, b) =>
                               {
                                Func<string, string> outerKey = str => str;
                                Func<string, string> innerKey = str => str;
                                Func<string, GroupedEnumerable<string, string>, string> result = (k, g) => k;

                                try
                                {
                                    a.GroupJoin(b, default(Func<string, string>), innerKey, result, StringComparer.InvariantCultureIgnoreCase);
                                    Assert.Fail();
                                }
                                catch(ArgumentNullException exc)
                                {
                                    Assert.AreEqual(""""outerKeySelector"""", exc.ParamName);
                                }

                                try
                                {
                                    a.GroupJoin(b, outerKey, default(Func<string, string>), result, StringComparer.InvariantCultureIgnoreCase);
                                    Assert.Fail();
                                }
                                catch(ArgumentNullException exc)
                                {
                                    Assert.AreEqual(""""innerKeySelector"""", exc.ParamName);
                                }

                                try
                                {
                                    a.GroupJoin(b, outerKey, innerKey, default(Func<string, GroupedEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase);
                                    Assert.Fail();
                                }
                                catch(ArgumentNullException exc)
                                {
                                    Assert.AreEqual(""""resultSelector"""", exc.ParamName);
                                }

                                return Helper.NoCallValue;
                               }"",
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird_Default()
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
                try { empty.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                try { emptyOrdered.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                try { groupByDefault.GroupJoin(empty, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(empty, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(empty, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrdered, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefault, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefault, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecific, x => x.Key, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecific, x => x.Key, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookup, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookup, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookup, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(range, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(range, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(range, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeat, default(Func<GroupingEnumerable<int, int>, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeat, x => x.Key, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeat, x => x.Key, x => x.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRange, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRange, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRange, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefault, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefault, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecific, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecific, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrdered, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrdered, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupBySpecific
            {
                try { groupBySpecific.GroupJoin(empty, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(empty, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(empty, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrdered, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrdered, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrdered, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefault, default(Func<GroupingEnumerable<string, string>, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefault, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefault, x => x.Key.Length, x => x.Key, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecific, default(Func<GroupingEnumerable<string, string>, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecific, x => x.Key.Length, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecific, x => x.Key.Length, x => x.Key.Length, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookup, default(Func<GroupingEnumerable<string, string>, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookup, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookup, x => x.Key.Length, x => x.Key, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(range, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(range, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(range, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeat, default(Func<GroupingEnumerable<string, string>, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeat, x => x.Key.Length, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeat, x => x.Key.Length, x => x.Length, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRange, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRange, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRange, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefault, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefault, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefault, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecific, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecific, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecific, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrdered, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrdered, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrdered, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrdered, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrdered, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrdered, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key.Length, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // lookup
            {
                try { lookup.GroupJoin(empty, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(empty, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(empty, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrdered, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefault, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefault, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecific, x => x.Key, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecific, x => x.Key, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(lookup, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(lookup, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(lookup, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(range, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(range, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(range, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(repeat, default(Func<GroupingEnumerable<int, int>, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(repeat, x => x.Key, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(repeat, x => x.Key, x => x.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(reverseRange, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(reverseRange, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(reverseRange, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefault, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefault, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecific, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecific, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrdered, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrdered, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // range
            {
                try { range.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                try { repeat.GroupJoin(empty, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(empty, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(empty, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrdered, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrdered, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrdered, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefault, default(Func<string, string>), x => x.Key.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefault, x => x, x => x.Key.ToString(), default(Func<string, GroupedEnumerable<string, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecific, default(Func<string, string>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecific, x => x, x => x.Key, default(Func<string, GroupedEnumerable<string, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(lookup, default(Func<string, string>), x => x.Key.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(lookup, x => x, x => x.Key.ToString(), default(Func<string, GroupedEnumerable<string, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(range, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(range, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(range, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(repeat, default(Func<string, string>), x => x.Length.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(repeat, x => x, default(Func<string, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(repeat, x => x, x => x, default(Func<string, GroupedEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(reverseRange, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(reverseRange, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(reverseRange, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefault, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefault, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefault, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecific, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecific, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecific, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrdered, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrdered, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrdered, default(Func<string, string>), x => x.ToString(), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrdered, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { "" },
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<string, string>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<string, string>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<string, GroupedEnumerable<string, string>, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                try { reverseRange.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                try { oneItemDefault.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird_Specific()
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
                try { empty.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                try { emptyOrdered.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                try { groupByDefault.GroupJoin(empty, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(empty, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(empty, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrdered, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefault, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefault, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecific, x => x.Key, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecific, x => x.Key, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookup, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookup, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookup, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(range, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(range, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(range, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeat, default(Func<GroupingEnumerable<int, int>, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeat, x => x.Key, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeat, x => x.Key, x => x.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRange, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRange, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRange, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefault, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefault, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecific, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecific, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrdered, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrdered, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupBySpecific
            {
                try { groupBySpecific.GroupJoin(empty, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(empty, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(empty, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrdered, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrdered, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrdered, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefault, default(Func<GroupingEnumerable<string, string>, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefault, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefault, x => x.Key.Length, x => x.Key, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecific, default(Func<GroupingEnumerable<string, string>, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecific, x => x.Key.Length, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecific, x => x.Key.Length, x => x.Key.Length, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookup, default(Func<GroupingEnumerable<string, string>, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookup, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookup, x => x.Key.Length, x => x.Key, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(range, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(range, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(range, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeat, default(Func<GroupingEnumerable<string, string>, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeat, x => x.Key.Length, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeat, x => x.Key.Length, x => x.Length, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRange, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRange, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRange, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefault, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefault, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefault, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecific, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecific, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecific, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrdered, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrdered, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrdered, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrdered, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrdered, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrdered, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<GroupingEnumerable<string, string>, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key.Length, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key.Length, x => x, default(Func<GroupingEnumerable<string, string>, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // lookup
            {
                try { lookup.GroupJoin(empty, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(empty, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(empty, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrdered, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefault, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefault, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecific, x => x.Key, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecific, x => x.Key, x => x.Key.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(lookup, default(Func<GroupingEnumerable<int, int>, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(lookup, x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(lookup, x => x.Key, x => x.Key, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(range, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(range, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(range, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(repeat, default(Func<GroupingEnumerable<int, int>, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(repeat, x => x.Key, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(repeat, x => x.Key, x => x.Length, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(reverseRange, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(reverseRange, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(reverseRange, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefault, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefault, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefault, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecific, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecific, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecific, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrdered, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrdered, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrdered, x => x.Key, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<GroupingEnumerable<int, int>, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, default(Func<GroupingEnumerable<int, int>, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // range
            {
                try { range.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                try { repeat.GroupJoin(empty, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(empty, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(empty, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrdered, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrdered, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrdered, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefault, default(Func<string, string>), x => x.Key.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefault, x => x, x => x.Key.ToString(), default(Func<string, GroupedEnumerable<string, GroupingEnumerable<int, int>>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecific, default(Func<string, string>), x => x.Key, (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecific, x => x, x => x.Key, default(Func<string, GroupedEnumerable<string, GroupingEnumerable<string, string>>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(lookup, default(Func<string, string>), x => x.Key.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(lookup, x => x, x => x.Key.ToString(), default(Func<string, GroupedEnumerable<string, GroupingEnumerable<int, int>>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(range, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(range, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(range, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(repeat, default(Func<string, string>), x => x.Length.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(repeat, x => x, default(Func<string, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(repeat, x => x, x => x, default(Func<string, GroupedEnumerable<string, string>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(reverseRange, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(reverseRange, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(reverseRange, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefault, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefault, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefault, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecific, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecific, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecific, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrdered, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrdered, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrdered, default(Func<string, string>), x => x.ToString(), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrdered, x => x, x => x.ToString(), default(Func<string, GroupedEnumerable<string, int>, int>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { "" },
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<string, string>), x => x, (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<string, string>), (x, y) => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<string, GroupedEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                try { reverseRange.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                try { oneItemDefault.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.GroupJoin(empty, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(empty, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(empty, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefault, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefault, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecific, default(Func<int, int>), x => x.Key.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecific, x => x, x => x.Key.Length, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<string, string>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookup, default(Func<int, int>), x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookup, x => x, default(Func<GroupingEnumerable<int, int>, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookup, x => x, x => x.Key, default(Func<int, GroupedEnumerable<int, GroupingEnumerable<int, int>>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(range, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(range, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(range, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeat, default(Func<int, int>), x => x.Length, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeat, x => x, default(Func<string, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeat, x => x, x => x.Length, default(Func<int, GroupedEnumerable<int, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRange, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRange, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRange, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefault, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefault, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefault, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecific, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecific, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecific, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, default(Func<int, int>), x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, x => x, default(Func<int, int>), (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrdered, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[1],
                    res => { },
                    @"(a, b) =>
                      {
            
                        try { a.GroupJoin(b, default(Func<int, int>), x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, default(Func<int, int>), (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, default(Func<int, GroupedEnumerable<int, int>, int>), new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Default()
        {
            // default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Helper.ForEachEnumerableExpression(
                            a,
                            new string[0],
                            res => { },
                            @""(a, b) =>
                               {
                                Func<string, string> outerKey = str => str;
                                Func<string, string> innerKey = str => str;
                                Func<string, GroupedEnumerable<string, string>, string> result = (k, g) => k;

                                try
                                {
                                    a.GroupJoin(b, outerKey, innerKey, result);
                                    Assert.Fail();
                                }
                                catch(ArgumentException exc)
                                {
                                    Assert.AreEqual(""""outer"""", exc.ParamName);
                                }

                                try
                                {
                                    b.GroupJoin(a, outerKey, innerKey, result);
                                    Assert.Fail();
                                }
                                catch(ArgumentException exc)
                                {
                                    Assert.AreEqual(""""inner"""", exc.ParamName);
                                }

                                return Helper.NoCallValue;
                               }"",
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        );
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Specific()
        {
            // specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Helper.ForEachEnumerableExpression(
                            a,
                            new string[0],
                            res => { },
                            @""(a, b) =>
                               {
                                Func<string, string> outerKey = str => str;
                                Func<string, string> innerKey = str => str;
                                Func<string, GroupedEnumerable<string, string>, string> result = (k, g) => k;

                                try
                                {
                                    a.GroupJoin(b, outerKey, innerKey, result, StringComparer.InvariantCultureIgnoreCase);
                                    Assert.Fail();
                                }
                                catch(ArgumentException exc)
                                {
                                    Assert.AreEqual(""""outer"""", exc.ParamName);
                                }

                                try
                                {
                                    b.GroupJoin(a, outerKey, innerKey, result, StringComparer.InvariantCultureIgnoreCase);
                                    Assert.Fail();
                                }
                                catch(ArgumentException exc)
                                {
                                    Assert.AreEqual(""""inner"""", exc.ParamName);
                                }

                                return Helper.NoCallValue;
                               }"",
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        );
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
            var lookup = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                var emptyGood = Enumerable.Empty<int>();
                try { empty.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { empty.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { empty.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(empty, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(empty, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { empty.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(empty, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(empty, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { empty.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(empty, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(empty, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { empty.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { empty.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { empty.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { empty.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { empty.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { empty.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { empty.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { empty.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(empty, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(empty, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { emptyOrdered.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { emptyOrdered.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyOrdered.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyOrdered.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyOrdered.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(emptyOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { emptyOrdered.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { emptyOrdered.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { emptyOrdered.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { emptyOrdered.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { emptyOrdered.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { emptyOrdered.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { emptyOrdered.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrdered.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(emptyOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                var emptyGood = Enumerable.Empty<int>();
                try { groupByDefault.GroupJoin(emptyGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { groupByDefault.GroupJoin(emptyOrderedGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(emptyOrderedGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupByDefault.GroupJoin(groupByDefaultGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupByDefaultGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupByDefault.GroupJoin(groupBySpecificGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(groupBySpecificGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupByDefault.GroupJoin(lookupGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(lookupGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { groupByDefault.GroupJoin(rangeGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(rangeGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { groupByDefault.GroupJoin(repeatGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(repeatGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { groupByDefault.GroupJoin(reverseRangeGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(reverseRangeGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(groupByDefault, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupByDefault.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(groupByDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                try { groupByDefault.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(groupByDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { groupByDefault.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemDefaultOrderedGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(groupByDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { groupByDefault.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefault.GroupJoin(oneItemSpecificOrderedGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(groupByDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(groupByDefault, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        
                        try { b.GroupJoin(a, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x.Key, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupBySpecific
            {
                var emptyGood = Enumerable.Empty<int>();
                try { groupBySpecific.GroupJoin(emptyGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { groupBySpecific.GroupJoin(emptyOrderedGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(emptyOrderedGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupBySpecific.GroupJoin(groupByDefaultGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupByDefaultGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupBySpecific.GroupJoin(groupBySpecificGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(groupBySpecificGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupBySpecific.GroupJoin(lookupGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(lookupGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { groupBySpecific.GroupJoin(rangeGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(rangeGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { groupBySpecific.GroupJoin(repeatGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(repeatGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { groupBySpecific.GroupJoin(reverseRangeGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(reverseRangeGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(groupBySpecific, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupBySpecific.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(groupBySpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                try { groupBySpecific.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(groupBySpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { groupBySpecific.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemDefaultOrderedGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(groupBySpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { groupBySpecific.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecific.GroupJoin(oneItemSpecificOrderedGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(groupBySpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(groupBySpecific, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        
                        try { b.GroupJoin(a, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x.Key, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // lookup
            {
                var emptyGood = Enumerable.Empty<int>();
                try { lookup.GroupJoin(emptyGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(emptyGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { lookup.GroupJoin(emptyOrderedGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(emptyOrderedGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { lookup.GroupJoin(groupByDefaultGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(groupByDefaultGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { lookup.GroupJoin(groupBySpecificGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(groupBySpecificGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { lookup.GroupJoin(lookupGood, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(lookupGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { lookup.GroupJoin(rangeGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(rangeGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { lookup.GroupJoin(repeatGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(repeatGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { lookup.GroupJoin(reverseRangeGood, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(reverseRangeGood, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(lookup, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { lookup.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(lookup, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                try { lookup.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(lookup, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { lookup.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(oneItemDefaultOrderedGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(lookup, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { lookup.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookup.GroupJoin(oneItemSpecificOrderedGood, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(lookup, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(lookup, x => x.Key, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x.Key, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        
                        try { b.GroupJoin(a, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x.Key, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // range
            {
                var emptyGood = Enumerable.Empty<int>();
                try { range.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { range.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { range.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(range, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(range, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { range.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(range, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(range, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { range.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(range, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(range, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { range.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { range.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { range.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { range.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { range.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { range.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { range.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { range.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(range, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(range, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                var emptyGood = Enumerable.Empty<int>();
                try { repeat.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { repeat.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { repeat.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(repeat, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(repeat, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { repeat.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(repeat, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(repeat, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { repeat.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(repeat, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(repeat, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { repeat.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { repeat.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { repeat.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { repeat.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { repeat.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { repeat.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { repeat.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeat.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(repeat, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(repeat, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                var emptyGood = Enumerable.Empty<int>();
                try { reverseRange.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { reverseRange.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { reverseRange.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { reverseRange.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { reverseRange.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(reverseRange, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { reverseRange.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { reverseRange.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { reverseRange.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { reverseRange.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { reverseRange.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { reverseRange.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { reverseRange.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRange.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(reverseRange, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefault.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemDefault.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemDefault.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemDefault.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemDefault.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemDefault, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefault.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefault.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemDefault.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefault.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefault.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemDefault.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemDefault.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefault.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemDefault, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecific.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemSpecific.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemSpecific.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemSpecific.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemSpecific.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemSpecific, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecific.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecific.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemSpecific.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecific.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecific.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecific.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemSpecific, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefaultOrdered.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemDefaultOrdered.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemDefaultOrdered.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemDefaultOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefaultOrdered.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefaultOrdered.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemDefaultOrdered.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrdered.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemDefaultOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }



            // oneItemSpecificOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecificOrdered.GroupJoin(emptyGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(emptyOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemSpecificOrdered.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupByDefaultGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefaultGood.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(groupBySpecificGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecificGood.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemSpecificOrdered.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(lookupGood, x => x, x => x.Key, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookupGood.GroupJoin(oneItemSpecificOrdered, x => x.Key, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecificOrdered.GroupJoin(rangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(rangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { rangeGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecificOrdered.GroupJoin(repeatGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(repeatGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemSpecificOrdered.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(reverseRangeGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRangeGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemDefaultOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrderedGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrdered.GroupJoin(oneItemSpecificOrderedGood, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrderedGood.GroupJoin(oneItemSpecificOrdered, x => x, x => x, (x, y) => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {

                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { a.GroupJoin(b, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { b.GroupJoin(a, x => x, x => x, (x, y) => x, new GroupJoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }
    }
}
