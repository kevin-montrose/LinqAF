using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void Simple()
        {
            var asRange = Enumerable.Range(5, 5);

            Assert.IsTrue(asRange.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asRange)
            {
                ret.Add(item);
            }

            Assert.AreEqual(5, ret.Count);
            Assert.AreEqual(5, ret[0]);
            Assert.AreEqual(6, ret[1]);
            Assert.AreEqual(7, ret[2]);
            Assert.AreEqual(8, ret[3]);
            Assert.AreEqual(9, ret[4]);
        }

        [TestMethod]
        public void OptimizationSkipLast()
        {
            // basic
            {
                var shouldBe = new[] { 2, 3, 4 }.SkipLast(2);
                var actuallyIs = Enumerable.Range(2, 3).SkipLast(2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // negative
            {
                var shouldBe = new[] { 2, 3, 4 }.SkipLast(-2);
                var actuallyIs = Enumerable.Range(2, 3).SkipLast(-2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // bigger
            {
                var shouldBe = new[] { 2, 3, 4 }.SkipLast(4);
                var actuallyIs = Enumerable.Range(2, 3).SkipLast(4);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // zero
            {
                var shouldBe = new[] { 2, 3, 4 }.SkipLast(0);
                var actuallyIs = Enumerable.Range(2, 3).SkipLast(0);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationSkipLast_Reverse()
        {
            // basic
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().SkipLast(2);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().SkipLast(2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // negative
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().SkipLast(-2);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().SkipLast(-2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // bigger
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().SkipLast(4);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().SkipLast(4);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // zero
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().SkipLast(0);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().SkipLast(0);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationTake()
        {
            // basic
            {
                var shouldBe = new[] { 2, 3, 4 }.Take(2);
                var actuallyIs = Enumerable.Range(2, 3).Take(2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // negative
            {
                var shouldBe = new[] { 2, 3, 4 }.Take(-2);
                var actuallyIs = Enumerable.Range(2, 3).Take(-2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // bigger
            {
                var shouldBe = new[] { 2, 3, 4 }.Take(4);
                var actuallyIs = Enumerable.Range(2, 3).Take(4);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // zero
            {
                var shouldBe = new[] { 2, 3, 4 }.Take(0);
                var actuallyIs = Enumerable.Range(2, 3).Take(0);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationTake_Reverse()
        {
            // basic
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().Take(2);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().Take(2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // negative
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().Take(-2);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().Take(-2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // bigger
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().Take(4);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().Take(4);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // zero
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().Take(0);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().Take(0);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationTakeLast()
        {
            // basic
            {
                var shouldBe = new[] { 2, 3, 4 }.TakeLast(2);
                var actuallyIs = Enumerable.Range(2, 3).TakeLast(2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // negative
            {
                var shouldBe = new[] { 2, 3, 4 }.TakeLast(-2);
                var actuallyIs = Enumerable.Range(2, 3).TakeLast(-2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // bigger
            {
                var shouldBe = new[] { 2, 3, 4 }.TakeLast(4);
                var actuallyIs = Enumerable.Range(2, 3).TakeLast(4);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // zero
            {
                var shouldBe = new[] { 2, 3, 4 }.TakeLast(0);
                var actuallyIs = Enumerable.Range(2, 3).TakeLast(0);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationTakeLast_Reverse()
        {
            // basic
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().TakeLast(2);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().TakeLast(2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // negative
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().TakeLast(-2);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().TakeLast(-2);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // bigger
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().TakeLast(4);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().TakeLast(4);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // zero
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().TakeLast(0);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().TakeLast(0);

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationToArray()
        {
            // empty
            {
                var shouldBe = new int[0].ToArray();
                var actuallyIs = Enumerable.Range(1, 0).ToArray();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // one
            {
                var shouldBe = new[] { 1 }.ToArray();
                var actuallyIs = Enumerable.Range(1, 1).ToArray();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // many
            {
                var shouldBe = new[] { 2, 3, 4, 5 }.ToArray();
                var actuallyIs = Enumerable.Range(2, 4).ToArray();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationToArray_Reverse()
        {
            // empty
            {
                var shouldBe = new int[0].Reverse().ToArray();
                var actuallyIs = Enumerable.Range(1, 0).Reverse().ToArray();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // one
            {
                var shouldBe = new[] { 1 }.Reverse().ToArray();
                var actuallyIs = Enumerable.Range(1, 1).Reverse().ToArray();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // many
            {
                var shouldBe = new[] { 2, 3, 4, 5 }.Reverse().ToArray();
                var actuallyIs = Enumerable.Range(2, 4).Reverse().ToArray();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationToList()
        {
            // empty
            {
                var shouldBe = new int[0].ToList();
                var actuallyIs = Enumerable.Range(1, 0).ToList();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // one
            {
                var shouldBe = new[] { 1 }.ToList();
                var actuallyIs = Enumerable.Range(1, 1).ToList();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // many
            {
                var shouldBe = new[] { 2, 3, 4, 5 }.ToList();
                var actuallyIs = Enumerable.Range(2, 4).ToList();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationToList_Reverse()
        {
            // empty
            {
                var shouldBe = new int[0].Reverse().ToList();
                var actuallyIs = Enumerable.Range(1, 0).Reverse().ToList();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // one
            {
                var shouldBe = new[] { 1 }.Reverse().ToList();
                var actuallyIs = Enumerable.Range(1, 1).Reverse().ToList();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }

            // many
            {
                var shouldBe = new[] { 2, 3, 4, 5 }.Reverse().ToList();
                var actuallyIs = Enumerable.Range(2, 4).Reverse().ToList();

                Assert.IsTrue(shouldBe.SequenceEqual(actuallyIs));
            }
        }

        [TestMethod]
        public void OptimizationContains()
        {
            // empty
            {
                var shouldBe = new int[0].Contains(0);
                var actuallyIs = Enumerable.Range(0, 0).Contains(0);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // first
            {
                var shouldBe = new[] { 1, 2, 3 }.Contains(1);
                var actuallyIs = Enumerable.Range(1, 3).Contains(1);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // last
            {
                var shouldBe = new[] { 1, 2, 3 }.Contains(3);
                var actuallyIs = Enumerable.Range(1, 3).Contains(3);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // before
            {
                var shouldBe = new[] { 2, 3, 4 }.Contains(1);
                var actuallyIs = Enumerable.Range(2, 3).Contains(1);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // after
            {
                var shouldBe = new[] { 2, 3, 4 }.Contains(5);
                var actuallyIs = Enumerable.Range(2, 3).Contains(5);
                Assert.AreEqual(shouldBe, actuallyIs);
            }
        }

        [TestMethod]
        public void OptimizationContains_Reverse()
        {
            // empty
            {
                var shouldBe = new int[0].Reverse().Contains(0);
                var actuallyIs = Enumerable.Range(0, 0).Reverse().Contains(0);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // first
            {
                var shouldBe = new[] { 1, 2, 3 }.Reverse().Contains(1);
                var actuallyIs = Enumerable.Range(1, 3).Reverse().Contains(1);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // last
            {
                var shouldBe = new[] { 1, 2, 3 }.Reverse().Contains(3);
                var actuallyIs = Enumerable.Range(1, 3).Reverse().Contains(3);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // before
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().Contains(1);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().Contains(1);
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // after
            {
                var shouldBe = new[] { 2, 3, 4 }.Reverse().Contains(5);
                var actuallyIs = Enumerable.Range(2, 3).Reverse().Contains(5);
                Assert.AreEqual(shouldBe, actuallyIs);
            }
        }

        [TestMethod]
        public void OptimizationMax()
        {
            var shouldBe = new[] { 1, 2, 3 }.Max();
            var actuallyIs = Enumerable.Range(1, 3).Max();
            Assert.AreEqual(shouldBe, actuallyIs);
        }

        [TestMethod]
        public void OptimizationMax_Reverse()
        {
            var shouldBe = new[] { 1, 2, 3 }.Reverse().Max();
            var actuallyIs = Enumerable.Range(1, 3).Reverse().Max();
            Assert.AreEqual(shouldBe, actuallyIs);
        }

        [TestMethod]
        public void OptimizationMin()
        {
            var shouldBe = new[] { 1, 2, 3 }.Min();
            var actuallyIs = Enumerable.Range(1, 3).Min();
            Assert.AreEqual(shouldBe, actuallyIs);
        }

        [TestMethod]
        public void OptimizationMin_Reverse()
        {
            var shouldBe = new[] { 1, 2, 3 }.Reverse().Min();
            var actuallyIs = Enumerable.Range(1, 3).Reverse().Min();
            Assert.AreEqual(shouldBe, actuallyIs);
        }

        [TestMethod]
        public void OptimizationSum()
        {
            // odd
            {
                var shouldBe = new[] { 1, 2, 3 }.Sum();
                var actuallyIs = Enumerable.Range(1, 3).Sum();
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // even
            {
                var shouldBe = new[] { 3, 4, 5, 6, 7, 8 }.Sum();
                var actuallyIs = Enumerable.Range(3, 6).Sum();
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // empty
            {
                var shouldBe = new int[0].Sum();
                var actuallyIs = Enumerable.Range(1, 0).Sum();
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // overflow
            {
                try
                {
                    var actuallyIs = Enumerable.Range(int.MaxValue - 20, 10).Sum();
                    Assert.Fail("Should have thrown");
                }
                catch (OverflowException) { }
            }

            // underflow
            {
                try
                {
                    var actuallyIs = Enumerable.Range(int.MinValue, 2).Sum();
                    Assert.Fail("Should have thrown");
                }
                catch (OverflowException) { }
            }
        }

        [TestMethod]
        public void OptimizationSum_Reverse()
        {
            // odd
            {
                var shouldBe = new[] { 1, 2, 3 }.Reverse().Sum();
                var actuallyIs = Enumerable.Range(1, 3).Reverse().Sum();
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // even
            {
                var shouldBe = new[] { 3, 4, 5, 6, 7, 8 }.Reverse().Sum();
                var actuallyIs = Enumerable.Range(3, 6).Reverse().Sum();
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // empty
            {
                var shouldBe = new int[0].Reverse().Sum();
                var actuallyIs = Enumerable.Range(1, 0).Reverse().Sum();
                Assert.AreEqual(shouldBe, actuallyIs);
            }

            // overflow
            {
                try
                {
                    var actuallyIs = Enumerable.Range(int.MaxValue - 20, 10).Reverse().Sum();
                    Assert.Fail("Should have thrown");
                }
                catch (OverflowException) { }
            }

            // underflow
            {
                try
                {
                    var actuallyIs = Enumerable.Range(int.MinValue, 2).Reverse().Sum();
                    Assert.Fail("Should have thrown");
                }
                catch (OverflowException) { }
            }
        }
    }
}
