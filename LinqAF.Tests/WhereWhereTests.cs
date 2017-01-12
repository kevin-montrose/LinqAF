using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class WhereWhereTests
    {
        [TestMethod]
        public void Single()
        {
            var asWhere = new[] { 1, 2, 3 }.Where(x => x % 2 == 1);
            var asWhereWhere = asWhere.Where(y => y % 3 == 0);

            Assert.IsTrue(asWhereWhere.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asWhereWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(3, res[0]);
        }

        [TestMethod]
        public void Double()
        {
            var asWhere = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Where(x => x % 2 == 1);
            var asWhereWhere = asWhere.Where(y => y % 3 == 0).Where(z => z % 5 == 4);

            Assert.IsTrue(asWhereWhere.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asWhereWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(9, res[0]);
        }

        [TestMethod]
        public void Triple()
        {
            var asWhere = Enumerable.Range(0, 100).Where(x => x % 2 == 1);
            var asWhereWhere = asWhere.Where(y => y % 3 == 0).Where(z => z % 5 == 4).Where(a => a + 2 > 10);

            Assert.IsTrue(asWhereWhere.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asWhereWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(9, res[0]);
            Assert.AreEqual(39, res[1]);
            Assert.AreEqual(69, res[2]);
            Assert.AreEqual(99, res[3]);
        }

        [TestMethod]
        public void ChainedSelect()
        {
            var asWhere = new[] { 1, 2, 3 }.Where(x => x % 2 == 1);
            var asWhereWhere = asWhere.Where(y => y % 3 == 0);
            var asWhereSelect = asWhereWhere.Select(z => z.ToString());

            Assert.IsTrue(asWhereSelect.GetType().IsValueType);

            var res = new List<string>();
            foreach(var item in asWhereSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("3", res[0]);
        }

        [TestMethod]
        public void Errors()
        {
            var foo = new int[0].Where(x => true);

            // simple
            {
                try
                {
                    foo.Where(default(Func<int, bool>));
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            // where
            {
                var foo = new WhereEnumerable<int, EmptyEnumerable<int>, EmptyEnumerator<int>>();

                // simple
                {
                    try
                    {
                        foo.Where(default(Func<int, bool>));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("source", exc.ParamName);
                    }
                }
            }
        }
    }
}
