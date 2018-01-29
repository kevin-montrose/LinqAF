using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ConvenienceTests
    {
        [TestMethod]
        public void CheckForMissing()
        {
            var unique =
                new[]
                {
                    typeof(System.Collections.Generic.Dictionary<,>),
                    typeof(System.Collections.Generic.Dictionary<,>.KeyCollection),
                    typeof(System.Collections.Generic.Dictionary<,>.ValueCollection),
                    typeof(System.Collections.Generic.HashSet<>),
                    typeof(System.Collections.Generic.LinkedList<>),
                    typeof(System.Collections.Generic.List<>),
                    typeof(System.Collections.Generic.Queue<>),
                    typeof(System.Collections.Generic.SortedDictionary<,>),
                    typeof(System.Collections.Generic.SortedDictionary<,>.KeyCollection),
                    typeof(System.Collections.Generic.SortedDictionary<,>.ValueCollection),
                    typeof(System.Collections.Generic.SortedSet<>),
                    typeof(System.Collections.Generic.Stack<>)
                }
                .SelectMany(x => x.Assembly.GetExportedTypes().Where(t => (t.Namespace ?? "").StartsWith("System.Collections.Generic")).ToArray())
                .Distinct()
                .ToArray();

            var convenienceMethods = typeof(ConvenienceExtensionMethods).GetMethods(BindingFlags.Public | BindingFlags.Static);

            var missing = new System.Collections.Generic.HashSet<string>();

            foreach (var type in unique)
            {
                var publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m => !m.IsConstructor).ToArray();
                var takingIEnumerable =
                    publicMethods
                        .Where(
                            m =>
                                m.GetParameters()
                                  .Select(p => p.ParameterType)
                                  .Any(p => ((p.IsGenericType && !p.IsGenericTypeDefinition) ? p.GetGenericTypeDefinition() : p) == typeof(System.Collections.Generic.IEnumerable<>))
                        )
                        .ToArray();

                foreach(var shouldHaveConvenienceMtd in takingIEnumerable)
                {
                    var originalMethodPs = shouldHaveConvenienceMtd.GetParameters();

                    var convenienceWithName = convenienceMethods.Where(m => m.Name == shouldHaveConvenienceMtd.Name).ToArray();
                    var convenienceWithNameAndFirstType = convenienceWithName.Where(m => AreEquivalent(m.GetParameters()[0].ParameterType, type)).ToArray();
                    var convenienceWithNameAndFirstTypeAndCorrectParamLength = convenienceWithNameAndFirstType.Where(m => m.GetParameters().Length == originalMethodPs.Length + 1).ToArray();
                    var convenienceWithNameAndFirstTypeAndParams =
                        convenienceWithNameAndFirstTypeAndCorrectParamLength
                            .Where(
                                m =>
                                {
                                    var convenienceParams = m.GetParameters();

                                    for(var i = 0; i < originalMethodPs.Length; i++)
                                    {
                                        var convenienceParam = convenienceParams[i + 1];
                                        var originalParam = originalMethodPs[i];

                                        if (!AreEquivalent(convenienceParam.ParameterType, originalParam.ParameterType)) return false;
                                    }

                                    return true;
                                }
                            )
                            .ToArray();

                    if (!convenienceWithNameAndFirstTypeAndParams.Any())
                    {
                        missing.Add(type.Name + "." + shouldHaveConvenienceMtd.Name);
                    }
                }
            }

            Assert.AreEqual(0, missing.Count, string.Join(", ", missing));
        }

        [TestMethod]
        public void MatchExpectations()
        {
            var pubStatic = typeof(ConvenienceExtensionMethods).GetMethods(BindingFlags.Public | BindingFlags.Static);

            foreach(var mtd in pubStatic)
            {
                Assert.IsNotNull(mtd.GetCustomAttribute<ExtensionAttribute>(), $"Convenience method {mtd.Name} isn't an extension method");

                var mtdParams = mtd.GetParameters();

                var thisType = mtdParams[0].ParameterType;

                var mtdOnThisType = thisType.GetMethod(mtd.Name);
                Assert.IsNotNull(mtdOnThisType, $"Convenience method {mtd.Name} not found on real type {thisType.Name}");

                var thisTypeParams = mtdOnThisType.GetParameters();

                Assert.AreEqual(thisTypeParams.Length, mtdParams.Length - 1, $"Incorrect number of parameters for {mtd.Name}");

                for(var i = 0; i < thisTypeParams.Length; i++)
                {
                    var thisTypeParam = thisTypeParams[i];
                    var mtdParam = mtdParams[i + 1];

                    Assert.IsTrue(AreEquivalent(thisTypeParam.ParameterType, mtdParam.ParameterType), "Parameters not equivalent");
                }
            }
        }

        static bool AreEquivalent(Type a, Type b)
        {
            if (a == b) return true;
            if (a.IsAssignableFrom(b) && b.IsAssignableFrom(a)) return true;
            if (a.IsEquivalentTo(b) && b.IsEquivalentTo(a)) return true;
            if (a.IsGenericParameter && b.IsGenericParameter) return true;

            var aGen = a;
            var bGen = b;

            if (aGen.IsGenericType && !aGen.IsGenericTypeDefinition) aGen = aGen.GetGenericTypeDefinition();
            if (bGen.IsGenericType && !bGen.IsGenericTypeDefinition) bGen = bGen.GetGenericTypeDefinition();

            var aIsEnumerable = aGen == typeof(System.Collections.Generic.IEnumerable<>) || aGen.GetInterface("IStructEnumerable`2") != null;
            var bIsEnumerable = bGen == typeof(System.Collections.Generic.IEnumerable<>) || bGen.GetInterface("IStructEnumerable`2") != null;

            if (aIsEnumerable && bIsEnumerable) return true;

            if (aGen != a || bGen != b) return AreEquivalent(aGen, bGen);

            return false;
        }

        #region HashSet
        [TestMethod]
        public void HashSet_ExceptWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "ExceptWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_ExceptWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    hashset1.ExceptWith(a);

                    Assert.AreEqual(3, hashset1.Count);
                    Assert.IsTrue(hashset1.Contains(4));
                    Assert.IsTrue(hashset1.Contains(5));
                    Assert.IsTrue(hashset1.Contains(6));

                    var hashset2 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    hashset2.ExceptWith(new [] { 1, 2, 3 });

                    Assert.IsTrue(hashset1.SetEquals(hashset2));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset1.ExceptWith(Enumerable.Empty<int>());
                Assert.AreEqual(4, hashset1.Count);
                Assert.IsTrue(hashset1.Contains(3));
                Assert.IsTrue(hashset1.Contains(4));
                Assert.IsTrue(hashset1.Contains(5));
                Assert.IsTrue(hashset1.Contains(6));

                var hashset2 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset2.ExceptWith(System.Linq.Enumerable.Empty<int>());

                Assert.IsTrue(hashset1.SetEquals(hashset2));
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset1.ExceptWith(Enumerable.Empty<int>().OrderBy(x => x));
                Assert.AreEqual(4, hashset1.Count);
                Assert.IsTrue(hashset1.Contains(3));
                Assert.IsTrue(hashset1.Contains(4));
                Assert.IsTrue(hashset1.Contains(5));
                Assert.IsTrue(hashset1.Contains(6));

                var hashset2 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset2.ExceptWith(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), x => x));

                Assert.IsTrue(hashset1.SetEquals(hashset2));
            }
        }

        [TestMethod]
        public void HashSet_IntersectWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "IntersectWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_IntersectWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    hashset1.IntersectWith(a);

                    Assert.AreEqual(1, hashset1.Count);
                    Assert.IsTrue(hashset1.Contains(3));
                    

                    var hashset2 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    hashset2.IntersectWith(new [] { 1, 2, 3 });

                    Assert.IsTrue(hashset1.SetEquals(hashset2));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset1.IntersectWith(Enumerable.Empty<int>());
                Assert.AreEqual(0, hashset1.Count);

                var hashset2 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset2.IntersectWith(System.Linq.Enumerable.Empty<int>());

                Assert.IsTrue(hashset1.SetEquals(hashset2));
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset1.IntersectWith(Enumerable.Empty<int>().OrderBy(x => x));
                Assert.AreEqual(0, hashset1.Count);

                var hashset2 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                hashset2.IntersectWith(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), x => x));

                Assert.IsTrue(hashset1.SetEquals(hashset2));
            }
        }

        [TestMethod]
        public void HashSet_IsProperSubsetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "IsProperSubsetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_IsProperSubsetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = hashset1.IsProperSubsetOf(a);
                    var y = hashset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = hashset1.IsProperSubsetOf(a);
                    var y = hashset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>();
                    var x = hashset1.IsProperSubsetOf(a);
                    var y = hashset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsProperSubsetOf(Enumerable.Empty<int>());
                var y = hashset1.IsProperSubsetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsProperSubsetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = hashset1.IsProperSubsetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void HashSet_IsProperSupersetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "IsProperSupersetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_IsProperSupersetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = hashset1.IsProperSupersetOf(a);
                    var y = hashset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = hashset1.IsProperSupersetOf(a);
                    var y = hashset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>();
                    var x = hashset1.IsProperSupersetOf(a);
                    var y = hashset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsProperSupersetOf(Enumerable.Empty<int>());
                var y = hashset1.IsProperSupersetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsProperSupersetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = hashset1.IsProperSupersetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void HashSet_IsSubsetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "IsSubsetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_IsSubsetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = hashset1.IsSubsetOf(a);
                    var y = hashset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = hashset1.IsSubsetOf(a);
                    var y = hashset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>();
                    var x = hashset1.IsSubsetOf(a);
                    var y = hashset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsSubsetOf(Enumerable.Empty<int>());
                var y = hashset1.IsSubsetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsSubsetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = hashset1.IsSubsetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void HashSet_IsSupersetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "IsSupersetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_IsSupersetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = hashset1.IsSupersetOf(a);
                    var y = hashset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = hashset1.IsSupersetOf(a);
                    var y = hashset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>();
                    var x = hashset1.IsSupersetOf(a);
                    var y = hashset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsSupersetOf(Enumerable.Empty<int>());
                var y = hashset1.IsSupersetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = hashset1.IsSupersetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = hashset1.IsSupersetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void HashSet_Overlaps_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "Overlaps")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_Overlaps()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x1 = hashset1.Overlaps(a);
                    var y1 = hashset1.Overlaps(new [] { 1, 2, 3 });

                    Assert.AreEqual(y1, x1);

                    var hashset2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                    var x2 = hashset2.Overlaps(a);
                    var y2 = hashset2.Overlaps(new [] { 1, 2, 3 });

                    Assert.AreEqual(y2, x2);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
            
            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x1 = hashset1.Overlaps(Enumerable.Empty<int>());
                var y1 = hashset1.Overlaps(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y1, x1);

                var hashset2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                var x2 = hashset2.Overlaps(Enumerable.Empty<int>());
                var y2 = hashset2.Overlaps(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y2, x2);
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x1 = hashset1.Overlaps(Enumerable.Empty<int>().OrderBy(z => z));
                var y1 = hashset1.Overlaps(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y1, x1);

                var hashset2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                var x2 = hashset2.Overlaps(Enumerable.Empty<int>().OrderBy(z => z));
                var y2 = hashset2.Overlaps(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y2, x2);
            }
        }

        [TestMethod]
        public void HashSet_SetEquals_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "SetEquals")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_SetEquals()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 2, 2, 1, 3 },
                @"a =>
                  {
                    var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                    var x1 = hashset1.SetEquals(a);
                    var y1 = hashset1.SetEquals(new [] { 1, 2, 3 });

                    Assert.AreEqual(y1, x1);

                    var hashset2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                    var x2 = hashset2.SetEquals(a);
                    var y2 = hashset2.SetEquals(new [] { 1, 2, 3 });

                    Assert.AreEqual(y2, x2);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                var x1 = hashset1.SetEquals(Enumerable.Empty<int>());
                var y1 = hashset1.SetEquals(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y1, x1);

                var hashset2 = new System.Collections.Generic.HashSet<int>();
                var x2 = hashset2.SetEquals(Enumerable.Empty<int>());
                var y2 = hashset2.SetEquals(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y2, x2);
            }

            // emptyOrdered
            {
                var hashset1 = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                var x1 = hashset1.SetEquals(Enumerable.Empty<int>().OrderBy(z => z));
                var y1 = hashset1.SetEquals(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y1, x1);

                var hashset2 = new System.Collections.Generic.HashSet<int>();
                var x2 = hashset2.SetEquals(Enumerable.Empty<int>().OrderBy(z => z));
                var y2 = hashset2.SetEquals(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y2, x2);
            }
        }

        [TestMethod]
        public void HashSet_SymmetricExceptWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "SymmetricExceptWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_SymmetricExceptWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var hashset = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                    hashset.SymmetricExceptWith(a);

                    Assert.AreEqual(3, hashset.Count);
                    Assert.IsTrue(hashset.Contains(2));
                    Assert.IsTrue(hashset.Contains(1));
                    Assert.IsTrue(hashset.Contains(5));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset = new System.Collections.Generic.HashSet<int>() {3, 2, 1 };
                hashset.SymmetricExceptWith(Enumerable.Empty<int>());

                Assert.AreEqual(3, hashset.Count);
                Assert.IsTrue(hashset.Contains(3));
                Assert.IsTrue(hashset.Contains(2));
                Assert.IsTrue(hashset.Contains(1));
            }

            // emptyOrdered
            {
                var hashset = new System.Collections.Generic.HashSet<int>() { 3, 2, 1 };
                hashset.SymmetricExceptWith(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, hashset.Count);
                Assert.IsTrue(hashset.Contains(3));
                Assert.IsTrue(hashset.Contains(2));
                Assert.IsTrue(hashset.Contains(1));
            }
        }

        [TestMethod]
        public void HashSet_UnionWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.HashSet<>))
                .Where(p => p.Name == "UnionWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void HashSet_UnionWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var hashset = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                    hashset.UnionWith(a);

                    Assert.AreEqual(4, hashset.Count);
                    Assert.IsTrue(hashset.Contains(1));
                    Assert.IsTrue(hashset.Contains(2));
                    Assert.IsTrue(hashset.Contains(3));
                    Assert.IsTrue(hashset.Contains(5));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var hashset = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                hashset.UnionWith(Enumerable.Empty<int>());

                Assert.AreEqual(3, hashset.Count);
                Assert.IsTrue(hashset.Contains(1));
                Assert.IsTrue(hashset.Contains(2));
                Assert.IsTrue(hashset.Contains(3));
            }

            // emptyOrdered
            {
                var hashset = new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                hashset.UnionWith(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, hashset.Count);
                Assert.IsTrue(hashset.Contains(1));
                Assert.IsTrue(hashset.Contains(2));
                Assert.IsTrue(hashset.Contains(3));
            }
        }
        #endregion

        #region List
        [TestMethod]
        public void List_AddRange_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>))
                .Where(p => p.Name == "AddRange")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void List_AddRange()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var list = new System.Collections.Generic.List<int>() { 1, 2, 4 };
                    list.AddRange(a);

                    Assert.AreEqual(5, list.Count);
                    Assert.AreEqual(1, list[0]);
                    Assert.AreEqual(2, list[1]);
                    Assert.AreEqual(4, list[2]);
                    Assert.AreEqual(3, list[3]);
                    Assert.AreEqual(5, list[4]);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var list = new System.Collections.Generic.List<int>() { 1, 2, 4 };
                list.AddRange(Enumerable.Empty<int>());

                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(1, list[0]);
                Assert.AreEqual(2, list[1]);
                Assert.AreEqual(4, list[2]);
            }

            // emptyOrdered
            {
                var list = new System.Collections.Generic.List<int>() { 1, 2, 4 };
                list.AddRange(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(1, list[0]);
                Assert.AreEqual(2, list[1]);
                Assert.AreEqual(4, list[2]);
            }
        }

        [TestMethod]
        public void List_InsertRange_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>))
                .Where(p => p.Name == "InsertRange")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType == typeof(int) && m.GetParameters()[2].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void List_InsertRange()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var list = new System.Collections.Generic.List<int>() { 1, 2, 4 };
                    list.InsertRange(1, a);

                    Assert.AreEqual(5, list.Count);
                    Assert.AreEqual(1, list[0]);
                    Assert.AreEqual(3, list[1]);
                    Assert.AreEqual(5, list[2]);
                    Assert.AreEqual(2, list[3]);
                    Assert.AreEqual(4, list[4]);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var list = new System.Collections.Generic.List<int>() { 1, 2, 4 };
                list.InsertRange(1, Enumerable.Empty<int>());

                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(1, list[0]);
                Assert.AreEqual(2, list[1]);
                Assert.AreEqual(4, list[2]);
            }

            // emptyOrdered
            {
                var list = new System.Collections.Generic.List<int>() { 1, 2, 4 };
                list.InsertRange(1, Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(1, list[0]);
                Assert.AreEqual(2, list[1]);
                Assert.AreEqual(4, list[2]);
            }
        }
        #endregion

        #region ISet
        public void ISet_ExceptWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "ExceptWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_ExceptWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    iset1.ExceptWith(a);

                    Assert.AreEqual(3, iset1.Count);
                    Assert.IsTrue(iset1.Contains(4));
                    Assert.IsTrue(iset1.Contains(5));
                    Assert.IsTrue(iset1.Contains(6));

                    var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int> { 3, 4, 5, 6 };
                    iset2.ExceptWith(new [] { 1, 2, 3 });

                    Assert.IsTrue(iset1.SetEquals(iset2));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset1.ExceptWith(Enumerable.Empty<int>());
                Assert.AreEqual(4, iset1.Count);
                Assert.IsTrue(iset1.Contains(3));
                Assert.IsTrue(iset1.Contains(4));
                Assert.IsTrue(iset1.Contains(5));
                Assert.IsTrue(iset1.Contains(6));

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset2.ExceptWith(System.Linq.Enumerable.Empty<int>());

                Assert.IsTrue(iset1.SetEquals(iset2));
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int> { 3, 4, 5, 6 };
                iset1.ExceptWith(Enumerable.Empty<int>().OrderBy(x => x));
                Assert.AreEqual(4, iset1.Count);
                Assert.IsTrue(iset1.Contains(3));
                Assert.IsTrue(iset1.Contains(4));
                Assert.IsTrue(iset1.Contains(5));
                Assert.IsTrue(iset1.Contains(6));

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset2.ExceptWith(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), x => x));

                Assert.IsTrue(iset1.SetEquals(iset2));
            }
        }

        [TestMethod]
        public void ISet_IntersectWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "IntersectWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_IntersectWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    iset1.IntersectWith(a);

                    Assert.AreEqual(1, iset1.Count);
                    Assert.IsTrue(iset1.Contains(3));
                    

                    var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    iset2.IntersectWith(new [] { 1, 2, 3 });

                    Assert.IsTrue(iset1.SetEquals(iset2));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset1.IntersectWith(Enumerable.Empty<int>());
                Assert.AreEqual(0, iset1.Count);

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset2.IntersectWith(System.Linq.Enumerable.Empty<int>());

                Assert.IsTrue(iset1.SetEquals(iset2));
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset1.IntersectWith(Enumerable.Empty<int>().OrderBy(x => x));
                Assert.AreEqual(0, iset1.Count);

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                iset2.IntersectWith(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), x => x));

                Assert.IsTrue(iset1.SetEquals(iset2));
            }
        }

        [TestMethod]
        public void ISet_IsProperSubsetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "IsProperSubsetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_IsProperSubsetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = iset1.IsProperSubsetOf(a);
                    var y = iset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = iset1.IsProperSubsetOf(a);
                    var y = iset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>();
                    var x = iset1.IsProperSubsetOf(a);
                    var y = iset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsProperSubsetOf(Enumerable.Empty<int>());
                var y = iset1.IsProperSubsetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsProperSubsetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = iset1.IsProperSubsetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void ISet_IsProperSupersetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "IsProperSupersetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_IsProperSupersetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = iset1.IsProperSupersetOf(a);
                    var y = iset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = iset1.IsProperSupersetOf(a);
                    var y = iset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>();
                    var x = iset1.IsProperSupersetOf(a);
                    var y = iset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsProperSupersetOf(Enumerable.Empty<int>());
                var y = iset1.IsProperSupersetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsProperSupersetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = iset1.IsProperSupersetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void ISet_IsSubsetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "IsSubsetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_IsSubsetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = iset1.IsSubsetOf(a);
                    var y = iset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = iset1.IsSubsetOf(a);
                    var y = iset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>();
                    var x = iset1.IsSubsetOf(a);
                    var y = iset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsSubsetOf(Enumerable.Empty<int>());
                var y = iset1.IsSubsetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsSubsetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = iset1.IsSubsetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void ISet_IsSupersetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "IsSupersetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_IsSupersetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x = iset1.IsSupersetOf(a);
                    var y = iset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = iset1.IsSupersetOf(a);
                    var y = iset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>();
                    var x = iset1.IsSupersetOf(a);
                    var y = iset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsSupersetOf(Enumerable.Empty<int>());
                var y = iset1.IsSupersetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x = iset1.IsSupersetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = iset1.IsSupersetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void ISet_Overlaps_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "Overlaps")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_Overlaps()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                    var x1 = iset1.Overlaps(a);
                    var y1 = iset1.Overlaps(new [] { 1, 2, 3 });

                    Assert.AreEqual(y1, x1);

                    var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                    var x2 = iset2.Overlaps(a);
                    var y2 = iset2.Overlaps(new [] { 1, 2, 3 });

                    Assert.AreEqual(y2, x2);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x1 = iset1.Overlaps(Enumerable.Empty<int>());
                var y1 = iset1.Overlaps(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y1, x1);

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                var x2 = iset2.Overlaps(Enumerable.Empty<int>());
                var y2 = iset2.Overlaps(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y2, x2);
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 4, 5, 6 };
                var x1 = iset1.Overlaps(Enumerable.Empty<int>().OrderBy(z => z));
                var y1 = iset1.Overlaps(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y1, x1);

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                var x2 = iset2.Overlaps(Enumerable.Empty<int>().OrderBy(z => z));
                var y2 = iset2.Overlaps(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y2, x2);
            }
        }

        [TestMethod]
        public void ISet_SetEquals_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "SetEquals")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_SetEquals()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 2, 2, 1, 3 },
                @"a =>
                  {
                    var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                    var x1 = iset1.SetEquals(a);
                    var y1 = iset1.SetEquals(new [] { 1, 2, 3 });

                    Assert.AreEqual(y1, x1);

                    var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 4, 5, 6 };
                    var x2 = iset2.SetEquals(a);
                    var y2 = iset2.SetEquals(new [] { 1, 2, 3 });

                    Assert.AreEqual(y2, x2);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                var x1 = iset1.SetEquals(Enumerable.Empty<int>());
                var y1 = iset1.SetEquals(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y1, x1);

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>();
                var x2 = iset2.SetEquals(Enumerable.Empty<int>());
                var y2 = iset2.SetEquals(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y2, x2);
            }

            // emptyOrdered
            {
                var iset1 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                var x1 = iset1.SetEquals(Enumerable.Empty<int>().OrderBy(z => z));
                var y1 = iset1.SetEquals(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y1, x1);

                var iset2 = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>();
                var x2 = iset2.SetEquals(Enumerable.Empty<int>().OrderBy(z => z));
                var y2 = iset2.SetEquals(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y2, x2);
            }
        }

        [TestMethod]
        public void ISet_SymmetricExceptWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "SymmetricExceptWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_SymmetricExceptWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var iset = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                    iset.SymmetricExceptWith(a);

                    Assert.AreEqual(3, iset.Count);
                    Assert.IsTrue(iset.Contains(2));
                    Assert.IsTrue(iset.Contains(1));
                    Assert.IsTrue(iset.Contains(5));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 1 };
                iset.SymmetricExceptWith(Enumerable.Empty<int>());

                Assert.AreEqual(3, iset.Count);
                Assert.IsTrue(iset.Contains(3));
                Assert.IsTrue(iset.Contains(2));
                Assert.IsTrue(iset.Contains(1));
            }

            // emptyOrdered
            {
                var iset = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 1 };
                iset.SymmetricExceptWith(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, iset.Count);
                Assert.IsTrue(iset.Contains(3));
                Assert.IsTrue(iset.Contains(2));
                Assert.IsTrue(iset.Contains(1));
            }
        }

        [TestMethod]
        public void ISet_UnionWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ISet<>))
                .Where(p => p.Name == "UnionWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void ISet_UnionWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var iset = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                    iset.UnionWith(a);

                    Assert.AreEqual(4, iset.Count);
                    Assert.IsTrue(iset.Contains(1));
                    Assert.IsTrue(iset.Contains(2));
                    Assert.IsTrue(iset.Contains(3));
                    Assert.IsTrue(iset.Contains(5));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var iset = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                iset.UnionWith(Enumerable.Empty<int>());

                Assert.AreEqual(3, iset.Count);
                Assert.IsTrue(iset.Contains(1));
                Assert.IsTrue(iset.Contains(2));
                Assert.IsTrue(iset.Contains(3));
            }

            // emptyOrdered
            {
                var iset = (System.Collections.Generic.ISet<int>)new System.Collections.Generic.HashSet<int>() { 3, 2, 2, 1, 3 };
                iset.UnionWith(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, iset.Count);
                Assert.IsTrue(iset.Contains(1));
                Assert.IsTrue(iset.Contains(2));
                Assert.IsTrue(iset.Contains(3));
            }
        }
        #endregion

        #region SortedSet
        [TestMethod]
        public void SortedSet_ExceptWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "ExceptWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_ExceptWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    sortedset1.ExceptWith(a);

                    Assert.AreEqual(3, sortedset1.Count);
                    Assert.IsTrue(sortedset1.Contains(4));
                    Assert.IsTrue(sortedset1.Contains(5));
                    Assert.IsTrue(sortedset1.Contains(6));

                    var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    sortedset2.ExceptWith(new [] { 1, 2, 3 });

                    Assert.IsTrue(sortedset1.SetEquals(sortedset2));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset1.ExceptWith(Enumerable.Empty<int>());
                Assert.AreEqual(4, sortedset1.Count);
                Assert.IsTrue(sortedset1.Contains(3));
                Assert.IsTrue(sortedset1.Contains(4));
                Assert.IsTrue(sortedset1.Contains(5));
                Assert.IsTrue(sortedset1.Contains(6));

                var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset2.ExceptWith(System.Linq.Enumerable.Empty<int>());

                Assert.IsTrue(sortedset1.SetEquals(sortedset2));
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset1.ExceptWith(Enumerable.Empty<int>().OrderBy(x => x));
                Assert.AreEqual(4, sortedset1.Count);
                Assert.IsTrue(sortedset1.Contains(3));
                Assert.IsTrue(sortedset1.Contains(4));
                Assert.IsTrue(sortedset1.Contains(5));
                Assert.IsTrue(sortedset1.Contains(6));

                var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset2.ExceptWith(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), x => x));

                Assert.IsTrue(sortedset1.SetEquals(sortedset2));
            }
        }

        [TestMethod]
        public void SortedSet_IntersectWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "IntersectWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_IntersectWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    sortedset1.IntersectWith(a);

                    Assert.AreEqual(1, sortedset1.Count);
                    Assert.IsTrue(sortedset1.Contains(3));
                    

                    var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    sortedset2.IntersectWith(new [] { 1, 2, 3 });

                    Assert.IsTrue(sortedset1.SetEquals(sortedset2));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset1.IntersectWith(Enumerable.Empty<int>());
                Assert.AreEqual(0, sortedset1.Count);

                var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset2.IntersectWith(System.Linq.Enumerable.Empty<int>());

                Assert.IsTrue(sortedset1.SetEquals(sortedset2));
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset1.IntersectWith(Enumerable.Empty<int>().OrderBy(x => x));
                Assert.AreEqual(0, sortedset1.Count);

                var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                sortedset2.IntersectWith(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), x => x));

                Assert.IsTrue(sortedset1.SetEquals(sortedset2));
            }
        }

        [TestMethod]
        public void SortedSet_IsProperSubsetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "IsProperSubsetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_IsProperSubsetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    var x = sortedset1.IsProperSubsetOf(a);
                    var y = sortedset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = sortedset1.IsProperSubsetOf(a);
                    var y = sortedset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>();
                    var x = sortedset1.IsProperSubsetOf(a);
                    var y = sortedset1.IsProperSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsProperSubsetOf(Enumerable.Empty<int>());
                var y = sortedset1.IsProperSubsetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsProperSubsetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = sortedset1.IsProperSubsetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void SortedSet_IsProperSupersetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "IsProperSupersetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_IsProperSupersetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    var x = sortedset1.IsProperSupersetOf(a);
                    var y = sortedset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = sortedset1.IsProperSupersetOf(a);
                    var y = sortedset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>();
                    var x = sortedset1.IsProperSupersetOf(a);
                    var y = sortedset1.IsProperSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsProperSupersetOf(Enumerable.Empty<int>());
                var y = sortedset1.IsProperSupersetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsProperSupersetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = sortedset1.IsProperSupersetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void SortedSet_IsSubsetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "IsSubsetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_IsSubsetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    var x = sortedset1.IsSubsetOf(a);
                    var y = sortedset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = sortedset1.IsSubsetOf(a);
                    var y = sortedset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>();
                    var x = sortedset1.IsSubsetOf(a);
                    var y = sortedset1.IsSubsetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsSubsetOf(Enumerable.Empty<int>());
                var y = sortedset1.IsSubsetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsSubsetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = sortedset1.IsSubsetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void SortedSet_IsSupersetOf_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "IsSupersetOf")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_IsSupersetOf()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    var x = sortedset1.IsSupersetOf(a);
                    var y = sortedset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 1, 2, 3, 4, 5, 6 };
                    var x = sortedset1.IsSupersetOf(a);
                    var y = sortedset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>();
                    var x = sortedset1.IsSupersetOf(a);
                    var y = sortedset1.IsSupersetOf(new [] { 1, 2, 3 });

                    Assert.AreEqual(y, x);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsSupersetOf(Enumerable.Empty<int>());
                var y = sortedset1.IsSupersetOf(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y, x);
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x = sortedset1.IsSupersetOf(Enumerable.Empty<int>().OrderBy(z => z));
                var y = sortedset1.IsSupersetOf(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y, x);
            }
        }

        [TestMethod]
        public void SortedSet_Overlaps_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "Overlaps")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_Overlaps()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                    var x1 = sortedset1.Overlaps(a);
                    var y1 = sortedset1.Overlaps(new [] { 1, 2, 3 });

                    Assert.AreEqual(y1, x1);

                    var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 4, 5, 6 };
                    var x2 = sortedset2.Overlaps(a);
                    var y2 = sortedset2.Overlaps(new [] { 1, 2, 3 });

                    Assert.AreEqual(y2, x2);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x1 = sortedset1.Overlaps(Enumerable.Empty<int>());
                var y1 = sortedset1.Overlaps(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y1, x1);

                var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 4, 5, 6 };
                var x2 = sortedset2.Overlaps(Enumerable.Empty<int>());
                var y2 = sortedset2.Overlaps(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y2, x2);
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 4, 5, 6 };
                var x1 = sortedset1.Overlaps(Enumerable.Empty<int>().OrderBy(z => z));
                var y1 = sortedset1.Overlaps(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y1, x1);

                var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 4, 5, 6 };
                var x2 = sortedset2.Overlaps(Enumerable.Empty<int>().OrderBy(z => z));
                var y2 = sortedset2.Overlaps(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y2, x2);
            }
        }

        [TestMethod]
        public void SortedSet_SetEquals_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "SetEquals")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_SetEquals()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 2, 2, 1, 3 },
                @"a =>
                  {
                    var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                    var x1 = sortedset1.SetEquals(a);
                    var y1 = sortedset1.SetEquals(new [] { 1, 2, 3 });

                    Assert.AreEqual(y1, x1);

                    var sortedset2 = new System.Collections.Generic.SortedSet<int>() { 4, 5, 6 };
                    var x2 = sortedset2.SetEquals(a);
                    var y2 = sortedset2.SetEquals(new [] { 1, 2, 3 });

                    Assert.AreEqual(y2, x2);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                var x1 = sortedset1.SetEquals(Enumerable.Empty<int>());
                var y1 = sortedset1.SetEquals(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y1, x1);

                var sortedset2 = new System.Collections.Generic.SortedSet<int>();
                var x2 = sortedset2.SetEquals(Enumerable.Empty<int>());
                var y2 = sortedset2.SetEquals(System.Linq.Enumerable.Empty<int>());

                Assert.AreEqual(y2, x2);
            }

            // emptyOrdered
            {
                var sortedset1 = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                var x1 = sortedset1.SetEquals(Enumerable.Empty<int>().OrderBy(z => z));
                var y1 = sortedset1.SetEquals(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y1, x1);

                var sortedset2 = new System.Collections.Generic.SortedSet<int>();
                var x2 = sortedset2.SetEquals(Enumerable.Empty<int>().OrderBy(z => z));
                var y2 = sortedset2.SetEquals(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.Empty<int>(), z => z));

                Assert.AreEqual(y2, x2);
            }
        }

        [TestMethod]
        public void SortedSet_SymmetricExceptWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "SymmetricExceptWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_SymmetricExceptWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var sortedset = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                    sortedset.SymmetricExceptWith(a);

                    Assert.AreEqual(3, sortedset.Count);
                    Assert.IsTrue(sortedset.Contains(2));
                    Assert.IsTrue(sortedset.Contains(1));
                    Assert.IsTrue(sortedset.Contains(5));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset = new System.Collections.Generic.SortedSet<int>() { 3, 2, 1 };
                sortedset.SymmetricExceptWith(Enumerable.Empty<int>());

                Assert.AreEqual(3, sortedset.Count);
                Assert.IsTrue(sortedset.Contains(3));
                Assert.IsTrue(sortedset.Contains(2));
                Assert.IsTrue(sortedset.Contains(1));
            }

            // emptyOrdered
            {
                var sortedset = new System.Collections.Generic.SortedSet<int>() { 3, 2, 1 };
                sortedset.SymmetricExceptWith(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, sortedset.Count);
                Assert.IsTrue(sortedset.Contains(3));
                Assert.IsTrue(sortedset.Contains(2));
                Assert.IsTrue(sortedset.Contains(1));
            }
        }

        [TestMethod]
        public void SortedSet_UnionWith_Universal()
        {
            var exceptWithMethods =
                typeof(ConvenienceExtensionMethods)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.SortedSet<>))
                .Where(p => p.Name == "UnionWith")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void SortedSet_UnionWith()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 3, 5 },
                @"a =>
                  {
                    var sortedset = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                    sortedset.UnionWith(a);

                    Assert.AreEqual(4, sortedset.Count);
                    Assert.IsTrue(sortedset.Contains(1));
                    Assert.IsTrue(sortedset.Contains(2));
                    Assert.IsTrue(sortedset.Contains(3));
                    Assert.IsTrue(sortedset.Contains(5));
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var sortedset = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                sortedset.UnionWith(Enumerable.Empty<int>());

                Assert.AreEqual(3, sortedset.Count);
                Assert.IsTrue(sortedset.Contains(1));
                Assert.IsTrue(sortedset.Contains(2));
                Assert.IsTrue(sortedset.Contains(3));
            }

            // emptyOrdered
            {
                var sortedset = new System.Collections.Generic.SortedSet<int>() { 3, 2, 2, 1, 3 };
                sortedset.UnionWith(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(3, sortedset.Count);
                Assert.IsTrue(sortedset.Contains(1));
                Assert.IsTrue(sortedset.Contains(2));
                Assert.IsTrue(sortedset.Contains(3));
            }
        }
        #endregion

        #region String
        [TestMethod]
        public void String_CheckForPassthroughs()
        {
            var publicStaticStringMtds = typeof(string).GetMethods(BindingFlags.Public | BindingFlags.Static).ToArray();
            var linqAFMtds = typeof(LinqAFString).GetMethods(BindingFlags.Public | BindingFlags.Static).ToArray();

            var missing = new System.Collections.Generic.HashSet<string>();
            var tooMany = new System.Collections.Generic.HashSet<string>();

            foreach (var mtd in publicStaticStringMtds)
            {
                // ignore operators
                if (mtd.Name == "op_Equality") continue;
                if (mtd.Name == "op_Inequality") continue;

                if(mtd.MetadataToken == 100664475)
                {
                    Console.WriteLine();
                }

                var mtdPs = mtd.GetParameters();

                var sameName = linqAFMtds.Where(m => m.Name == mtd.Name).ToArray();
                var sameNumParams = sameName.Where(m => m.GetParameters().Length == mtdPs.Length).ToArray();
                var samePs =
                    sameNumParams
                        .Where(
                            m =>
                            {
                                var mPs = m.GetParameters();
                                for(var i = 0; i < mPs.Length; i++)
                                {
                                    var same = String_AreEquivalent(mPs[i].ParameterType, mtdPs[i].ParameterType);

                                    if (!same) return false;
                                }

                                return true;
                            }
                        )
                        .ToArray();

                if(samePs.Length == 0)
                {
                    missing.Add(mtd.Name + "(" + string.Join(", ", mtdPs.Select(p => p.ParameterType.Name).ToArray()) + ")");
                }
                else
                {
                    if(samePs.Length > 1)
                    {
                        tooMany.Add(mtd.Name + "(" + string.Join(", ", mtdPs.Select(p => p.ParameterType.Name).ToArray()) + ")");
                    }
                }
            }

            Assert.AreEqual(0, missing.Count, string.Join("\n", missing));
            Assert.AreEqual(0, tooMany.Count, string.Join("\n", tooMany));
        }

        static bool String_AreEquivalent(Type a, Type b)
        {
            if (a == b) return true;
            if (a.IsAssignableFrom(b) && b.IsAssignableFrom(a)) return true;
            if (a.IsEquivalentTo(b) && b.IsEquivalentTo(a)) return true;
            if (a.IsGenericParameter && b.IsGenericParameter) return true;

            if (a.IsGenericType != b.IsGenericType) return false;

            var aGen = a;
            var bGen = b;

            if (aGen.IsGenericType)
            {
                aGen = aGen.GetGenericTypeDefinition();
                bGen = bGen.GetGenericTypeDefinition();

                var aGenParams = a.GetGenericArguments();
                var bGenParams = b.GetGenericArguments();

                if (aGenParams.Length != bGenParams.Length) return false;

                for(var i = 0; i < aGenParams.Length; i++)
                {
                    if (!String_AreEquivalent(aGenParams[i], bGenParams[i])) return false;
                }
            }
            
            if (aGen != a || bGen != b) return String_AreEquivalent(aGen, bGen);

            return false;
        }

        [TestMethod]
        public void String_Concat_Universal()
        {
            var exceptWithMethods =
                typeof(LinqAFString)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.Name == "Concat")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[0].ParameterType.IsGenericType && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void String_Concat()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var res = LinqAFString.Concat(a);
                    var aArr = a.Select(x => (object)x).ToArray();

                    Assert.AreEqual(string.Concat(aArr), res);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
            
            // empty
            {
                var res = LinqAFString.Concat(Enumerable.Empty<int>());

                Assert.AreEqual(string.Concat(Enumerable.Empty<int>().ToArray()), res);
            }

            // emptyOrdered
            {
                var res = LinqAFString.Concat(Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(string.Concat(Enumerable.Empty<int>().OrderBy(x => x).ToArray()), res);
            }
        }

        [TestMethod]
        public void String_Join_Universal()
        {
            var exceptWithMethods =
                typeof(LinqAFString)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.Name == "Join")
                .ToList();

            var allEnumerables = typeof(IStructEnumerable<,>).Assembly.GetTypes().Where(t => t.GetInterface("IStructEnumerable`2") != null).ToList();

            var missing = new System.Collections.Generic.List<string>();

            foreach (var e in allEnumerables)
            {
                var exceptWithTaking = exceptWithMethods.SingleOrDefault(m => m.GetParameters()[1].ParameterType.IsGenericType && m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == e);

                if (exceptWithTaking == null)
                {
                    missing.Add(e.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, "Missing methods that take: " + string.Join(", ", missing));
        }

        [TestMethod]
        public void String_Join()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  {
                    var res = LinqAFString.Join("", "", a);
                    var aArr = a.Select(x => (object)x).ToArray();

                    Assert.AreEqual(string.Join("", "", aArr), res);
                  }",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            // empty
            {
                var res = LinqAFString.Join(", ", Enumerable.Empty<int>());

                Assert.AreEqual(string.Join(", ", Enumerable.Empty<int>().ToArray()), res);
            }

            // emptyOrdered
            {
                var res = LinqAFString.Join(", ", Enumerable.Empty<int>().OrderBy(x => x));

                Assert.AreEqual(string.Join(", ", Enumerable.Empty<int>().OrderBy(x => x).ToArray()), res);
            }
        }
        #endregion
    }
}
