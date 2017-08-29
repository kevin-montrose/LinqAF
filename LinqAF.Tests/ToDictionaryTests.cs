using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelpers;
using System;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class ToDictionaryTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IToDictionary<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IToDictionary ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var dict = new[] { 1, 2, 3 }.ToDictionary(i => i, i => i * 2);

            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual(2, dict[1]);
            Assert.AreEqual(4, dict[2]);
            Assert.AreEqual(6, dict[3]);
        }

        [TestMethod]
        public void Empty()
        {
            var dict = Enumerable.Empty<int>().ToDictionary(i => i, i => i * 2);

            Assert.AreEqual(0, dict.Count);
        }

        class _Comparer : System.Collections.Generic.IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Length == y.Length;

            public int GetHashCode(string obj) => obj.Length;
        }

        class _IntComparer : System.Collections.Generic.IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
        }

        [TestMethod]
        public void Chaining()
        {
            // no element, default
            {
                Func<string, string> key = _ => _ + _;
                foreach(var e in Helper.GetEnumerables(new[] { "foo", "bar", "hello", "world" }))
                {
                    var x = e.ToDictionary(key);
                    Assert.AreEqual(typeof(System.Collections.Generic.Dictionary<string, string>), x.GetType());
                    var y = (System.Collections.Generic.Dictionary<string, string>)x;
                    Assert.AreEqual(4, y.Count);
                    Assert.AreEqual("foo", y["foofoo"]);
                    Assert.AreEqual("bar", y["barbar"]);
                    Assert.AreEqual("hello", y["hellohello"]);
                    Assert.AreEqual("world", y["worldworld"]);
                }
            }

            // element, default
            {
                Func<string, string> key = _ => _;
                Func<string, int> value = _ => _.Length;

                foreach (var e in Helper.GetEnumerables(new[] { "foo", "bar", "hello", "world" }))
                {
                    var x = e.ToDictionary(key, value);
                    Assert.AreEqual(typeof(System.Collections.Generic.Dictionary<string, int>), x.GetType());
                    var y = (System.Collections.Generic.Dictionary<string, int>)x;
                    Assert.AreEqual(4, y.Count);
                    Assert.AreEqual(3, y["foo"]);
                    Assert.AreEqual(3, y["bar"]);
                    Assert.AreEqual(5, y["hello"]);
                    Assert.AreEqual(5, y["world"]);
                }
            }

            // no element, specific
            {
                Func<string, string> key = _ => _ + _;
                foreach (var e in Helper.GetEnumerables(new[] { "foo", "hello" }))
                {
                    var x = e.ToDictionary(key, new _Comparer());
                    Assert.AreEqual(typeof(System.Collections.Generic.Dictionary<string, string>), x.GetType());
                    var y = (System.Collections.Generic.Dictionary<string, string>)x;
                    Assert.AreEqual(2, y.Count);
                    Assert.AreEqual("foo", y["foofoo"]);
                    Assert.AreEqual("foo", y["barbar"]);
                    Assert.AreEqual("hello", y["hellohello"]);
                    Assert.AreEqual("hello", y["worldworld"]);
                }
            }

            // element, specific
            {
                Func<string, string> key = _ => _;
                Func<string, int> value = _ => _.Length;

                foreach (var e in Helper.GetEnumerables(new[] { "foo", "hello" }))
                {
                    var x = e.ToDictionary(key, value, new _Comparer());
                    Assert.AreEqual(typeof(System.Collections.Generic.Dictionary<string, int>), x.GetType());
                    var y = (System.Collections.Generic.Dictionary<string, int>)x;
                    Assert.AreEqual(2, y.Count);
                    Assert.AreEqual(3, y["foo"]);
                    Assert.AreEqual(3, y["bar"]);
                    Assert.AreEqual(5, y["hello"]);
                    Assert.AreEqual(5, y["world"]);
                }
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
            var repeat = Enumerable.Repeat("foo", 1);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                Assert.IsTrue(empty.ToDictionary(x => x).Count == 0);
                Assert.IsTrue(empty.ToDictionary(x => x, new _IntComparer()).Count == 0);
                Assert.IsTrue(empty.ToDictionary(x => x, x => x).Count == 0);
                Assert.IsTrue(empty.ToDictionary(x => x, x => x, new _IntComparer()).Count == 0);
            }

            // emptyOrdered
            {
                Assert.IsTrue(empty.ToDictionary(x => x).Count == 0);
                Assert.IsTrue(empty.ToDictionary(x => x, new _IntComparer()).Count == 0);
                Assert.IsTrue(empty.ToDictionary(x => x, x => x).Count == 0);
                Assert.IsTrue(empty.ToDictionary(x => x, x => x, new _IntComparer()).Count == 0);
            }

            // groupByDefault
            {
                Func<Dictionary<int, GroupingEnumerable<int, int>>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 3) return false;
                        if (dict[1].Count() != 2) return false;
                        if (dict[2].Count() != 2) return false;
                        if (dict[3].Count() != 2) return false;

                        if (!dict[1].SequenceEqual(new int[] { 1, 1 })) return false;
                        if (!dict[2].SequenceEqual(new int[] { 2, 2 })) return false;
                        if (!dict[3].SequenceEqual(new int[] { 3, 3 })) return false;

                        return true;
                    };

                Assert.IsTrue(correct(groupByDefault.ToDictionary(x => x.Key)));
                Assert.IsTrue(correct(groupByDefault.ToDictionary(x => x.Key, new _IntComparer())));
                Assert.IsTrue(correct(groupByDefault.ToDictionary(x => x.Key, x => x)));
                Assert.IsTrue(correct(groupByDefault.ToDictionary(x => x.Key, x => x, new _IntComparer())));
            }

            // groupBySpecific
            {
                Func<Dictionary<string, GroupingEnumerable<string, string>>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 3) return false;
                        if (dict["hello"].Count() != 2) return false;
                        if (dict["world"].Count() != 2) return false;
                        if (dict["foo"].Count() != 2) return false;

                        if (!dict["hello"].SequenceEqual(new [] { "hello", "HELLO" })) return false;
                        if (!dict["world"].SequenceEqual(new [] { "world", "WORLD" })) return false;
                        if (!dict["foo"].SequenceEqual(new [] { "foo", "FOO" })) return false;

                        return true;
                    };

                Assert.IsTrue(correct(groupBySpecific.ToDictionary(x => x.Key)));
                Assert.IsTrue(correct(groupBySpecific.ToDictionary(x => x.Key, StringComparer.OrdinalIgnoreCase)));
                Assert.IsTrue(correct(groupBySpecific.ToDictionary(x => x.Key, x => x)));
                Assert.IsTrue(correct(groupBySpecific.ToDictionary(x => x.Key, x => x, StringComparer.OrdinalIgnoreCase)));
            }

            // lookupDefault
            {
                Func<Dictionary<int, GroupingEnumerable<int, int>>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 3) return false;
                        if (dict[1].Count() != 2) return false;
                        if (dict[2].Count() != 2) return false;
                        if (dict[3].Count() != 2) return false;

                        if (!dict[1].SequenceEqual(new int[] { 1, 1 })) return false;
                        if (!dict[2].SequenceEqual(new int[] { 2, 2 })) return false;
                        if (!dict[3].SequenceEqual(new int[] { 3, 3 })) return false;

                        return true;
                    };

                Assert.IsTrue(correct(lookupDefault.ToDictionary(x => x.Key)));
                Assert.IsTrue(correct(lookupDefault.ToDictionary(x => x.Key, new _IntComparer())));
                Assert.IsTrue(correct(lookupDefault.ToDictionary(x => x.Key, x => x)));
                Assert.IsTrue(correct(lookupDefault.ToDictionary(x => x.Key, x => x, new _IntComparer())));
            }

            // lookupSpecific
            {
                Func<Dictionary<int, GroupingEnumerable<int, int>>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 3) return false;
                        if (dict[1].Count() != 2) return false;
                        if (dict[2].Count() != 2) return false;
                        if (dict[3].Count() != 2) return false;

                        if (!dict[1].SequenceEqual(new int[] { 1, 1 })) return false;
                        if (!dict[2].SequenceEqual(new int[] { 2, 2 })) return false;
                        if (!dict[3].SequenceEqual(new int[] { 3, 3 })) return false;

                        return true;
                    };

                Assert.IsTrue(correct(lookupSpecific.ToDictionary(x => x.Key)));
                Assert.IsTrue(correct(lookupSpecific.ToDictionary(x => x.Key, new _IntComparer())));
                Assert.IsTrue(correct(lookupSpecific.ToDictionary(x => x.Key, x => x)));
                Assert.IsTrue(correct(lookupSpecific.ToDictionary(x => x.Key, x => x, new _IntComparer())));
            }

            // range
            {
                Func<Dictionary<int, int>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 5) return false;

                        if (dict[1] != 1) return false;
                        if (dict[2] != 2) return false;
                        if (dict[3] != 3) return false;
                        if (dict[4] != 4) return false;
                        if (dict[5] != 5) return false;

                        return true;
                    };

                Assert.IsTrue(correct(range.ToDictionary(x => x)));
                Assert.IsTrue(correct(range.ToDictionary(x => x, new _IntComparer())));
                Assert.IsTrue(correct(range.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(range.ToDictionary(x => x, x => x, new _IntComparer())));
            }
            
            // repeat
            {
                Func<Dictionary<string, string>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 1) return false;

                        if (dict["foo"] != "foo") return false;

                        return true;
                    };

                Assert.IsTrue(correct(repeat.ToDictionary(x => x)));
                Assert.IsTrue(correct(repeat.ToDictionary(x => x, StringComparer.OrdinalIgnoreCase)));
                Assert.IsTrue(correct(repeat.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(repeat.ToDictionary(x => x, x => x, StringComparer.OrdinalIgnoreCase)));
            }

            // reverseRange
            {
                Func<Dictionary<int, int>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 5) return false;

                        if (dict[1] != 1) return false;
                        if (dict[2] != 2) return false;
                        if (dict[3] != 3) return false;
                        if (dict[4] != 4) return false;
                        if (dict[5] != 5) return false;

                        return true;
                    };

                Assert.IsTrue(correct(reverseRange.ToDictionary(x => x)));
                Assert.IsTrue(correct(reverseRange.ToDictionary(x => x, new _IntComparer())));
                Assert.IsTrue(correct(reverseRange.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(reverseRange.ToDictionary(x => x, x => x, new _IntComparer())));
            }

            // oneItemDefault
            {
                Func<Dictionary<int, int>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 1) return false;

                        return dict[0] == 0;
                    };

                Assert.IsTrue(correct(oneItemDefault.ToDictionary(x => x)));
                Assert.IsTrue(correct(oneItemDefault.ToDictionary(x => x, new _IntComparer())));
                Assert.IsTrue(correct(oneItemDefault.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(oneItemDefault.ToDictionary(x => x, x => x, new _IntComparer())));
            }

            // oneItemSpecific
            {
                Func<Dictionary<int, int>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 1) return false;

                        return dict[4] == 4;
                    };

                Assert.IsTrue(correct(oneItemSpecific.ToDictionary(x => x)));
                Assert.IsTrue(correct(oneItemSpecific.ToDictionary(x => x, new _IntComparer())));
                Assert.IsTrue(correct(oneItemSpecific.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(oneItemSpecific.ToDictionary(x => x, x => x, new _IntComparer())));
            }

            // oneItemDefaultOrdered
            {
                Func<Dictionary<int, int>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 1) return false;

                        return dict[0] == 0;
                    };

                Assert.IsTrue(correct(oneItemDefaultOrdered.ToDictionary(x => x)));
                Assert.IsTrue(correct(oneItemDefaultOrdered.ToDictionary(x => x, new _IntComparer())));
                Assert.IsTrue(correct(oneItemDefaultOrdered.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(oneItemDefaultOrdered.ToDictionary(x => x, x => x, new _IntComparer())));
            }

            // oneItemSpecificOrdered
            {
                Func<Dictionary<int, int>, bool> correct =
                    dict =>
                    {
                        if (dict.Count != 1) return false;

                        return dict[4] == 4;
                    };

                Assert.IsTrue(correct(oneItemSpecificOrdered.ToDictionary(x => x)));
                Assert.IsTrue(correct(oneItemSpecificOrdered.ToDictionary(x => x, new _IntComparer())));
                Assert.IsTrue(correct(oneItemSpecificOrdered.ToDictionary(x => x, x => x)));
                Assert.IsTrue(correct(oneItemSpecificOrdered.ToDictionary(x => x, x => x, new _IntComparer())));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // no element, default
            {
                foreach (var e in Helper.GetEnumerables(new[] { "foo", "bar", "hello", "world" }))
                {
                    try
                    {
                        e.ToDictionary(default(Func<string, string>));
                        Assert.Fail();
                    }
                    catch (ArgumentNullException exc)
                    {
                        Assert.AreEqual("keySelector", exc.ParamName);
                    }
                }
            }

            // element, default
            {
                foreach (var e in Helper.GetEnumerables(new[] { "foo", "bar", "hello", "world" }))
                {
                    try
                    {
                        e.ToDictionary(default(Func<string, string>), (Func<string, string>)(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentNullException exc)
                    {
                        Assert.AreEqual("keySelector", exc.ParamName);
                    }

                    try
                    {
                        e.ToDictionary((Func<string, string>)(x => x), default(Func<string, string>));
                        Assert.Fail();
                    }
                    catch (ArgumentNullException exc)
                    {
                        Assert.AreEqual("elementSelector", exc.ParamName);
                    }
                }
            }

            // no element, specific
            {
                foreach (var e in Helper.GetEnumerables(new[] { "foo", "bar", "hello", "world" }))
                {
                    try
                    {
                        e.ToDictionary(default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase);
                        Assert.Fail();
                    }
                    catch (ArgumentNullException exc)
                    {
                        Assert.AreEqual("keySelector", exc.ParamName);
                    }
                }
            }

            // element, specific
            {
                foreach (var e in Helper.GetEnumerables(new[] { "foo", "bar", "hello", "world" }))
                {
                    try
                    {
                        e.ToDictionary(default(Func<string, string>), (Func<string, string>)(x => x), StringComparer.InvariantCultureIgnoreCase);
                        Assert.Fail();
                    }
                    catch (ArgumentNullException exc)
                    {
                        Assert.AreEqual("keySelector", exc.ParamName);
                    }

                    try
                    {
                        e.ToDictionary((Func<string, string>)(x => x), default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase);
                        Assert.Fail();
                    }
                    catch (ArgumentNullException exc)
                    {
                        Assert.AreEqual("elementSelector", exc.ParamName);
                    }
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
            var repeat = Enumerable.Repeat("foo", 1);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                // no element, default
                try { empty.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { empty.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { empty.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { empty.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { empty.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { empty.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // emptyOrdered
            {
                // no element, default
                try { emptyOrdered.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { emptyOrdered.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { emptyOrdered.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { emptyOrdered.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { emptyOrdered.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { emptyOrdered.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // groupbydefault
            {
                {
                    // no element, default
                    try { groupByDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // no element, specific
                    try { groupByDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // element, default
                    {
                        try { groupByDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { groupByDefault.ToDictionary(x => x, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                    // element, specific
                    {
                        try { groupByDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { groupByDefault.ToDictionary(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                }
            }

            // groupbyspecific
            {
                {
                    // no element, default
                    try { groupBySpecific.ToDictionary(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // no element, specific
                    try { groupBySpecific.ToDictionary(default(Func<GroupingEnumerable<string, string>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // element, default
                    {
                        try { groupBySpecific.ToDictionary(default(Func<GroupingEnumerable<string, string>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { groupBySpecific.ToDictionary(x => x, default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                    // element, specific
                    {
                        try { groupBySpecific.ToDictionary(default(Func<GroupingEnumerable<string, string>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { groupBySpecific.ToDictionary(x => x.Key, default(Func<GroupingEnumerable<string, string>, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                }
            }

            // lookupDefault
            {
                {
                    // no element, default
                    try { lookupDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // no element, specific
                    try { lookupDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // element, default
                    {
                        try { lookupDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { lookupDefault.ToDictionary(x => x, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                    // element, specific
                    {
                        try { lookupDefault.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { lookupDefault.ToDictionary(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                }
            }

            // lookupSpecific
            {
                {
                    // no element, default
                    try { lookupSpecific.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // no element, specific
                    try { lookupSpecific.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    // element, default
                    {
                        try { lookupSpecific.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { lookupSpecific.ToDictionary(x => x, default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                    // element, specific
                    {
                        try { lookupSpecific.ToDictionary(default(Func<GroupingEnumerable<int, int>, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                        try { lookupSpecific.ToDictionary(x => x.Key, default(Func<GroupingEnumerable<int, int>, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                    }
                }
            }

            // range
            {
                // no element, default
                try { range.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { range.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { range.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { range.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { range.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { range.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // repeat
            {
                // no element, default
                try { repeat.ToDictionary(default(Func<string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { repeat.ToDictionary(default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { repeat.ToDictionary(default(Func<string, string>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { repeat.ToDictionary(x => x, default(Func<string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { repeat.ToDictionary(default(Func<string, string>), x => x, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { repeat.ToDictionary(x => x, default(Func<string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // reverseRange
            {
                // no element, default
                try { reverseRange.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { reverseRange.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { reverseRange.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { reverseRange.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { reverseRange.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { reverseRange.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // oneItemDefault
            {
                // no element, default
                try { oneItemDefault.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { oneItemDefault.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { oneItemDefault.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemDefault.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { oneItemDefault.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemDefault.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // oneItemSpecific
            {
                // no element, default
                try { oneItemSpecific.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { oneItemSpecific.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { oneItemSpecific.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemSpecific.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { oneItemSpecific.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemSpecific.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // oneItemDefaultOrdered
            {
                // no element, default
                try { oneItemDefaultOrdered.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { oneItemDefaultOrdered.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { oneItemDefaultOrdered.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemDefaultOrdered.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { oneItemDefaultOrdered.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemDefaultOrdered.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
            }

            // oneItemSpecificOrdered
            {
                // no element, default
                try { oneItemSpecificOrdered.ToDictionary(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // no element, specific
                try { oneItemSpecificOrdered.ToDictionary(default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                // element, default
                {
                    try { oneItemSpecificOrdered.ToDictionary(default(Func<int, int>), x => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemSpecificOrdered.ToDictionary(x => x, default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
                // element, specific
                {
                    try { oneItemSpecificOrdered.ToDictionary(default(Func<int, int>), x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                    try { oneItemSpecificOrdered.ToDictionary(x => x, default(Func<int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("elementSelector", exc.ParamName); }
                }
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
                    try
                    {
                        a.ToDictionary(x => x);
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
                        try
                        {
                            a.ToDictionary(x => x, x => x);
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
                        try
                        {
                            a.ToDictionary(x => x, StringComparer.InvariantCultureIgnoreCase);
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
                        try
                        {
                            a.ToDictionary(x => x, x => x, StringComparer.InvariantCultureIgnoreCase);
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
                try { empty.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.ToDictionary(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.ToDictionary(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.ToDictionary(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.ToDictionary(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.ToDictionary(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.ToDictionary(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.ToDictionary(x => x.Key, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.ToDictionary(x => x.Key, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.ToDictionary(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.ToDictionary(x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.ToDictionary(x => x, x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.ToDictionary(x => x, x => x, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
