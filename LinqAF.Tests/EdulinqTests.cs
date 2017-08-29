#region Copyright and license information
// Copyright 2010-2011 Jon Skeet
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

// modified from https://github.com/jskeet/edulinq/tree/master/src/Edulinq.Tests; license reproduced above

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LinqAF;
using TestHelpers;
using System.Globalization;

namespace LinqAF.Tests
{
    [TestClass]
    public class EdulinqTests
    {
        // Aggregate

        [TestMethod]
        public void Aggregate_NullSourceUnseeded()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Aggregate((x, y) => x + y));
        }

        [TestMethod]
        public void Aggregate_NullFuncUnseeded()
        {
            int[] source = { 1, 3 };
            Helper.Throws<ArgumentNullException>(() => source.Aggregate(null));
        }

        [TestMethod]
        public void Aggregate_UnseededAggregation()
        {
            int[] source = { 1, 4, 5 };
            // First iteration: 0 * 2 + 1 = 1
            // Second iteration: 1 * 2 + 4 = 6
            // Third iteration: 6 * 2 + 5 = 17
            Assert.AreEqual(17, source.Aggregate((current, value) => current * 2 + value));
        }

        [TestMethod]
        public void Aggregate_NullSourceSeeded()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y));
        }

        [TestMethod]
        public void Aggregate_NullFuncSeeded()
        {
            int[] source = { 1, 3 };
            Helper.Throws<ArgumentNullException>(() => source.Aggregate(5, null));
        }

        [TestMethod]
        public void Aggregate_SeededAggregation()
        {
            int[] source = { 1, 4, 5 };
            int seed = 5;
            Func<int, int, int> func = (current, value) => current * 2 + value;
            // First iteration: 5 * 2 + 1 = 11
            // Second iteration: 11 * 2 + 4 = 26
            // Third iteration: 26 * 2 + 5 = 57
            Assert.AreEqual(57, source.Aggregate(seed, func));
        }

        [TestMethod]
        public void Aggregate_NullSourceSeededWithResultSelector()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y, result => result.ToInvariantString()));
        }

        [TestMethod]
        public void Aggregate_NullFuncSeededWithResultSelector()
        {
            int[] source = { 1, 3 };
            Helper.Throws<ArgumentNullException>(() => source.Aggregate(5, null, result => result.ToInvariantString()));
        }

        [TestMethod]
        public void Aggregate_NullProjectionSeededWithResultSelector()
        {
            int[] source = { 1, 3 };
            Func<int, string> resultSelector = null;
            Helper.Throws<ArgumentNullException>(() => source.Aggregate(5, (x, y) => x + y, resultSelector));
        }

        [TestMethod]
        public void Aggregate_SeededAggregationWithResultSelector()
        {
            int[] source = { 1, 4, 5 };
            int seed = 5;
            Func<int, int, int> func = (current, value) => current * 2 + value;
            Func<int, string> resultSelector = result => result.ToInvariantString();
            // First iteration: 5 * 2 + 1 = 11
            // Second iteration: 11 * 2 + 4 = 26
            // Third iteration: 26 * 2 + 5 = 57
            // Result projection: 57.ToInvariantString() = "57"
            Assert.AreEqual("57", source.Aggregate(seed, func, resultSelector));
        }

        [TestMethod]
        public void Aggregate_DifferentSourceAndAccumulatorTypes()
        {
            int largeValue = 2000000000;
            int[] source = { largeValue, largeValue, largeValue };
            long sum = source.Aggregate(0L, (acc, value) => acc + value);
            Assert.AreEqual(6000000000L, sum);
            // Just to prove we haven't missed off a zero...
            Assert.IsTrue(sum > int.MaxValue);
        }

        [TestMethod]
        public void Aggregate_EmptySequenceUnseeded()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Aggregate((x, y) => x + y));
        }

        [TestMethod]
        public void Aggregate_EmptySequenceSeeded()
        {
            int[] source = { };
            Assert.AreEqual(5, source.Aggregate(5, (x, y) => x + y));
        }

        [TestMethod]
        public void Aggregate_EmptySequenceSeededWithResultSelector()
        {
            int[] source = { };
            Assert.AreEqual("5", source.Aggregate(5, (x, y) => x + y, x => x.ToInvariantString()));
        }

        // Originally I'd thought it was the default value of TSource which was used as the seed...
        [TestMethod]
        public void Aggregate_FirstElementOfInputIsUsedAsSeedForUnseededOverload()
        {
            int[] source = { 5, 3, 2 };
            Assert.AreEqual(30, source.Aggregate((acc, value) => acc * value));
        }

        // All
        [TestMethod]
        public void All_NullSource()
        {
            int[] src = null;
            Helper.Throws<ArgumentNullException>(() => src.All(x => x > 10));
        }

        [TestMethod]
        public void All_NullPredicate()
        {
            int[] src = { 1, 3, 5 };
            Helper.Throws<ArgumentNullException>(() => src.All(null));
        }

        [TestMethod]
        public void All_EmptySequenceReturnsTrue()
        {
            Assert.IsTrue(new int[0].All(x => x > 0));
        }

        [TestMethod]
        public void All_PredicateMatchingNoElements()
        {
            int[] src = { 1, 5, 20, 30 };
            Assert.IsFalse(src.All(x => x < 0));
        }

        [TestMethod]
        public void All_PredicateMatchingSomeElements()
        {
            int[] src = { 1, 5, 8, 9 };
            Assert.IsFalse(src.All(x => x > 3));
        }

        [TestMethod]
        public void All_PredicateMatchingAllElements()
        {
            int[] src = { 1, 5, 8, 9 };
            Assert.IsTrue(src.All(x => x > 0));
        }

        [TestMethod]
        public void All_SequenceIsNotEvaluatedAfterFirstNonMatch()
        {
            int[] src = { 2, 10, 0, 3 };
            var query = src.Select(x => 10 / x);
            // This will finish at the second element (x = 10, so 10/x = 1)
            // It won't evaluate 10/0, which would throw an exception
            Assert.IsFalse(query.All(y => y > 2));
        }

        // Any
        [TestMethod]
        public void Any_NullSourceWithoutPredicate()
        {
            int[] src = null;
            Helper.Throws<ArgumentNullException>(() => src.Any());
        }

        [TestMethod]
        public void Any_NullSourceWithPredicate()
        {
            int[] src = null;
            Helper.Throws<ArgumentNullException>(() => src.Any(x => x > 10));
        }

        [TestMethod]
        public void Any_NullPredicate()
        {
            int[] src = { 1, 3, 5 };
            Helper.Throws<ArgumentNullException>(() => src.Any(null));
        }

        [TestMethod]
        public void Any_EmptySequenceWithoutPredicate()
        {
            Assert.IsFalse(new int[0].Any());
        }

        [TestMethod]
        public void Any_EmptySequenceWithPredicate()
        {
            Assert.IsFalse(new int[0].Any(x => x > 10));
        }

        [TestMethod]
        public void Any_NonEmptySequenceWithoutPredicate()
        {
            Assert.IsTrue(new int[1].Any());
        }

        [TestMethod]
        public void Any_NonEmptySequenceWithPredicateMatchingElement()
        {
            int[] src = { 1, 5, 20, 30 };
            Assert.IsTrue(src.Any(x => x > 10));
        }

        [TestMethod]
        public void Any_NonEmptySequenceWithPredicateNotMatchingElement()
        {
            int[] src = { 1, 5, 8, 9 };
            Assert.IsFalse(src.Any(x => x > 10));
        }

        [TestMethod]
        public void Any_SequenceIsNotEvaluatedAfterFirstMatch()
        {
            int[] src = { 10, 2, 0, 3 };
            var query = src.Select(x => 10 / x);
            // This will finish at the second element (x = 2, so 10/x = 5)
            // It won't evaluate 10/0, which would throw an exception
            Assert.IsTrue(query.Any(y => y > 2));
        }

        // AsEnumerable
        [TestMethod]
        public void AsEnumerable_NullSourceIsPermitted()
        {
            IEnumerable<string> source = null;
            Assert.IsNull(source.AsEnumerable());
        }

        [TestMethod]
        public void AsEnumerable_DoesNotCallGetEnumerator()
        {
            var source = new ThrowingEnumerable();
            Assert.AreSame(source, source.AsEnumerable());
        }

        // not valid for LinqAF
        /*[TestMethod]
        public void AsEnumerable_NormalSequence()
        {
            var range = Enumerable.Range(0, 10);
            Assert.AreSame(range, range.AsEnumerable());
        }*/

        [TestMethod]
        public void AsEnumerable_AnonymousType()
        {
            var list = new[] {
                new { FirstName = "Jon", Surname = "Skeet" },
                new { FirstName = "Holly", Surname = "Skeet" }
            }.ToList();

            // We can't cast to IEnumerable<T> as we can't express T.
            var sequence = list.AsEnumerable();
            // This will now use Enumerable.Contains instead of List.Contains
            Assert.IsFalse(sequence.Contains(new { FirstName = "Tom", Surname = "Skeet" }));
        }

        // Average
        #region General Int32 tests
        [TestMethod]
        public void Average_NullSourceInt32NoSelector()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Average());
        }

        [TestMethod]
        public void Average_NullSourceInt32WithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Average(x => x.Length));
        }

        [TestMethod]
        public void Average_NullSourceInt32Selector()
        {
            string[] source = { "" };
            Func<string, int> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Average(selector));
        }

        [TestMethod]
        public void Average_NullSourceNullableInt32NoSelector()
        {
            int?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Average());
        }

        [TestMethod]
        public void Average_NullSourceNullableInt32WithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Average(x => (int?)x.Length));
        }

        [TestMethod]
        public void Average_NullSelectorNullableInt32()
        {
            string[] source = { "" };
            Func<string, int?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Average(selector));
        }

        [TestMethod]
        public void Average_EmptySequenceInt32NoSelector()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Average());
        }

        [TestMethod]
        public void Average_EmptySequenceInt32WithSelector()
        {
            string[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Average(x => x.Length));
        }

        [TestMethod]
        public void Average_EmptySequenceNullableInt32NoSelector()
        {
            int?[] source = { };
            Assert.IsNull(source.Average());
        }

        [TestMethod]
        public void Average_EmptySequenceNullableInt32WithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Average(x => (int?)x.Length));
        }

        [TestMethod]
        public void Average_AllNullsSequenceNullableInt32NoSelector()
        {
            int?[] source = { null, null, null };
            Assert.IsNull(source.Average());
        }

        [TestMethod]
        public void Average_AllNullsSequenceNullableInt32WithSelector()
        {
            string[] source = { "x", "y", "z" };
            Assert.IsNull(source.Average(x => (int?)null));
        }

        [TestMethod]
        public void Average_SimpleAverageInt32NoSelector()
        {
            // Note that 7.5 is exactly representable as a double, so we
            // shouldn't need to worry about floating-point inaccuracies
            int[] source = { 5, 10, 0, 15 };
            Assert.AreEqual(7.5d, source.Average());
        }

        [TestMethod]
        public void Average_SimpleAverageNullableInt32NoSelector()
        {
            int?[] source = { 5, 10, 0, 15 };
            Assert.AreEqual((double?)7.5d, source.Average());
        }

        [TestMethod]
        public void Average_AverageIgnoresNullsNullableInt32NoSelector()
        {
            // The nulls here don't reduce the average
            int?[] source = { 5, null, 10, null, 0, null, 15 };
            Assert.AreEqual((double?)7.5d, source.Average());
        }

        [TestMethod]
        public void Average_SimpleAverageInt32WithSelector()
        {
            string[] source = { "", "abcd", "a", "b" };
            Assert.AreEqual(1.5d, source.Average(x => x.Length));
        }

        [TestMethod]
        public void Average_SimpleAverageNullableInt32WithSelector()
        {
            string[] source = { "", "abcd", "a", "b" };
            Assert.AreEqual((double?)1.5d, source.Average(x => (int?)x.Length));
        }

        [TestMethod]
        public void Average_AverageIgnoresNullsNullableInt32WithSelector()
        {
            // The nulls here don't reduce the average
            string[] source = { "", null, "abcd", null, "a", null, "b" };
            Assert.AreEqual((double?)1.5d, source.Average(x => x == null ? null : (int?)x.Length));
        }

        /*[TestMethod]
        public void Average_MoreThanInt32MaxValueElements()
        {
            var query = Enumerable.Repeat(1, int.MaxValue)
                                  .Concat(Enumerable.Repeat(1, 5));
            Assert.AreEqual(1d, query.Average());
        }*/

        #endregion

        #region Floating point fun
        [TestMethod]
        public void Average_SingleUsesDoubleAccumulator()
        {
            // All the values in the array are exactly representable as floats,
            // as is the correct average... but intermediate totals aren't.
            float[] array = { 20000000f, 1f, 1f, 2f };
            Assert.AreEqual(5000001f, array.Average());
        }

        [TestMethod]
        public void Average_SequenceContainingNan()
        {
            double[] array = { 1, 2, 3, double.NaN, 4, 5, 6 };
            Helper.IsNaN(array.Average());
        }
        #endregion

        #region Overflow tests
        [TestMethod]
        public void Average_Int32DoesNotOverflowAtInt32MaxValue()
        {
            int[] source = { int.MaxValue, int.MaxValue,
                             -int.MaxValue, -int.MaxValue};
            Assert.AreEqual(0, source.Average());
        }

        [TestMethod]
        public void Average_Int64OverflowsAtInt64MaxValue()
        {
            long[] source = { long.MaxValue, long.MaxValue,
                              -long.MaxValue, -long.MaxValue};
            Helper.Throws<OverflowException>(() => source.Average());
        }

        [TestMethod]
        public void Average_SingleDoesNotOverflowAtSingleMaxValue()
        {
            float[] source = { float.MaxValue, float.MaxValue,
                               -float.MaxValue, -float.MaxValue };
            Assert.AreEqual(0f, source.Average());
        }

        [TestMethod]
        public void Average_DoubleOverflowsToInfinity()
        {
            double[] source = { double.MaxValue, double.MaxValue,
                               -double.MaxValue, -double.MaxValue };
            Assert.IsTrue(double.IsPositiveInfinity(source.Average()));
        }

        [TestMethod]
        public void Average_DoubleOverflowsToNegativeInfinity()
        {
            double[] source = { -double.MaxValue, -double.MaxValue,
                                double.MaxValue, double.MaxValue };
            Assert.IsTrue(double.IsNegativeInfinity(source.Average()));
        }

        [TestMethod]
        public void Average_DecimalOverflowsAtDecimalMaxValue()
        {
            decimal[] source = { decimal.MaxValue, decimal.MaxValue,
                                 -decimal.MaxValue, -decimal.MaxValue };
            Helper.Throws<OverflowException>(() => source.Average());
        }

        [TestMethod]
        public void Average_Int64KeepsPrecisionAtLargeValues()
        {
            // At long.MaxValue / 2, double precision doesn't get us
            // exact integers.
            long halfMax = long.MaxValue / 2;
            double halfMaxAsDouble = (double)halfMax;
            Assert.AreNotEqual(halfMax, (long)halfMaxAsDouble);

            long[] source = { halfMax, halfMax };
            Assert.AreEqual(halfMax, source.Average());
        }
        #endregion

        // Cast
        [TestMethod]
        public void Cast_NullSource()
        {
            System.Collections.IEnumerable source = null;
            Helper.Throws<ArgumentNullException>(() => source.Cast<string>());
        }

        [TestMethod]
        public void Cast_ExecutionIsDeferred()
        {
            System.Collections.IEnumerable source = new ThrowingEnumerable();
            // No exception
            source.Cast<string>();
        }

        // these don't make sense in LinqAF
        /*[TestMethod]
        public void Cast_OriginalSourceReturnedForObviousNoOp()
        {
            System.Collections.IEnumerable strings = new List<string>();
            Assert.AreSame(strings, strings.Cast<string>());
        }

        [TestMethod]
        public void Cast_OriginalSourceReturnedDueToGenericCovariance()
        {
            System.Collections.IEnumerable strings = new List<string>();
            Assert.AreSame(strings, strings.Cast<object>());
        }

        [TestMethod]
        public void Cast_OriginalSourceReturnedForInt32ArrayToUInt32SequenceConversion()
        {
            System.Collections.IEnumerable enums = new int[10];
            Assert.AreSame(enums, enums.Cast<uint>());
        }

        [TestMethod]
        public void Cast_OriginalSourceReturnedForEnumArrayToInt32SequenceConversion()
        {
            System.Collections.IEnumerable enums = new DayOfWeek[10];
            Assert.AreSame(enums, enums.Cast<int>());
        }*/

        [TestMethod]
        public void Cast_SequenceWithAllValidValues()
        {
            System.Collections.IEnumerable strings = new object[] { "first", "second", "third" };
            Assert.IsTrue(
                strings.Cast<string>().SequenceEqual(new[] { "first", "second", "third" })
            );
        }

        [TestMethod]
        public void Cast_NullsAreIncluded()
        {
            System.Collections.IEnumerable strings = new object[] { "first", null, "third" };
            Assert.IsTrue(
                strings.Cast<string>().SequenceEqual(new[] { "first", null, "third" })
            );
        }

        [TestMethod]
        public void Cast_UnboxToInt32()
        {
            System.Collections.IEnumerable ints = new object[] { 10, 30, 50 };
            Assert.IsTrue(
                ints.Cast<int>().SequenceEqual(new[] { 10, 30, 50 })
            );
        }

        [TestMethod]
        public void Cast_UnboxToNullableInt32WithNulls()
        {
            System.Collections.IEnumerable ints = new object[] { 10, null, 30, null, 50 };
            Assert.IsTrue(
                ints.Cast<int?>().SequenceEqual(new int?[] { 10, null, 30, null, 50 })
            );
        }

        [TestMethod]
        public void Cast_CastExceptionOnWrongElementType()
        {
            System.Collections.IEnumerable objects = new object[] { "first", new object(), "third" };
            using (var iterator = objects.Cast<string>().GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual("first", iterator.Current);
                Helper.Throws<InvalidCastException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Cast_CastExceptionWhenUnboxingWrongType()
        {
            System.Collections.IEnumerable objects = new object[] { 100L, 100 };
            using (var iterator = objects.Cast<long>().GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(100L, iterator.Current);
                Helper.Throws<InvalidCastException>(() => iterator.MoveNext());
            }
        }

        // Concat
        [TestMethod]
        public void Cast_SimpleConcatenation()
        {
            IEnumerable<string> first = new string[] { "a", "b" };
            IEnumerable<string> second = new string[] { "c", "d" };
            Assert.IsTrue(
                first.Concat(second).SequenceEqual(new[] { "a", "b", "c", "d" })
            );
        }

        [TestMethod]
        public void Cast_NullFirstThrowsNullArgumentException()
        {
            IEnumerable<string> first = null;
            IEnumerable<string> second = new string[] { "hello" };
            Helper.Throws<ArgumentNullException>(() => first.Concat(second));
        }

        [TestMethod]
        public void Cast_NullSecondThrowsNullArgumentException()
        {
            IEnumerable<string> first = new string[] { "hello" };
            IEnumerable<string> second = null;
            Helper.Throws<ArgumentNullException>(() => first.Concat(second));
        }

        [TestMethod]
        public void Cast_FirstSequenceIsntAccessedBeforeFirstUse()
        {
            IEnumerable<int> first = new ThrowingEnumerable();
            IEnumerable<int> second = new int[] { 5 };
            // No exception yet...
            var query = first.Concat(second);
            // Still no exception...
            using (var iterator = query.GetEnumerator())
            {
                // Now it will go bang
                Helper.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Cast_SecondSequenceIsntAccessedBeforeFirstUse()
        {
            IEnumerable<int> first = new int[] { 5 };
            IEnumerable<int> second = new ThrowingEnumerable();
            // No exception yet...
            var query = first.Concat(second);
            // Still no exception...
            using (var iterator = query.GetEnumerator())
            {
                // First element is fine...
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);
                // Now it will go bang, as we move into the second sequence
                Helper.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        // Contains
        [TestMethod]
        public void Contains_NullSourceNoComparer()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Contains("x"));
        }

        [TestMethod]
        public void Contains_NullSourceWithComparer()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Contains("x", StringComparer.Ordinal));
        }

        [TestMethod]
        public void Contains_NoMatchNoComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsFalse(source.Contains("BAR"));
        }

        [TestMethod]
        public void Contains_MatchNoComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            // Clone the string to verify it's not just using reference identity
            string barClone = new String("bar".ToCharArray());
            Assert.IsTrue(source.Contains(barClone));
        }

        [TestMethod]
        public void Contains_NoMatchNullComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsFalse(source.Contains("BAR", null));
        }

        [TestMethod]
        public void Contains_MatchNullComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            // Clone the string to verify it's not just using reference identity
            string barClone = new String("bar".ToCharArray());
            Assert.IsTrue(source.Contains(barClone, null));
        }

        [TestMethod]
        public void Contains_NoMatchWithCustomComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsFalse(source.Contains("gronk", StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void Contains_MatchWithCustomComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsTrue(source.Contains("BAR", StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void Contains_ImmediateReturnWhenMatchIsFound()
        {
            int[] source = { 10, 1, 5, 0 };
            var query = source.Select(x => 10 / x);
            // If we continued past 2, we'd see a division by zero exception
            Assert.IsTrue(query.Contains(2));
        }

        /// <summary>
        /// I dislike this test. It tests for what I consider to be broken behaviour :(
        /// See the blog post on Contains for more information.
        /// </summary>
        [TestMethod]
        public void Contains_SetWithDifferentComparer()
        {
            ICollection<string> sourceAsCollection = HashSetProvider.NewHashSet
                (StringComparer.OrdinalIgnoreCase, "foo", "bar", "baz");
            IEnumerable<string> sourceAsSequence = sourceAsCollection;
            Assert.IsTrue(sourceAsCollection.Contains("BAR"));
            Assert.IsTrue(sourceAsSequence.Contains("BAR")); // This is the line that concerns me
            Assert.IsFalse(sourceAsSequence.Contains("BAR", null));
            Assert.IsFalse(sourceAsSequence.Contains("BAR", StringComparer.Ordinal));
        }

        // Count
        [TestMethod]
        public void Count_NonCollectionCount()
        {
            Assert.AreEqual(5, Enumerable.Range(2, 5).Count());
        }

        [TestMethod]
        public void Count_GenericOnlyCollectionCount()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(2, 5).AsEnumerable()).Count());
        }

        [TestMethod]
        public void Count_SemiGenericCollectionCount()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(2, 5).AsEnumerable()).Count());
        }

        [TestMethod]
        public void Count_RegularGenericCollectionCount()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5).AsEnumerable()).Count());
        }

        [TestMethod]
        public void Count_NullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Count());
        }

        [TestMethod]
        public void Count_PredicatedNullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Count(x => x == 1));
        }

        [TestMethod]
        public void Count_PredicatedNullPredicateThrowsArgumentNullException()
        {
            Helper.Throws<ArgumentNullException>(() => new int[0].Count(null));
        }

        [TestMethod]
        public void Count_PredicatedCount()
        {
            // Counts even numbers within 2, 3, 4, 5, 6
            Assert.AreEqual(3, Enumerable.Range(2, 5).Count(x => x % 2 == 0));
        }

        /*[TestMethod]
        //[Ignore("Takes an enormous amount of time!")]
        public void Count_Overflow()
        {
            var largeSequence = Enumerable.Range(0, int.MaxValue)
                                          .Concat(Enumerable.Range(0, 1));
            Helper.Throws<OverflowException>(() => largeSequence.Count());
        }

        [TestMethod]
        //[Ignore("Takes an enormous amount of time!")]
        public void Count_OverflowWithPredicate()
        {
            var largeSequence = Enumerable.Range(0, int.MaxValue)
                                          .Concat(Enumerable.Range(0, 1));
            Helper.Throws<OverflowException>(() => largeSequence.Count(x => x >= 0));
        }*/

        // DefaultIfEmpty
        [TestMethod]
        public void DefaultIfEmpty_NullSourceNoDefaultValue()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.DefaultIfEmpty());
        }

        [TestMethod]
        public void DefaultIfEmpty_NullSourceWithDefaultValue()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.DefaultIfEmpty(5));
        }

        [TestMethod]
        public void DefaultIfEmpty_EmptySequenceNoDefaultValue()
        {
            Assert.IsTrue(
                Enumerable.Empty<int>().DefaultIfEmpty().SequenceEqual(new[] { 0 })
            );
        }

        [TestMethod]
        public void DefaultIfEmpty_EmptySequenceWithDefaultValue()
        {
            Assert.IsTrue(
                Enumerable.Empty<int>().DefaultIfEmpty(5).SequenceEqual(new[] { 5 })
            );
        }

        [TestMethod]
        public void DefaultIfEmpty_NonEmptySequenceNoDefaultValue()
        {
            int[] source = { 3, 1, 4 };
            Assert.IsTrue(
                source.DefaultIfEmpty().SequenceEqual(source)
            );
        }

        [TestMethod]
        public void DefaultIfEmpty_NonEmptySequenceWithDefaultValue()
        {
            int[] source = { 3, 1, 4 };
            Assert.IsTrue(
                source.DefaultIfEmpty(5).SequenceEqual(source)
            );
        }

        // Distinct
        // Distinct_TestString1 and Distinct_TestString2 are references to different but equal strings
        private static readonly string Distinct_TestString1 = "test";
        private static readonly string Distinct_TestString2 = new string(Distinct_TestString1.ToCharArray());

        [TestMethod]
        public void Distinct_NullSourceNoComparer()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Distinct());
        }

        [TestMethod]
        public void Distinct_NullSourceWithComparer()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Distinct(StringComparer.Ordinal));
        }

        [TestMethod]
        public void Distinct_NullElementsArePassedToComparer()
        {
            IEqualityComparer<object> comparer = new Distinct_SimpleEqualityComparer();
            Helper.Throws<NullReferenceException>(() => comparer.GetHashCode(null));
            Helper.Throws<NullReferenceException>(() => comparer.Equals(null, "xyz"));
            string[] source = { "xyz", null, "xyz", null, "abc" };
            var distinct = source.Distinct(comparer);
            Helper.Throws<NullReferenceException>(() => distinct.Count());
        }

        [TestMethod]
        public void Distinct_HashSetCopesWithNullElementsIfComparerDoes()
        {
            IEqualityComparer<string> comparer = EqualityComparer<string>.Default;
            Assert.AreEqual(comparer.GetHashCode(null), comparer.GetHashCode(null));
            Assert.IsTrue(comparer.Equals(null, null));
            string[] source = { "xyz", null, "xyz", null, "abc" };
            Assert.IsTrue(
                source.Distinct(comparer).SequenceEqual(new[] { "xyz", null, "abc" })
            );
        }

        [TestMethod]
        public void Distinct_NoComparerSpecifiedUsesDefault()
        {
            string[] source = { "xyz", Distinct_TestString1, "XYZ", Distinct_TestString2, "def" };
            Assert.IsTrue(
                source.Distinct().SequenceEqual(new[] { "xyz", Distinct_TestString1, "XYZ", "def" })
            );
        }

        [TestMethod]
        public void Distinct_NullComparerUsesDefault()
        {
            string[] source = { "xyz", Distinct_TestString1, "XYZ", Distinct_TestString2, "def" };
            Assert.IsTrue(
                source.Distinct(null).SequenceEqual(new[] { "xyz", Distinct_TestString1, "XYZ", "def" })
            );
        }

        [TestMethod]
        public void Distinct_DistinctStringsWithCaseInsensitiveComparer()
        {
            string[] source = { "xyz", Distinct_TestString1, "XYZ", Distinct_TestString2, "def" };
            Assert.IsTrue(
                source.Distinct(StringComparer.OrdinalIgnoreCase).SequenceEqual(new[] { "xyz", Distinct_TestString1, "def" })
            );
        }

        [TestMethod]
        public void Distinct_DistinctStringsCustomComparer()
        {
            // This time we'll make sure that Distinct_TestString1 and Distinct_TestString2 are treated differently
            string[] source = { "xyz", Distinct_TestString1, "XYZ", Distinct_TestString2, Distinct_TestString1 };
            Assert.IsTrue(
                source.Distinct(new Distinct_ReferenceEqualityComparer()).SequenceEqual(new[] { "xyz", Distinct_TestString1, "XYZ", Distinct_TestString2 })
            );
        }

        // ElementAtOrDefault
        [TestMethod]
        public void ElementAtOrDefault_NullSource()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.ElementAtOrDefault(0));
        }

        [TestMethod]
        public void ElementAtOrDefault_NegativeIndex()
        {
            int[] source = { 90, 91, 92 };
            Assert.AreEqual(0, source.ElementAtOrDefault(-1));
        }

        /*[TestMethod]
        [Ignore("LINQ to Objects doesn't test for collection separately")]
        public void ElementAtOrDefault_OvershootIndexOnCollection()
        {
            IEnumerable<int> source = new NonEnumerableCollection<int> { 90, 91, 92 };
            Assert.AreEqual(0, source.ElementAtOrDefault(3));
        }*/

        [TestMethod]
        public void ElementAtOrDefault_OvershootIndexOnList()
        {
            var source = new NonEnumerableList<int> { 90, 91, 92 };
            Assert.AreEqual(0, source.ElementAtOrDefault(3));
        }

        [TestMethod]
        public void ElementAtOrDefault_OvershootIndexOnLazySequence()
        {
            var source = Enumerable.Range(0, 3);
            Assert.AreEqual(0, source.ElementAtOrDefault(3));
        }

        [TestMethod]
        public void ElementAtOrDefault_ValidIndexOnList()
        {
            var source = new NonEnumerableList<int>(100, 56, 93, 22);
            Assert.AreEqual(93, source.ElementAtOrDefault(2));
        }

        [TestMethod]
        public void ElementAtOrDefault_ValidIndexOnLazySequence()
        {
            var source = Enumerable.Range(10, 5);
            Assert.AreEqual(12, source.ElementAtOrDefault(2));
        }

        // ElementAt
        [TestMethod]
        public void ElementAt_NullSource()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.ElementAt(0));
        }

        [TestMethod]
        public void ElementAt_NegativeIndex()
        {
            int[] source = { 0, 1, 2 };
            Helper.Throws<ArgumentOutOfRangeException>(() => source.ElementAt(-1));
        }

        /*[TestMethod]
        [Ignore("LINQ to Objects doesn't test for collection separately")]
        public void ElementAt_OvershootIndexOnCollection()
        {
            IEnumerable<int> source = new NonEnumerableCollection<int> { 90, 91, 92 };
            Helper.Throws<ArgumentOutOfRangeException>(() => source.ElementAt(3));
        }*/

        [TestMethod]
        public void ElementAt_OvershootIndexOnList()
        {
            IEnumerable<int> source = new NonEnumerableList<int> { 90, 91, 92 };
            Helper.Throws<ArgumentOutOfRangeException>(() => source.ElementAt(3));
        }

        [TestMethod]
        public void ElementAt_OvershootIndexOnLazySequence()
        {
            var source = Enumerable.Range(0, 3);
            Helper.Throws<ArgumentOutOfRangeException>(() => source.ElementAt(3));
        }

        [TestMethod]
        public void ElementAt_ValidIndexOnList()
        {
            IEnumerable<int> source = new NonEnumerableList<int>(100, 56, 93, 22);
            Assert.AreEqual(93, source.ElementAt(2));
        }

        [TestMethod]
        public void ElementAt_ValidIndexOnLazySequence()
        {
            var source = Enumerable.Range(10, 5);
            Assert.AreEqual(12, source.ElementAt(2));
        }

        // Empty
        [TestMethod]
        public void Empty_EmptyContainsNoElements()
        {
            using (var empty = Enumerable.Empty<int>().GetEnumerator())
            {
                Assert.IsFalse(empty.MoveNext());
            }
        }

        // doesn't make sense in LinqAF
        /*
        [TestMethod]
        public void Empty_EmptyIsASingletonPerElementType()
        {
            Assert.AreSame(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            Assert.AreSame(Enumerable.Empty<long>(), Enumerable.Empty<long>());
            Assert.AreSame(Enumerable.Empty<string>(), Enumerable.Empty<string>());
            Assert.AreSame(Enumerable.Empty<object>(), Enumerable.Empty<object>());

            Assert.AreNotSame(Enumerable.Empty<long>(), Enumerable.Empty<int>());
            Assert.AreNotSame(Enumerable.Empty<string>(), Enumerable.Empty<object>());
        }*/

        // Except
        [TestMethod]
        public void Except_NullFirstWithoutComparer()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.Except(second));
        }

        [TestMethod]
        public void Except_NullSecondWithoutComparer()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.Except(second));
        }

        [TestMethod]
        public void Except_NullFirstWithComparer()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.Except(second, StringComparer.Ordinal));
        }

        [TestMethod]
        public void Except_NullSecondWithComparer()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.Except(second, StringComparer.Ordinal));
        }

        [TestMethod]
        public void Except_NoComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b", "c" };
            string[] second = { "b", "a", "d", "a" };
            Assert.IsTrue(
                first.Except(second).SequenceEqual(new[] { "A", "c" })
            );
        }

        [TestMethod]
        public void Except_NullComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b", "c" };
            string[] second = { "b", "a", "d", "a" };
            Assert.IsTrue(
                first.Except(second, null).SequenceEqual(new[] { "A", "c" })
            );
        }

        [TestMethod]
        public void Except_CaseInsensitiveComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b" };
            string[] second = { "b", "a", "d", "a" };
            Assert.IsTrue(
                first.Except(second, StringComparer.OrdinalIgnoreCase).SequenceEqual(new[] { "c" })
            );
        }

        [TestMethod]
        public void Except_NoSequencesUsedBeforeIteration()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            // No exceptions!
            var query = first.Union(second);
            // Still no exceptions... we're not calling MoveNext.
            using (var iterator = query.GetEnumerator())
            {
            }
        }

        [TestMethod]
        public void Except_SecondSequenceReadFullyOnFirstResultIteration()
        {
            int[] first = { 1 };
            var secondQuery = new[] { 10, 2, 0 }.Select(x => 10 / x);

            var query = first.Except(secondQuery);
            using (var iterator = query.GetEnumerator())
            {
                Helper.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Except_FirstSequenceOnlyReadAsResultsAreRead()
        {
            var firstQuery = new[] { 10, 2, 0, 2 }.Select(x => 10 / x);
            int[] second = { 1 };

            var query = firstQuery.Except(second);
            using (var iterator = query.GetEnumerator())
            {
                // We can get the first value with no problems
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);

                // Getting at the *second* value of the result sequence requires
                // reading from the first input sequence until the "bad" division
                Helper.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        // FirstOrDefault
        [TestMethod]
        public void FirstOrDefault_NullSourceWithoutPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.FirstOrDefault());
        }

        [TestMethod]
        public void FirstOrDefault_NullSourceWithPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Helper.Throws<ArgumentNullException>(() => source.FirstOrDefault(null));
        }

        [TestMethod]
        public void FirstOrDefault_EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Assert.AreEqual(0, source.FirstOrDefault());
        }

        [TestMethod]
        public void FirstOrDefault_SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.FirstOrDefault());
        }

        [TestMethod]
        public void FirstOrDefault_MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Assert.AreEqual(5, source.FirstOrDefault());
        }

        [TestMethod]
        public void FirstOrDefault_EmptySequenceWithPredicate()
        {
            int[] source = { };
            Assert.AreEqual(0, source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Assert.AreEqual(0, source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Assert.AreEqual(0, source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Assert.AreEqual(5, source.FirstOrDefault(x => x > 3));
        }

        [TestMethod]
        public void FirstOrDefault_EarlyOutAfterFirstElementWithoutPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(0, query.FirstOrDefault());
        }

        [TestMethod]
        public void FirstOrDefault_EarlyOutAfterFirstElementWithPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(10, query.FirstOrDefault(y => y > 5));
        }

        // First
        [TestMethod]
        public void First_NullSourceWithoutPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.First());
        }

        [TestMethod]
        public void First_NullSourceWithPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.First(x => x > 3));
        }

        [TestMethod]
        public void First_NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Helper.Throws<ArgumentNullException>(() => source.First(null));
        }

        [TestMethod]
        public void First_EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.First());
        }

        [TestMethod]
        public void First_SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.First());
        }

        [TestMethod]
        public void First_MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Assert.AreEqual(5, source.First());
        }

        [TestMethod]
        public void First_EmptySequenceWithPredicate()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.First(x => x > 3));
        }

        [TestMethod]
        public void First_SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.First(x => x > 3));
        }

        [TestMethod]
        public void First_SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Helper.Throws<InvalidOperationException>(() => source.First(x => x > 3));
        }

        [TestMethod]
        public void First_MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Helper.Throws<InvalidOperationException>(() => source.First(x => x > 3));
        }

        [TestMethod]
        public void First_MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.First(x => x > 3));
        }

        [TestMethod]
        public void First_MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Assert.AreEqual(5, source.First(x => x > 3));
        }

        [TestMethod]
        public void First_EarlyOutAfterFirstElementWithoutPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(0, query.First());
        }

        [TestMethod]
        public void First_EarlyOutAfterFirstElementWithPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(10, query.First(y => y > 5));
        }

        // GroupBy
        [TestMethod]
        public void GroupBy_ExecutionIsPartiallyDeferred()
        {
            // No exception yet...
            new ThrowingEnumerable().GroupBy(x => x);
            // Note that for LINQ to Objects, calling GetEnumerator() starts iterating
            // over the input sequence, so we're not testing that...
        }

        [TestMethod]
        public void GroupBy_SequenceIsReadFullyBeforeFirstResultReturned()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            // Final projection will throw
            var query = numbers.Select(x => 10 / x);

            var groups = query.GroupBy(x => x);
            // Either GetEnumerator or MoveNext will throw. See blog post for details.
            Helper.Throws<DivideByZeroException>(() =>
            {
                using (var iterator = groups.GetEnumerator())
                {
                    iterator.MoveNext();
                }
            });
        }

        [TestMethod]
        public void GroupBy_SimpleGroupBy()
        {
            string[] source = { "abc", "hello", "def", "there", "four" };
            var groups = source.GroupBy(x => x.Length);

            var list = groups.ToList();
            Assert.AreEqual(3, list.Count);

            Assert.IsTrue(
                list[0].SequenceEqual(new[] { "abc", "def" })
            );
            Assert.AreEqual(3, list[0].Key);

            Assert.IsTrue(
                list[1].SequenceEqual(new[] { "hello", "there" })
            );
            Assert.AreEqual(5, list[1].Key);

            Assert.IsTrue(
                list[2].SequenceEqual(new[] { "four" })
            );
            Assert.AreEqual(4, list[2].Key);
        }

        [TestMethod]
        public void GroupBy_GroupByWithElementProjection()
        {
            string[] source = { "abc", "hello", "def", "there", "four" };
            var groups = source.GroupBy(x => x.Length, x => x[0]);

            var list = groups.ToList();
            Assert.AreEqual(3, list.Count);

            Assert.IsTrue(
                list[0].SequenceEqual(new[] { 'a', 'd' })
            );
            Assert.AreEqual(3, list[0].Key);

            Assert.IsTrue(
                list[1].SequenceEqual(new[] { 'h', 't' })
            );
            Assert.AreEqual(5, list[1].Key);

            Assert.IsTrue(
                list[2].SequenceEqual(new[] { 'f' })
            );
            Assert.AreEqual(4, list[2].Key);
        }

        [TestMethod]
        public void GroupBy_GroupByWithCollectionProjection()
        {
            string[] source = { "abc", "hello", "def", "there", "four" };
            var groups = source.GroupBy(x => x.Length,
                                        (key, values) => key + ":" + StringEx.Join(";", values.AsEnumerable()));
            Assert.IsTrue(
                groups.SequenceEqual(new[] { "3:abc;def", "5:hello;there", "4:four" })
            );
        }

        [TestMethod]
        public void GroupBy_GroupByWithElementProjectionAndCollectionProjection()
        {
            string[] source = { "abc", "hello", "def", "there", "four" };
            // This time "values" will be an IEnumerable<char>, the first character of each
            // source string contributing to the group
            var groups = source.GroupBy(x => x.Length,
                                        x => x[0],
                                        (key, values) => key + ":" + StringEx.Join(";", values.AsEnumerable()));

            Assert.IsTrue(
                groups.SequenceEqual(new[] { "3:a;d", "5:h;t", "4:f" })
            );
        }

        [TestMethod]
        public void GroupBy_ChangesToSourceAreIgnoredInWhileIteratingOverResultsAfterFirstElementRetrieved()
        {
            var source = new List<string> { "a", "b", "c", "def" };

            var groups = source.GroupBy(x => x.Length);
            using (var iterator = groups.GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.IsTrue(
                    iterator.Current.SequenceEqual(new[] { "a", "b", "c" })
                );

                // If GroupBy still needed to iterate over the source, this would cause a
                // InvalidOperationException when we next fetched an element from groups.
                source.Add("ghi");

                Assert.IsTrue(iterator.MoveNext());
                // ghi isn't in the group
                Assert.IsTrue(
                    iterator.Current.SequenceEqual(new[] { "def" })
                );

                Assert.IsFalse(iterator.MoveNext());
            }

            // If we iterate again now - without calling GroupBy again - we'll see the difference:
            using (var iterator = groups.GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.IsTrue(
                    iterator.Current.SequenceEqual(new[] { "a", "b", "c" })
                );

                Assert.IsTrue(iterator.MoveNext());
                Assert.IsTrue(
                    iterator.Current.SequenceEqual(new[] { "def", "ghi" })
                );
            }
        }

        [TestMethod]
        public void GroupBy_NullKeys()
        {
            string[] source = { "first", "null", "nothing", "second" };
            // This time "values" will be an IEnumerable<char>, the first character of each
            // source string contributing to the group
            var groups = source.GroupBy(x => x.StartsWith("n") ? null : x,
                                        (key, values) => key + ":" + StringEx.Join(";", values.AsEnumerable()));

            Assert.IsTrue(
                groups.SequenceEqual(new[] { "first:first", ":null;nothing", "second:second" })
            );
        }

        // GroupJoin
        [TestMethod]
        public void GroupJoin_ExecutionIsDeferred()
        {
            var outer = new ThrowingEnumerable();
            var inner = new ThrowingEnumerable();
            outer.GroupJoin(inner, x => x, y => y, (x, y) => x + y.Count());
        }

        [TestMethod]
        public void GroupJoin_SimpleGroupJoin()
        {
            // We're going to join on the first character in the outer sequence item
            // being equal to the second character in the inner sequence item
            string[] outer = { "first", "second", "third" };
            string[] inner = { "essence", "offer", "eating", "psalm" };

            var query = outer.GroupJoin(inner,
                                   outerElement => outerElement[0],
                                   innerElement => innerElement[1],
                                   (outerElement, innerElements) => outerElement + ":" + StringEx.Join(";", innerElements.AsEnumerable()));

            Assert.IsTrue(
                query.SequenceEqual(new[] { "first:offer", "second:essence;psalm", "third:" })
            );
        }

        [TestMethod]
        public void GroupJoin_CustomComparer()
        {
            // We're going to match the start of the outer sequence item
            // with the end of the inner sequence item, in a case-insensitive manner
            string[] outer = { "ABCxxx", "abcyyy", "defzzz", "ghizzz" };
            string[] inner = { "000abc", "111gHi", "222333", "333AbC" };

            var query = outer.GroupJoin(inner,
                                   outerElement => outerElement.Substring(0, 3),
                                   innerElement => innerElement.Substring(3),
                                   (outerElement, innerElements) => outerElement + ":" + StringEx.Join(";", innerElements.AsEnumerable()),
                                   StringComparer.OrdinalIgnoreCase);
            // ABCxxx matches 000abc and 333AbC
            // abcyyy matches 000abc and 333AbC
            // defzzz doesn't match anything
            // ghizzz matches 111gHi
            Assert.IsTrue(
                query.SequenceEqual(new[] { "ABCxxx:000abc;333AbC", "abcyyy:000abc;333AbC", "defzzz:", "ghizzz:111gHi" })
            );
        }

        [TestMethod]
        public void GroupJoin_DifferentSourceTypes()
        {
            int[] outer = { 5, 3, 7, 4 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = outer.GroupJoin(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElements) => outerElement + ":" + StringEx.Join(";", innerElements.AsEnumerable()));
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:tiger", "3:bee;cat;dog", "7:giraffe", "4:" })
            );
        }

        // Note that LINQ to Objects ignores null keys for Join and GroupJoin
        [TestMethod]
        public void GroupJoin_NullKeys()
        {
            string[] outer = { "first", null, "second" };
            string[] inner = { "first", "null", "nothing" };
            var query = outer.GroupJoin(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.StartsWith("n") ? null : innerElement,
                                   (outerElement, innerElements) => outerElement + ":" + StringEx.Join(";", innerElements.AsEnumerable()));
            // No matches for the null outer key
            Assert.IsTrue(
                query.SequenceEqual(new[] { "first:first", ":", "second:" })
            );
        }

        // Intersect
        [TestMethod]
        public void Intersect_NullFirstWithoutComparer()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.Intersect(second));
        }

        [TestMethod]
        public void Intersect_NullSecondWithoutComparer()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.Intersect(second));
        }

        [TestMethod]
        public void Intersect_NullFirstWithComparer()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.Intersect(second, StringComparer.Ordinal));
        }

        [TestMethod]
        public void Intersect_NullSecondWithComparer()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.Intersect(second, StringComparer.Ordinal));
        }

        [TestMethod]
        public void Intersect_NoComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b" };
            string[] second = { "b", "a", "d", "a" };
            Assert.IsTrue(
                first.Intersect(second).SequenceEqual(new[] { "a", "b" })
            );
        }

        [TestMethod]
        public void Intersect_NullComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b" };
            string[] second = { "b", "a", "d", "a" };
            Assert.IsTrue(
                first.Intersect(second, null).SequenceEqual(new[] { "a", "b" })
            );
        }

        [TestMethod]
        public void Intersect_CaseInsensitiveComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b" };
            string[] second = { "b", "a", "d", "a" };
            Assert.IsTrue(
                first.Intersect(second, StringComparer.OrdinalIgnoreCase).SequenceEqual(new[] { "A", "b" })
            );
        }

        [TestMethod]
        public void Intersect_NoSequencesUsedBeforeIteration()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            // No exceptions!
            var query = first.Union(second);
            // Still no exceptions... we're not calling MoveNext.
            using (var iterator = query.GetEnumerator())
            {
            }
        }

        [TestMethod]
        public void Intersect_SecondSequenceReadFullyOnFirstResultIteration()
        {
            int[] first = { 1 };
            var secondQuery = new[] { 10, 2, 0 }.Select(x => 10 / x);

            var query = first.Intersect(secondQuery);
            using (var iterator = query.GetEnumerator())
            {
                Helper.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Intersect_FirstSequenceOnlyReadAsResultsAreRead()
        {
            var firstQuery = new[] { 10, 2, 0, 2 }.Select(x => 10 / x);
            int[] second = { 1 };

            var query = firstQuery.Intersect(second);
            using (var iterator = query.GetEnumerator())
            {
                // We can get the first value with no problems
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(1, iterator.Current);

                // Getting at the *second* value of the result sequence requires
                // reading from the first input sequence until the "bad" division
                Helper.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        // Join
        [TestMethod]
        public void Join_ExecutionIsDeferred()
        {
            var outer = new ThrowingEnumerable();
            var inner = new ThrowingEnumerable();
            // No exception
            outer.Join(inner, x => x, y => y, (x, y) => x + y);
        }

        [TestMethod]
        public void Join_OuterSequenceIsStreamed()
        {
            var outer = new[] { 10, 0, 2 }.Select(x => 10 / x);
            var inner = new[] { 1, 2, 3 };
            var query = outer.Join(inner, x => x, y => y, (x, y) => x + y);

            using (var iterator = query.GetEnumerator())
            {
                // First element is fine
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(2, iterator.Current);

                // Attempting to get to the second element causes division by 0
                Helper.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Join_InnerSequenceIsBuffered()
        {
            var outer = new[] { 1, 2, 3 };
            var inner = new[] { 10, 0, 2 }.Select(x => 10 / x);
            var query = outer.Join(inner, x => x, y => y, (x, y) => x + y);

            using (var iterator = query.GetEnumerator())
            {
                // Even though we could sensibly see the first element before anything
                // is returned, that doesn't happen: the inner sequence is read completely
                // before we start reading the outer sequence
                Helper.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Join_SimpleJoin()
        {
            // We're going to join on the first character in the outer sequence item
            // being equal to the second character in the inner sequence item
            string[] outer = { "first", "second", "third" };
            string[] inner = { "essence", "offer", "eating", "psalm" };

            var query = outer.Join(inner,
                                   outerElement => outerElement[0],
                                   innerElement => innerElement[1],
                                   (outerElement, innerElement) => outerElement + ":" + innerElement);

            // Note: no matches for "third"
            Assert.IsTrue(
                query.SequenceEqual(new[] { "first:offer", "second:essence", "second:psalm" })
            );
        }

        [TestMethod]
        public void Join_CustomComparer()
        {
            // We're going to match the start of the outer sequence item
            // with the end of the inner sequence item, in a case-insensitive manner
            string[] outer = { "ABCxxx", "abcyyy", "defzzz", "ghizzz" };
            string[] inner = { "000abc", "111gHi", "222333" };

            var query = outer.Join(inner,
                                   outerElement => outerElement.Substring(0, 3),
                                   innerElement => innerElement.Substring(3),
                                   (outerElement, innerElement) => outerElement + ":" + innerElement,
                                   StringComparer.OrdinalIgnoreCase);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "ABCxxx:000abc", "abcyyy:000abc", "ghizzz:111gHi" })
            );
        }

        [TestMethod]
        public void Join_DifferentSourceTypes()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = outer.Join(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:tiger", "3:bee", "3:cat", "3:dog", "7:giraffe" })
            );
        }

        // Note that LINQ to Objects ignores null keys for Join and GroupJoin
        [TestMethod]
        public void Join_NullKeys()
        {
            string[] outer = { "first", "null", "nothing", "second" };
            string[] inner = { "nuff", "second" };
            var query = outer.Join(inner,
                                   outerElement => outerElement.StartsWith("n") ? null : outerElement,
                                   innerElement => innerElement.StartsWith("n") ? null : innerElement,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement);

            Assert.IsTrue(
                query.SequenceEqual(new[] { "second:second" })
            );
        }

        // LastOrDefault
        [TestMethod]
        public void LastOrDefault_NullSourceWithoutPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.LastOrDefault());
        }

        [TestMethod]
        public void LastOrDefault_NullSourceWithPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_NullPredicate()
        {
            var source = new LinkedList<int>(new int[] { 1, 3, 5 });
            Helper.Throws<ArgumentNullException>(() => source.LastOrDefault(null));
        }

        [TestMethod]
        public void LastOrDefault_EmptySequenceWithoutPredicate()
        {
            var source = new LinkedList<int>();
            Assert.AreEqual(0, source.LastOrDefault());
        }

        [TestMethod]
        public void LastOrDefault_SingleElementSequenceWithoutPredicate()
        {
            var source = new LinkedList<int>(new int[] { 5 });
            Assert.AreEqual(5, source.LastOrDefault());
        }

        [TestMethod]
        public void LastOrDefault_MultipleElementSequenceWithoutPredicate()
        {
            var source = new LinkedList<int>(new int[] { 5, 10 });
            Assert.AreEqual(10, source.LastOrDefault());
        }

        [TestMethod]
        public void LastOrDefault_EmptySequenceWithPredicate()
        {
            var source = new LinkedList<int>();
            Assert.AreEqual(0, source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_SingleElementSequenceWithMatchingPredicate()
        {
            var source = new LinkedList<int>(new int[] { 5 });
            Assert.AreEqual(5, source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_SingleElementSequenceWithNonMatchingPredicate()
        {
            var source = new LinkedList<int>(new int[] { 2 });
            Assert.AreEqual(0, source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_MultipleElementSequenceWithNoPredicateMatches()
        {
            var source = new LinkedList<int>(new int[] { 1, 2, 2, 1 });
            Assert.AreEqual(0, source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_MultipleElementSequenceWithSinglePredicateMatch()
        {
            var source = new LinkedList<int>(new int[] { 1, 2, 5, 2, 1 });
            Assert.AreEqual(5, source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_MultipleElementSequenceWithMultiplePredicateMatches()
        {
            var source = new LinkedList<int>(new int[] { 1, 2, 5, 10, 2, 1 });
            Assert.AreEqual(10, source.LastOrDefault(x => x > 3));
        }

        [TestMethod]
        public void LastOrDefault_ListWithoutPredicateDoesntIterate()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);
            Assert.AreEqual(3, source.LastOrDefault());
        }

        // See discussion in blog post around this... it could be optimized, but the framework doesn't.
        [TestMethod]
        public void LastOrDefault_ListWithPredicateStillIterates()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);
            Helper.Throws<NotSupportedException>(() => source.LastOrDefault(x => x > 3));
        }

        // Last
        [TestMethod]
        public void Last_NullSourceWithoutPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Last());
        }

        [TestMethod]
        public void Last_NullSourceWithPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_NullPredicate()
        {
            var source = new LinkedList<int>(new int[] { 1, 3, 5 });
            Helper.Throws<ArgumentNullException>(() => source.Last(null));
        }

        [TestMethod]
        public void Last_EmptySequenceWithoutPredicate()
        {
            var source = new LinkedList<int>();
            Helper.Throws<InvalidOperationException>(() => source.Last());
        }

        [TestMethod]
        public void Last_SingleElementSequenceWithoutPredicate()
        {
            var source = new LinkedList<int>(new int[] { 5 });
            Assert.AreEqual(5, source.Last());
        }

        [TestMethod]
        public void Last_MultipleElementSequenceWithoutPredicate()
        {
            var source = new LinkedList<int>(new int[] { 5, 10 });
            Assert.AreEqual(10, source.Last());
        }

        [TestMethod]
        public void Last_EmptySequenceWithPredicate()
        {
            var source = new LinkedList<int>();
            Helper.Throws<InvalidOperationException>(() => source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_SingleElementSequenceWithMatchingPredicate()
        {
            var source = new LinkedList<int>(new int[] { 5 });
            Assert.AreEqual(5, source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_SingleElementSequenceWithNonMatchingPredicate()
        {
            var source = new LinkedList<int>(new int[] { 2 });
            Helper.Throws<InvalidOperationException>(() => source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_MultipleElementSequenceWithNoPredicateMatches()
        {
            var source = new LinkedList<int>(new int[] { 1, 2, 2, 1 });
            Helper.Throws<InvalidOperationException>(() => source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_MultipleElementSequenceWithSinglePredicateMatch()
        {
            var source = new LinkedList<int>(new int[] { 1, 2, 5, 2, 1 });
            Assert.AreEqual(5, source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_MultipleElementSequenceWithMultiplePredicateMatches()
        {
            var source = new LinkedList<int>(new int[] { 1, 2, 5, 10, 2, 1 });
            Assert.AreEqual(10, source.Last(x => x > 3));
        }

        [TestMethod]
        public void Last_ListWithoutPredicateDoesntIterate()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);
            Assert.AreEqual(3, source.Last());
        }

        // See discussion in blog post around this... it could be optimized, but the framework doesn't.
        [TestMethod]
        public void Last_ListWithPredicateStillIterates()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);
            Helper.Throws<NotSupportedException>(() => source.Last(x => x > 3));
        }

        // LongCount
        [TestMethod]
        public void LongCount_NonCollectionCount()
        {
            Assert.AreEqual(5, Enumerable.Range(2, 5).LongCount());
        }

        [TestMethod]
        public void LongCount_GenericOnlyCollectionCount()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(2, 5).AsEnumerable()).LongCount());
        }

        [TestMethod]
        public void LongCount_SemiGenericCollectionCount()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(2, 5).AsEnumerable()).LongCount());
        }

        [TestMethod]
        public void LongCount_RegularGenericCollectionCount()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5).AsEnumerable()).LongCount());
        }

        [TestMethod]
        public void LongCount_NullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.LongCount());
        }

        [TestMethod]
        public void LongCount_PredicatedNullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.LongCount(x => x == 1));
        }

        [TestMethod]
        public void LongCount_PredicatedNullPredicateThrowsArgumentNullException()
        {
            Helper.Throws<ArgumentNullException>(() => new int[0].LongCount(null));
        }

        [TestMethod]
        public void LongCount_PredicatedCount()
        {
            // Counts even numbers within 2, 3, 4, 5, 6
            Assert.AreEqual(3, Enumerable.Range(2, 5).LongCount(x => x % 2 == 0));
        }

        /*[TestMethod]
        //[Ignore("Takes an enormous amount of time!")]
        public void LongCount_CollectionBiggerThanMaxInt32CanBeCountedWithLongCount()
        {
            var hugeCollection = Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 1));
            Assert.AreEqual(int.MaxValue + 1L, hugeCollection.LongCount());
        }

        [TestMethod]
        //[Ignore("Takes an enormous amount of time!")]
        public void LongCount_CollectionBiggerThanMaxInt32CanBeCountedWithLongCountWithPredicate()
        {
            var hugeCollection = Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 1));
            Assert.AreEqual(int.MaxValue + 1L, hugeCollection.LongCount(x => x >= 0));
        }*/

        // Max
        #region Int32 tests
        [TestMethod]
        public void Max_NullInt32Source()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Max());
        }

        [TestMethod]
        public void Max_NullInt32Selector()
        {
            string[] source = { "" };
            Func<string, int> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Max(selector));
        }

        [TestMethod]
        public void Max_EmptySequenceInt32NoSelector()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Max());
        }

        [TestMethod]
        public void Max_EmptySequenceInt32WithSelector()
        {
            string[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Max(x => x.Length));
        }

        [TestMethod]
        public void Max_SimpleSequenceInt32NoSelector()
        {
            int[] source = { 5, 10, 6, 2, 13, 8 };
            Assert.AreEqual(13, source.Max());
        }

        [TestMethod]
        public void Max_SimpleSequenceInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(5, source.Max(x => x.Length));
        }

        [TestMethod]
        public void Max_NullNullableInt32Source()
        {
            int?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Max());
        }

        [TestMethod]
        public void Max_NullNullableInt32Selector()
        {
            string[] source = { "" };
            Func<string, int?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Max(selector));
        }

        [TestMethod]
        public void Max_EmptySequenceNullableInt32NoSelector()
        {
            int?[] source = { };
            Assert.IsNull(source.Max());
        }

        [TestMethod]
        public void Max_EmptySequenceNullableInt32WithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Max(x => (int?)x.Length));
        }

        [TestMethod]
        public void Max_AllNullsSequenceNullableInt32NoSelector()
        {
            int?[] source = { null, null };
            Assert.IsNull(source.Max());
        }

        [TestMethod]
        public void Max_AllNullsSequenceNullableInt32WithSelector()
        {
            string[] source = { "x", "y", "z" };
            Assert.IsNull(source.Max(x => (int?)null));
        }

        [TestMethod]
        public void Max_SimpleSequenceNullableInt32NoSelector()
        {
            int?[] source = { 5, 10, 6, 2, 13, 8 };
            Assert.AreEqual(13, source.Max());
        }

        [TestMethod]
        public void Max_SequenceIncludingNullsNullableInt32NoSelector()
        {
            int?[] source = { 5, null, 10, null, 6, null, 2, 13, 8 };
            Assert.AreEqual(13, source.Max());
        }

        [TestMethod]
        public void Max_SimpleSequenceNullableInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(5, source.Max(x => (int?)x.Length));
        }

        [TestMethod]
        public void Max_SequenceIncludingNullsNullableInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(5, source.Max(x => x == "ab" ? null : (int?)x.Length));
        }
        #endregion

        #region Double tests
        // "Not a number" values have some interesting properties...
        [TestMethod]
        public void Max_SimpleSequenceDouble()
        {
            double[] source = { -2.5d, 2.5d, 0d };
            Assert.AreEqual(2.5d, source.Max());
        }

        [TestMethod]
        public void Max_SequenceContainingBothInfinities()
        {
            double[] source = { 1d, double.PositiveInfinity, double.NegativeInfinity };
            Assert.IsTrue(double.IsPositiveInfinity(source.Max()));
        }

        [TestMethod]
        public void Max_SequenceContainingNaN()
        {
            // Comparisons with NaN are odd, basically...
            double[] source = { 1d, double.PositiveInfinity, double.NaN, double.NegativeInfinity };
            // Enumerable.Max thinks that infinity is more than NaN
            Assert.IsTrue(double.IsPositiveInfinity(source.Max()));
            // Math.Max thinks that NaN is more than infinity
            Assert.IsTrue(double.IsNaN(Math.Max(double.PositiveInfinity, double.NaN)));
        }

        #endregion

        #region Generic tests
        // String implements IEnumerable<char>, which is quite handy. For most non-selector
        // methods we use a single string; for selector methods we use a string
        // and a projection to the first character.
        // However, the behaviour between nullable and non-nullable types varies, unfortunately,
        // so there are some extra tests for string sequences.

        [TestMethod]
        public void Max_NullGenericSource()
        {
            IEnumerable<char> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Max());
        }

        [TestMethod]
        public void Max_NullGenericSelector()
        {
            string[] source = { "" };
            Func<string, char> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Max(selector));
        }

        [TestMethod]
        public void Max_EmptyCharSequenceGenericNoSelector()
        {
            IEnumerable<char> source = "";
            Helper.Throws<InvalidOperationException>(() => source.Max());
        }

        [TestMethod]
        public void Max_EmptyCharSequenceGenericWithSelector()
        {
            string[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Max(x => x[0]));
        }

        [TestMethod]
        public void Max_EmptyStringSequenceGenericNoSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Max());
        }

        [TestMethod]
        public void Max_EmptyStringSequenceGenericWithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Max());
        }

        [TestMethod]
        public void Max_SimpleSequenceGenericNoSelector()
        {
            string source = "alphabet soup";
            Assert.AreEqual('u', source.Max());
        }

        [TestMethod]
        public void Max_SimpleSequenceGenericWithSelector()
        {
            string[] source = { "zyx", "ab", "abcde", "0" };
            Assert.AreEqual('z', source.Max(x => x[0]));
        }

        [TestMethod]
        public void Max_SimpleNullableValueTypeSequenceNoSelector()
        {
            char?[] source = { 'z', null, 'a', null, 'a', '0' };
            Assert.AreEqual((char?)'z', source.Max());
        }

        [TestMethod]
        public void Max_AllNullSequenceOfStrings()
        {
            string[] source = { null, null, null };
            Assert.IsNull(source.Max());
        }

        [TestMethod]
        public void Max_SimpleSequenceOfStringsIncludingNull()
        {
            // Just for this test, we'll assume that single-letter strings can be ordered#
            // simply in the default culture...
            string[] source = { "A", "D", null, "B", "C" };
            // null values are ignored when finding the maximum
            Assert.AreEqual("D", source.Max());
        }

        [TestMethod]
        public void Max_AllNullSequenceOfNullableGuids()
        {
            Guid?[] source = { null, null, null };
            Assert.IsNull(source.Max());
        }

        [TestMethod]
        public void Max_IncomparableValues()
        {
            EdulinqTests[] source = { new EdulinqTests(), new EdulinqTests() };
            Helper.Throws<ArgumentException>(() => source.Max());
        }
        #endregion

        // Min
        #region Int32 tests
        [TestMethod]
        public void Min_NullInt32Source()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Min());
        }

        [TestMethod]
        public void Min_NullInt32Selector()
        {
            string[] source = { "" };
            Func<string, int> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Min(selector));
        }

        [TestMethod]
        public void Min_EmptySequenceInt32NoSelector()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Min());
        }

        [TestMethod]
        public void Min_EmptySequenceInt32WithSelector()
        {
            string[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Min(x => x.Length));
        }

        [TestMethod]
        public void Min_SimpleSequenceInt32NoSelector()
        {
            int[] source = { 5, 10, 6, 2, 13, 8 };
            Assert.AreEqual(2, source.Min());
        }

        [TestMethod]
        public void Min_SimpleSequenceInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(1, source.Min(x => x.Length));
        }

        [TestMethod]
        public void Min_NullNullableInt32Source()
        {
            int?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Min());
        }

        [TestMethod]
        public void Min_NullNullableInt32Selector()
        {
            string[] source = { "" };
            Func<string, int?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Min(selector));
        }

        [TestMethod]
        public void Min_EmptySequenceNullableInt32NoSelector()
        {
            int?[] source = { };
            Assert.IsNull(source.Min());
        }

        [TestMethod]
        public void Min_EmptySequenceNullableInt32WithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Min(x => (int?)x.Length));
        }

        [TestMethod]
        public void Min_AllNullsSequenceNullableInt32NoSelector()
        {
            int?[] source = { null, null };
            Assert.IsNull(source.Min());
        }

        [TestMethod]
        public void Min_AllNullsSequenceNullableInt32WithSelector()
        {
            string[] source = { "x", "y", "z" };
            Assert.IsNull(source.Min(x => (int?)null));
        }

        [TestMethod]
        public void Min_SimpleSequenceNullableInt32NoSelector()
        {
            int?[] source = { 5, 10, 6, 2, 13, 8 };
            Assert.AreEqual(2, source.Min());
        }

        [TestMethod]
        public void Min_SequenceIncludingNullsNullableInt32NoSelector()
        {
            int?[] source = { 5, null, 10, null, 6, null, 2, 13, 8 };
            Assert.AreEqual(2, source.Min());
        }

        [TestMethod]
        public void Min_SimpleSequenceNullableInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(1, source.Min(x => (int?)x.Length));
        }

        [TestMethod]
        public void Min_SequenceIncludingNullsNullableInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(1, source.Min(x => x == "ab" ? null : (int?)x.Length));
        }
        #endregion

        #region Double tests
        // "Not a number" values have some interesting properties...
        [TestMethod]
        public void Min_SimpleSequenceDouble()
        {
            double[] source = { -2.5d, 2.5d, 0d };
            Assert.AreEqual(-2.5d, source.Min());
        }

        [TestMethod]
        public void Min_SequenceContainingBothInfinities()
        {
            double[] source = { 1d, double.PositiveInfinity, double.NegativeInfinity };
            Assert.IsTrue(double.IsNegativeInfinity(source.Min()));
        }

        [TestMethod]
        public void Min_SequenceContainingNaN()
        {
            // Comparisons with NaN are odd, basically...
            // In this case, they're consistent, but look at MaxTest.SequenceContainingNaN for oddities.
            double[] source = { 1d, double.PositiveInfinity, double.NaN, double.NegativeInfinity };
            // Enumerable.Min thinks that NaN is less than negative infinity
            Assert.IsTrue(double.IsNaN(source.Min()));
            // Math.Min thinks that NaN is less than negative infinity
            Assert.IsTrue(double.IsNaN(Math.Min(double.NegativeInfinity, double.NaN)));
        }

        #endregion

        #region Generic tests
        // String implements IEnumerable<char>, which is quite handy. For non-selector
        // methods we use a single string; for selector methods we use a string
        // and a projection to the first character

        [TestMethod]
        public void Min_NullGenericSource()
        {
            IEnumerable<char> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Min());
        }

        [TestMethod]
        public void Min_NullGenericSelector()
        {
            string[] source = { "" };
            Func<string, char> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Min(selector));
        }

        [TestMethod]
        public void Min_EmptyCharSequenceGenericNoSelector()
        {
            IEnumerable<char> source = "";
            Helper.Throws<InvalidOperationException>(() => source.Min());
        }

        [TestMethod]
        public void Min_EmptyCharSequenceGenericWithSelector()
        {
            string[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Min(x => x[0]));
        }

        [TestMethod]
        public void Min_EmptyStringSequenceGenericNoSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Min());
        }

        [TestMethod]
        public void Min_EmptyStringSequenceGenericWithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Min());
        }

        [TestMethod]
        public void Min_SimpleSequenceGenericNoSelector()
        {
            string source = "alphabet soup";
            Assert.AreEqual(' ', source.Min());
        }

        [TestMethod]
        public void Min_SimpleSequenceGenericWithSelector()
        {
            string[] source = { "zyx", "ab", "abcde", "0" };
            Assert.AreEqual('0', source.Min(x => x[0]));
        }

        [TestMethod]
        public void Min_SimpleNullableValueTypeSequenceNoSelector()
        {
            char?[] source = { 'z', null, 'a', null, 'a', '0' };
            Assert.AreEqual((char?)'0', source.Min());
        }

        [TestMethod]
        public void Min_AllNullSequenceOfStrings()
        {
            string[] source = { null, null, null };
            Assert.IsNull(source.Min());
        }

        [TestMethod]
        public void Min_SimpleSequenceOfStringsIncludingNull()
        {
            // Just for this test, we'll assume that single-letter strings can be ordered#
            // simply in the default culture...
            string[] source = { "A", "D", null, "B", "C" };
            // null values are ignored when finding the minimum
            Assert.AreEqual("A", source.Min());
        }

        [TestMethod]
        public void Min_AllNullSequenceOfNullableGuids()
        {
            Guid?[] source = { null, null, null };
            Assert.IsNull(source.Min());
        }

        [TestMethod]
        public void Min_IncomparableValues()
        {
            EdulinqTests[] source = { new EdulinqTests(), new EdulinqTests() };
            Helper.Throws<ArgumentException>(() => source.Min());
        }
        #endregion

        // OfType
        [TestMethod]
        public void OfType_NullSource()
        {
            System.Collections.IEnumerable source = null;
            Helper.Throws<ArgumentNullException>(() => source.OfType<string>());
        }

        [TestMethod]
        public void OfType_ExecutionIsDeferred()
        {
            System.Collections.IEnumerable source = new ThrowingEnumerable();
            // No exception
            source.OfType<string>();
        }

        [TestMethod]
        public void OfType_OriginalSourceNotReturnedForReferenceTypes()
        {
            System.Collections.IEnumerable strings = new List<string>();
            Assert.AreNotSame(strings, strings.OfType<string>());
        }

        [TestMethod]
        public void OfType_OriginalSourceNotReturnedForNullableValueTypes()
        {
            System.Collections.IEnumerable nullableInts = new List<int?>();
            Assert.AreNotSame(nullableInts, nullableInts.OfType<int?>());
        }

        /*[TestMethod]
        [Ignore("Fails in LINQ to Objects - see blog for design discussion")]
        public void OfType_OriginalSourceReturnedForSequenceOfCorrectNonNullableValueType()
        {
            IEnumerable ints = new List<int>();
            Assert.AreSame(ints, ints.OfType<int>());
        }*/

        [TestMethod]
        public void OfType_SequenceWithAllValidValues()
        {
            System.Collections.IEnumerable strings = new object[] { "first", "second", "third" };
            Assert.IsTrue(
                strings.OfType<string>().SequenceEqual(new[] { "first", "second", "third" })
            );
        }

        [TestMethod]
        public void OfType_NullsAreExcluded()
        {
            System.Collections.IEnumerable strings = new object[] { "first", null, "third" };
            Assert.IsTrue(
                strings.OfType<string>().SequenceEqual(new[] { "first", "third" })
            );
        }

        [TestMethod]
        public void OfType_UnboxToInt32()
        {
            System.Collections.IEnumerable ints = new object[] { 10, 30, 50 };
            Assert.IsTrue(
                ints.OfType<int>().SequenceEqual(new[] { 10, 30, 50 })
            );
        }

        [TestMethod]
        public void OfType_UnboxToNullableInt32WithNulls()
        {
            System.Collections.IEnumerable ints = new object[] { 10, null, 30, null, 50 };
            Assert.IsTrue(
                ints.OfType<int?>().SequenceEqual(new int?[] { 10, 30, 50 })
            );
        }

        [TestMethod]
        public void OfType_WrongElementTypesAreIgnored()
        {
            System.Collections.IEnumerable objects = new object[] { "first", new object(), "third" };
            Assert.IsTrue(
                objects.OfType<string>().SequenceEqual(new[] { "first", "third" })
            );
            using (var iterator = objects.Cast<string>().GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual("first", iterator.Current);
                Helper.Throws<InvalidCastException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void OfType_UnboxingWithWrongElementTypes()
        {
            System.Collections.IEnumerable objects = new object[] { 100L, 100, 300L };
            Assert.IsTrue(
                objects.OfType<long>().SequenceEqual(new[] { 100L, 300L })
            );
        }

        // OrderBy
        [TestMethod]
        public void OrderBy_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderByDescending(x => x);
        }

        [TestMethod]
        public void OrderBy_NullSourceNoComparer()
        {
            int[] source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector));
        }

        [TestMethod]
        public void OrderBy_NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector));
        }

        [TestMethod]
        public void OrderBy_NullSourceWithComparer()
        {
            int[] source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector, Comparer<int>.Default));
        }

        [TestMethod]
        public void OrderBy_NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector, Comparer<int>.Default));
        }

        [TestMethod]
        public void OrderBy_SimpleUniqueKeys()
        {
            var source = new[]
            {
                new { Value = 1, Key = 10 },
                new { Value = 2, Key = 12 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderByDescending(x => x.Key)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1 })
            );
        }

        [TestMethod]
        public void OrderBy_NullsAreLast()
        {
            var source = new[]
            {
                new { Value = 1, Key = "abc" },
                new { Value = 2, Key = (string) null },
                new { Value = 3, Key = "def" }
            };
            var query = source.OrderByDescending(x => x.Key, StringComparer.Ordinal)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 1, 2 })
            );
        }

        [TestMethod]
        public void OrderBy_OrderingIsStable()
        {
            var source = new[]
            {
                new { Value = 1, Key = 10 },
                new { Value = 2, Key = 11 },
                new { Value = 3, Key = 11 },
                new { Value = 4, Key = 10 },
            };
            var query = source.OrderByDescending(x => x.Key)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1, 4 })
            );
        }

        [TestMethod]
        public void OrderBy_NullComparerIsDefault()
        {
            var source = new[]
            {
                new { Value = 1, Key = 15 },
                new { Value = 2, Key = -13 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderByDescending(x => x.Key, null)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 2 })
            );
        }

        [TestMethod]
        public void OrderBy_CustomComparer()
        {
            var source = new[]
            {
                new { Value = 1, Key = 15 },
                new { Value = 2, Key = -13 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderByDescending(x => x.Key, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 2, 3 })
            );
        }

        /*[TestMethod]
        [Ignore("Fails in LINQ to Objects. See Connect issue: http://goo.gl/p12su")]
        public void OrderBy_CustomExtremeComparer()
        {
            int[] values = { 1, 3, 2, 4, 8, 5, 7, 6 };
            var query = values.OrderByDescending(x => x, new OrderBy_ExtremeComparer());
            Assert.IsTrue(
                query.SequenceEqual(new[] { 8, 7, 6, 5, 4, 3, 2, 1 })
            );
        }*/

        // OrderByDescending
        [TestMethod]
        public void OrderByDescending_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderByDescending(x => x);
        }

        [TestMethod]
        public void OrderByDescending_NullSourceNoComparer()
        {
            int[] source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector));
        }

        [TestMethod]
        public void OrderByDescending_NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector));
        }

        [TestMethod]
        public void OrderByDescending_NullSourceWithComparer()
        {
            int[] source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector, Comparer<int>.Default));
        }

        [TestMethod]
        public void OrderByDescending_NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderByDescending(keySelector, Comparer<int>.Default));
        }

        [TestMethod]
        public void OrderByDescending_SimpleUniqueKeys()
        {
            var source = new[]
            {
                new { Value = 1, Key = 10 },
                new { Value = 2, Key = 12 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderByDescending(x => x.Key)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1 })
            );
        }

        [TestMethod]
        public void OrderByDescending_NullsAreLast()
        {
            var source = new[]
            {
                new { Value = 1, Key = "abc" },
                new { Value = 2, Key = (string) null },
                new { Value = 3, Key = "def" }
            };
            var query = source.OrderByDescending(x => x.Key, StringComparer.Ordinal)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 1, 2 })
            );
        }

        [TestMethod]
        public void OrderByDescending_OrderingIsStable()
        {
            var source = new[]
            {
                new { Value = 1, Key = 10 },
                new { Value = 2, Key = 11 },
                new { Value = 3, Key = 11 },
                new { Value = 4, Key = 10 },
            };
            var query = source.OrderByDescending(x => x.Key)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1, 4 })
            );
        }

        [TestMethod]
        public void OrderByDescending_NullComparerIsDefault()
        {
            var source = new[]
            {
                new { Value = 1, Key = 15 },
                new { Value = 2, Key = -13 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderByDescending(x => x.Key, null)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 2 })
            );
        }

        [TestMethod]
        public void OrderByDescending_CustomComparer()
        {
            var source = new[]
            {
                new { Value = 1, Key = 15 },
                new { Value = 2, Key = -13 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderByDescending(x => x.Key, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 2, 3 })
            );
        }

        /*[TestMethod]
        [Ignore("Fails in LINQ to Objects. See Connect issue: http://goo.gl/p12su")]
        public void OrderByDescending_CustomExtremeComparer()
        {
            int[] values = { 1, 3, 2, 4, 8, 5, 7, 6 };
            var query = values.OrderByDescending(x => x, new OrderByDescending_ExtremeComparer());
            Assert.IsTrue(
                query.SequenceEqual(new[] { 8, 7, 6, 5, 4, 3, 2, 1 })
            );
        }*/

        [TestMethod]
        public void QueryExpression_WhereAndSelect()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = from x in source
                         where x < 4
                         select x * 2;
            Assert.IsTrue(
                result.SequenceEqual(new[] { 2, 6, 4, 2 })
            );
        }

        [TestMethod]
        public void QueryExpression_Join()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length
                        select x + ":" + y;
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:tiger", "3:bee", "3:cat", "3:dog", "7:giraffe" })
            );
        }

        [TestMethod]
        public void QueryExpression_GroupJoin()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length into matches
                        select x + ":" + StringEx.Join(";", matches.AsEnumerable());
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:tiger", "3:bee;cat;dog", "7:giraffe" })
            );
        }

        [TestMethod]
        public void QueryExpression_GroupJoinWithDefaultIfEmpty()
        {
            int[] outer = { 5, 3, 4, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length into matches
                        select x + ":" + StringEx.Join(";", matches.DefaultIfEmpty("null").AsEnumerable());
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:tiger", "3:bee;cat;dog", "4:null", "7:giraffe" })
            );
        }

        [TestMethod]
        public void QueryExpression_GroupJoinWithDefaultIfEmptyAndSelectMany()
        {
            int[] outer = { 5, 3, 4, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length into matches
                        from z in matches.DefaultIfEmpty("null")
                        select x + ":" + z;
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:tiger", "3:bee", "3:cat", "3:dog", "4:null", "7:giraffe" })
            );
        }

        // Equivalent to GroupByTest.GroupByWithElementProjection
        [TestMethod]
        public void QueryExpression_GroupBy()
        {
            string[] source = { "abc", "hello", "def", "there", "four" };
            var groups = from x in source
                         group x[0] by x.Length;

            var list = groups.ToList();
            Assert.AreEqual(3, list.Count);

            Assert.IsTrue(
                list[0].SequenceEqual(new[] { 'a', 'd' })
            );
            Assert.AreEqual(3, list[0].Key);

            Assert.IsTrue(
                list[1].SequenceEqual(new[] { 'h', 't' })
            );
            Assert.AreEqual(5, list[1].Key);

            Assert.IsTrue(
                list[2].SequenceEqual(new[] { 'f' })
            );
            Assert.AreEqual(4, list[2].Key);
        }

        [TestMethod]
        public void QueryExpression_CastWithFrom()
        {
            System.Collections.IEnumerable strings = new[] { "first", "second", "third" };
            var query = from string x in strings
                        select x;
            Assert.IsTrue(
                query.SequenceEqual(new[] { "first", "second", "third" })
            );
        }

        [TestMethod]
        public void QueryExpression_CastWithJoin()
        {
            var ints = Enumerable.Range(0, 10);
            System.Collections.IEnumerable strings = new[] { "first", "second", "third" };
            var query = from x in ints
                        join string y in strings on x equals y.Length
                        select x + ":" + y;
            Assert.IsTrue(
                query.SequenceEqual(new[] { "5:first", "5:third", "6:second" })
            );
        }

        // Range
        [TestMethod]
        public void Range_NegativeCount()
        {
            Helper.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(10, -1));
        }

        [TestMethod]
        public void Range_CountTooLarge()
        {
            Helper.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(int.MaxValue, 2));
            Helper.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(2, int.MaxValue));
            // int.MaxValue is odd, hence the +3 instead of +2
            Helper.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(int.MaxValue / 2, (int.MaxValue / 2) + 3));
        }

        [TestMethod]
        public void Range_LargeButValidCount()
        {
            // Essentially the edge conditions for CountTooLarge, but just below the boundary
            Enumerable.Range(int.MaxValue, 1);
            Enumerable.Range(1, int.MaxValue);
            Enumerable.Range(int.MaxValue / 2, (int.MaxValue / 2) + 2);
        }

        [TestMethod]
        public void Range_ValidRange()
        {
            Assert.IsTrue(
                Enumerable.Range(5, 3).SequenceEqual(new[] { 5, 6, 7 })
            );
        }

        [TestMethod]
        public void Range_NegativeStart()
        {
            Assert.IsTrue(
                Enumerable.Range(-2, 5).SequenceEqual(new[] { -2, -1, 0, 1, 2 })
            );
        }

        [TestMethod]
        public void Range_EmptyRange()
        {
            Assert.IsTrue(
                Enumerable.Range(100, 0).SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Range_SingleValueOfMaxInt32()
        {
            Assert.IsTrue(
                Enumerable.Range(int.MaxValue, 1).SequenceEqual(new[] { int.MaxValue })
            );
        }

        [TestMethod]
        public void Range_EmptyRangeStartingAtMinInt32()
        {
            Assert.IsTrue(
                Enumerable.Range(int.MinValue, 0).SequenceEqual(new int[0])
            );
        }

        // Repeat
        [TestMethod]
        public void Repeat_SimpleRepeat()
        {
            Assert.IsTrue(
                Enumerable.Repeat("foo", 3).SequenceEqual(new[] { "foo", "foo", "foo" })
            );
        }

        [TestMethod]
        public void Repeat_EmptyRepeat()
        {
            Assert.IsTrue(
                Enumerable.Repeat("foo", 0).SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void Repeat_NullElement()
        {
            Assert.IsTrue(
                Enumerable.Repeat<string>(null, 2).SequenceEqual(new string[] { null, null })
            );
        }

        [TestMethod]
        public void Repeat_NegativeCount()
        {
            Helper.Throws<ArgumentOutOfRangeException>(() => Enumerable.Repeat("foo", -1));
        }

        // Reverse
        [TestMethod]
        public void Reverse_NullSource()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Reverse());
        }

        [TestMethod]
        public void Reverse_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().Reverse();
        }

        [TestMethod]
        public void Reverse_InputIsBuffered()
        {
            int[] values = { 10, 0, 20 };
            var query = values.Select(x => 10 / x).Reverse();
            Helper.Throws<DivideByZeroException>(() => {
                using (var iterator = query.GetEnumerator())
                {
                    iterator.MoveNext();
                }
            });
        }

        [TestMethod]
        public void Reverse_ArraysAreBuffered()
        {
            // A sneaky implementation may try to optimize for the case where the collection
            // implements IList or (even more "reliable") is an array: it mustn't do this,
            // as otherwise the results can be tainted by side-effects within iteration
            int[] source = { 0, 1, 2, 3 };

            var query = source.Reverse();
            source[1] = 99; // This change *will* be seen due to deferred execution
            using (var iterator = query.GetEnumerator())
            {
                iterator.MoveNext();
                Assert.AreEqual(3, iterator.Current);

                source[2] = 100; // This change *won't* be seen                
                iterator.MoveNext();
                Assert.AreEqual(2, iterator.Current);

                iterator.MoveNext();
                Assert.AreEqual(99, iterator.Current);

                iterator.MoveNext();
                Assert.AreEqual(0, iterator.Current);
            }
        }

        [TestMethod]
        public void Reverse_ReversedRange()
        {
            var query = Enumerable.Range(5, 5).Reverse();
            Assert.IsTrue(
                query.SequenceEqual(new[] { 9, 8, 7, 6, 5 })
            );
        }

        [TestMethod]
        public void Reverse_ReversedList()
        {
            // Note: mustn't give source a compile-time type of List<int> as
            // List<T> has a Reverse method.
            IEnumerable<int> source = new List<int> { 5, 6, 7, 8, 9 };
            var query = source.Reverse();
            Assert.IsTrue(
                query.SequenceEqual(new[] { 9, 8, 7, 6, 5 })
            );
        }

        [TestMethod]
        public void Reverse_EmptyInput()
        {
            int[] input = { };
            Assert.IsTrue(
                input.Reverse().SequenceEqual(new int[0])
            );
        }

        // SelectMany
        [TestMethod]
        public void SelectMany_SimpleFlatten()
        {
            int[] numbers = { 3, 5, 20, 15 };
            // The ToCharArray is unnecessary really, as string implements IEnumerable<char>
            var query = numbers.SelectMany(x => x.ToInvariantString().ToCharArray());
            Assert.IsTrue(
                query.SequenceEqual(new[] { '3', '5', '2', '0', '1', '5' })
            );
        }

        [TestMethod]
        public void SelectMany_SimpleFlattenWithIndex()
        {
            int[] numbers = { 3, 5, 20, 15 };
            // The ToCharArray is unnecessary really, as string implements IEnumerable<char>
            var query = numbers.SelectMany((x, index) => (x + index).ToInvariantString().ToCharArray());
            // 3 => '3'
            // 5 => '6'
            // 20 => '2', '2'
            // 15 => '1', '8'
            Assert.IsTrue(
                query.SequenceEqual(new[] { '3', '6', '2', '2', '1', '8' })
            );
        }

        [TestMethod]
        public void SelectMany_FlattenWithProjection()
        {
            int[] numbers = { 3, 5, 20, 15 };
            // Flatten each number to its constituent characters, but then project each character
            // to a string of the original element which is responsible for "creating" that character,
            // as well as the character itself. So 20 will go to "20: 2" and "20: 0".
            var query = numbers.SelectMany(x => x.ToInvariantString().ToCharArray(),
                                           (x, c) => x + ": " + c);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "3: 3", "5: 5", "20: 2", "20: 0", "15: 1", "15: 5" })
            );
        }

        [TestMethod]
        public void SelectMany_FlattenWithProjectionAndIndex()
        {
            int[] numbers = { 3, 5, 20, 15 };
            var query = numbers.SelectMany((x, index) => (x + index).ToInvariantString().ToCharArray(),
                                           (x, c) => x + ": " + c);
            // 3 => "3: 3"
            // 5 => "5: 6"
            // 20 => "20: 2", "20: 2"
            // 15 => "15: 1", "15: 8"
            Assert.IsTrue(
                query.SequenceEqual(new[] { "3: 3", "5: 6", "20: 2", "20: 2", "15: 1", "15: 8" })
            );
        }

        // Select
        [TestMethod]
        public void Select_NullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Select(x => x + 1));
        }

        [TestMethod]
        public void Select_NullProjectionThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, int> projection = null;
            Helper.Throws<ArgumentNullException>(() => source.Select(projection));
        }

        [TestMethod]
        public void Select_WithIndexNullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Select((x, index) => x + index));
        }

        [TestMethod]
        public void Select_WithIndexNullPredicateThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, int, int> projection = null;
            Helper.Throws<ArgumentNullException>(() => source.Select(projection));
        }

        [TestMethod]
        public void Select_SimpleProjection()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select(x => x * 2);
            Assert.IsTrue(
                result.SequenceEqual(new[] { 2, 10, 4 })
            );
        }

        [TestMethod]
        public void Select_SimpleProjectionWithQueryExpression()
        {
            int[] source = { 1, 5, 2 };
            var result = from x in source
                         select x * 2;
            Assert.IsTrue(
                result.SequenceEqual(new[] { 2, 10, 4 })
            );
        }

        [TestMethod]
        public void Select_SimpleProjectionToDifferentType()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select(x => x.ToInvariantString());
            Assert.IsTrue(
                result.SequenceEqual(new[] { "1", "5", "2" })
            );
        }

        [TestMethod]
        public void Select_EmptySource()
        {
            int[] source = new int[0];
            var result = source.Select(x => x * 2);
            Assert.IsTrue(
                result.SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Select_ExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Select(x => x * 2).AsEnumerable());
        }

        [TestMethod]
        public void Select_WithIndexSimpleProjection()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select((x, index) => x + index * 10);
            Assert.IsTrue(
                result.SequenceEqual(new[] { 1, 15, 22 })
            );
        }

        [TestMethod]
        public void Select_WithIndexEmptySource()
        {
            int[] source = new int[0];
            var result = source.Select((x, index) => x + index);
            Assert.IsTrue(
                result.SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Select_WithIndexExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Select((x, index) => x + index).AsEnumerable());
        }

        [TestMethod]
        public void Select_SideEffectsInProjection()
        {
            int[] source = new int[3]; // Actual values won't be relevant
            int count = 0;
            var query = source.Select(x => count++);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 0, 1, 2 })
            );
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 4, 5 })
            );
            count = 10;
            Assert.IsTrue(
                query.SequenceEqual(new[] { 10, 11, 12 })
            );
        }

        // SequenceEqual
        // TestString1 and TestString2 are distinct but equal strings
        private static readonly string TestString1 = "test";
        private static readonly string TestString2 = new string(TestString1.ToCharArray());

        [TestMethod]
        public void SequenceEqual_FirstSourceNull()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_SecondSourceNull()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_NullComparerUsesDefault()
        {
            string[] first = { TestString1 };
            string[] second = { TestString2 };
            Assert.IsTrue(first.SequenceEqual(second, null));
            // Check it's not defaulting to case-insensitive matching...
            first = new[] { "FOO" };
            second = new[] { "BAR" };
            Assert.IsFalse(first.SequenceEqual(second, null));
        }

        [TestMethod]
        public void SequenceEqual_UnequalLengthsBothArrays()
        {
            int[] first = { 1, 5, 3 };
            int[] second = { 1, 5, 3, 10 };
            Assert.IsFalse(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_UnequalLengthsBothRangesFirstLonger()
        {
            var first = Enumerable.Range(0, 11);
            var second = Enumerable.Range(0, 10);
            Assert.IsFalse(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_UnequalLengthsBothRangesSecondLonger()
        {
            var first = Enumerable.Range(0, 10);
            var second = Enumerable.Range(0, 11);
            Assert.IsFalse(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_UnequalData()
        {
            int[] first = { 1, 5, 3, 9 };
            int[] second = { 1, 5, 3, 10 };
            Assert.IsFalse(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_EqualDataBothArrays()
        {
            int[] first = { 1, 5, 3, 10 };
            int[] second = { 1, 5, 3, 10 };
            Assert.IsTrue(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_EqualDataBothRanges()
        {
            var first = Enumerable.Range(0, 10);
            var second = Enumerable.Range(0, 10);
            Assert.IsTrue(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_IdentityNonEqualityOfSequences()
        {
            // In this case we just use side-effects. The sequence could be
            // a sequence of random numbers though.
            int counter = 0;
            var source = Enumerable.Range(0, 5).Select(x => counter++);
            Assert.IsFalse(source.SequenceEqual(source));
        }

        [TestMethod]
        public void SequenceEqual_InfiniteSequenceFirst()
        {
            var first = SequenceEqual_GetInfiniteSequence();
            int[] second = { 1, 1, 1 }; // Same elements to start with, but we stop
            Assert.IsFalse(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_InfiniteSequenceSecond()
        {
            int[] first = { 1, 1, 1 }; // Same elements to start with, but we stop
            var second = SequenceEqual_GetInfiniteSequence();
            Assert.IsFalse(first.SequenceEqual(second));
        }

        [TestMethod]
        public void SequenceEqual_CustomEqualityComparer()
        {
            string[] first = { "foo", "BAR", "baz" };
            string[] second = { "FOO", "bar", "Baz" };
            Assert.IsTrue(first.SequenceEqual(second, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void SequenceEqual_OrderMatters()
        {
            int[] first = { 1, 2 };
            int[] second = { 2, 1 };
            Assert.IsFalse(first.SequenceEqual(second));
        }

        /*[TestMethod]
        [Ignore("LINQ to Objects doesn't optimize by count")]
        public void SequenceEqual_CountOptimization()
        {
            // The counts are different, so we don't need to iterate
            var first = new NonEnumerableCollection<int> { 1, 2, 3 };
            var second = new NonEnumerableCollection<int> { 1, 2 };
            Assert.IsFalse(first.SequenceEqual(second));
        }*/

#if !LINQBRIDGE
        [TestMethod]
        public void SequenceEqual_DefaultComparerOfTypeIsUsedRegardlessOfCollection()
        {
            ICollection<string> set = HashSetProvider.NewHashSet<string>(
                StringComparer.OrdinalIgnoreCase, "abc");
            Assert.IsTrue(set.Contains("ABC"));
            Assert.AreEqual(1, set.Count);
            Assert.IsFalse(set.SequenceEqual(new[] { "ABC" }));
        }
#endif

        [TestMethod]
        public void SequenceEqual_ReturnAtFirstDifference()
        {
            int[] source1 = { 1, 5, 10, 2, 0 };
            int[] source2 = { 1, 5, 10, 1, 0 };
            var query1 = source1.Select(x => 10 / x);
            var query2 = source2.Select(x => 10 / x);
            // If we ever needed to get to the final elements, we'd go bang
            Assert.IsFalse(query1.SequenceEqual(query2));
        }

        private static IEnumerable<int> SequenceEqual_GetInfiniteSequence()
        {
            while (true)
            {
                yield return 1;
            }
        }

        // SingleOrDefault
        [TestMethod]
        public void SingleOrDefault_NullSourceWithoutPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.SingleOrDefault());
        }

        [TestMethod]
        public void SingleOrDefault_NullSourceWithPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Helper.Throws<ArgumentNullException>(() => source.SingleOrDefault(null));
        }

        [TestMethod]
        public void SingleOrDefault_EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Assert.AreEqual(0, source.SingleOrDefault());
        }

        [TestMethod]
        public void SingleOrDefault_SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.SingleOrDefault());
        }

        [TestMethod]
        public void SingleOrDefault_MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Helper.Throws<InvalidOperationException>(() => source.SingleOrDefault());
        }

        [TestMethod]
        public void SingleOrDefault_EmptySequenceWithPredicate()
        {
            int[] source = { };
            Assert.AreEqual(0, source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Assert.AreEqual(0, source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Assert.AreEqual(0, source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Helper.Throws<InvalidOperationException>(() => source.SingleOrDefault(x => x > 3));
        }

        [TestMethod]
        public void SingleOrDefault_EarlyOutWithoutPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Helper.Throws<InvalidOperationException>(() => query.SingleOrDefault());
        }

        [TestMethod]
        public void SingleOrDefault_EarlyOutWithPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Helper.Throws<InvalidOperationException>(() => query.SingleOrDefault(x => true));
        }

        // Single
        [TestMethod]
        public void Single_NullSourceWithoutPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Single());
        }

        [TestMethod]
        public void Single_NullSourceWithPredicate()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Helper.Throws<ArgumentNullException>(() => source.Single(null));
        }

        [TestMethod]
        public void Single_EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Single());
        }

        [TestMethod]
        public void Single_SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.Single());
        }

        [TestMethod]
        public void Single_MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Helper.Throws<InvalidOperationException>(() => source.Single());
        }

        [TestMethod]
        public void Single_EmptySequenceWithPredicate()
        {
            int[] source = { };
            Helper.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Helper.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Helper.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Helper.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [TestMethod]
        public void Single_EarlyOutWithoutPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Helper.Throws<InvalidOperationException>(() => query.Single());
        }

        [TestMethod]
        public void Single_EarlyOutWithPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Helper.Throws<InvalidOperationException>(() => query.Single(x => true));
        }

        // Skip
        [TestMethod]
        public void Skip_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().Skip(10);
        }

        [TestMethod]
        public void Skip_NullSource()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Skip(10));
        }

        [TestMethod]
        public void Skip_NegativeCount()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Skip(-5).SequenceEqual(new[] { 0, 1, 2, 3, 4 })
            );
        }

        [TestMethod]
        public void Skip_ZeroCount()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Skip(0).SequenceEqual(new[] { 0, 1, 2, 3, 4 })
            );
        }

        [TestMethod]
        public void Skip_NegativeCountWithArray()
        {
            Assert.IsTrue(
                new int[] { 0, 1, 2, 3, 4 }.Skip(-5).SequenceEqual(new[] { 0, 1, 2, 3, 4 })
            );
        }

        [TestMethod]
        public void Skip_ZeroCountWithArray()
        {
            Assert.IsTrue(
                new int[] { 0, 1, 2, 3, 4 }.Skip(0).SequenceEqual(new[] { 0, 1, 2, 3, 4 })
            );
        }

        [TestMethod]
        public void Skip_CountShorterThanSource()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Skip(3).SequenceEqual(new[] { 3, 4 })
            );
        }

        [TestMethod]
        public void Skip_CountEqualToSourceLength()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Skip(5).SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Skip_CountGreaterThanSourceLength()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Skip(100).SequenceEqual(new int[0])
            );
        }

        // SkipWhile
        [TestMethod]
        public void SkipWhile_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().SkipWhile(x => x > 10);
        }

        [TestMethod]
        public void SkipWhile_NullSourceNoIndex()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.SkipWhile(x => x > 10));
        }

        [TestMethod]
        public void SkipWhile_NullSourceUsingIndex()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.SkipWhile((x, index) => x > 10));
        }

        [TestMethod]
        public void SkipWhile_NullPredicateNoIndex()
        {
            int[] source = { 1, 2 };
            Func<int, bool> predicate = null;
            Helper.Throws<ArgumentNullException>(() => source.SkipWhile(predicate));
        }

        [TestMethod]
        public void SkipWhile_NullPredicateUsingIndex()
        {
            int[] source = { 1, 2 };
            Func<int, int, bool> predicate = null;
            Helper.Throws<ArgumentNullException>(() => source.SkipWhile(predicate));
        }

        [TestMethod]
        public void SkipWhile_PredicateFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.SkipWhile(x => x.Length > 4).SequenceEqual(new[] { "zero", "one", "two", "three", "four", "five" })
            );
        }

        [TestMethod]
        public void SkipWhile_PredicateWithIndexFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.SkipWhile((x, index) => index + x.Length > 4).SequenceEqual(new[] { "zero", "one", "two", "three", "four", "five" })
            );
        }

        [TestMethod]
        public void SkipWhile_PredicateMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.SkipWhile(x => x.Length < 5).SequenceEqual(new[] { "three", "four", "five" })
            );
        }

        [TestMethod]
        public void SkipWhile_PredicateWithIndexMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.SkipWhile((x, index) => x.Length > index).SequenceEqual(new[] { "four", "five" })
            );
        }

        [TestMethod]
        public void SkipWhile_PredicateMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.SkipWhile(x => x.Length < 100).SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void SkipWhile_PredicateWithIndexMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.SkipWhile((x, index) => x.Length < 100).SequenceEqual(new string[0])
            );
        }

        // Sum
        #region Int32
        [TestMethod]
        public void Sum_NullSourceInt32NoSelector()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceNullableInt32NoSelector()
        {
            int?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceInt32WithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => x.Length));
        }

        [TestMethod]
        public void Sum_NullSourceNullableInt32WithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (int?)x.Length));
        }

        [TestMethod]
        public void Sum_NullSelectorInt32()
        {
            string[] source = { };
            Func<string, int> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_NullSelectorNullableInt32()
        {
            string[] source = { };
            Func<string, int?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_EmptySequenceInt32()
        {
            int[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableInt32()
        {
            int?[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableInt32()
        {
            int?[] source = { null, null };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceInt32WithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => x.Length));
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableInt32WithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (int?)x.Length));
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableInt32WithSelector()
        {
            string[] source = { "x", "y" };
            Assert.AreEqual(0, source.Sum(x => (int?)null));
        }

        [TestMethod]
        public void Sum_SimpleSumInt32()
        {
            int[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt32()
        {
            int?[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt32IncludingNulls()
        {
            int?[] source = { 1, null, 3, null, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumInt32WithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt32WithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (int?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt32WithSelectorIncludingNulls()
        {
            string[] source = { "x", "null", "abc", "null", "de" };
            Assert.AreEqual(6, source.Sum(x => x == "null" ? null : (int?)x.Length));
        }

        [TestMethod]
        public void Sum_NegativeOverflowInt32()
        {
            // Only test this once per type - the other overflow tests should be enough
            // for different method calls
            int[] source = { int.MinValue, int.MinValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowInt32()
        {
            int[] source = { int.MaxValue, int.MaxValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowInt32WithSelector()
        {
            string[] source = { "x", "y" };
            Helper.Throws<OverflowException>(() => source.Sum(x => int.MaxValue));
        }

        [TestMethod]
        public void Sum_OverflowNullableInt32()
        {
            int?[] source = { int.MaxValue, int.MaxValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowNullableInt32WithSelector()
        {
            string[] source = { "x", "y" };
            Helper.Throws<OverflowException>(() => source.Sum(x => (int?)int.MaxValue));
        }

        [TestMethod]
        public void Sum_OverflowOfComputableSumInt32()
        {
            int[] source = { int.MaxValue, 1, -1, -int.MaxValue };
            // In a world where we summed using a long accumulator, the
            // result would be 0.
            Helper.Throws<OverflowException>(() => source.Sum());
        }
        #endregion

        #region Int64
        [TestMethod]
        public void Sum_NullSourceInt64NoSelector()
        {
            long[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceNullableInt64NoSelector()
        {
            long?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceInt64WithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (long)x.Length));
        }

        [TestMethod]
        public void Sum_NullSourceNullableInt64WithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (long?)x.Length));
        }

        [TestMethod]
        public void Sum_NullSelectorInt64()
        {
            string[] source = { };
            Func<string, long> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_NullSelectorNullableInt64()
        {
            string[] source = { };
            Func<string, long?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_EmptySequenceInt64()
        {
            long[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableInt64()
        {
            long?[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableInt64()
        {
            long?[] source = { null, null };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceInt64WithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (long)x.Length));
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableInt64WithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (long?)x.Length));
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableInt64WithSelector()
        {
            string[] source = { "x", "y" };
            Assert.AreEqual(0, source.Sum(x => (long?)null));
        }

        [TestMethod]
        public void Sum_SimpleSumInt64()
        {
            long[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt64()
        {
            long?[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt64IncludingNulls()
        {
            long?[] source = { 1, null, 3, null, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumInt64WithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (long)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt64WithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (long?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableInt64WithSelectorIncludingNulls()
        {
            string[] source = { "x", "null", "abc", "null", "de" };
            Assert.AreEqual(6, source.Sum(x => x == "null" ? null : (long?)x.Length));
        }

        [TestMethod]
        public void Sum_NegativeOverflowInt64()
        {
            // Only test this once per type - the other overflow tests should be enough
            // for different method calls
            long[] source = { long.MinValue, long.MinValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowInt64()
        {
            long[] source = { long.MaxValue, long.MaxValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowInt64WithSelector()
        {
            string[] source = { "x", "y" };
            Helper.Throws<OverflowException>(() => source.Sum(x => long.MaxValue));
        }

        [TestMethod]
        public void Sum_OverflowNullableInt64()
        {
            long?[] source = { long.MaxValue, long.MaxValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowNullableInt64WithSelector()
        {
            string[] source = { "x", "y" };
            Helper.Throws<OverflowException>(() => source.Sum(x => (long?)long.MaxValue));
        }
        #endregion

        #region Decimal
        [TestMethod]
        public void Sum_NullSourceDecimalNoSelector()
        {
            decimal[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceNullableDecimalNoSelector()
        {
            decimal?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceDecimalWithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (decimal)x.Length));
        }

        [TestMethod]
        public void Sum_NullSourceNullableDecimalWithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (decimal?)x.Length));
        }

        [TestMethod]
        public void Sum_NullSelectorDecimal()
        {
            string[] source = { };
            Func<string, decimal> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_NullSelectorNullableDecimal()
        {
            string[] source = { };
            Func<string, decimal?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_EmptySequenceDecimal()
        {
            decimal[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableDecimal()
        {
            decimal?[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableDecimal()
        {
            decimal?[] source = { null, null };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceDecimalWithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (decimal)x.Length));
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableDecimalWithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (decimal?)x.Length));
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableDecimalWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.AreEqual(0, source.Sum(x => (decimal?)null));
        }

        [TestMethod]
        public void Sum_SimpleSumDecimal()
        {
            decimal[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDecimal()
        {
            decimal?[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDecimalIncludingNulls()
        {
            decimal?[] source = { 1, null, 3, null, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumDecimalWithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (decimal)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDecimalWithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (decimal?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDecimalWithSelectorIncludingNulls()
        {
            string[] source = { "x", "null", "abc", "null", "de" };
            Assert.AreEqual(6, source.Sum(x => x == "null" ? null : (decimal?)x.Length));
        }

        [TestMethod]
        public void Sum_NegativeOverflowDecimal()
        {
            // Only test this once per type - the other overflow tests should be enough
            // for different method calls
            decimal[] source = { decimal.MinValue, decimal.MinValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowDecimal()
        {
            decimal[] source = { decimal.MaxValue, decimal.MaxValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowDecimalWithSelector()
        {
            string[] source = { "x", "y" };
            Helper.Throws<OverflowException>(() => source.Sum(x => decimal.MaxValue));
        }

        [TestMethod]
        public void Sum_OverflowNullableDecimal()
        {
            decimal?[] source = { decimal.MaxValue, decimal.MaxValue };
            Helper.Throws<OverflowException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_OverflowNullableDecimalWithSelector()
        {
            string[] source = { "x", "y" };
            Helper.Throws<OverflowException>(() => source.Sum(x => (decimal?)decimal.MaxValue));
        }
        #endregion

        #region Single
        [TestMethod]
        public void Sum_NullSourceSingleNoSelector()
        {
            float[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceNullableSingleNoSelector()
        {
            float?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceSingleWithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (float)x.Length));
        }

        [TestMethod]
        public void Sum_NullSourceNullableSingleWithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (float?)x.Length));
        }

        [TestMethod]
        public void Sum_NullSelectorSingle()
        {
            string[] source = { };
            Func<string, float> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_NullSelectorNullableSingle()
        {
            string[] source = { };
            Func<string, float?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_EmptySequenceSingle()
        {
            float[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableSingle()
        {
            float?[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableSingle()
        {
            float?[] source = { null, null };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceSingleWithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (float)x.Length));
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableSingleWithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (float?)x.Length));
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableSingleWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.AreEqual(0, source.Sum(x => (float?)null));
        }

        [TestMethod]
        public void Sum_SimpleSumSingle()
        {
            float[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SumWithNaNSingle()
        {
            float[] source = { 1, 3, float.NaN, 2 };
            Helper.IsNaN(source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableSingle()
        {
            float?[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SumWithNaNNullableSingle()
        {
            float[] source = { 1, 3, float.NaN, 2 };
            Helper.IsNaN(source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableSingleIncludingNulls()
        {
            float?[] source = { 1, null, 3, null, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumSingleWithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (float)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumSingleWithSelectorWithNan()
        {
            string[] source = { "x", "abc", "de" };
            Helper.IsNaN(source.Sum(x => x.Length == 3 ? x.Length : float.NaN));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableSingleWithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (float?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableSingleWithSelectorIncludingNulls()
        {
            string[] source = { "x", "null", "abc", "null", "de" };
            Assert.AreEqual(6, source.Sum(x => x == "null" ? null : (float?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableSingleWithSelectorWithNan()
        {
            string[] source = { "x", "abc", "de" };
            Helper.IsNaN(source.Sum(x => x.Length == 3 ? x.Length : (float?)float.NaN));
        }

        [TestMethod]
        public void Sum_OverflowToNegativeInfinitySingle()
        {
            // Only test this once per type - the other overflow tests should be enough
            // for different method calls
            float[] source = { float.MinValue, float.MinValue };
            Assert.IsTrue(float.IsNegativeInfinity(source.Sum()));
        }

        [TestMethod]
        public void Sum_OverflowToInfinitySingle()
        {
            float[] source = { float.MaxValue, float.MaxValue };
            Assert.IsTrue(float.IsPositiveInfinity(source.Sum()));
        }

        [TestMethod]
        public void Sum_OverflowToInfinitySingleWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.IsTrue(float.IsPositiveInfinity(source.Sum(x => float.MaxValue)));
        }

        [TestMethod]
        public void Sum_OverflowToInfinityNullableSingle()
        {
            float?[] source = { float.MaxValue, float.MaxValue };
            Assert.IsTrue(float.IsPositiveInfinity(source.Sum().Value));
        }

        [TestMethod]
        public void Sum_OverflowToInfinityNullableSingleWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.IsTrue(float.IsPositiveInfinity(source.Sum(x => (float?)float.MaxValue).Value));
        }

        [TestMethod]
        public void Sum_NonOverflowOfComputableSumSingle()
        {
            float[] source = { float.MaxValue, float.MaxValue,
                              -float.MaxValue, -float.MaxValue };
            // In a world where we summed using a float accumulator, the
            // result would be infinity.
            Assert.AreEqual(0f, source.Sum());
        }

        [TestMethod]
        public void Sum_AccumulatorAccuracyForSingle()
        {
            // 20000000 and 20000004 are both exactly representable as
            // float values, but 20000001 is not. Therefore if we use
            // a float accumulator, we'll end up with 20000000. However,
            // if we use a double accumulator, we'll get the right value.
            float[] array = { 20000000f, 1f, 1f, 1f, 1f };
            Assert.AreEqual(20000004f, array.Sum());
        }
        #endregion

        #region Double
        [TestMethod]
        public void Sum_NullSourceDoubleNoSelector()
        {
            double[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceNullableDoubleNoSelector()
        {
            double?[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum());
        }

        [TestMethod]
        public void Sum_NullSourceDoubleWithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (double)x.Length));
        }

        [TestMethod]
        public void Sum_NullSourceNullableDoubleWithSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(x => (double?)x.Length));
        }

        [TestMethod]
        public void Sum_NullSelectorDouble()
        {
            string[] source = { };
            Func<string, double> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_NullSelectorNullableDouble()
        {
            string[] source = { };
            Func<string, double?> selector = null;
            Helper.Throws<ArgumentNullException>(() => source.Sum(selector));
        }

        [TestMethod]
        public void Sum_EmptySequenceDouble()
        {
            double[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableDouble()
        {
            double?[] source = { };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableDouble()
        {
            double?[] source = { null, null };
            Assert.AreEqual(0, source.Sum());
        }

        [TestMethod]
        public void Sum_EmptySequenceDoubleWithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (double)x.Length));
        }

        [TestMethod]
        public void Sum_EmptySequenceNullableDoubleWithSelector()
        {
            string[] source = { };
            Assert.AreEqual(0, source.Sum(x => (double?)x.Length));
        }

        [TestMethod]
        public void Sum_SequenceOfNullsNullableDoubleWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.AreEqual(0, source.Sum(x => (double?)null));
        }

        [TestMethod]
        public void Sum_SimpleSumDouble()
        {
            double[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SumWithNaNDouble()
        {
            double[] source = { 1, 3, double.NaN, 2 };
            Helper.IsNaN(source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDouble()
        {
            double?[] source = { 1, 3, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SumWithNaNNullableDouble()
        {
            double[] source = { 1, 3, double.NaN, 2 };
            Helper.IsNaN(source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDoubleIncludingNulls()
        {
            double?[] source = { 1, null, 3, null, 2 };
            Assert.AreEqual(6, source.Sum());
        }

        [TestMethod]
        public void Sum_SimpleSumDoubleWithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (double)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumDoubleWithSelectorWithNan()
        {
            string[] source = { "x", "abc", "de" };
            Helper.IsNaN(source.Sum(x => x.Length == 3 ? x.Length : double.NaN));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDoubleWithSelector()
        {
            string[] source = { "x", "abc", "de" };
            Assert.AreEqual(6, source.Sum(x => (double?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDoubleWithSelectorIncludingNulls()
        {
            string[] source = { "x", "null", "abc", "null", "de" };
            Assert.AreEqual(6, source.Sum(x => x == "null" ? null : (double?)x.Length));
        }

        [TestMethod]
        public void Sum_SimpleSumNullableDoubleWithSelectorWithNan()
        {
            string[] source = { "x", "abc", "de" };
            Helper.IsNaN(source.Sum(x => x.Length == 3 ? x.Length : (double?)double.NaN));
        }

        [TestMethod]
        public void Sum_OverflowToNegativeInfinityDouble()
        {
            // Only test this once per type - the other overflow tests should be enough
            // for different method calls
            double[] source = { double.MinValue, double.MinValue };
            Assert.IsTrue(double.IsNegativeInfinity(source.Sum()));
        }

        [TestMethod]
        public void Sum_OverflowToInfinityDouble()
        {
            double[] source = { double.MaxValue, double.MaxValue };
            Assert.IsTrue(double.IsPositiveInfinity(source.Sum()));
        }

        [TestMethod]
        public void Sum_OverflowToInfinityDoubleWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.IsTrue(double.IsPositiveInfinity(source.Sum(x => double.MaxValue)));
        }

        [TestMethod]
        public void Sum_OverflowToInfinityNullableDouble()
        {
            double?[] source = { double.MaxValue, double.MaxValue };
            Assert.IsTrue(double.IsPositiveInfinity(source.Sum().Value));
        }

        [TestMethod]
        public void Sum_OverflowToInfinityNullableDoubleWithSelector()
        {
            string[] source = { "x", "y" };
            Assert.IsTrue(double.IsPositiveInfinity(source.Sum(x => (double?)double.MaxValue).Value));
        }
        #endregion

        // Take

        [TestMethod]
        public void Take_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().Take(10);
        }

        [TestMethod]
        public void Take_NullSource()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.Take(10));
        }

        [TestMethod]
        public void Take_NegativeCount()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Take(-5).SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Take_ZeroCount()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Take(-5).SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Take_CountShorterThanSource()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Take(3).SequenceEqual(new[] { 0, 1, 2 })
            );
        }

        [TestMethod]
        public void Take_CountEqualToSourceLength()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Take(5).SequenceEqual(new[] { 0, 1, 2, 3, 4 })
            );
        }

        [TestMethod]
        public void Take_CountGreaterThanSourceLength()
        {
            Assert.IsTrue(
                Enumerable.Range(0, 5).Take(100).SequenceEqual(new[] { 0, 1, 2, 3, 4 })
            );
        }

        [TestMethod]
        public void Take_OnlyEnumerateTheGivenNumberOfElements()
        {
            int[] source = { 1, 2, 0 };
            // If we try to move onto the third element, we'll die.
            var query = source.Select(x => 10 / x);
            Assert.IsTrue(
                query.Take(2).SequenceEqual(new[] { 10, 5 })
            );
        }

        // TakeWhile
        [TestMethod]
        public void TakeWhile_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().TakeWhile(x => x > 10);
        }

        [TestMethod]
        public void TakeWhile_NullSourceNoIndex()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.TakeWhile(x => x > 10));
        }

        [TestMethod]
        public void TakeWhile_NullSourceUsingIndex()
        {
            int[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.TakeWhile((x, index) => x > 10));
        }

        [TestMethod]
        public void TakeWhile_NullPredicateNoIndex()
        {
            int[] source = { 1, 2 };
            Func<int, bool> predicate = null;
            Helper.Throws<ArgumentNullException>(() => source.TakeWhile(predicate));
        }

        [TestMethod]
        public void TakeWhile_NullPredicateUsingIndex()
        {
            int[] source = { 1, 2 };
            Func<int, int, bool> predicate = null;
            Helper.Throws<ArgumentNullException>(() => source.TakeWhile(predicate));
        }

        [TestMethod]
        public void TakeWhile_PredicateFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five", "six" };
            Assert.IsTrue(
                source.TakeWhile(x => x.Length > 4).SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void TakeWhile_PredicateWithIndexFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.TakeWhile((x, index) => index + x.Length > 4).SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void TakeWhile_PredicateMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.TakeWhile(x => x.Length < 5).SequenceEqual(new[] { "zero", "one", "two" })
            );
        }

        [TestMethod]
        public void TakeWhile_PredicateWithIndexMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.TakeWhile((x, index) => x.Length > index).SequenceEqual(new[] { "zero", "one", "two", "three" })
            );
        }

        [TestMethod]
        public void TakeWhile_PredicateMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.TakeWhile(x => x.Length < 100).SequenceEqual(new[] { "zero", "one", "two", "three", "four", "five" })
            );
        }

        [TestMethod]
        public void TakeWhile_PredicateWithIndexMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            Assert.IsTrue(
                source.TakeWhile((x, index) => x.Length < 100).SequenceEqual(new[] { "zero", "one", "two", "three", "four", "five" })
            );
        }

        // ThenBy
        [TestMethod]
        public void ThenBy_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderBy(x => x).ThenBy(x => x);
        }

        // doesn't make sense in LinqAF
        /*[TestMethod]
        public void ThenBy_NullSourceNoComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.ThenBy(keySelector));
        }*/

        [TestMethod]
        public void ThenBy_NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenBy(keySelector));
        }

        // doesn't make sense in LinqAF
        /*[TestMethod]
        public void ThenBy_NullSourceWithComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.ThenBy(keySelector, Comparer<int>.Default));
        }*/

        [TestMethod]
        public void ThenBy_NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenBy(keySelector, Comparer<int>.Default));
        }

        [TestMethod]
        public void ThenBy_PrimaryOrderingTakesPrecedence()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 20 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 11, SecondaryKey = 22 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 2 })
            );
        }

        [TestMethod]
        public void ThenBy_SecondOrderingIsUsedWhenPrimariesAreEqual()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 1, 2 })
            );
        }

        [TestMethod]
        public void ThenBy_TertiaryKeys()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22, TertiaryKey = 30 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21, TertiaryKey = 31, },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 33 },
                new { Value = 4, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 32 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .ThenBy(x => x.TertiaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 4, 3, 1, 2 })
            );
        }

        [TestMethod]
        public void ThenBy_ThenByAfterOrderByDescending()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderByDescending(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1 })
            );
        }

        [TestMethod]
        public void ThenBy_NullsAreFirst()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = "abc" },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = (string) null },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = "def" }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey, StringComparer.Ordinal)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 1, 3 })
            );
        }

        [TestMethod]
        public void ThenBy_OrderingIsStable()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 10 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = 11 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 },
                new { Value = 4, PrimaryKey = 1, SecondaryKey = 10 },
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 4, 2, 3 })
            );
        }

        [TestMethod]
        public void ThenBy_NullComparerIsDefault()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 15 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = -13 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey, null)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1 })
            );
        }

        [TestMethod]
        public void ThenBy_CustomComparer()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 15 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = -13 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 2, 1 })
            );
        }

        // ThenByDescending
        [TestMethod]
        public void ThenByDescending_ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderBy(x => x).ThenByDescending(x => x);
        }

        // doesn't make sense in LinqAF
        /*[TestMethod]
        public void ThenByDescending_NullSourceNoComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.ThenByDescending(keySelector));
        }*/

        [TestMethod]
        public void ThenByDescending_NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenByDescending(keySelector));
        }

        // doesn't make sense in LinqAF
        /*
        [TestMethod]
        public void ThenByDescending_NullSourceWithComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Helper.Throws<ArgumentNullException>(() => source.ThenByDescending(keySelector, Comparer<int>.Default));
        }*/

        [TestMethod]
        public void ThenByDescending_NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenByDescending(keySelector, Comparer<int>.Default));
        }

        [TestMethod]
        public void ThenByDescending_PrimaryOrderingTakesPrecedence()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 20 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 11, SecondaryKey = 22 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 2 })
            );
        }

        [TestMethod]
        public void ThenByDescending_SecondOrderingIsUsedWhenPrimariesAreEqual()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 2 })
            );
        }

        [TestMethod]
        public void ThenByDescending_TertiaryKeys()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22, TertiaryKey = 30 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21, TertiaryKey = 31, },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 33 },
                new { Value = 4, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 32 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey)
                              .ThenByDescending(x => x.TertiaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 4, 2 })
            );
        }

        [TestMethod]
        public void ThenByDescending_TertiaryKeysWithMixedOrdering()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22, TertiaryKey = 30 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21, TertiaryKey = 31, },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 33 },
                new { Value = 4, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 32 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .ThenByDescending(x => x.TertiaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 4, 1, 2 })
            );
        }

        [TestMethod]
        public void ThenByDescending_ThenByDescendingAfterOrderByDescending()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderByDescending(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 1, 3 })
            );
        }

        [TestMethod]
        public void ThenByDescending_NullsAreLast()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = "abc" },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = (string) null },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = "def" }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey, StringComparer.Ordinal)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 3, 1, 2 })
            );
        }

        [TestMethod]
        public void ThenByDescending_OrderingIsStable()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 10 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = 11 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 },
                new { Value = 4, PrimaryKey = 1, SecondaryKey = 10 },
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 2, 3, 1, 4 })
            );
        }

        [TestMethod]
        public void ThenByDescending_NullComparerIsDefault()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 15 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = -13 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey, null)
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 3, 2 })
            );
        }

        [TestMethod]
        public void ThenByDescending_CustomComparer()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 15 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = -13 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            Assert.IsTrue(
                query.SequenceEqual(new[] { 1, 2, 3 })
            );
        }

        // ToArray
        [TestMethod]
        public void ToArray_ResultIsIndependentOfSource()
        {
            List<string> source = new List<string> { "xyz", "abc" };
            // Make it obvious we're not calling List<T>.ToArray
            string[] result = source.ToArray();
            Assert.IsTrue(
                result.SequenceEqual(new[] { "xyz", "abc" })
            );

            // Change the source: result won't have changed
            source[0] = "xxx";
            Assert.AreEqual("xyz", result[0]);

            // And the reverse
            result[1] = "yyy";
            Assert.AreEqual("abc", source[1]);
        }

        [TestMethod]
        public void ToArray_SequenceIsEvaluatedEagerly()
        {
            int[] numbers = { 5, 3, 0 };
            var query = numbers.Select(x => 10 / x);
            Helper.Throws<DivideByZeroException>(() => query.ToArray());
        }

        [TestMethod]
        public void ToArray_NullSource()
        {
            IEnumerable<string> source = null;
            Helper.Throws<ArgumentNullException>(() => source.ToArray());
        }

        [TestMethod]
        public void ToArray_ConversionOfLazilyEvaluatedSequence()
        {
            var range = Enumerable.Range(3, 3);
            var query = range.Select(x => x * 2);
            var list = query.ToArray();
            Assert.IsTrue(
                list.SequenceEqual(new[] { 6, 8, 10 })
            );
        }

        [TestMethod]
        public void ToArray_ICollectionOptimization()
        {
            var source = new NonEnumerableCollection<string> { "hello", "there" };
            // If ToList just iterated over the list, this would throw
            var list = source.ToArray();
            Assert.IsTrue(
                list.SequenceEqual(new[] { "hello", "there" })
            );
        }

        // ToDictionary
        [TestMethod]
        public void ToDictionary_NullSourceNoComparerNoElementSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x));
        }

        [TestMethod]
        public void ToDictionary_NullKeySelectorNoComparerNoElementSelector()
        {
            string[] source = { };
            Func<string, string> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector));
        }

        [TestMethod]
        public void ToDictionary_NullSourceWithComparerNoElementSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ToDictionary_NullKeySelectorWithComparerNoElementSelector()
        {
            string[] source = { };
            Func<string, string> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ToDictionary_NullSourceNoComparerWithElementSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, x => x));
        }

        [TestMethod]
        public void ToDictionary_NullKeySelectorNoComparerWithElementSelector()
        {
            string[] source = { };
            Func<string, string> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector, x => x));
        }

        [TestMethod]
        public void ToDictionary_NullElementSelectorNoComparer()
        {
            string[] source = { };
            Func<string, string> elementSelector = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, elementSelector));
        }

        [TestMethod]
        public void ToDictionary_NullSourceWithComparerWithElementSelector()
        {
            string[] source = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, x => x, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ToDictionary_NullKeySelectorWithComparerWithElementSelector()
        {
            string[] source = { };
            Func<string, string> keySelector = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(keySelector, x => x, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ToDictionary_NullElementSelectorWithComparer()
        {
            string[] source = { };
            Func<string, string> elementSelector = null;
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x, elementSelector, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ToDictionary_JustKeySelector()
        {
            string[] source = { "zero", "one", "two" };
            var result = source.ToDictionary(x => x[0]);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("zero", result['z']);
            Assert.AreEqual("one", result['o']);
            Assert.AreEqual("two", result['t']);
        }

        [TestMethod]
        public void ToDictionary_KeyAndElementSelector()
        {
            // Map the first character of each string to the string's length
            string[] source = { "zero", "one", "two" };
            var result = source.ToDictionary(x => x[0], x => x.Length);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(4, result['z']); // Length of "zero"
            Assert.AreEqual(3, result['o']); // Length of "one"
            Assert.AreEqual(3, result['t']); // Length of "two"
        }

        [TestMethod]
        public void ToDictionary_CustomEqualityComparer()
        {
            // Map the first character of each string (*as* a string) to the string's length,
            // using a case-insensitive comparer
            string[] source = { "zero", "One", "Two" };
            var result = source.ToDictionary(x => x.Substring(0, 1),
                                             x => x.Length,
                                             StringComparer.OrdinalIgnoreCase);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(4, result["z"]); // Length of "zero"
            // These two rely on the dictionary being case-insensitive
            Assert.AreEqual(3, result["o"]); // Length of "one"
            Assert.AreEqual(3, result["t"]); // Length of "two"            
        }

        [TestMethod]
        public void ToDictionary_DuplicateKey()
        {
            // Oh no! "Two" and "three" start with the same letter (case-insensitively)
            string[] source = { "zero", "One", "Two", "three" };

            Helper.Throws<ArgumentException>(() =>
                source.ToDictionary(x => x.Substring(0, 1), StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ToDictionary_NullEntryKeyCausesException()
        {
            string[] source = { "a", "b", null };
            Helper.Throws<ArgumentNullException>(() => source.ToDictionary(x => x));
        }

        [TestMethod]
        public void ToDictionary_NullEntryValueIsAllowed()
        {
            string[] source = { "a", "b" };
            var result = source.ToDictionary(x => x, x => (string)null);
            Assert.AreEqual(2, result.Count);
            Assert.IsNull(result["a"]);
            Assert.IsNull(result["b"]);
        }

        [TestMethod]
        public void ToDictionary_NullComparerMeansDefault()
        {
            // The default string comparer is case-sensitive
            string[] source = { "zero", "One", "Two", "three" };

            var result = source.ToDictionary(x => x.Substring(0, 1), null);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("Two", result["T"]);
            Assert.AreEqual("three", result["t"]);
        }

        // ToList
        [TestMethod]
        public void ToList_ResultIsIndependentOfSource()
        {
            List<string> source = new List<string> { "xyz", "abc" };
            List<string> result = source.ToList();
            Assert.IsTrue(
                result.SequenceEqual(new[] { "xyz", "abc" })
            );
            Assert.AreNotSame(source, result);

            source.Add("extra element");
            // The extra element hasn't been added to the result
            Assert.AreNotEqual(source.Count, result.Count);
        }

        [TestMethod]
        public void ToList_SequenceIsEvaluatedEagerly()
        {
            int[] numbers = { 5, 3, 0 };
            var query = numbers.Select(x => 10 / x);
            Helper.Throws<DivideByZeroException>(() => query.ToList());
        }

        [TestMethod]
        public void ToList_NullSource()
        {
            IEnumerable<string> source = null;
            Helper.Throws<ArgumentNullException>(() => source.ToList());
        }

        [TestMethod]
        public void ToList_ConversionOfLazilyEvaluatedSequence()
        {
            var range = Enumerable.Range(3, 3);
            var query = range.Select(x => x * 2);
            var list = query.ToList();
            Assert.IsTrue(
                list.SequenceEqual(new[] { 6, 8, 10 })
            );
        }

        [TestMethod]
        public void ToList_ICollectionOptimization()
        {
            var source = new NonEnumerableCollection<string> { "hello", "there" };
            // If ToList just iterated over the list, this would throw
            var list = source.ToList();
            Assert.IsTrue(
                list.SequenceEqual(new[] { "hello", "there" })
            );
        }

        // ToLookup
        [TestMethod]
        public void ToLookup_SourceSequenceIsReadEagerly()
        {
            var source = new ThrowingEnumerable();
            Helper.Throws<InvalidOperationException>(() => source.ToLookup(x => x));
        }

        [TestMethod]
        public void ToLookup_ChangesToSourceSequenceAfterToLookupAreNotNoticed()
        {
            List<string> source = new List<string> { "abc" };
            var lookup = source.ToLookup(x => x.Length);
            Assert.AreEqual(1, lookup.Count);

            // Potential new key is ignored
            source.Add("x");
            Assert.AreEqual(1, lookup.Count);

            // Potential new value for existing key is ignored
            source.Add("xyz");
            Assert.IsTrue(
                lookup[3].SequenceEqual(new[] { "abc" })
            );
        }

        [TestMethod]
        public void ToLookup_LookupWithNoComparerOrElementSelector()
        {
            string[] source = { "abc", "def", "x", "y", "ghi", "z", "00" };
            var lookup = source.ToLookup(value => value.Length);
            Assert.IsTrue(
                lookup[3].SequenceEqual(new[] { "abc", "def", "ghi" })
            );
            Assert.IsTrue(
                lookup[1].SequenceEqual(new[] { "x", "y", "z" })
            );
            Assert.IsTrue(
                lookup[2].SequenceEqual(new[] { "00" })
            );

            Assert.AreEqual(3, lookup.Count);

            // An unknown key returns an empty sequence
            Assert.IsTrue(
                lookup[100].SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void ToLookup_LookupWithComparerButNoElementSelector()
        {
            string[] source = { "abc", "def", "ABC" };
            var lookup = source.ToLookup(value => value, StringComparer.OrdinalIgnoreCase);
            Assert.IsTrue(
                lookup["abc"].SequenceEqual(new[] { "abc", "ABC" })
            );
            Assert.IsTrue(
                lookup["def"].SequenceEqual(new[] { "def" })
            );
        }

        [TestMethod]
        public void ToLookup_LookupWithNullComparerButNoElementSelector()
        {
            string[] source = { "abc", "def", "ABC" };
            var lookup = source.ToLookup(value => value, null);
            Assert.IsTrue(
                lookup["abc"].SequenceEqual(new[] { "abc" })
            );
            Assert.IsTrue(
                lookup["ABC"].SequenceEqual(new[] { "ABC" })
            );
            Assert.IsTrue(
                lookup["def"].SequenceEqual(new[] { "def" })
            );
        }

        [TestMethod]
        public void ToLookup_LookupWithElementSelectorButNoComparer()
        {
            string[] source = { "abc", "def", "x", "y", "ghi", "z", "00" };
            // Use the length as the key selector, and the first character as the element
            var lookup = source.ToLookup(value => value.Length, value => value[0]);
            Assert.IsTrue(
                lookup[3].SequenceEqual(new[] { 'a', 'd', 'g' })
            );
            Assert.IsTrue(
                lookup[1].SequenceEqual(new[] { 'x', 'y', 'z' })
            );
            Assert.IsTrue(
                lookup[2].SequenceEqual(new[] { '0' })
            );
        }

        [TestMethod]
        public void ToLookup_LookupWithComparareAndElementSelector()
        {
            var people = new[] {
                new { First = "Jon", Last = "Skeet" },
                new { First = "Tom", Last = "SKEET" }, // Note upper-cased name
                new { First = "Juni", Last = "Cortez" },
                new { First = "Holly", Last = "Skeet" },
                new { First = "Abbey", Last = "Bartlet" },
                new { First = "Carmen", Last = "Cortez" },
                new { First = "Jed", Last = "Bartlet" }
            };

            var lookup = people.ToLookup(p => p.Last, p => p.First, StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(
                lookup["Skeet"].SequenceEqual(new[] { "Jon", "Tom", "Holly" })
            );
            Assert.IsTrue(
                lookup["Cortez"].SequenceEqual(new[] { "Juni", "Carmen" })
            );
            // The key comparer is used for lookups too
            Assert.IsTrue(
                lookup["BARTLET"].SequenceEqual(new[] { "Abbey", "Jed" })
            );

            Assert.IsTrue(
                lookup.Select(x => x.Key).SequenceEqual(new[] { "Skeet", "Cortez", "Bartlet" })
            );
        }

        [TestMethod]
        public void ToLookup_FindByNullKeyNonePresent()
        {
            string[] source = { "first", "second" };
            var lookup = source.ToLookup(x => x);
            Assert.IsTrue(
                lookup[null].SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void ToLookup_FindByNullKeyWhenPresent()
        {
            string[] source = { "first", "null", "nothing", "second" };
            var lookup = source.ToLookup(x => x.StartsWith("n") ? null : x);
            Assert.IsTrue(
                lookup[null].SequenceEqual(new[] { "null", "nothing" })
            );
            Assert.IsTrue(
                lookup.Select(x => x.Key).SequenceEqual(new[] { "first", null, "second" })
            );
            Assert.AreEqual(3, lookup.Count);
            Assert.IsTrue(
                lookup[null].SequenceEqual(new[] { "null", "nothing" })
            );
        }

        // Union
        [TestMethod]
        public void Union_NullFirstWithoutComparer()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.Union(second));
        }

        [TestMethod]
        public void Union_NullSecondWithoutComparer()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.Union(second));
        }

        [TestMethod]
        public void Union_NullFirstWithComparer()
        {
            string[] first = null;
            string[] second = { };
            Helper.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.Ordinal));
        }

        [TestMethod]
        public void Union_NullSecondWithComparer()
        {
            string[] first = { };
            string[] second = null;
            Helper.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.Ordinal));
        }

        [TestMethod]
        public void Union_UnionWithoutComparer()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { "d", "e", "d", "a" };
            Assert.IsTrue(
                first.Union(second).SequenceEqual(new[] { "a", "b", "B", "c", "d", "e" })
            );
        }

        [TestMethod]
        public void Union_UnionWithNullComparer()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { "d", "e", "d", "a" };
            Assert.IsTrue(
                first.Union(second, null).SequenceEqual(new[] { "a", "b", "B", "c", "d", "e" })
            );
        }

        [TestMethod]
        public void Union_UnionWithCaseInsensitiveComparer()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { "d", "e", "d", "a" };
            Assert.IsTrue(
                first.Union(second, StringComparer.OrdinalIgnoreCase).SequenceEqual(new[] { "a", "b", "c", "d", "e" })
            );
        }

        [TestMethod]
        public void Union_UnionWithEmptyFirstSequence()
        {
            string[] first = { };
            string[] second = { "d", "e", "d", "a" };
            Assert.IsTrue(
                first.Union(second).SequenceEqual(new[] { "d", "e", "a" })
            );
        }

        [TestMethod]
        public void Union_UnionWithEmptySecondSequence()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { };
            Assert.IsTrue(
                first.Union(second).SequenceEqual(new[] { "a", "b", "B", "c" })
            );
        }

        [TestMethod]
        public void Union_UnionWithTwoEmptySequences()
        {
            string[] first = { };
            string[] second = { };
            Assert.IsTrue(
                first.Union(second).SequenceEqual(new string[0])
            );
        }

        [TestMethod]
        public void Union_FirstSequenceIsNotUsedUntilQueryIsIterated()
        {
            var first = new ThrowingEnumerable();
            int[] second = { 2 };
            var query = first.Union(second);
            using (var iterator = query.GetEnumerator())
            {
                // Still no exception... until we call MoveNext
                Helper.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        [TestMethod]
        public void Union_SecondSequenceIsNotUsedUntilFirstIsExhausted()
        {
            int[] first = { 3, 5, 3 };
            var second = new ThrowingEnumerable();
            using (var iterator = first.Union(second).GetEnumerator())
            {
                // Check the first two elements...
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(3, iterator.Current);
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);

                // But as soon as we move past the first sequence, bang!
                Helper.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        // Where
        [TestMethod]
        public void Where_NullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Where(x => x > 5));
        }

        [TestMethod]
        public void Where_NullPredicateThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, bool> predicate = null;
            Helper.Throws<ArgumentNullException>(() => source.Where(predicate));
        }

        [TestMethod]
        public void Where_WithIndexNullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Helper.Throws<ArgumentNullException>(() => source.Where((x, index) => x > 5));
        }

        [TestMethod]
        public void Where_WithIndexNullPredicateThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, int, bool> predicate = null;
            Helper.Throws<ArgumentNullException>(() => source.Where(predicate));
        }

        [TestMethod]
        public void Where_SimpleFiltering()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = source.Where(x => x < 4);
            Assert.IsTrue(
                result.SequenceEqual(new[] { 1, 3, 2, 1 })
            );
        }

        [TestMethod]
        public void Where_SimpleFilteringWithQueryExpression()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = from x in source
                         where x < 4
                         select x;
            Assert.IsTrue(
                result.SequenceEqual(new[] { 1, 3, 2, 1 })
            );
        }

        [TestMethod]
        public void Where_EmptySource()
        {
            int[] source = new int[0];
            var result = source.Where(x => x < 4);
            Assert.IsTrue(
                result.SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Where_ExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Where(x => x > 0).AsEnumerable());
        }

        [TestMethod]
        public void Where_WithIndexSimpleFiltering()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = source.Where((x, index) => x < index);
            Assert.IsTrue(
                result.SequenceEqual(new[] { 2, 1 })
            );
        }

        [TestMethod]
        public void Where_WithIndexEmptySource()
        {
            int[] source = new int[0];
            var result = source.Where((x, index) => x < 4);
            Assert.IsTrue(
                result.SequenceEqual(new int[0])
            );
        }

        [TestMethod]
        public void Where_WithIndexExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Where((x, index) => x > 0).AsEnumerable());
        }

        // Zip
        [TestMethod]
        public void Zip_NullFirst()
        {
            string[] first = null;
            int[] second = { };
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            Helper.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [TestMethod]
        public void Zip_NullSecond()
        {
            string[] first = { };
            int[] second = null;
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            Helper.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [TestMethod]
        public void Zip_NullResultSelector()
        {
            string[] first = { };
            int[] second = { };
            Func<string, int, string> resultSelector = null;
            Helper.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [TestMethod]
        public void Zip_ExecutionIsDeferred()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            first.Zip(second, (x, y) => x + y);
        }

        [TestMethod]
        public void Zip_ShortFirst()
        {
            string[] first = { "a", "b", "c" };
            var second = Enumerable.Range(5, 10);
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            var query = first.Zip(second, resultSelector);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "a:5", "b:6", "c:7" })
            );
        }

        [TestMethod]
        public void Zip_ShortSecond()
        {
            string[] first = { "a", "b", "c", "d", "e" };
            var second = Enumerable.Range(5, 3);
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            var query = first.Zip(second, resultSelector);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "a:5", "b:6", "c:7" })
            );
        }

        [TestMethod]
        public void Zip_EqualLengthSequences()
        {
            string[] first = { "a", "b", "c" };
            var second = Enumerable.Range(5, 3);
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            var query = first.Zip(second, resultSelector);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "a:5", "b:6", "c:7" })
            );
        }

        [TestMethod]
        public void Zip_AdjacentElements()
        {
            string[] elements = { "a", "b", "c", "d", "e" };
            var query = elements.Zip(elements.Skip(1), (x, y) => x + y);
            Assert.IsTrue(
                query.SequenceEqual(new[] { "ab", "bc", "cd", "de" })
            );
        }
    }

    /// <summary>
    /// Implementation of ICollection[T] (but not IList[T]) which can't be iterated over.
    /// </summary>
    class NonEnumerableCollection<T> : ICollection<T>
    {
        private readonly List<T> backingList = new List<T>();

        public void Add(T item)
        {
            backingList.Add(item);
        }

        public void Clear()
        {
            backingList.Clear();
        }

        public bool Contains(T item)
        {
            return backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            backingList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return backingList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>)backingList).IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return backingList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    // Comparer which is equivalent to the default comparer for int, but
    // always returns int.MinValue, 0, or int.MaxValue.
    class OrderByDescending_ExtremeComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x == y ? 0
                : x < y ? int.MinValue
                : int.MaxValue;
        }
    }

    /// <summary>
    /// Implementation of IComparer[int] which simply compares absolute values.
    /// </summary>
    sealed class AbsoluteValueComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return Math.Abs(x).CompareTo(Math.Abs(y));
        }
    }

    // Comparer which is equivalent to the default comparer for int, but
    // always returns int.MinValue, 0, or int.MaxValue.
    class OrderBy_ExtremeComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x == y ? 0
                : x < y ? int.MinValue
                : int.MaxValue;
        }
    }

    // Implementation of IEqualityComparer[T] which uses object identity
    class Distinct_ReferenceEqualityComparer : IEqualityComparer<object>
    {
        // Use explicit interface implementation to avoid warnings about hiding
        // the static object.Equals(object, object)
        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            return object.ReferenceEquals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }

    // Implementation of IEqualityComparer[T] which uses object's Equals/GetHashCode methods
    // in the simplest possible way, without any attempt to guard against NullReferenceException.
    class Distinct_SimpleEqualityComparer : IEqualityComparer<object>
    {
        // Use explicit interface implementation to avoid warnings about hiding
        // the static object.Equals(object, object)
        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    static class EdulinqExtensionMethods
    {
        public static string ToInvariantString(this int n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }
    }

    sealed class ThrowingEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Check that the given function uses deferred execution.
        /// A "spiked" source is given to the function: the function
        /// call itself shouldn't throw an exception. However, using
        /// the result (by calling GetEnumerator() then MoveNext() on it) *should*
        /// throw InvalidOperationException.
        /// </summary>
        public static void AssertDeferred<T>(
            Func<IEnumerable<int>, IEnumerable<T>> deferredFunction)
        {
            ThrowingEnumerable source = new ThrowingEnumerable();
            var result = deferredFunction(source);
            using (var iterator = result.GetEnumerator())
            {
                Helper.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }
    }

    static class HashSetProvider
    {
        /// <summary>
        /// Allows a HashSet to be created in test classes, even though the Edulinq
        /// configuration of the tests project doesn't have a reference to System.Core.
        /// Basically it's a grotty hack.
        /// </summary>
        public static ICollection<T> NewHashSet<T>(IEqualityComparer<T> comparer, params T[] items)
        {
            return new HashSet<T>(items, comparer);
        }
    }

    /// <summary>
    /// Class which implements ICollection[T] but not ICollection.
    /// </summary>
    class GenericOnlyCollection<T> : ICollection<T>
    {
        private readonly List<T> backingList;

        public GenericOnlyCollection(IEnumerable<T> items)
        {
            backingList = new List<T>(items);
        }

        public void Add(T item)
        {
            backingList.Add(item);
        }

        public void Clear()
        {
            backingList.Clear();
        }

        public bool Contains(T item)
        {
            return backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            backingList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return backingList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>)backingList).IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return backingList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return backingList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// This is a distinctly odd collection - it implements the nongeneric ICollection interface, but the
    /// generic IEnumerable interface. Handy for testing all combinations though.
    /// </summary>
    class SemiGenericCollection : System.Collections.ICollection, IEnumerable<int>
    {
        private readonly List<int> list;

        public SemiGenericCollection()
        {
            list = new List<int>();
        }

        public SemiGenericCollection(IEnumerable<int> items)
        {
            list = new List<int>(items);
        }

        public void Add(int item)
        {
            list.Add(item);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((System.Collections.ICollection)list).CopyTo(array, index);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }
    }

    /// <summary>
    /// Collection class backed by a List[T] which delegates everything to the list
    /// *except* iteration - GetEnumerator() throwsNotSupportedException.
    /// </summary>
    class NonEnumerableList<T> : IList<T>
    {
        private readonly List<T> backingList;

        public NonEnumerableList(params T[] items) : this((IEnumerable<T>)items)
        {
        }

        public NonEnumerableList(IEnumerable<T> items)
        {
            backingList = new List<T>(items);
        }

        public int IndexOf(T item)
        {
            return backingList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            backingList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            backingList.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return backingList[index];
            }
            set
            {
                backingList[index] = value;
            }
        }

        public void Add(T item)
        {
            backingList.Add(item);
        }

        public void Clear()
        {
            backingList.Clear();
        }

        public bool Contains(T item)
        {
            return backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            backingList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return backingList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>)backingList).IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return backingList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Testing against LinqBridge, we can't use StringEx.Join(string, IEnumerable[T]) because it's
    /// in .NET 4. This is as simple an equivalent as we can easily achieve.
    /// </summary>
    static class StringEx
    {
        public static string Join<T>(string delimiter, IEnumerable<T> source)
        {
            return string.Join(delimiter, source.Select(x => x.ToString()).ToArray());
        }
    }
}
