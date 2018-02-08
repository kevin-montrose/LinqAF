using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class OrderByTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IOrderBy<,,>), out instOverlaps, out extOverlaps);

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
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IOrderBy<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IOrderBy ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void SimpleAscending()
        {
            // 1 element
            {
                var asOrderBy = new[] { 3 }.OrderBy(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(1, res.Count);
                Assert.AreEqual(3, res[0]);
            }

            // 2 elements
            {
                var asOrderBy = new[] { 3, 2 }.OrderBy(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(2, res.Count);
                Assert.AreEqual(2, res[0]);
                Assert.AreEqual(3, res[1]);
            }

            // 3 elements
            {
                var asOrderBy = new[] { 3, 1, 2 }.OrderBy(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(3, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
            }

            // 4 elements
            {
                var asOrderBy = new[] { 3, 3, 1, 2 }.OrderBy(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(4, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
                Assert.AreEqual(3, res[3]);
            }
        }

        [TestMethod]
        public void SimpleDescending()
        {
            // 1 element
            {
                var asOrderBy = new[] { 3 }.OrderByDescending(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(1, res.Count);
                Assert.AreEqual(3, res[0]);
            }

            // 2 elements
            {
                var asOrderBy = new[] { 3, 2 }.OrderByDescending(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(2, res.Count);
                Assert.AreEqual(3, res[0]);
                Assert.AreEqual(2, res[1]);
            }

            // 3 elements
            {
                var asOrderBy = new[] { 3, 1, 2 }.OrderByDescending(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(3, res.Count);
                Assert.AreEqual(3, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(1, res[2]);
            }

            // 4 elements
            {
                var asOrderBy = new[] { 3, 3, 1, 2 }.OrderByDescending(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(4, res.Count);
                Assert.AreEqual(3, res[0]);
                Assert.AreEqual(3, res[1]);
                Assert.AreEqual(2, res[2]);
                Assert.AreEqual(1, res[3]);
            }
        }

        [TestMethod]
        public void Empty()
        {
            // ascending
            {
                var asOrderBy = Enumerable.Empty<int>().OrderBy(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(0, res.Count);
            }

            // descending
            {
                var asOrderBy = Enumerable.Empty<int>().OrderByDescending(x => x);
                Assert.IsTrue(asOrderBy.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asOrderBy)
                {
                    res.Add(item);
                }

                Assert.AreEqual(0, res.Count);
            }
        }

        [TestMethod]
        public void Fuzzy()
        {
            var rand = new Random(20170524);
            for (var i = 0; i < 1000; i++)
            {
                var e = Enumerable.Range(0, rand.Next(100)).Select(x => rand.Next()).ToList();
                var asOrderBy = e.OrderBy(x => x);

                var sortedArray = e.ToArray();
                Array.Sort(sortedArray);

                Assert.IsTrue(sortedArray.SequenceEqual(asOrderBy));
            }

            for (var i = 0; i < 1000; i++)
            {
                var e = Enumerable.Range(0, rand.Next(100)).Select(x => rand.Next()).ToList();
                var asOrderBy = e.OrderByDescending(x => x);

                var sortedArray = e.ToArray();
                Array.Sort(sortedArray);
                Array.Reverse(sortedArray);

                Assert.IsTrue(sortedArray.SequenceEqual(asOrderBy));
            }
        }

        public class _Comparer : IComparer<string>
        {
            public int Compare(string x, string y) => x.Length.CompareTo(y.Length);
        }

        [TestMethod]
        public void Chaining()
        {
            // ascending, default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 8, 1, 4, 3, 6 },
                    res =>
                    {
                        Assert.AreEqual(5, res.Count);
                        Assert.AreEqual(1, res[0]);
                        Assert.AreEqual(3, res[1]);
                        Assert.AreEqual(4, res[2]);
                        Assert.AreEqual(6, res[3]);
                        Assert.AreEqual(8, res[4]);
                    },
                    "(__, a) => a.OrderBy(_ => _)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );

            }

            // descending, default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 8, 1, 4, 3, 6 },
                    res =>
                    {
                        Assert.AreEqual(5, res.Count);
                        Assert.AreEqual(8, res[0]);
                        Assert.AreEqual(6, res[1]);
                        Assert.AreEqual(4, res[2]);
                        Assert.AreEqual(3, res[3]);
                        Assert.AreEqual(1, res[4]);
                    },
                    "(__, a) => a.OrderByDescending(_ => _)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // ascending, specific
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "foo", "buzz", "bar", "aaaaa" },
                    res =>
                    {
                        Assert.AreEqual(5, res.Count);
                        Assert.AreEqual("foo", res[0]);
                        Assert.AreEqual("bar", res[1]);
                        Assert.AreEqual("buzz", res[2]);
                        Assert.AreEqual("hello", res[3]);
                        Assert.AreEqual("aaaaa", res[4]);
                    },
                    "(__, a) => a.OrderBy(_ => _, new OrderByTests._Comparer())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // descending, specific
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "foo", "buzz", "bar", "aaaaa" },
                    res =>
                    {
                        Assert.AreEqual(5, res.Count);
                        Assert.AreEqual("hello", res[0]);
                        Assert.AreEqual("aaaaa", res[1]);
                        Assert.AreEqual("buzz", res[2]);
                        Assert.AreEqual("foo", res[3]);
                        Assert.AreEqual("bar", res[4]);
                    },
                    "(__, a) => a.OrderByDescending(_ => _, new OrderByTests._Comparer())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        class _IntComparer : IComparer<int>, IEqualityComparer<int>
        {
            public int Compare(int x, int y) => Comparer<int>.Default.Compare(x, y);

            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
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
                Assert.IsTrue(empty.OrderBy(x => x).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.OrderBy(x => x, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.OrderByDescending(x => x).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(new int[0]));
            }

            // emptyOrdered
            {
                Assert.IsTrue(emptyOrdered.OrderBy(x => x).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.OrderBy(x => x, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.OrderByDescending(x => x).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(new int[0]));
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.OrderBy(x => x.Key).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.OrderBy(x => x.Key, new _IntComparer()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.OrderByDescending(x => x.Key).SequenceEqual(groupByDefault.Reverse(), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.OrderByDescending(x => x.Key, new _IntComparer()).SequenceEqual(groupByDefault.Reverse(), new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.OrderBy(x => x.Key).SequenceEqual(new[] { groupBySpecific.ElementAt(2), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1) }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.OrderBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { groupBySpecific.ElementAt(2), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1) }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.OrderByDescending(x => x.Key).SequenceEqual(new[] { groupBySpecific.ElementAt(2), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1) }.Reverse(), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.OrderByDescending(x => x.Key, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { groupBySpecific.ElementAt(2), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1) }.Reverse(), new _GroupingComparer<string>()));
            }

            // lookupDefault
            {
                Assert.IsTrue(lookupDefault.OrderBy(x => x.Key).SequenceEqual(lookupDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(lookupDefault.OrderBy(x => x.Key, new _IntComparer()).SequenceEqual(lookupDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(lookupDefault.OrderByDescending(x => x.Key).SequenceEqual(lookupDefault.Reverse(), new _GroupingComparer<int>()));
                Assert.IsTrue(lookupDefault.OrderByDescending(x => x.Key, new _IntComparer()).SequenceEqual(lookupDefault.Reverse(), new _GroupingComparer<int>()));
            }

            // lookupSpecific
            {
                Assert.IsTrue(lookupSpecific.OrderBy(x => x.Key).SequenceEqual(lookupDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(lookupSpecific.OrderBy(x => x.Key, new _IntComparer()).SequenceEqual(lookupDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(lookupSpecific.OrderByDescending(x => x.Key).SequenceEqual(lookupDefault.Reverse(), new _GroupingComparer<int>()));
                Assert.IsTrue(lookupSpecific.OrderByDescending(x => x.Key, new _IntComparer()).SequenceEqual(lookupDefault.Reverse(), new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.OrderBy(x => x).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.OrderBy(x => x, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.OrderByDescending(x => x).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(range.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            }

            // repeat
            {
                Assert.IsTrue(repeat.OrderBy(x => x).SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.OrderBy(x => x, new _Comparer()).SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.OrderByDescending(x => x).SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.OrderByDescending(x => x, new _Comparer()).SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.OrderBy(x => x).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(reverseRange.OrderBy(x => x, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(reverseRange.OrderByDescending(x => x).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.OrderBy(x => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.OrderBy(x => x, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.OrderByDescending(x => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(oneItemDefault));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.OrderBy(x => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.OrderBy(x => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.OrderByDescending(x => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(oneItemSpecific));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.OrderBy(x => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.OrderBy(x => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.OrderByDescending(x => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.OrderBy(x => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.OrderBy(x => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.OrderByDescending(x => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.OrderByDescending(x => x, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // ascending, default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new int[0],
                    @"a =>
                      {
                        try
                        {
                            a.OrderBy(default(Func<int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // descending, default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new int[0],
                    @"a =>
                      {
                        try
                        {
                            a.OrderByDescending(default(Func<int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // ascending, specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[0],
                    @"a =>
                      {
                        try
                        {
                            a.OrderBy(default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // descending, specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[0],
                    @"a =>
                      {
                        try
                        {
                            a.OrderByDescending(default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""keySelector"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird()
        {
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

            // groupByDefault
            {
                try { groupByDefault.OrderBy(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.OrderBy(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.OrderByDescending(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupByDefault.OrderByDescending(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.OrderBy(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.OrderBy(default(Func<GroupingEnumerable<string, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.OrderByDescending(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { groupBySpecific.OrderByDescending(default(Func<GroupingEnumerable<string, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.OrderBy(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.OrderBy(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.OrderByDescending(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupDefault.OrderByDescending(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }
            
            // lookupSpecific
            {
                try { lookupSpecific.OrderBy(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.OrderBy(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.OrderByDescending(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { lookupSpecific.OrderByDescending(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // range
            {
                try { range.OrderBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.OrderBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.OrderByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { range.OrderByDescending(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.OrderBy(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.OrderBy(default(Func<string, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.OrderByDescending(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { repeat.OrderByDescending(default(Func<string, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.OrderBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.OrderBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.OrderByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { reverseRange.OrderByDescending(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.OrderBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.OrderBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.OrderByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefault.OrderByDescending(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.OrderBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.OrderBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.OrderByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecific.OrderByDescending(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.OrderBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.OrderBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.OrderByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.OrderByDescending(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.OrderBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.OrderBy(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.OrderByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.OrderByDescending(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            // ascending, default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;

                        try
                        {
                            a.OrderBy(key);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // descending, default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;

                        try
                        {
                            a.OrderByDescending(key);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // ascending, specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;

                        try
                        {
                            a.OrderBy(key, StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // descending, specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      {
                        Func<string, string> key = str => str;

                        try
                        {
                            a.OrderByDescending(key, StringComparer.InvariantCultureIgnoreCase);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""source"", exc.ParamName);
                        }
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Weird()
        {
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

            // groupByDefault
            {
                try { groupByDefault.OrderBy(x => x.Key); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.OrderBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.OrderByDescending(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.OrderByDescending(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.OrderBy(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.OrderBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.OrderByDescending(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.OrderByDescending(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.OrderBy(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.OrderBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.OrderByDescending(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.OrderByDescending(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.OrderBy(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.OrderBy(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.OrderByDescending(x => x.Key); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.OrderByDescending(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.OrderBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.OrderBy(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.OrderByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.OrderByDescending(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
