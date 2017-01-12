using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ResetTests
    {
        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var res1 = new List<int>();
                    var res2 = new List<int>();
                    using(var e = a.GetEnumerator())
                    {
                        while(e.MoveNext())
                        {
                            res1.Add(e.Current);
                        }
    
                        e.Reset();

                        while(e.MoveNext())
                        {
                            res2.Add(e.Current);
                        }
                    }

                    Assert.IsTrue(res1.SequenceEqual(res2));
                  }",
                typeof(int[]),
                typeof(IEnumerable<>),
                typeof(List<>),
                typeof(LinkedList<>),
                typeof(HashSet<>),
                typeof(Dictionary<,>.KeyCollection),
                typeof(Dictionary<,>.ValueCollection),
                typeof(SortedDictionary<,>.KeyCollection),
                typeof(SortedDictionary<,>.ValueCollection),
                typeof(SortedSet<>),
                typeof(Stack<>),
                typeof(Queue<>),
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
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
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = empty.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // emptyOrdered
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = emptyOrdered.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // groupByDefault
            {
                var res1 = new List<GroupingEnumerable<int, int>>();
                var res2 = new List<GroupingEnumerable<int, int>>();
                using (var e = groupByDefault.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                var res1 = new List<GroupingEnumerable<string, string>>();
                var res2 = new List<GroupingEnumerable<string, string>>();
                using (var e = groupBySpecific.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2, new _GroupingComparer<string>()));
            }

            // lookup
            {
                var res1 = new List<GroupingEnumerable<int, int>>();
                var res2 = new List<GroupingEnumerable<int, int>>();
                using (var e = lookup.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2, new _GroupingComparer<int>()));
            }

            // range
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = range.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // repeat
            {
                var res1 = new List<string>();
                var res2 = new List<string>();
                using (var e = repeat.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // reverseRange
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = reverseRange.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // oneItemDefault
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = oneItemDefault.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // oneItemSpecific
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = oneItemSpecific.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // oneItemDefaultOrdered
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = oneItemDefaultOrdered.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }

            // oneItemSpecificOrdered
            {
                var res1 = new List<int>();
                var res2 = new List<int>();
                using (var e = oneItemSpecificOrdered.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        res1.Add(e.Current);
                    }

                    e.Reset();

                    while (e.MoveNext())
                    {
                        res2.Add(e.Current);
                    }
                }

                Assert.IsTrue(res1.SequenceEqual(res2));
            }
        }
    }
}
