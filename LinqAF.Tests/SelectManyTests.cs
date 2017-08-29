using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class SelectManyTests
    {
        static void _Universal(IEnumerable<Type> enums)
        {
            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ISelectMany<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ISelectMany ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Universal1()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 2 == 0).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void Universal2()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 2 == 1).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingSimple = new List<string>();
            var missingSimpleIndexed = new List<string>();
            var missingCollection = new List<string>();
            var missingCollectionIndexed = new List<string>();

            var iselectmany = typeof(Impl.ISelectMany<,,>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: true);
            foreach (var e in enums)
            {
                var simple =
                    iselectmany
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 1) return false;

                                var p = ps[0];
                                if (Helper.GetGenericTypeDefinition(p.ParameterType) != typeof(Func<,>)) return false;

                                var passedEnumerable = Helper.GetGenericArguments(p.ParameterType)[1];
                                if (passedEnumerable.IsGenericType && !passedEnumerable.IsGenericTypeDefinition)
                                {
                                    passedEnumerable = Helper.GetGenericTypeDefinition(passedEnumerable);
                                }
                                return passedEnumerable == e;
                            }
                        )
                        .SingleOrDefault();
                if (simple == null) missingSimple.Add(e.Name);

                var simpleIndexed =
                    iselectmany
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 1) return false;

                                var p = ps[0];
                                if (Helper.GetGenericTypeDefinition(p.ParameterType) != typeof(Func<,,>)) return false;

                                var passedEnumerable = Helper.GetGenericArguments(p.ParameterType)[2];
                                if (passedEnumerable.IsGenericType && !passedEnumerable.IsGenericTypeDefinition)
                                {
                                    passedEnumerable = Helper.GetGenericTypeDefinition(passedEnumerable);
                                }
                                return passedEnumerable == e;
                            }
                        )
                        .SingleOrDefault();
                if (simpleIndexed == null) missingSimpleIndexed.Add(e.Name);

                var collection =
                    iselectmany
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 2) return false;

                                var p = ps[0];
                                if (Helper.GetGenericTypeDefinition(p.ParameterType) != typeof(Func<,>)) return false;

                                var passedEnumerable = Helper.GetGenericArguments(p.ParameterType)[1];
                                if (passedEnumerable.IsGenericType && !passedEnumerable.IsGenericTypeDefinition)
                                {
                                    passedEnumerable = Helper.GetGenericTypeDefinition(passedEnumerable);
                                }
                                return passedEnumerable == e;
                            }
                        )
                        .SingleOrDefault();
                if (collection == null) missingCollection.Add(e.Name);

                var collectionIndexed =
                    iselectmany
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 2) return false;

                                var p = ps[0];
                                if (Helper.GetGenericTypeDefinition(p.ParameterType) != typeof(Func<,,>)) return false;

                                var passedEnumerable = Helper.GetGenericArguments(p.ParameterType)[2];
                                if (passedEnumerable.IsGenericType && !passedEnumerable.IsGenericTypeDefinition)
                                {
                                    passedEnumerable = Helper.GetGenericTypeDefinition(passedEnumerable);
                                }
                                return passedEnumerable == e;
                            }
                        )
                        .SingleOrDefault();
                if (collectionIndexed == null) missingCollectionIndexed.Add(e.Name);
            }

            if (missingSimple.Any())
            {
                Assert.Fail("Missing simple methods for: \r\n" + string.Join("\r\n", missingSimple));
            }

            if (missingSimpleIndexed.Any())
            {
                Assert.Fail("Missing simple indexed methods for: \r\n" + string.Join("\r\n", missingSimpleIndexed));
            }

            if (missingCollection.Any())
            {
                Assert.Fail("Missing collection methods for: \r\n" + string.Join("\r\n", missingCollection));
            }

            if (missingCollectionIndexed.Any())
            {
                Assert.Fail("Missing collection indexed methods for: \r\n" + string.Join("\r\n", missingCollectionIndexed));
            }
        }

        class _IntComparer : IEqualityComparer<int>
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
        public void Chaining_Weird1()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(3, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);
            
            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = x => empty;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = x => emptyOrdered;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = x => groupByDefault;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = x => groupBySpecific;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = x => lookup;
            Func<int, RangeEnumerable<int>> intToRange = x => range;
            Func<int, RepeatEnumerable<int>> intToRepeat = x => repeat;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = x => reverseRange;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = x => oneItemDefault;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = x => oneItemSpecific;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = x => oneItemDefaultOrdered;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = x => oneItemSpecificOrdered;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = x => empty;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = x => emptyOrdered;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = x => groupByDefault;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = x => groupBySpecific;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = x => lookup;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = x => range;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = x => repeat;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = x => reverseRange;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = x => oneItemDefault;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = x => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = x => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = x => oneItemSpecificOrdered;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = (x, _) => empty;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = (x, _) => emptyOrdered;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = (x, _) => groupByDefault;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = (x, _) => groupBySpecific;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = (x, _) => lookup;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = (x, _) => range;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = (x, _) => repeat;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = (x, _) => reverseRange;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = (x, _) => oneItemDefault;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = (x, _) => oneItemSpecific;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrdered;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrdered;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = (x, _) => empty;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = (x, _) => emptyOrdered;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = (x, _) => groupByDefault;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = (x, _) => groupBySpecific;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = (x, _) => lookup;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = (x, _) => range;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = (x, _) => repeat;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = (x, _) => reverseRange;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = (x, _) => oneItemDefault;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = (x, _) => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrdered;

            // result
            Func<int, int, int> resultSelector = (a, _) => a;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = (a, _) => a;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = (a, _) => a.Key;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = (a, _) => a.Key;

            // empty
            {
                Assert.IsFalse(empty.SelectMany(intToEmpty).Any());
                Assert.IsFalse(empty.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(empty.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupByDefault).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupByDefault_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupByDefault, resultSelector_grouping).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupBySpecific).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupBySpecific_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupBySpecific, resultSelector_grouping).Any());
                Assert.IsFalse(empty.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).Any());
                Assert.IsFalse(empty.SelectMany(intToLookup).Any());
                Assert.IsFalse(empty.SelectMany(intToLookup_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToLookup, resultSelector_grouping).Any());
                Assert.IsFalse(empty.SelectMany(intToLookup_indexed, resultSelector_grouping).Any());
                Assert.IsFalse(empty.SelectMany(intToRange).Any());
                Assert.IsFalse(empty.SelectMany(intToRange_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToRange, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToRange_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToRepeat).Any());
                Assert.IsFalse(empty.SelectMany(intToRepeat_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToRepeat, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToRepeat_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToReverseRange).Any());
                Assert.IsFalse(empty.SelectMany(intToReverseRange_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToReverseRange, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToReverseRange_indexed, resultSelector).Any());

                Assert.IsFalse(empty.SelectMany(intToOneItemDefault).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefault_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefault, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefault_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecific).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecific_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecific, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecific_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefaultOrdered).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefaultOrdered_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefaultOrdered, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecificOrdered).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecificOrdered_indexed).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecificOrdered, resultSelector).Any());
                Assert.IsFalse(empty.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).Any());

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SelectMany(x => b).Any());
                        Assert.IsFalse(a.SelectMany((x, _) => b).Any());
                        Assert.IsFalse(a.SelectMany(x => b, (y, z) => y+z).Any());
                        Assert.IsFalse(a.SelectMany((x, _) => b, (y, z) => y+z).Any());

                        Assert.IsFalse(b.SelectMany(x => a).Any());
                        Assert.IsFalse(b.SelectMany((x, _) => a).Any());
                        Assert.IsFalse(b.SelectMany(x => a, (y, z) => y+z).Any());
                        Assert.IsFalse(b.SelectMany((x, _) => a, (y, z) => y+z).Any());

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
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmpty).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupByDefault).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupByDefault_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupByDefault, resultSelector_grouping).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupBySpecific).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupBySpecific_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupBySpecific, resultSelector_grouping).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToLookup).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToLookup_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToLookup, resultSelector_grouping).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToLookup_indexed, resultSelector_grouping).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRange).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRange_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRange, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRange_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRepeat).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRepeat_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRepeat, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToRepeat_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToReverseRange).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToReverseRange_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToReverseRange, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToReverseRange_indexed, resultSelector).Any());

                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefault).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefault_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefault, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefault_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecific).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecific_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecific, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecific_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefaultOrdered).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefaultOrdered_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefaultOrdered, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecificOrdered).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecificOrdered_indexed).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecificOrdered, resultSelector).Any());
                Assert.IsFalse(emptyOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).Any());

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SelectMany(x => b).Any());
                        Assert.IsFalse(a.SelectMany((x, _) => b).Any());
                        Assert.IsFalse(a.SelectMany(x => b, (y, z) => y+z).Any());
                        Assert.IsFalse(a.SelectMany((x, _) => b, (y, z) => y+z).Any());

                        Assert.IsFalse(b.SelectMany(x => a).Any());
                        Assert.IsFalse(b.SelectMany((x, _) => a).Any());
                        Assert.IsFalse(b.SelectMany(x => a, (y, z) => y+z).Any());
                        Assert.IsFalse(b.SelectMany((x, _) => a, (y, z) => y+z).Any());

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
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmpty).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmpty_indexed).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmpty, groupingResultSelector).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmpty_indexed, groupingResultSelector).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmptyOrdered).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmptyOrdered_indexed).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmptyOrdered, groupingResultSelector).Any());
                Assert.IsFalse(groupByDefault.SelectMany(groupedIntToEmptyOrdered_indexed, groupingResultSelector).Any());

                var groupByDefault3 = groupByDefault.Concat(groupByDefault).Concat(groupByDefault).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupByDefault).SequenceEqual(groupByDefault3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupByDefault_indexed).SequenceEqual(groupByDefault3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupByDefault, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupByDefault_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var groupBySpecific3 = groupBySpecific.Concat(groupBySpecific).Concat(groupBySpecific).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupBySpecific).SequenceEqual(groupBySpecific3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupBySpecific_indexed).SequenceEqual(groupBySpecific3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupBySpecific, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToGroupBySpecific_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var lookup3 = lookup.Concat(lookup).Concat(lookup).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToLookup).SequenceEqual(lookup3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToLookup_indexed).SequenceEqual(lookup3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToLookup, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToLookup_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var range3 = range.Concat(range).Concat(range).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRange).SequenceEqual(range3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRange_indexed).SequenceEqual(range3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRange, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRange_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var repeat3 = repeat.Concat(repeat).Concat(repeat).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRepeat).SequenceEqual(repeat3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRepeat_indexed).SequenceEqual(repeat3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRepeat, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToRepeat_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var reverseRange3 = reverseRange.Concat(reverseRange).Concat(reverseRange).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToReverseRange).SequenceEqual(reverseRange3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToReverseRange_indexed).SequenceEqual(reverseRange3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToReverseRange, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToReverseRange_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var oneItemDefault3 = oneItemDefault.Concat(oneItemDefault).Concat(oneItemDefault).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefault).SequenceEqual(oneItemDefault3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefault_indexed).SequenceEqual(oneItemDefault3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefault, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefault_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemSpecific3 = oneItemSpecific.Concat(oneItemSpecific).Concat(oneItemSpecific).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecific).SequenceEqual(oneItemSpecific3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecific, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecific_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemDefaultOrdered3 = oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefaultOrdered3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemSpecificOrdered3 = oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).ToArray();
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered3));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        var foo = a.SelectMany(x => b).ToList();

                        LinqAF.Tests.SelectManyTests.Probe(a, b, foo);

                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1, 1, 1 }), ""1"");
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1, 1, 1 }), ""2"");
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y.Key).SequenceEqual(new [] { 1, 2, 3 }), ""3"");
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y.Key).SequenceEqual(new [] { 1, 2, 3 }), ""4"");

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a, new SelectManyTests._GroupingComparer<int>()), ""5"");
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a, new SelectManyTests._GroupingComparer<int>()), ""6"");
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1 }), ""7"");
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1 }), ""8"");

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
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmpty).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmpty_indexed).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmpty, groupingResultSelector).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmpty_indexed, groupingResultSelector).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmptyOrdered).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmptyOrdered_indexed).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmptyOrdered, groupingResultSelector).Any());
                Assert.IsFalse(groupBySpecific.SelectMany(groupedIntToEmptyOrdered_indexed, groupingResultSelector).Any());

                var groupByDefault3 = groupByDefault.Concat(groupByDefault).Concat(groupByDefault).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupByDefault).SequenceEqual(groupByDefault3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupByDefault_indexed).SequenceEqual(groupByDefault3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupByDefault, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupByDefault_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var groupBySpecific3 = groupBySpecific.Concat(groupBySpecific).Concat(groupBySpecific).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupBySpecific).SequenceEqual(groupBySpecific3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupBySpecific_indexed).SequenceEqual(groupBySpecific3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupBySpecific, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToGroupBySpecific_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var lookup3 = lookup.Concat(lookup).Concat(lookup).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToLookup).SequenceEqual(lookup3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToLookup_indexed).SequenceEqual(lookup3, new _GroupingComparer<int>()));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToLookup, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToLookup_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var range3 = range.Concat(range).Concat(range).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRange).SequenceEqual(range3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRange_indexed).SequenceEqual(range3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRange, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRange_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var repeat3 = repeat.Concat(repeat).Concat(repeat).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRepeat).SequenceEqual(repeat3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRepeat_indexed).SequenceEqual(repeat3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRepeat, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToRepeat_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var reverseRange3 = reverseRange.Concat(reverseRange).Concat(reverseRange).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToReverseRange).SequenceEqual(reverseRange3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToReverseRange_indexed).SequenceEqual(reverseRange3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToReverseRange, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToReverseRange_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var oneItemDefault3 = oneItemDefault.Concat(oneItemDefault).Concat(oneItemDefault).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefault).SequenceEqual(oneItemDefault3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefault_indexed).SequenceEqual(oneItemDefault3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefault, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefault_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemSpecific3 = oneItemSpecific.Concat(oneItemSpecific).Concat(oneItemSpecific).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecific).SequenceEqual(oneItemSpecific3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecific, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecific_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemDefaultOrdered3 = oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefaultOrdered3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemSpecificOrdered3 = oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).ToArray();
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered3));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y.Key).SequenceEqual(new [] { 1, 2, 3 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y.Key).SequenceEqual(new [] { 1, 2, 3 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a, new SelectManyTests._GroupingComparer<int>()));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a, new SelectManyTests._GroupingComparer<int>()));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1 }));

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

        public static void Probe<A, B, C>(A a, B b, C val)
        {
            Console.WriteLine(val);
        }

        [TestMethod]
        public void Chaining_Weird2()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(3, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = x => empty;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = x => emptyOrdered;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = x => groupByDefault;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = x => groupBySpecific;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = x => lookup;
            Func<int, RangeEnumerable<int>> intToRange = x => range;
            Func<int, RepeatEnumerable<int>> intToRepeat = x => repeat;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = x => reverseRange;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = x => oneItemDefault;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = x => oneItemSpecific;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = x => oneItemDefaultOrdered;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = x => oneItemSpecificOrdered;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = x => empty;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = x => emptyOrdered;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = x => groupByDefault;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = x => groupBySpecific;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = x => lookup;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = x => range;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = x => repeat;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = x => reverseRange;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = x => oneItemDefault;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = x => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = x => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = x => oneItemSpecificOrdered;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = (x, _) => empty;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = (x, _) => emptyOrdered;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = (x, _) => groupByDefault;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = (x, _) => groupBySpecific;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = (x, _) => lookup;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = (x, _) => range;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = (x, _) => repeat;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = (x, _) => reverseRange;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = (x, _) => oneItemDefault;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = (x, _) => oneItemSpecific;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrdered;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrdered;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = (x, _) => empty;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = (x, _) => emptyOrdered;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = (x, _) => groupByDefault;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = (x, _) => groupBySpecific;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = (x, _) => lookup;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = (x, _) => range;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = (x, _) => repeat;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = (x, _) => reverseRange;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = (x, _) => oneItemDefault;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = (x, _) => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrdered;

            // result
            Func<int, int, int> resultSelector = (a, _) => a;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = (a, _) => a;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = (a, _) => a.Key;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = (a, _) => a.Key;

            // lookup
            {
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmpty).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmpty_indexed).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmpty, groupingResultSelector).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmpty_indexed, groupingResultSelector).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmptyOrdered).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmptyOrdered_indexed).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmptyOrdered, groupingResultSelector).Any());
                Assert.IsFalse(lookup.SelectMany(groupedIntToEmptyOrdered_indexed, groupingResultSelector).Any());

                var groupByDefault3 = groupByDefault.Concat(groupByDefault).Concat(groupByDefault).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupByDefault).SequenceEqual(groupByDefault3, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupByDefault_indexed).SequenceEqual(groupByDefault3, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupByDefault, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupByDefault_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var groupBySpecific3 = groupBySpecific.Concat(groupBySpecific).Concat(groupBySpecific).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupBySpecific).SequenceEqual(groupBySpecific3, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupBySpecific_indexed).SequenceEqual(groupBySpecific3, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupBySpecific, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToGroupBySpecific_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var lookup3 = lookup.Concat(lookup).Concat(lookup).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToLookup).SequenceEqual(lookup3, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.SelectMany(groupedIntToLookup_indexed).SequenceEqual(lookup3, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.SelectMany(groupedIntToLookup, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToLookup_indexed, groupingResultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }));

                var range3 = range.Concat(range).Concat(range).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToRange).SequenceEqual(range3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToRange_indexed).SequenceEqual(range3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToRange, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToRange_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var repeat3 = repeat.Concat(repeat).Concat(repeat).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToRepeat).SequenceEqual(repeat3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToRepeat_indexed).SequenceEqual(repeat3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToRepeat, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToRepeat_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var reverseRange3 = reverseRange.Concat(reverseRange).Concat(reverseRange).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToReverseRange).SequenceEqual(reverseRange3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToReverseRange_indexed).SequenceEqual(reverseRange3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToReverseRange, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToReverseRange_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 }));

                var oneItemDefault3 = oneItemDefault.Concat(oneItemDefault).Concat(oneItemDefault).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefault).SequenceEqual(oneItemDefault3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefault_indexed).SequenceEqual(oneItemDefault3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefault, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefault_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemSpecific3 = oneItemSpecific.Concat(oneItemSpecific).Concat(oneItemSpecific).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecific).SequenceEqual(oneItemSpecific3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecific, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecific_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemDefaultOrdered3 = oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefaultOrdered3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefaultOrdered, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                var oneItemSpecificOrdered3 = oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).ToArray();
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered3));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecificOrdered, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(lookup.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, groupingResultSelector).SequenceEqual(new[] { 1, 2, 3 }));

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y.Key).SequenceEqual(new [] { 1, 2, 3 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y.Key).SequenceEqual(new [] { 1, 2, 3 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a, new SelectManyTests._GroupingComparer<int>()));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a, new SelectManyTests._GroupingComparer<int>()));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1 }));

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
                Assert.IsFalse(range.SelectMany(intToEmpty).Any());
                Assert.IsFalse(range.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(range.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(range.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(range.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(range.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(range.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(range.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                var groupByDefault5 = groupByDefault.Concat(groupByDefault).Concat(groupByDefault).Concat(groupByDefault).Concat(groupByDefault).ToArray();
                Assert.IsTrue(range.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault5, new _GroupingComparer<int>()));
                Assert.IsTrue(range.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault5, new _GroupingComparer<int>()));
                Assert.IsTrue(range.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 }));
                Assert.IsTrue(range.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 }));

                var groupBySpecific5 = groupBySpecific.Concat(groupBySpecific).Concat(groupBySpecific).Concat(groupBySpecific).Concat(groupBySpecific).ToArray();
                Assert.IsTrue(range.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific5, new _GroupingComparer<int>()));
                Assert.IsTrue(range.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific5, new _GroupingComparer<int>()));
                Assert.IsTrue(range.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 }));
                Assert.IsTrue(range.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 }));

                var lookup5 = lookup.Concat(lookup).Concat(lookup).Concat(lookup).Concat(lookup).ToArray();
                Assert.IsTrue(range.SelectMany(intToLookup).SequenceEqual(lookup5, new _GroupingComparer<int>()));
                Assert.IsTrue(range.SelectMany(intToLookup_indexed).SequenceEqual(lookup5, new _GroupingComparer<int>()));
                Assert.IsTrue(range.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 }));
                Assert.IsTrue(range.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 }));

                var range5 = range.Concat(range).Concat(range).Concat(range).Concat(range).ToArray();
                Assert.IsTrue(range.SelectMany(intToRange).SequenceEqual(range5));
                Assert.IsTrue(range.SelectMany(intToRange_indexed).SequenceEqual(range5));
                Assert.IsTrue(range.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 }));
                Assert.IsTrue(range.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 }));

                Assert.IsTrue(range.SelectMany(intToRepeat).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(range.SelectMany(intToRepeat_indexed).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(range.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 }));
                Assert.IsTrue(range.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 }));

                var reverseRange5 = reverseRange.Concat(reverseRange).Concat(reverseRange).Concat(reverseRange).Concat(reverseRange).ToArray();
                Assert.IsTrue(range.SelectMany(intToReverseRange).SequenceEqual(reverseRange5));
                Assert.IsTrue(range.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange5));
                Assert.IsTrue(range.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 }));
                Assert.IsTrue(range.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 }));

                var oneItemDefault5 = oneItemDefault.Concat(oneItemDefault).Concat(oneItemDefault).Concat(oneItemDefault).Concat(oneItemDefault).ToArray();
                Assert.IsTrue(range.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault5));
                Assert.IsTrue(range.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault5));
                Assert.IsTrue(range.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                var oneItemSpecific5 = oneItemSpecific.Concat(oneItemSpecific).Concat(oneItemSpecific).Concat(oneItemSpecific).Concat(oneItemSpecific).ToArray();
                Assert.IsTrue(range.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific5));
                Assert.IsTrue(range.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific5));
                Assert.IsTrue(range.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                var oneItemDefaultOrdered5 = oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).ToArray();
                Assert.IsTrue(range.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered5));
                Assert.IsTrue(range.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefaultOrdered5));
                Assert.IsTrue(range.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                var oneItemSpecificOrdered5 = oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).ToArray();
                Assert.IsTrue(range.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered5));
                Assert.IsTrue(range.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered5));
                Assert.IsTrue(range.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));

                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 1, 2, 3, 4, 5 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 1, 2, 3, 4, 5 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));

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
                Assert.IsFalse(repeat.SelectMany(intToEmpty).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(repeat.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                var groupByDefault5 = groupByDefault.Concat(groupByDefault).Concat(groupByDefault).Concat(groupByDefault).Concat(groupByDefault).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault5, new _GroupingComparer<int>()));
                Assert.IsTrue(repeat.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault5, new _GroupingComparer<int>()));
                Assert.IsTrue(repeat.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));

                var groupBySpecific5 = groupBySpecific.Concat(groupBySpecific).Concat(groupBySpecific).Concat(groupBySpecific).Concat(groupBySpecific).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific5, new _GroupingComparer<int>()));
                Assert.IsTrue(repeat.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific5, new _GroupingComparer<int>()));
                Assert.IsTrue(repeat.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));

                var lookup5 = lookup.Concat(lookup).Concat(lookup).Concat(lookup).Concat(lookup).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToLookup).SequenceEqual(lookup5, new _GroupingComparer<int>()));
                Assert.IsTrue(repeat.SelectMany(intToLookup_indexed).SequenceEqual(lookup5, new _GroupingComparer<int>()));
                Assert.IsTrue(repeat.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));

                var range5 = range.Concat(range).Concat(range).Concat(range).Concat(range).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToRange).SequenceEqual(range5));
                Assert.IsTrue(repeat.SelectMany(intToRange_indexed).SequenceEqual(range5));
                Assert.IsTrue(repeat.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));

                Assert.IsTrue(repeat.SelectMany(intToRepeat).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToRepeat_indexed).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));

                var reverseRange5 = reverseRange.Concat(reverseRange).Concat(reverseRange).Concat(reverseRange).Concat(reverseRange).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToReverseRange).SequenceEqual(reverseRange5));
                Assert.IsTrue(repeat.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange5));
                Assert.IsTrue(repeat.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));

                var oneItemDefault5 = oneItemDefault.Concat(oneItemDefault).Concat(oneItemDefault).Concat(oneItemDefault).Concat(oneItemDefault).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                var oneItemSpecific5 = oneItemSpecific.Concat(oneItemSpecific).Concat(oneItemSpecific).Concat(oneItemSpecific).Concat(oneItemSpecific).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                var oneItemDefaultOrdered5 = oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefaultOrdered5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                var oneItemSpecificOrdered5 = oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).ToArray();
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered5));
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));
                Assert.IsTrue(repeat.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(new[] { 3, 3, 3, 3, 3 }));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 3, 3, 3, 3, 3 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 3, 3, 3, 3, 3 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));

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
                Assert.IsFalse(reverseRange.SelectMany(intToEmpty).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(reverseRange.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                var groupByDefault5 = groupByDefault.Concat(groupByDefault).Concat(groupByDefault).Concat(groupByDefault).Concat(groupByDefault).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault5, new _GroupingComparer<int>()));
                Assert.IsTrue(reverseRange.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault5, new _GroupingComparer<int>()));
                Assert.IsTrue(reverseRange.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1 }));

                var groupBySpecific5 = groupBySpecific.Concat(groupBySpecific).Concat(groupBySpecific).Concat(groupBySpecific).Concat(groupBySpecific).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific5, new _GroupingComparer<int>()));
                Assert.IsTrue(reverseRange.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific5, new _GroupingComparer<int>()));
                Assert.IsTrue(reverseRange.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1 }));

                var lookup5 = lookup.Concat(lookup).Concat(lookup).Concat(lookup).Concat(lookup).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToLookup).SequenceEqual(lookup5, new _GroupingComparer<int>()));
                Assert.IsTrue(reverseRange.SelectMany(intToLookup_indexed).SequenceEqual(lookup5, new _GroupingComparer<int>()));
                Assert.IsTrue(reverseRange.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1 }));

                var range5 = range.Concat(range).Concat(range).Concat(range).Concat(range).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToRange).SequenceEqual(range5));
                Assert.IsTrue(reverseRange.SelectMany(intToRange_indexed).SequenceEqual(range5));
                Assert.IsTrue(reverseRange.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 }));

                Assert.IsTrue(reverseRange.SelectMany(intToRepeat).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(reverseRange.SelectMany(intToRepeat_indexed).SequenceEqual(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }));
                Assert.IsTrue(reverseRange.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 }));

                var reverseRange5 = reverseRange.Concat(reverseRange).Concat(reverseRange).Concat(reverseRange).Concat(reverseRange).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToReverseRange).SequenceEqual(reverseRange5));
                Assert.IsTrue(reverseRange.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange5));
                Assert.IsTrue(reverseRange.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 }));

                var oneItemDefault5 = oneItemDefault.Concat(oneItemDefault).Concat(oneItemDefault).Concat(oneItemDefault).Concat(oneItemDefault).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                var oneItemSpecific5 = oneItemSpecific.Concat(oneItemSpecific).Concat(oneItemSpecific).Concat(oneItemSpecific).Concat(oneItemSpecific).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                var oneItemDefaultOrdered5 = oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).Concat(oneItemDefaultOrdered).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefaultOrdered5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                var oneItemSpecificOrdered5 = oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).Concat(oneItemSpecificOrdered).ToArray();
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered5));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 5, 4, 3, 2, 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 5, 4, 3, 2, 1 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1, 1, 1, 1, 1 }));

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
        public void Chaining_Weird3()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(3, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = x => empty;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = x => emptyOrdered;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = x => groupByDefault;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = x => groupBySpecific;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = x => lookup;
            Func<int, RangeEnumerable<int>> intToRange = x => range;
            Func<int, RepeatEnumerable<int>> intToRepeat = x => repeat;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = x => reverseRange;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = x => oneItemDefault;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = x => oneItemSpecific;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = x => oneItemDefaultOrdered;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = x => oneItemSpecificOrdered;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = x => empty;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = x => emptyOrdered;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = x => groupByDefault;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = x => groupBySpecific;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = x => lookup;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = x => range;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = x => repeat;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = x => reverseRange;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = x => oneItemDefault;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = x => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = x => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = x => oneItemSpecificOrdered;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = (x, _) => empty;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = (x, _) => emptyOrdered;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = (x, _) => groupByDefault;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = (x, _) => groupBySpecific;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = (x, _) => lookup;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = (x, _) => range;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = (x, _) => repeat;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = (x, _) => reverseRange;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = (x, _) => oneItemDefault;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = (x, _) => oneItemSpecific;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrdered;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrdered;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = (x, _) => empty;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = (x, _) => emptyOrdered;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = (x, _) => groupByDefault;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = (x, _) => groupBySpecific;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = (x, _) => lookup;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = (x, _) => range;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = (x, _) => repeat;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = (x, _) => reverseRange;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = (x, _) => oneItemDefault;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = (x, _) => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrdered;

            // result
            Func<int, int, int> resultSelector = (a, _) => a;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = (a, _) => a;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = (a, _) => a.Key;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = (a, _) => a.Key;

            // oneItemDefault
            {
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmpty).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(oneItemDefault.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));
                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));

                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));
                Assert.IsTrue(oneItemDefault.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));

                Assert.IsTrue(oneItemDefault.SelectMany(intToLookup).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefault.SelectMany(intToLookup_indexed).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefault.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));
                Assert.IsTrue(oneItemDefault.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));

                Assert.IsTrue(oneItemDefault.SelectMany(intToRange).SequenceEqual(range));
                Assert.IsTrue(oneItemDefault.SelectMany(intToRange_indexed).SequenceEqual(range));
                Assert.IsTrue(oneItemDefault.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));
                Assert.IsTrue(oneItemDefault.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));

                Assert.IsTrue(oneItemDefault.SelectMany(intToRepeat).SequenceEqual(repeat));
                Assert.IsTrue(oneItemDefault.SelectMany(intToRepeat_indexed).SequenceEqual(repeat));
                Assert.IsTrue(oneItemDefault.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));
                Assert.IsTrue(oneItemDefault.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));

                Assert.IsTrue(oneItemDefault.SelectMany(intToReverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemDefault.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemDefault.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));
                Assert.IsTrue(oneItemDefault.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));

                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(oneItemDefault));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 0 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 0 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1 }));

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
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmpty).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(oneItemSpecific.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecific.SelectMany(intToLookup).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToLookup_indexed).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecific.SelectMany(intToRange).SequenceEqual(range));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToRange_indexed).SequenceEqual(range));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecific.SelectMany(intToRepeat).SequenceEqual(repeat));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToRepeat_indexed).SequenceEqual(repeat));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecific.SelectMany(intToReverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(oneItemSpecific));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 4 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 4 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1 }));

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
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmpty).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(oneItemDefaultOrdered.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToLookup).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToLookup_indexed).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 0, 0, 0 }));

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRange).SequenceEqual(range));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRange_indexed).SequenceEqual(range));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRepeat).SequenceEqual(repeat));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRepeat_indexed).SequenceEqual(repeat));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToReverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 0, 0, 0, 0, 0 }));

                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(oneItemDefault));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 0 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 0 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1 }));

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
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmpty).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmpty_indexed).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmpty, resultSelector).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmpty_indexed, resultSelector).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmptyOrdered).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmptyOrdered_indexed).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmptyOrdered, resultSelector).Any());
                Assert.IsFalse(oneItemSpecificOrdered.SelectMany(intToEmptyOrdered_indexed, resultSelector).Any());

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupByDefault_indexed).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupByDefault, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupBySpecific_indexed).SequenceEqual(groupBySpecific, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupBySpecific, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToLookup).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToLookup_indexed).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToLookup, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToLookup_indexed, resultSelector_grouping).SequenceEqual(new[] { 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRange).SequenceEqual(range));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRange_indexed).SequenceEqual(range));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRange, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRange_indexed, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRepeat).SequenceEqual(repeat));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRepeat_indexed).SequenceEqual(repeat));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRepeat, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToRepeat_indexed, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToReverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToReverseRange_indexed).SequenceEqual(reverseRange));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToReverseRange, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToReverseRange_indexed, resultSelector).SequenceEqual(new[] { 4, 4, 4, 4, 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefault_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefault, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefault_indexed, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecific_indexed).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecific, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecific_indexed, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered_indexed).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered_indexed).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered, resultSelector).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector).SequenceEqual(oneItemSpecific));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                    {
                        Assert.IsTrue(a.SelectMany(x => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(a.SelectMany(x => b, (y, _) => y).SequenceEqual(new [] { 4 }));
                        Assert.IsTrue(a.SelectMany((x, _) => b, (y, _) => y).SequenceEqual(new [] { 4 }));

                        Assert.IsTrue(b.SelectMany(x => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany((x, _) => a).SequenceEqual(a));
                        Assert.IsTrue(b.SelectMany(x => a, (y, _) => y).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.SelectMany((x, _) => a, (y, _) => y).SequenceEqual(new [] { 1 }));

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
        public void Chaining_SingleSelector()
        {
            // single selector
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { 2, 4 },
                            res =>
                            {
                                Assert.AreEqual(6, res.Count);
                                Assert.AreEqual(2, res[0]);
                                Assert.AreEqual(4, res[1]);
                                Assert.AreEqual(2, res[2]); 
                                Assert.AreEqual(4, res[3]);
                                Assert.AreEqual(2, res[4]);
                                Assert.AreEqual(4, res[5]);
                            },
                            ""(a, b) => a.SelectMany(f => b)"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
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
        public void Chaining_IndexedSelector()
        {
            // indexed selector
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { 2, 4 },
                            res =>
                            {
                                Assert.AreEqual(6, res.Count);
                                Assert.AreEqual(2, res[0]);
                                Assert.AreEqual(4, res[1]);
                                Assert.AreEqual(2, res[2]); 
                                Assert.AreEqual(4, res[3]);
                                Assert.AreEqual(2, res[4]);
                                Assert.AreEqual(4, res[5]);
                            },
                            ""(a, b) => a.SelectMany((f, ix) => b)"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
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
        public void Chaining_CollectionResultSelector()
        {
            // collection, result selector
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { 2, 4 },
                            res =>
                            {
                                Assert.AreEqual(6, res.Count);
                                Assert.AreEqual(2, res[0]);
                                Assert.AreEqual(4, res[1]);
                                Assert.AreEqual(4, res[2]); 
                                Assert.AreEqual(8, res[3]);
                                Assert.AreEqual(6, res[4]);
                                Assert.AreEqual(12, res[5]);
                            },
                            ""(a, b) => a.SelectMany(f => b, (s, c) => s * c)"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
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
        public void Chaining_IndexCollectionResultSelector()
        {
            // indexed collection, result selector
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new[] { 2, 4 },
                            res =>
                            {
                                Assert.AreEqual(6, res.Count);
                                Assert.AreEqual(2, res[0]);
                                Assert.AreEqual(4, res[1]);
                                Assert.AreEqual(4, res[2]); 
                                Assert.AreEqual(8, res[3]);
                                Assert.AreEqual(6, res[4]);
                                Assert.AreEqual(12, res[5]);
                            },
                            ""(a, b) => a.SelectMany((f, ix) => b, (s, c) => s * c)"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        public static Func<string, TInnerEnumerable> _MakeNullErrorsProjection<TInnerEnumerable>(TInnerEnumerable inner) => default(Func<string, TInnerEnumerable>);

        [TestMethod]
        public void Errors_Simple()
        {
            // simple
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
                            try
                            {
                                var f = SelectManyTests._MakeNullErrorsProjection(b);
                                a.SelectMany(f);
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""selector"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(CastEnumerable<,,,>)
                    )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(CastEnumerable<,,,>) // cast requires some extra work
                );
            }
        }

        public static Func<string, TInnerEnumerable> _MakeNullErrorsIndexedProjection<TInnerEnumerable>(TInnerEnumerable inner) => default(Func<string, TInnerEnumerable>);

        [TestMethod]
        public void Errors_Indexed()
        {
            // indexed
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
                            try
                            {
                                var f = SelectManyTests._MakeNullErrorsIndexedProjection(b);
                                a.SelectMany(f);
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""selector"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(CastEnumerable<,,,>)
                    )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(CastEnumerable<,,,>) // cast requires some extra work
                );
            }
        }

        public static Func<string, TInnerEnumerable> _MakeNonNullErrorsProjection<TInnerEnumerable>(TInnerEnumerable inner)
        {
            return str => default(TInnerEnumerable);
        }

        [TestMethod]
        public void Errors_Collection1()
        {
            // collection
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
                            try
                            {
                                var f = SelectManyTests._MakeNonNullErrorsProjection(b);
                                Func<string, string, string> result = null;
                                a.SelectMany(f, result);
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
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(CastEnumerable<,,,>)
                    )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(CastEnumerable<,,,>) // cast requires some extra work
                );
            }
        }

        [TestMethod]
        public void Errors_Collection2()
        {
            // collection
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
                            try
                            {
                                var f = SelectManyTests._MakeNonNullErrorsProjection(b);
                                Func<string, string, string> result = null;
                                a.SelectMany(f, result);
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
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(CastEnumerable<,,,>)
                    )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(CastEnumerable<,,,>) // cast requires some extra work
                );
            }
        }

        public static Func<string, int, TInnerEnumerable> _MakeNonNullErrorsIndexedProjection<TInnerEnumerable>(TInnerEnumerable inner)
        {
            return (str, ix) => default(TInnerEnumerable);
        }

        [TestMethod]
        public void Errors_CollectionIndexed1()
        {
            // collection, indexed
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
                            try
                            {
                                var f = SelectManyTests._MakeNullErrorsIndexedProjection(b);
                                Func<string, string, string> result = (x, y) => null;
                                a.SelectMany(f, result);
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""collectionSelector"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(CastEnumerable<,,,>)
                    )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(CastEnumerable<,,,>) // cast requires some extra work
                );
            }
        }

        [TestMethod]
        public void Errors_CollectionIndexed2()
        {
            // collection, indexed
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
                            try
                            {
                                var f = SelectManyTests._MakeNonNullErrorsIndexedProjection(b);
                                Func<string, string, string> result = null;
                                a.SelectMany(f, result);
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
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(CastEnumerable<,,,>)
                    )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(CastEnumerable<,,,>) // cast requires some extra work
                );
            }
        }

        [TestMethod]
        public void Errors_Weird1()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(3, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = null;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = null;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = null;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = null;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = null;
            Func<int, RangeEnumerable<int>> intToRange = null;
            Func<int, RepeatEnumerable<int>> intToRepeat = null;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = null;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = null;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = null;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = null;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = null;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = null;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = null;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = null;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = null;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = null;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = null;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = null;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = null;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = null;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = null;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = null;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = null;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = null;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = null;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = null;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = null;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = null;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = null;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = null;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = null;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = null;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = null;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = null;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = null;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = null;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = null;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = null;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = null;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = null;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = null;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = null;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = null;

            // result
            Func<int, int, int> resultSelector = null;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = null;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = null;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = null;

            // empty
            {
                try { empty.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { empty.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                try { emptyOrdered.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { emptyOrdered.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                try { groupByDefault.SelectMany(groupedIntToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => empty, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => empty, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => emptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => emptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => groupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => groupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => groupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => groupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => lookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => lookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => range, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => range, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => repeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => repeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => reverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => reverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var orderBy = new[] { groupByDefault.First() }.OrderBy(x => x);
                try { groupByDefault.SelectMany(default(Func<GroupingEnumerable<int, int>, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(default(Func<GroupingEnumerable<int, int>, int, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(default(Func<GroupingEnumerable<int, int>, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(x => orderBy, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(default(Func<GroupingEnumerable<int, int>, int, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => orderBy, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { orderBy.SelectMany(x => groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { orderBy.SelectMany((x, ix) => groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => oneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => oneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => oneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => oneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => oneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => oneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(a => oneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupByDefault.SelectMany((a, ix) => oneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefault.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // groupBySpecific
            {
                try { groupBySpecific.SelectMany(groupedIntToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => empty, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => empty, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => emptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => emptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => groupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => groupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => groupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => groupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => lookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => lookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => range, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => range, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => repeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => repeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => reverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => reverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var orderBy = new[] { groupBySpecific.First() }.OrderBy(x => x);
                try { groupBySpecific.SelectMany(default(Func<GroupingEnumerable<int, int>, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(default(Func<GroupingEnumerable<int, int>, int, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(default(Func<GroupingEnumerable<int, int>, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(x => orderBy, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(default(Func<GroupingEnumerable<int, int>, int, OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => orderBy, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { orderBy.SelectMany(x => groupBySpecific, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { orderBy.SelectMany((x, ix) => groupBySpecific, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => oneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => oneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => oneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => oneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => oneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => oneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(a => oneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { groupBySpecific.SelectMany((a, ix) => oneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecific.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird2()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(3, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = null;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = null;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = null;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = null;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = null;
            Func<int, RangeEnumerable<int>> intToRange = null;
            Func<int, RepeatEnumerable<int>> intToRepeat = null;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = null;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = null;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = null;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = null;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = null;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = null;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = null;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = null;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = null;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = null;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = null;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = null;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = null;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = null;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = null;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = null;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = null;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = null;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = null;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = null;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = null;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = null;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = null;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = null;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = null;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = null;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = null;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = null;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = null;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = null;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = null;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = null;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = null;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = null;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = null;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = null;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = null;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = null;

            // result
            Func<int, int, int> resultSelector = null;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = null;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = null;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = null;

            // lookup
            {
                var orderBy = new[] { lookup.First() }.OrderBy(x => x);
                try { lookup.SelectMany(default(Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(default(Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(default(Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(x => orderBy, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(default(Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => orderBy, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { orderBy.SelectMany(x => lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { orderBy.SelectMany(default(Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>>), (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { orderBy.SelectMany((x, ix) => lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => empty, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => empty, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => emptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => emptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => groupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => groupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => groupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => groupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => lookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => lookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => range, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => range, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => repeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => repeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => reverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => reverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => oneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => oneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => oneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => oneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => oneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => oneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany(a => oneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { lookup.SelectMany((a, ix) => oneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookup.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // range
            {
                try { range.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { range.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                try { repeat.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { repeat.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                try { reverseRange.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { reverseRange.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird3()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat(3, 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = null;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = null;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = null;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = null;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = null;
            Func<int, RangeEnumerable<int>> intToRange = null;
            Func<int, RepeatEnumerable<int>> intToRepeat = null;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = null;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = null;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = null;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = null;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = null;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = null;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = null;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = null;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = null;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = null;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = null;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = null;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = null;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = null;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = null;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = null;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = null;

            // result
            Func<int, int, int> resultSelector = null;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = null;

            // oneItemDefault
            {
                try { oneItemDefault.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefault.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecific.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmpty, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmpty_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => empty, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => emptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => groupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => groupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToLookup, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToLookup_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => lookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => range, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRepeat, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRepeat_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => repeat, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToReverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToReverseRange_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => reverseRange, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => oneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => oneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => oneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(a => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("collectionSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany((a, ix) => oneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        var projFromBToA = (new SelectManyTests())._MakeIdentityProjection(b.FirstOrDefault(), a);
                        projFromBToA = null;
                        var projFromBToA_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(b.FirstOrDefault(), a);
                        projFromBToA_indexed = null;

                        var projFromAToB = (new SelectManyTests())._MakeIdentityProjection(a.FirstOrDefault(), b);
                        projFromAToB = null;
                        var projFromAToB_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(a.FirstOrDefault(), b);
                        projFromAToB_indexed = null;

                        Func<int, int, int> resultSelector = null;

                        try { a.SelectMany(projFromAToB); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { a.SelectMany(projFromAToB_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { a.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        try { b.SelectMany(projFromBToA); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany(x => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { b.SelectMany(projFromBToA_indexed, (x, y) => x); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""collectionSelector"", exc.ParamName); }
                        try { b.SelectMany((x, ix) => b, resultSelector); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        public Func<TItem, TEnumerable> _MakeIdentityProjection<TItem, TEnumerable>(TItem example, TEnumerable toRet)
        {
            return _ => toRet;
        }

        public Func<TItem, int, TEnumerable> _MakeIdentityIndexedProjection<TItem, TEnumerable>(TItem example, TEnumerable toRet)
        {
            return (_, __) => toRet;
        }

        [TestMethod]
        public void Malformed_Simple()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new string[0],
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            a.SelectMany(f => b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""""source"""", exc.ParamName);
                        }
                    
                        return Helper.NoCallValue;
                       }""
                  )",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed_Indexed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new string[0],
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            a.SelectMany((f, ix) => b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""""source"""", exc.ParamName);
                        }
                    
                        return Helper.NoCallValue;
                       }""
                  )",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed_Collection()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new string[0],
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            a.SelectMany(f => b, (x, y) => default(string));
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""""source"""", exc.ParamName);
                        }
                    
                        return Helper.NoCallValue;
                       }""
                  )",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed_CollectionIndexed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new string[0],
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            a.SelectMany((f, ix) => b, (x, y) => default(string));
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""""source"""", exc.ParamName);
                        }
                    
                        return Helper.NoCallValue;
                       }""
                  )",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void ProjectedMalformed_Simple()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new [] { ""hello"" },   // can't be empty!
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            var _ = b.SelectMany(f => a).ToList();
                            Assert.Fail();
                        }
                        catch(InvalidOperationException exc)
                        {
                            Assert.AreEqual(""""Uninitialized enumerable returned by projection"""", exc.Message);
                        }

                        return Helper.NoCallValue;
                       }"",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                  )",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void ProjectedMalformed_Indexed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new [] { ""hello"" },   // can't be empty!
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            var _ = b.SelectMany((f, ix) => a).ToList();
                            Assert.Fail();
                        }
                        catch(InvalidOperationException exc)
                        {
                            Assert.AreEqual(""""Uninitialized enumerable returned by projection"""", exc.Message);
                        }

                        return Helper.NoCallValue;
                       }"",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                  )",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void ProjectedMalformed_Collection()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new [] { ""hello"" },   // can't be empty!
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            var _ = b.SelectMany(f => a, (x, y) => """""""").ToList();
                            Assert.Fail();
                        }
                        catch(InvalidOperationException exc)
                        {
                            Assert.AreEqual(""""Uninitialized enumerable returned by projection"""", exc.Message);
                        }

                        return Helper.NoCallValue;
                       }"",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                  )",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void ProjectedMalformed_CollectionIndexed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new [] { ""hello"" },   // can't be empty!
                    res => { },
                    @""(a, b) =>
                       {
                        try
                        {
                            var _ = b.SelectMany((f, ix) => a, (x, y) => """""""").ToList();
                            Assert.Fail();
                        }
                        catch(InvalidOperationException exc)
                        {
                            Assert.AreEqual(""""Uninitialized enumerable returned by projection"""", exc.Message);
                        }

                        return Helper.NoCallValue;
                       }"",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                  )",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed_Weird1()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
            var lookup = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            var emptyGood = Enumerable.Empty<int>();
            var emptyOrderedGood = emptyGood.OrderBy(x => x);
            var groupByDefaultGood = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecificGood = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookupGood = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var rangeGood = Enumerable.Range(1, 5);
            var repeatGood = Enumerable.Repeat(3, 5);
            var reverseRangeGood = Enumerable.Range(1, 5).Reverse();
            var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
            var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = x => emptyGood;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = x => emptyOrderedGood;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = x => groupByDefaultGood;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = x => groupBySpecificGood;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = x => lookupGood;
            Func<int, RangeEnumerable<int>> intToRange = x => rangeGood;
            Func<int, RepeatEnumerable<int>> intToRepeat = x => repeatGood;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = x => reverseRangeGood;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = x => oneItemDefaultGood;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = x => oneItemSpecificGood;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = x => oneItemDefaultOrderedGood;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = x => oneItemSpecificOrderedGood;

            // no-index, bad
            Func<int, EmptyEnumerable<int>> intToEmptyBad = x => empty;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrderedBad = x => emptyOrdered;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefaultBad = x => groupByDefault;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecificBad = x => groupBySpecific;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookupBad = x => lookup;
            Func<int, RangeEnumerable<int>> intToRangeBad = x => range;
            Func<int, RepeatEnumerable<int>> intToRepeatBad = x => repeat;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRangeBad = x => reverseRange;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefaultBad = x => oneItemDefault;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecificBad = x => oneItemSpecific;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrderedBad = x => oneItemDefaultOrdered;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrderedBad = x => oneItemSpecificOrdered;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = x => emptyGood;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = x => emptyOrderedGood;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = x => groupByDefaultGood;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = x => groupBySpecificGood;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = x => lookupGood;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = x => rangeGood;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = x => repeatGood;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = x => reverseRangeGood;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = x => oneItemDefaultGood;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = x => oneItemSpecificGood;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = x => oneItemDefaultOrderedGood;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = x => oneItemSpecificOrderedGood;

            // no-index, grouped, bad
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmptyBad = x => empty;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrderedBad = x => emptyOrdered;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefaultBad = x => groupByDefault;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecificBad = x => groupBySpecific;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookupBad = x => lookup;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRangeBad = x => range;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeatBad = x => repeat;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRangeBad = x => reverseRange;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefaultBad = x => oneItemDefault;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecificBad = x => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrderedBad = x => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrderedBad = x => oneItemSpecificOrdered;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = (x, _) => emptyGood;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = (x, _) => emptyOrderedGood;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = (x, _) => groupByDefaultGood;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = (x, _) => groupBySpecificGood;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = (x, _) => lookupGood;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = (x, _) => rangeGood;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = (x, _) => repeatGood;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = (x, _) => reverseRangeGood;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = (x, _) => oneItemDefaultGood;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = (x, _) => oneItemSpecificGood;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrderedGood;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrderedGood;

            // indexed, bad
            Func<int, int, EmptyEnumerable<int>> intToEmptyBad_indexed = (x, _) => empty;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrderedBad_indexed = (x, _) => emptyOrdered;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefaultBad_indexed = (x, _) => groupByDefault;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecificBad_indexed = (x, _) => groupBySpecific;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookupBad_indexed = (x, _) => lookup;
            Func<int, int, RangeEnumerable<int>> intToRangeBad_indexed = (x, _) => range;
            Func<int, int, RepeatEnumerable<int>> intToRepeatBad_indexed = (x, _) => repeat;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRangeBad_indexed = (x, _) => reverseRange;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefaultBad_indexed = (x, _) => oneItemDefault;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecificBad_indexed = (x, _) => oneItemSpecific;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrderedBad_indexed = (x, _) => oneItemDefaultOrdered;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrderedBad_indexed = (x, _) => oneItemSpecificOrdered;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = (x, _) => emptyGood;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = (x, _) => emptyOrderedGood;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = (x, _) => groupByDefaultGood;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = (x, _) => groupBySpecificGood;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = (x, _) => lookupGood;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = (x, _) => rangeGood;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = (x, _) => repeatGood;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = (x, _) => reverseRangeGood;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = (x, _) => oneItemDefaultGood;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = (x, _) => oneItemSpecificGood;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrderedGood;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrderedGood;

            // indexed, grouped, bad
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmptyBad_indexed = (x, _) => empty;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrderedBad_indexed = (x, _) => emptyOrdered;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefaultBad_indexed = (x, _) => groupByDefault;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecificBad_indexed = (x, _) => groupBySpecific;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookupBad_indexed = (x, _) => lookup;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRangeBad_indexed = (x, _) => range;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeatBad_indexed = (x, _) => repeat;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRangeBad_indexed = (x, _) => reverseRange;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefaultBad_indexed = (x, _) => oneItemDefault;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecificBad_indexed = (x, _) => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrderedBad_indexed = (x, _) => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrderedBad_indexed = (x, _) => oneItemSpecificOrdered;

            // result
            Func<int, int, int> resultSelector = (a, _) => a;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = (a, _) => a;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = (a, _) => a.Key;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = (a, _) => a.Key;

            // empty
            {
                try { empty.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { empty.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                Helper.ForEachEnumerableExpression(
                    empty,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        // can infer no results given empty
                        /*try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }*/

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
                try { emptyOrdered.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        // can infer no results given empty
                        /*try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }*/

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
                try { groupByDefault.SelectMany(groupedIntToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmpty, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmpty_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToEmptyOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { groupByDefault.SelectMany(groupedIntToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupByDefault_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupByDefaultBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupByDefaultBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToGroupBySpecific_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupBySpecificBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToGroupBySpecificBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToLookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToLookup_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToLookupBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToLookupBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRange_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToRangeBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToRangeBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRepeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToRepeat_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToRepeatBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToRepeatBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToReverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToReverseRange_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToReverseRangeBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToReverseRangeBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var intersectDefaultInt = new[] { 1 }.Intersect(new[] { 1 });
                var intersectDefaultIntBad = new IntersectDefaultEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { groupByDefault.SelectMany(x => intersectDefaultInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => intersectDefaultInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(x => intersectDefaultInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => intersectDefaultInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(x => intersectDefaultIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => intersectDefaultIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(x => intersectDefaultIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => intersectDefaultIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var intersectSpecificInt = new[] { 1 }.Intersect(new[] { 1 }, new _IntComparer());
                var intersectSpecificIntBad = new IntersectSpecificEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { groupByDefault.SelectMany(x => intersectSpecificInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => intersectSpecificInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(x => intersectSpecificInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => intersectSpecificInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(x => intersectSpecificIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => intersectSpecificIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(x => intersectSpecificIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => intersectSpecificIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var whereInt = new[] { 1 }.Where(x => true);
                var whereIntBad = new WhereEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { groupByDefault.SelectMany(x => whereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => whereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(x => whereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => whereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(x => whereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => whereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(x => whereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => whereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var whereWhereInt = new[] { 1 }.Where(x => true).Where(x => true);
                var whereWhereIntBad = new WhereWhereEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, SinglePredicate<int>>();
                try { groupByDefault.SelectMany(x => whereWhereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => whereWhereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(x => whereWhereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => whereWhereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(x => whereWhereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => whereWhereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(x => whereWhereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => whereWhereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var orderByInt = new[] { 1 }.OrderBy(x => x);
                var orderByIntBad = new OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>();
                try { groupByDefault.SelectMany(x => orderByInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => orderByInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(x => orderByInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany((x, ix) => orderByInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(x => orderByIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => orderByIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(x => orderByIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany((x, ix) => orderByIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefault_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecific_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultOrderedBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemDefaultOrderedBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificOrderedBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupByDefaultGood.SelectMany(groupedIntToOneItemSpecificOrderedBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefaultGood.First() },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(GroupingEnumerable<int, int>), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(GroupingEnumerable<int, int>), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(GroupingEnumerable<int, int>), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(GroupingEnumerable<int, int>), good);

                        Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

                        return Helper.NoCallValue;
                    }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),

                    typeof(IntersectDefaultEnumerable<,,,,>),
                    typeof(IntersectSpecificEnumerable<,,,,>),
                    typeof(WhereEnumerable<,,>),
                    typeof(WhereWhereEnumerable<,,,>),

                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // groupBySpecific
            {
                try { groupBySpecific.SelectMany(groupedIntToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmpty, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmpty_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToEmptyOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupByDefault_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupByDefaultBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupByDefaultBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToGroupBySpecific_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupBySpecificBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToGroupBySpecificBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToLookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToLookup_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToLookupBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToLookupBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRange_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToRangeBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToRangeBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRepeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToRepeat_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToRepeatBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToRepeatBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToReverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToReverseRange_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToReverseRangeBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToReverseRangeBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var intersectDefaultInt = new[] { 1 }.Intersect(new[] { 1 });
                var intersectDefaultIntBad = new IntersectDefaultEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { groupBySpecific.SelectMany(x => intersectDefaultInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => intersectDefaultInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(x => intersectDefaultInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => intersectDefaultInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(x => intersectDefaultIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => intersectDefaultIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(x => intersectDefaultIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => intersectDefaultIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var intersectSpecificInt = new[] { 1 }.Intersect(new[] { 1 }, new _IntComparer());
                var intersectSpecificIntBad = new IntersectSpecificEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { groupBySpecific.SelectMany(x => intersectSpecificInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => intersectSpecificInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(x => intersectSpecificInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => intersectSpecificInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(x => intersectSpecificIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => intersectSpecificIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(x => intersectSpecificIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => intersectSpecificIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var whereInt = new[] { 1 }.Where(x => true);
                var whereIntBad = new WhereEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { groupBySpecific.SelectMany(x => whereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => whereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(x => whereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => whereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(x => whereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => whereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(x => whereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => whereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var whereWhereInt = new[] { 1 }.Where(x => true).Where(x => true);
                var whereWhereIntBad = new WhereWhereEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, SinglePredicate<int>>();
                try { groupBySpecific.SelectMany(x => whereWhereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => whereWhereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(x => whereWhereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => whereWhereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(x => whereWhereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => whereWhereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(x => whereWhereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => whereWhereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var orderByInt = new[] { 1 }.OrderBy(x => x);
                var orderByIntBad = new OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>();
                try { groupBySpecific.SelectMany(x => orderByInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => orderByInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(x => orderByInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany((x, ix) => orderByInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(x => orderByIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => orderByIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(x => orderByIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany((x, ix) => orderByIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefault_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecific_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultOrderedBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemDefaultOrderedBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificOrderedBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { groupBySpecificGood.SelectMany(groupedIntToOneItemSpecificOrderedBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecificGood.First() },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(GroupingEnumerable<int, int>), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(GroupingEnumerable<int, int>), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(GroupingEnumerable<int, int>), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(GroupingEnumerable<int, int>), good);

                        Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

                        return Helper.NoCallValue;
                    }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),

                    typeof(IntersectDefaultEnumerable<,,,,>),
                    typeof(IntersectSpecificEnumerable<,,,,>),
                    typeof(WhereEnumerable<,,>),
                    typeof(WhereWhereEnumerable<,,,>),

                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // lookup
            {
                try { lookup.SelectMany(groupedIntToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmpty, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmpty_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { lookup.SelectMany(groupedIntToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmptyOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToEmptyOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { lookup.SelectMany(groupedIntToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupByDefault, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupByDefault_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToGroupByDefaultBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToGroupByDefaultBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupBySpecific, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToGroupBySpecific_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToGroupBySpecificBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToGroupBySpecificBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToLookup, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToLookup_indexed, groupingResultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToLookupBad, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToLookupBad_indexed, groupingResultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRange_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToRangeBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToRangeBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRepeat, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToRepeat_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToRepeatBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToRepeatBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToReverseRange, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToReverseRange_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToReverseRangeBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToReverseRangeBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var intersectDefaultInt = new[] { 1 }.Intersect(new[] { 1 });
                var intersectDefaultIntBad = new IntersectDefaultEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { lookup.SelectMany(x => intersectDefaultInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => intersectDefaultInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(x => intersectDefaultInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => intersectDefaultInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(x => intersectDefaultIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => intersectDefaultIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(x => intersectDefaultIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => intersectDefaultIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var intersectSpecificInt = new[] { 1 }.Intersect(new[] { 1 }, new _IntComparer());
                var intersectSpecificIntBad = new IntersectSpecificEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { lookup.SelectMany(x => intersectSpecificInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => intersectSpecificInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(x => intersectSpecificInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => intersectSpecificInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(x => intersectSpecificIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => intersectSpecificIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(x => intersectSpecificIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => intersectSpecificIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var whereInt = new[] { 1 }.Where(x => true);
                var whereIntBad = new WhereEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
                try { lookup.SelectMany(x => whereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => whereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(x => whereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => whereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(x => whereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => whereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(x => whereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => whereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var whereWhereInt = new[] { 1 }.Where(x => true).Where(x => true);
                var whereWhereIntBad = new WhereWhereEnumerable<int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>, SinglePredicate<int>>();
                try { lookup.SelectMany(x => whereWhereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => whereWhereInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(x => whereWhereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => whereWhereInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(x => whereWhereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => whereWhereIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(x => whereWhereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => whereWhereIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                var orderByInt = new[] { 1 }.OrderBy(x => x);
                var orderByIntBad = new OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>();
                try { lookup.SelectMany(x => orderByInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => orderByInt); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(x => orderByInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany((x, ix) => orderByInt, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(x => orderByIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => orderByIntBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(x => orderByIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany((x, ix) => orderByIntBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefault, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefault_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecific, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecific_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemDefaultOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultOrderedBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemDefaultOrderedBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.SelectMany(groupedIntToOneItemSpecificOrdered_indexed, groupingResultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificOrderedBad, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { lookupGood.SelectMany(groupedIntToOneItemSpecificOrderedBad_indexed, groupingResultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { groupBySpecificGood.First() },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(GroupingEnumerable<int, int>), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(GroupingEnumerable<int, int>), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(GroupingEnumerable<int, int>), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(GroupingEnumerable<int, int>), good);

                        Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

                        return Helper.NoCallValue;
                    }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),

                    typeof(IntersectDefaultEnumerable<,,,,>),
                    typeof(IntersectSpecificEnumerable<,,,,>),
                    typeof(WhereEnumerable<,,>),
                    typeof(WhereWhereEnumerable<,,,>),

                    typeof(OrderByEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Weird2()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
            var lookup = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            var emptyGood = Enumerable.Empty<int>();
            var emptyOrderedGood = emptyGood.OrderBy(x => x);
            var groupByDefaultGood = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecificGood = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookupGood = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var rangeGood = Enumerable.Range(1, 5);
            var repeatGood = Enumerable.Repeat(3, 5);
            var reverseRangeGood = Enumerable.Range(1, 5).Reverse();
            var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
            var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = x => emptyGood;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = x => emptyOrderedGood;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = x => groupByDefaultGood;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = x => groupBySpecificGood;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = x => lookupGood;
            Func<int, RangeEnumerable<int>> intToRange = x => rangeGood;
            Func<int, RepeatEnumerable<int>> intToRepeat = x => repeatGood;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = x => reverseRangeGood;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = x => oneItemDefaultGood;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = x => oneItemSpecificGood;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = x => oneItemDefaultOrderedGood;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = x => oneItemSpecificOrderedGood;

            // no-index, bad
            Func<int, EmptyEnumerable<int>> intToEmptyBad = x => empty;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrderedBad = x => emptyOrdered;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefaultBad = x => groupByDefault;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecificBad = x => groupBySpecific;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookupBad = x => lookup;
            Func<int, RangeEnumerable<int>> intToRangeBad = x => range;
            Func<int, RepeatEnumerable<int>> intToRepeatBad = x => repeat;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRangeBad = x => reverseRange;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefaultBad = x => oneItemDefault;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecificBad = x => oneItemSpecific;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrderedBad = x => oneItemDefaultOrdered;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrderedBad = x => oneItemSpecificOrdered;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = x => emptyGood;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = x => emptyOrderedGood;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = x => groupByDefaultGood;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = x => groupBySpecificGood;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = x => lookupGood;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = x => rangeGood;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = x => repeatGood;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = x => reverseRangeGood;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = x => oneItemDefaultGood;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = x => oneItemSpecificGood;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = x => oneItemDefaultOrderedGood;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = x => oneItemSpecificOrderedGood;

            // no-index, grouped, bad
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmptyBad = x => empty;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrderedBad = x => emptyOrdered;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefaultBad = x => groupByDefault;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecificBad = x => groupBySpecific;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookupBad = x => lookup;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRangeBad = x => range;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeatBad = x => repeat;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRangeBad = x => reverseRange;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefaultBad = x => oneItemDefault;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecificBad = x => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrderedBad = x => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrderedBad = x => oneItemSpecificOrdered;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = (x, _) => emptyGood;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = (x, _) => emptyOrderedGood;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = (x, _) => groupByDefaultGood;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = (x, _) => groupBySpecificGood;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = (x, _) => lookupGood;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = (x, _) => rangeGood;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = (x, _) => repeatGood;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = (x, _) => reverseRangeGood;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = (x, _) => oneItemDefaultGood;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = (x, _) => oneItemSpecificGood;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrderedGood;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrderedGood;

            // indexed, bad
            Func<int, int, EmptyEnumerable<int>> intToEmptyBad_indexed = (x, _) => empty;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrderedBad_indexed = (x, _) => emptyOrdered;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefaultBad_indexed = (x, _) => groupByDefault;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecificBad_indexed = (x, _) => groupBySpecific;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookupBad_indexed = (x, _) => lookup;
            Func<int, int, RangeEnumerable<int>> intToRangeBad_indexed = (x, _) => range;
            Func<int, int, RepeatEnumerable<int>> intToRepeatBad_indexed = (x, _) => repeat;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRangeBad_indexed = (x, _) => reverseRange;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefaultBad_indexed = (x, _) => oneItemDefault;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecificBad_indexed = (x, _) => oneItemSpecific;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrderedBad_indexed = (x, _) => oneItemDefaultOrdered;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrderedBad_indexed = (x, _) => oneItemSpecificOrdered;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = (x, _) => emptyGood;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = (x, _) => emptyOrderedGood;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = (x, _) => groupByDefaultGood;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = (x, _) => groupBySpecificGood;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = (x, _) => lookupGood;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = (x, _) => rangeGood;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = (x, _) => repeatGood;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = (x, _) => reverseRangeGood;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = (x, _) => oneItemDefaultGood;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = (x, _) => oneItemSpecificGood;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrderedGood;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrderedGood;

            // indexed, grouped, bad
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmptyBad_indexed = (x, _) => empty;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrderedBad_indexed = (x, _) => emptyOrdered;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefaultBad_indexed = (x, _) => groupByDefault;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecificBad_indexed = (x, _) => groupBySpecific;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookupBad_indexed = (x, _) => lookup;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRangeBad_indexed = (x, _) => range;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeatBad_indexed = (x, _) => repeat;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRangeBad_indexed = (x, _) => reverseRange;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefaultBad_indexed = (x, _) => oneItemDefault;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecificBad_indexed = (x, _) => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrderedBad_indexed = (x, _) => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrderedBad_indexed = (x, _) => oneItemSpecificOrdered;

            // result
            Func<int, int, int> resultSelector = (a, _) => a;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = (a, _) => a;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = (a, _) => a.Key;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = (a, _) => a.Key;

            // range
            {
                try { range.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { range.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { range.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { range.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { rangeGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { rangeGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
                try { repeat.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { repeat.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { repeat.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { repeat.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeatGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { repeatGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
                try { reverseRange.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { reverseRange.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { reverseRange.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { reverseRange.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { reverseRangeGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
        public void Malformed_Weird3()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>();
            var lookup = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            var emptyGood = Enumerable.Empty<int>();
            var emptyOrderedGood = emptyGood.OrderBy(x => x);
            var groupByDefaultGood = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecificGood = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
            var lookupGood = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var rangeGood = Enumerable.Range(1, 5);
            var repeatGood = Enumerable.Repeat(3, 5);
            var reverseRangeGood = Enumerable.Range(1, 5).Reverse();
            var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
            var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);

            // no-index
            Func<int, EmptyEnumerable<int>> intToEmpty = x => emptyGood;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrdered = x => emptyOrderedGood;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault = x => groupByDefaultGood;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific = x => groupBySpecificGood;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookup = x => lookupGood;
            Func<int, RangeEnumerable<int>> intToRange = x => rangeGood;
            Func<int, RepeatEnumerable<int>> intToRepeat = x => repeatGood;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRange = x => reverseRangeGood;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefault = x => oneItemDefaultGood;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecific = x => oneItemSpecificGood;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered = x => oneItemDefaultOrderedGood;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered = x => oneItemSpecificOrderedGood;

            // no-index, bad
            Func<int, EmptyEnumerable<int>> intToEmptyBad = x => empty;
            Func<int, EmptyOrderedEnumerable<int>> intToEmptyOrderedBad = x => emptyOrdered;
            Func<int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefaultBad = x => groupByDefault;
            Func<int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecificBad = x => groupBySpecific;
            Func<int, LookupDefaultEnumerable<int, int>> intToLookupBad = x => lookup;
            Func<int, RangeEnumerable<int>> intToRangeBad = x => range;
            Func<int, RepeatEnumerable<int>> intToRepeatBad = x => repeat;
            Func<int, ReverseRangeEnumerable<int>> intToReverseRangeBad = x => reverseRange;
            Func<int, OneItemDefaultEnumerable<int>> intToOneItemDefaultBad = x => oneItemDefault;
            Func<int, OneItemSpecificEnumerable<int>> intToOneItemSpecificBad = x => oneItemSpecific;
            Func<int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrderedBad = x => oneItemDefaultOrdered;
            Func<int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrderedBad = x => oneItemSpecificOrdered;

            // no-index, grouped
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmpty = x => emptyGood;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered = x => emptyOrderedGood;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault = x => groupByDefaultGood;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific = x => groupBySpecificGood;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookup = x => lookupGood;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRange = x => rangeGood;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeat = x => repeatGood;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRange = x => reverseRangeGood;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault = x => oneItemDefaultGood;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific = x => oneItemSpecificGood;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered = x => oneItemDefaultOrderedGood;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered = x => oneItemSpecificOrderedGood;

            // no-index, grouped, bad
            Func<GroupingEnumerable<int, int>, EmptyEnumerable<int>> groupedIntToEmptyBad = x => empty;
            Func<GroupingEnumerable<int, int>, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrderedBad = x => emptyOrdered;
            Func<GroupingEnumerable<int, int>, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefaultBad = x => groupByDefault;
            Func<GroupingEnumerable<int, int>, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecificBad = x => groupBySpecific;
            Func<GroupingEnumerable<int, int>, LookupDefaultEnumerable<int, int>> groupedIntToLookupBad = x => lookup;
            Func<GroupingEnumerable<int, int>, RangeEnumerable<int>> groupedIntToRangeBad = x => range;
            Func<GroupingEnumerable<int, int>, RepeatEnumerable<int>> groupedIntToRepeatBad = x => repeat;
            Func<GroupingEnumerable<int, int>, ReverseRangeEnumerable<int>> groupedIntToReverseRangeBad = x => reverseRange;
            Func<GroupingEnumerable<int, int>, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefaultBad = x => oneItemDefault;
            Func<GroupingEnumerable<int, int>, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecificBad = x => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrderedBad = x => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrderedBad = x => oneItemSpecificOrdered;

            // indexed
            Func<int, int, EmptyEnumerable<int>> intToEmpty_indexed = (x, _) => emptyGood;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrdered_indexed = (x, _) => emptyOrderedGood;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefault_indexed = (x, _) => groupByDefaultGood;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecific_indexed = (x, _) => groupBySpecificGood;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookup_indexed = (x, _) => lookupGood;
            Func<int, int, RangeEnumerable<int>> intToRange_indexed = (x, _) => rangeGood;
            Func<int, int, RepeatEnumerable<int>> intToRepeat_indexed = (x, _) => repeatGood;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRange_indexed = (x, _) => reverseRangeGood;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefault_indexed = (x, _) => oneItemDefaultGood;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecific_indexed = (x, _) => oneItemSpecificGood;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrderedGood;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrderedGood;

            // indexed, bad
            Func<int, int, EmptyEnumerable<int>> intToEmptyBad_indexed = (x, _) => empty;
            Func<int, int, EmptyOrderedEnumerable<int>> intToEmptyOrderedBad_indexed = (x, _) => emptyOrdered;
            Func<int, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupByDefaultBad_indexed = (x, _) => groupByDefault;
            Func<int, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> intToGroupBySpecificBad_indexed = (x, _) => groupBySpecific;
            Func<int, int, LookupDefaultEnumerable<int, int>> intToLookupBad_indexed = (x, _) => lookup;
            Func<int, int, RangeEnumerable<int>> intToRangeBad_indexed = (x, _) => range;
            Func<int, int, RepeatEnumerable<int>> intToRepeatBad_indexed = (x, _) => repeat;
            Func<int, int, ReverseRangeEnumerable<int>> intToReverseRangeBad_indexed = (x, _) => reverseRange;
            Func<int, int, OneItemDefaultEnumerable<int>> intToOneItemDefaultBad_indexed = (x, _) => oneItemDefault;
            Func<int, int, OneItemSpecificEnumerable<int>> intToOneItemSpecificBad_indexed = (x, _) => oneItemSpecific;
            Func<int, int, OneItemDefaultOrderedEnumerable<int>> intToOneItemDefaultOrderedBad_indexed = (x, _) => oneItemDefaultOrdered;
            Func<int, int, OneItemSpecificOrderedEnumerable<int>> intToOneItemSpecificOrderedBad_indexed = (x, _) => oneItemSpecificOrdered;

            // indexed, grouped
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmpty_indexed = (x, _) => emptyGood;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrdered_indexed = (x, _) => emptyOrderedGood;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefault_indexed = (x, _) => groupByDefaultGood;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecific_indexed = (x, _) => groupBySpecificGood;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookup_indexed = (x, _) => lookupGood;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRange_indexed = (x, _) => rangeGood;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeat_indexed = (x, _) => repeatGood;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRange_indexed = (x, _) => reverseRangeGood;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefault_indexed = (x, _) => oneItemDefaultGood;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecific_indexed = (x, _) => oneItemSpecificGood;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrdered_indexed = (x, _) => oneItemDefaultOrderedGood;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrdered_indexed = (x, _) => oneItemSpecificOrderedGood;

            // indexed, grouped, bad
            Func<GroupingEnumerable<int, int>, int, EmptyEnumerable<int>> groupedIntToEmptyBad_indexed = (x, _) => empty;
            Func<GroupingEnumerable<int, int>, int, EmptyOrderedEnumerable<int>> groupedIntToEmptyOrderedBad_indexed = (x, _) => emptyOrdered;
            Func<GroupingEnumerable<int, int>, int, GroupByDefaultEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupByDefaultBad_indexed = (x, _) => groupByDefault;
            Func<GroupingEnumerable<int, int>, int, GroupBySpecificEnumerable<int, int, int, IdentityEnumerable<int, int[], ArrayBridger<int>, ArrayEnumerator<int>>, ArrayEnumerator<int>>> groupedIntToGroupBySpecificBad_indexed = (x, _) => groupBySpecific;
            Func<GroupingEnumerable<int, int>, int, LookupDefaultEnumerable<int, int>> groupedIntToLookupBad_indexed = (x, _) => lookup;
            Func<GroupingEnumerable<int, int>, int, RangeEnumerable<int>> groupedIntToRangeBad_indexed = (x, _) => range;
            Func<GroupingEnumerable<int, int>, int, RepeatEnumerable<int>> groupedIntToRepeatBad_indexed = (x, _) => repeat;
            Func<GroupingEnumerable<int, int>, int, ReverseRangeEnumerable<int>> groupedIntToReverseRangeBad_indexed = (x, _) => reverseRange;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultEnumerable<int>> groupedIntToOneItemDefaultBad_indexed = (x, _) => oneItemDefault;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificEnumerable<int>> groupedIntToOneItemSpecificBad_indexed = (x, _) => oneItemSpecific;
            Func<GroupingEnumerable<int, int>, int, OneItemDefaultOrderedEnumerable<int>> groupedIntToOneItemDefaultOrderedBad_indexed = (x, _) => oneItemDefaultOrdered;
            Func<GroupingEnumerable<int, int>, int, OneItemSpecificOrderedEnumerable<int>> groupedIntToOneItemSpecificOrderedBad_indexed = (x, _) => oneItemSpecificOrdered;

            // result
            Func<int, int, int> resultSelector = (a, _) => a;

            // result, grouped
            Func<int, GroupingEnumerable<int, int>, int> resultSelector_grouping = (a, _) => a;

            // grouped result
            Func<GroupingEnumerable<int, int>, int, int> groupingResultSelector = (a, _) => a.Key;

            // grouped result, grouped
            Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int> groupingResultSelector_grouping = (a, _) => a.Key;

            // oneItemDefault
            {
                try { oneItemDefault.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemDefault.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemDefault.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
                try { oneItemSpecific.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemSpecific.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemSpecific.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
                try { oneItemDefaultOrdered.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemDefaultOrderedGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
                try { oneItemSpecificOrdered.SelectMany(intToEmpty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmpty_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmpty, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmpty_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToEmptyOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                // projections can't error on empty

                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupByDefault_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupByDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupByDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupByDefaultBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupByDefaultBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToGroupBySpecific_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupBySpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupBySpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupBySpecificBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToGroupBySpecificBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToLookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToLookup_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToLookup, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToLookup_indexed, resultSelector_grouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToLookupBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToLookupBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToLookupBad, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToLookupBad_indexed, resultSelector_grouping).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToRepeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRepeat_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRepeat, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToRepeat_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRepeatBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRepeatBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRepeatBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToRepeatBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToReverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToReverseRange_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToReverseRange, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToReverseRange_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToReverseRangeBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToReverseRangeBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToReverseRangeBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToReverseRangeBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefault_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecific_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemDefaultOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemDefaultOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered_indexed); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.SelectMany(intToOneItemSpecificOrdered_indexed, resultSelector); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificOrderedBad).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificOrderedBad_indexed).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificOrderedBad, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }
                try { oneItemSpecificOrderedGood.SelectMany(intToOneItemSpecificOrderedBad_indexed, resultSelector).ToArray(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Uninitialized enumerable returned by projection", exc.Message); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(bad, good) =>
                      {
                        var projFromGoodToBad = (new SelectManyTests())._MakeIdentityProjection(default(int), bad);
                        var projFromGoodToBad_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), bad);

                        var projFromBadToGood = (new SelectManyTests())._MakeIdentityProjection(default(int), good);
                        var projFromBadToGood_indexed = (new SelectManyTests())._MakeIdentityIndexedProjection(default(int), good);

                        Func<int, int, int> resultSelector = (x, y) => x;

                        try { bad.SelectMany(projFromBadToGood); Assert.Fail(""1""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed); Assert.Fail(""2""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood, resultSelector); Assert.Fail(""3""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }
                        try { bad.SelectMany(projFromBadToGood_indexed, resultSelector); Assert.Fail(""4""); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); }

                        try { good.SelectMany(projFromGoodToBad).ToArray(); Assert.Fail(""5""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed).ToArray(); Assert.Fail(""6""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad, resultSelector).ToArray(); Assert.Fail(""7""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }
                        try { good.SelectMany(projFromGoodToBad_indexed, resultSelector).ToArray(); Assert.Fail(""8""); } catch (InvalidOperationException exc) { Assert.AreEqual(""Uninitialized enumerable returned by projection"", exc.Message); }

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
        public void SimpleBridge()
        {
            var e = new[] { 1, 2, 3 };
            var asSelectMany = e.SelectMany(i => new[] { i, i * 2 });

            Assert.IsTrue(asSelectMany.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asSelectMany)
            {
                ret.Add(item);
            }

            Assert.AreEqual(6, ret.Count);
            Assert.AreEqual(1, ret[0]);
            Assert.AreEqual(2, ret[1]);
            Assert.AreEqual(2, ret[2]);
            Assert.AreEqual(4, ret[3]);
            Assert.AreEqual(3, ret[4]);
            Assert.AreEqual(6, ret[5]);
        }

        [TestMethod]
        public void IndexedBridge()
        {
            var e = new[] { 1, 2, 3 };
            Func<int, int, IEnumerable<int>> f = (i, ix) => new[] { i, ix };
            var asSelectMany = e.SelectMany(f);

            Assert.IsTrue(asSelectMany.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asSelectMany)
            {
                ret.Add(item);
            }

            Assert.AreEqual(6, ret.Count);
            Assert.AreEqual(1, ret[0]);
            Assert.AreEqual(0, ret[1]);
            Assert.AreEqual(2, ret[2]);
            Assert.AreEqual(1, ret[3]);
            Assert.AreEqual(3, ret[4]);
            Assert.AreEqual(2, ret[5]);
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3 };
            Func<int, RangeEnumerable<int>> f = i => Enumerable.Range(i, 3);
            var asSelectMany = e.SelectMany(f);

            Assert.IsTrue(asSelectMany.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asSelectMany)
            {
                ret.Add(item);
            }

            Assert.AreEqual(9, ret.Count);
            Assert.AreEqual(1, ret[0]);
            Assert.AreEqual(2, ret[1]);
            Assert.AreEqual(3, ret[2]);
            Assert.AreEqual(2, ret[3]);
            Assert.AreEqual(3, ret[4]);
            Assert.AreEqual(4, ret[5]);
            Assert.AreEqual(3, ret[6]);
            Assert.AreEqual(4, ret[7]);
            Assert.AreEqual(5, ret[8]);
        }

        [TestMethod]
        public void CollectionBridge()
        {
            var e = new[] { 1, 2, 3 };
            Func<int, IEnumerable<string>> c = i => new[] { i.ToString(), (i * 2).ToString() };
            Func<int, string, double> r = (i, s) => double.Parse(i + "." + s);
            var asSelectMany = e.SelectMany(c, r);

            Assert.IsTrue(asSelectMany.GetType().IsValueType);

            var ret = new List<double>();
            foreach (var item in asSelectMany)
            {
                ret.Add(item);
            }

            // 1, 2, 3
            // (1, 2), (2, 4), (3, 6)
            // (1.1, 1.2), (2.2, 2.4), (3.3, 3.6)

            Assert.AreEqual(6, ret.Count);
            Assert.AreEqual(1.1, ret[0]);
            Assert.AreEqual(1.2, ret[1]);
            Assert.AreEqual(2.2, ret[2]);
            Assert.AreEqual(2.4, ret[3]);
            Assert.AreEqual(3.3, ret[4]);
            Assert.AreEqual(3.6, ret[5]);
        }

        [TestMethod]
        public void CollectionIndexedBridge()
        {
            var e = new[] { 1, 2, 3 };
            Func<int, int, IEnumerable<string>> c = (i, ix) => new[] { (i + ix).ToString(), (i * 2).ToString() };
            Func<int, string, double> r = (i, s) => double.Parse(i + "." + s);
            var asSelectMany = e.SelectMany(c, r);

            Assert.IsTrue(asSelectMany.GetType().IsValueType);

            var ret = new List<double>();
            foreach (var item in asSelectMany)
            {
                ret.Add(item);
            }

            // 1, 2, 3
            // (1, 2), (3, 4), (5, 6)
            // (1.1, 1.2), (2.3, 2.4), (3.5, 3.6)

            Assert.AreEqual(6, ret.Count);
            Assert.AreEqual(1.1, ret[0]);
            Assert.AreEqual(1.2, ret[1]);
            Assert.AreEqual(2.3, ret[2]);
            Assert.AreEqual(2.4, ret[3]);
            Assert.AreEqual(3.5, ret[4]);
            Assert.AreEqual(3.6, ret[5]);
        }
    }
}
