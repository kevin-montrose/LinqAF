using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class BoxedTests
    {
        void _Simple(Func<BoxedEnumerable<int>> foo)
        {
            Assert.IsNotNull(foo);
        }

        [TestMethod]
        public void Simple()
        {
            var rand = new Random();

            _Simple(() =>
            {
                var a = Enumerable.Repeat((object)1, 5).Cast<int>();
                var b = Enumerable.Range(0, 2).Concat(Enumerable.Range(2, 4));

                if (rand.Next(1) == 0) return (BoxedEnumerable<int>)a;

                return (BoxedEnumerable<int>)b;
            });
        }

        [TestMethod]
        public void Bridge()
        {
            var rand = new Random();

            _Simple(() =>
            {
                var a = new[] { rand.Next(), rand.Next() };
                var b = Enumerable.Range(0, 2).Concat(Enumerable.Range(2, 4));

                if (rand.Next(1) == 0) return (BoxedEnumerable<int>)a;

                return (BoxedEnumerable<int>)b;
            });
        }

        [TestMethod]
        public void Universal()
        {
            foreach (var e in Helper.AllEnumerables())
            {
                // doesn't make sense
                if (e == typeof(BoxedEnumerable<>)) continue;

                var potentialOps = e.GetMethods(BindingFlags.Public | BindingFlags.Static);

                if (potentialOps.Length == 0) Assert.Fail($"No boxing operator found for {e.Name}");

                var special = new List<MethodInfo>();
                foreach (var p in potentialOps)
                {
                    if (p.IsSpecialName) special.Add(p);
                }

                if (special.Count == 0) Assert.Fail($"No boxing operator found for {e.Name}");

                var @explicit = new List<MethodInfo>();
                foreach (var p in special)
                {
                    if (p.Name == "op_Explicit")
                    {
                        @explicit.Add(p);
                    }
                }

                if (@explicit.Count == 0) Assert.Fail($"No boxing operator found for {e.Name}");

                var toBoxed = new List<MethodInfo>();
                foreach (var p in @explicit)
                {
                    var retType = p.ReturnType;
                    if (!retType.IsGenericType) continue;

                    var gen = Helper.GetGenericTypeDefinition(retType);
                    if (gen == typeof(BoxedEnumerable<>))
                    {
                        toBoxed.Add(p);
                    }
                }

                if (toBoxed.Count != 1) Assert.Fail($"No boxing operator found for {e.Name}");
            }
        }

        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var boxed = (BoxedEnumerable<int>)a;

                    var res = new List<int>();
                    foreach(var item in boxed)
                    {
                        res.Add(item);
                    }

                    Assert.IsTrue(res.SequenceEqual(new [] { 1, 2, 3 }));
                  }",
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
                typeof(int[]),
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
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
                var boxed = (BoxedEnumerable<int>)empty;
                Assert.IsTrue(boxed.SequenceEqual(new int[0]));
            }

            // emptyOrdered
            {
                var boxed = (BoxedEnumerable<int>)emptyOrdered;
                Assert.IsTrue(boxed.SequenceEqual(new int[0]));
            }

            // groupByDefault
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)groupByDefault;
                Assert.IsTrue(boxed.SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<string, string>>)groupBySpecific;
                Assert.IsTrue(boxed.SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
            }

            // lookupDefault
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)lookupDefault;
                Assert.IsTrue(boxed.SequenceEqual(lookupDefault, new _GroupingComparer<int>()));
            }

            // lookupSpecific
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)lookupDefault;
                Assert.IsTrue(boxed.SequenceEqual(lookupSpecific, new _GroupingComparer<int>()));
            }

            // range
            {
                var boxed = (BoxedEnumerable<int>)range;
                Assert.IsTrue(boxed.SequenceEqual(range));
            }

            // repeat
            {
                var boxed = (BoxedEnumerable<string>)repeat;
                Assert.IsTrue(boxed.SequenceEqual(repeat));
            }

            // reverseRange
            {
                var boxed = (BoxedEnumerable<int>)reverseRange;
                Assert.IsTrue(boxed.SequenceEqual(reverseRange));
            }

            // oneItemDefault
            {
                var boxed = (BoxedEnumerable<int>)oneItemDefault;
                Assert.IsTrue(boxed.SequenceEqual(oneItemDefault));
            }

            // oneItemSpecific
            {
                var boxed = (BoxedEnumerable<int>)oneItemSpecific;
                Assert.IsTrue(boxed.SequenceEqual(oneItemSpecific));
            }

            // oneItemDefaultOrdered
            {
                var boxed = (BoxedEnumerable<int>)oneItemDefaultOrdered;
                Assert.IsTrue(boxed.SequenceEqual(oneItemDefaultOrdered));
            }

            // oneItemSpecificOrdered
            {
                var boxed = (BoxedEnumerable<int>)oneItemSpecificOrdered;
                Assert.IsTrue(boxed.SequenceEqual(oneItemSpecificOrdered));
            }
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a =>
                  {
                    var boxed = (BoxedEnumerable<int>)a;

                    try { boxed.ToList(); Assert.Fail(); } catch(ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                  }",
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
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                var boxed = (BoxedEnumerable<int>)empty;
                try { boxed.ToList(); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                var boxed = (BoxedEnumerable<int>)emptyOrdered;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)groupByDefault;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)groupBySpecific;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)lookupDefault;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                var boxed = (BoxedEnumerable<GroupingEnumerable<int, int>>)lookupSpecific;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                var boxed = (BoxedEnumerable<int>)range;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                var boxed = (BoxedEnumerable<int>)repeat;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                var boxed = (BoxedEnumerable<int>)reverseRange;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                var boxed = (BoxedEnumerable<int>)oneItemDefault;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                var boxed = (BoxedEnumerable<int>)oneItemSpecific;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                var boxed = (BoxedEnumerable<int>)oneItemDefaultOrdered;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                var boxed = (BoxedEnumerable<int>)oneItemSpecificOrdered;
                try { boxed.ToList(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
