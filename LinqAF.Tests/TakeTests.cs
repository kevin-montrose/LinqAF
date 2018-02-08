using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinqAF;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class TakeTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.ITake<,,>), out instOverlaps, out extOverlaps);

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
        public void QueueImpl()
        {
            // basic
            {
                var queue = new Impl.StructQueue<string>();
                queue.Initialize();

                queue.Enqueue("hello");
                queue.Enqueue("world");
                queue.Enqueue("fizz");
                queue.Enqueue("buzz");

                Assert.AreEqual(4, queue.Count);
                Assert.AreEqual("hello", queue.Dequeue());
                Assert.AreEqual(3, queue.Count);
                Assert.AreEqual("world", queue.Dequeue());
                Assert.AreEqual(2, queue.Count);
                Assert.AreEqual("fizz", queue.Dequeue());
                Assert.AreEqual(1, queue.Count);
                Assert.AreEqual("buzz", queue.Dequeue());
                Assert.AreEqual(0, queue.Count);
            }

            // grow
            {
                var queue = new Impl.StructQueue<string>();
                queue.Initialize(2);

                queue.Enqueue("hello");
                queue.Enqueue("world");
                queue.Enqueue("fizz");

                Assert.AreEqual(3, queue.Count);
                Assert.AreEqual("hello", queue.Dequeue());
                Assert.AreEqual(2, queue.Count);
                Assert.AreEqual("world", queue.Dequeue());
                Assert.AreEqual(1, queue.Count);
                Assert.AreEqual("fizz", queue.Dequeue());
                Assert.AreEqual(0, queue.Count);
            }

            // complex
            {
                var queue = new Impl.StructQueue<string>();
                queue.Initialize(2);

                queue.Enqueue("hello");
                Assert.AreEqual(1, queue.Count);
                Assert.AreEqual("hello", queue.Dequeue());
                Assert.AreEqual(0, queue.Count);

                queue.Enqueue("world");
                Assert.AreEqual(1, queue.Count);
                queue.Enqueue("fizz");
                Assert.AreEqual(2, queue.Count);
                queue.Enqueue("buzz");
                Assert.AreEqual(3, queue.Count);

                queue.DequeueAndEnqueue("foo");
                Assert.AreEqual(3, queue.Count);

                Assert.AreEqual("fizz", queue.Dequeue());
                Assert.AreEqual(2, queue.Count);

                Assert.AreEqual("buzz", queue.Dequeue());
                Assert.AreEqual(1, queue.Count);

                for(var i = 0; i < 5; i++)
                {
                    queue.Enqueue("bizz");
                    Assert.AreEqual(i + 1 + 1, queue.Count);
                }

                Assert.AreEqual("foo", queue.Dequeue());
                Assert.AreEqual(5, queue.Count);

                Assert.AreEqual("bizz", queue.Dequeue());
                Assert.AreEqual(4, queue.Count);
                Assert.AreEqual("bizz", queue.Dequeue());
                Assert.AreEqual(3, queue.Count);
                Assert.AreEqual("bizz", queue.Dequeue());
                Assert.AreEqual(2, queue.Count);
                Assert.AreEqual("bizz", queue.Dequeue());
                Assert.AreEqual(1, queue.Count);
                Assert.AreEqual("bizz", queue.Dequeue());
                Assert.AreEqual(0, queue.Count);
            }
        }

        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ITake<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ITake ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            // take
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(1, res.Count);
                        Assert.AreEqual(1, res[0]);
                    },
                    "(_, a) => a.Take(1)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // take while
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual(1, res[0]);
                        Assert.AreEqual(2, res[1]);
                    },
                    "(_, a) => a.TakeWhile(f => f < 3)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // take while indexed
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(1, res.Count);
                        Assert.AreEqual(1, res[0]);
                    },
                    "(__, a) => a.TakeWhile((_, ix) => ix < 1)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // take last
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(1, res.Count);
                        Assert.AreEqual(3, res[0]);
                    },
                    "(_, a) => a.TakeLast(1)",
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
                Assert.IsTrue(empty.Take(1).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.TakeWhile(x => false).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.TakeWhile((x, ix) => false).SequenceEqual(new int[0]));
            }

            // emptyOrdered
            {
                Assert.IsTrue(emptyOrdered.Take(1).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.TakeWhile(x => false).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.TakeWhile((x, ix) => false).SequenceEqual(new int[0]));
            }

            // groupByDefault
            {
                Assert.AreEqual(1, groupByDefault.Take(1).First().Key);
                Assert.AreEqual(1, groupByDefault.TakeWhile(x => x.Key <= 1).First().Key);
                Assert.AreEqual(1, groupByDefault.TakeWhile((x, ix) => x.Key <= 1).First().Key);
            }

            // groupBySpecific
            {
                Assert.AreEqual("hello", groupBySpecific.Take(1).First().Key);
                Assert.AreEqual("hello", groupBySpecific.TakeWhile(x => x.Key == "hello").First().Key);
                Assert.AreEqual("hello", groupBySpecific.TakeWhile((x, ix) => ix <= 0).First().Key);
            }

            // lookupDefault
            {
                Assert.AreEqual(1, lookupDefault.Take(1).First().Key);
                Assert.AreEqual(1, lookupDefault.TakeWhile(x => x.Key <= 1).First().Key);
                Assert.AreEqual(1, lookupDefault.TakeWhile((x, ix) => x.Key <= 1).First().Key);
            }

            // lookupSpecific
            {
                Assert.AreEqual(1, lookupSpecific.Take(1).First().Key);
                Assert.AreEqual(1, lookupSpecific.TakeWhile(x => x.Key <= 1).First().Key);
                Assert.AreEqual(1, lookupSpecific.TakeWhile((x, ix) => x.Key <= 1).First().Key);
            }

            // range
            {
                Assert.IsTrue(range.Take(1).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(range.TakeWhile(x => x <= 1).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(range.TakeWhile((x, ix) => x <= 1).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(range.TakeLast(1).SequenceEqual(new[] { 5 }));
            }

            // repeat
            {
                Assert.IsTrue(repeat.Take(1).SequenceEqual(new[] { "foo", }));
                Assert.IsTrue(repeat.TakeWhile(x => x == "foo").SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.TakeWhile((x, ix) => ix < 1).SequenceEqual(new[] { "foo",  }));
                Assert.IsTrue(repeat.TakeLast(1).SequenceEqual(new[] { "foo" }));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.Take(1).SequenceEqual(new[] { 5 }));
                Assert.IsTrue(reverseRange.TakeWhile(x => x >= 5).SequenceEqual(new[] { 5 }));
                Assert.IsTrue(reverseRange.TakeWhile((x, ix) => ix < 1).SequenceEqual(new[] { 5 }));
                Assert.IsTrue(reverseRange.TakeLast(1).SequenceEqual(new[] { 1 }));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.Take(1).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.TakeWhile(x => x != 1).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.TakeWhile((x, ix) => ix < 1).SequenceEqual(new[] { 0 }));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.Take(1).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.TakeWhile(x => x != 1).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.TakeWhile((x, ix) => ix < 1).SequenceEqual(new[] { 4 }));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.Take(1).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.TakeWhile(x => x != 1).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.TakeWhile((x, ix) => ix < 1).SequenceEqual(new[] { 0 }));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.Take(1).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.TakeWhile(x => x != 1).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.TakeWhile((x, ix) => ix < 1).SequenceEqual(new[] { 4 }));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // take while
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.TakeWhile(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""predicate"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // take while indexed
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello" },
                    @"a => { try { a.TakeWhile(default(Func<string, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""predicate"", exc.ParamName); } }",
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
                try { groupByDefault.TakeWhile(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); }catch(ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.TakeWhile(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.TakeWhile(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.TakeWhile(default(Func<GroupingEnumerable<string, string>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.TakeWhile(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookupDefault.TakeWhile(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.TakeWhile(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookupSpecific.TakeWhile(default(Func<GroupingEnumerable<int, int>, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.TakeWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.TakeWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.TakeWhile(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.TakeWhile(default(Func<string, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.TakeWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.TakeWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.TakeWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.TakeWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.TakeWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.TakeWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.TakeWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.TakeWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.TakeWhile(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.TakeWhile(default(Func<int, int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<double>(
                @"a => { try { a.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<double>(
                @"a => { try { a.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<double>(
                @"a => { try { a.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
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
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // groupByDefault
            {
                try { groupByDefault.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Take(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.TakeWhile(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.TakeWhile((x, ix) => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3 };
            var asTake = e.Take(2);
            Assert.IsTrue(asTake.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTake)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];
            var asTake = e.Take(2);
            Assert.IsTrue(asTake.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTake)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void UnderTake()
        {
            var e = new[] { 1, 2, 3 };
            var asTake = e.Take(-1);
            Assert.IsTrue(asTake.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTake)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void OverTake()
        {
            var e = new[] { 1, 2, 3 };
            var asTake = e.Take(int.MaxValue);
            Assert.IsTrue(asTake.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTake)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }

        [TestMethod]
        public void While()
        {
            var e = new[] { 1, 2, 3, 4, 5 };
            var asTakeWhile = e.TakeWhile(x => x * 2 < 6);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
        }

        [TestMethod]
        public void WhileEmpty()
        {
            var e = new int[0];
            var asTakeWhile = e.TakeWhile(x => x * 2 < 6);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileAll()
        {
            var e = new[] { 1, 2, 3, 4, 5 };
            var asTakeWhile = e.TakeWhile(x => true);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(5, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
            Assert.AreEqual(4, res[3]);
            Assert.AreEqual(5, res[4]);
        }

        [TestMethod]
        public void WhileNone()
        {
            var e = new[] { 1, 2, 3, 4, 5 };
            var asTakeWhile = e.TakeWhile(x => false);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileIndexed()
        {
            var e = new[] { 1, 2, 3, 4, 5 };
            var asTakeWhile = e.TakeWhile((_, ix) => ix < 2);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
        }

        [TestMethod]
        public void WhileIndexedEmpty()
        {
            var e = new int[0];
            var asTakeWhile = e.TakeWhile((_, ix) => ix < 2);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void WhileIndexedAll()
        {
            var e = new[] { 1, 2, 3 };
            var asTakeWhile = e.TakeWhile((_, ix) => true);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }

        [TestMethod]
        public void WhileIndexedNone()
        {
            var e = new[] { 1, 2, 3 };
            var asTakeWhile = e.TakeWhile((_, ix) => false);
            Assert.IsTrue(asTakeWhile.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asTakeWhile)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void Last()
        {
            var es = new int[][] {
                new [] {1, 2, 3, },
                new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
            };

            foreach (var e in es)
            {
                // simple
                {
                    var asTake = e.TakeLast(1);
                    Assert.IsTrue(asTake.GetType().IsValueType);

                    var res = new List<int>();
                    foreach (var item in asTake)
                    {
                        res.Add(item);
                    }

                    Assert.AreEqual(1, res.Count);
                    Assert.AreEqual(e[e.Length - 1], res[0]);
                }

                // all
                {
                    var asTake = e.TakeLast(e.Length);
                    Assert.IsTrue(asTake.GetType().IsValueType);

                    var res = new List<int>();
                    foreach (var item in asTake)
                    {
                        res.Add(item);
                    }

                    Assert.AreEqual(e.Length, res.Count);
                    Assert.IsTrue(e.SequenceEqual(res));
                }

                // over
                {
                    var asTake = e.TakeLast(e.Length + 1);
                    Assert.IsTrue(asTake.GetType().IsValueType);

                    var res = new List<int>();
                    foreach (var item in asTake)
                    {
                        res.Add(item);
                    }

                    Assert.AreEqual(e.Length, res.Count);
                    Assert.IsTrue(e.SequenceEqual(res));
                }

                // zero
                {
                    var asTake = e.TakeLast(0);
                    Assert.IsTrue(asTake.GetType().IsValueType);

                    var res = new List<int>();
                    foreach (var item in asTake)
                    {
                        res.Add(item);
                    }

                    Assert.AreEqual(0, res.Count);
                }

                // negative
                {
                    var asTake = e.TakeLast(-10);
                    Assert.IsTrue(asTake.GetType().IsValueType);

                    var res = new List<int>();
                    foreach (var item in asTake)
                    {
                        res.Add(item);
                    }

                    Assert.AreEqual(0, res.Count);
                }
            }
        }

        [TestMethod]
        public void ChainedTakeLast()
        {
            // under
            {
                var e = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var shouldMatch = e.TakeLast(3).AsEnumerable().TakeLast(2).ToList();
                var actually = e.TakeLast(3).TakeLast(2).ToList();

                Assert.IsTrue(shouldMatch.SequenceEqual(actually));
            }

            // exact
            {
                var e = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var shouldMatch = e.TakeLast(3).AsEnumerable().TakeLast(7).ToList();
                var actually = e.TakeLast(3).TakeLast(7).ToList();

                Assert.IsTrue(shouldMatch.SequenceEqual(actually));
            }

            // over
            {
                var e = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var shouldMatch = e.TakeLast(15).AsEnumerable().TakeLast(3).ToList();
                var actually = e.TakeLast(15).TakeLast(3).ToList();

                Assert.IsTrue(shouldMatch.SequenceEqual(actually));
            }
        }
    }
}