using LinqAF.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LinqAF.Tests
{
    [TestClass]
    public class AllocatorTests
    {
        class AllocatorSwapper : IDisposable
        {
            IAllocator Old;

            public AllocatorSwapper(IAllocator @new)
            {
                Old = Allocator.Current;
                Allocator.SetAllocator(@new);
            }

            public void Dispose()
            {
                if (Old != null)
                {
                    Allocator.SetAllocator(Old);
                }
                Old = null;
            }
        }

        class _Simple : IAllocator
        {
            public int Boxed;
            public void EnumerableBoxed<T>()
            {
                Boxed++;
            }

            public int Dictionary;
            public Dictionary<TKey, TValue> GetEmptyDictionary<TKey, TValue>(int? desiredCapacity, IEqualityComparer<TKey> comparer)
            {
                Dictionary++;

                if (desiredCapacity.HasValue)
                {
                    return new Dictionary<TKey, TValue>(desiredCapacity.Value, comparer);
                }

                return new Dictionary<TKey, TValue>(comparer);
            }
            
            public int EmptyList;
            public List<T> GetEmptyList<T>(int? desiredCapacity)
            {
                EmptyList++;

                if (desiredCapacity.HasValue)
                {
                    return new List<T>(desiredCapacity.Value);
                }

                return new List<T>();
            }

            public int PopulatedList;
            public List<T> GetPopulatedList<T>(IEnumerable<T> collection)
            {
                PopulatedList++;

                return new List<T>(collection);
            }

            public int Arrays;
            public T[] GetArray<T>(int length)
            {
                Arrays++;
                return new T[length];
            }

            public void ResizeArray<T>(ref T[] array, int length)
            {
                Array.Resize(ref array, length);
            }

            public int HashSets;
            public HashSet<TItem> GetEmptyHashSet<TItem>(IEqualityComparer<TItem> comparer)
            {
                HashSets++;
                return new HashSet<TItem>(comparer);
            }
        }

        [TestMethod]
        public void Simple()
        {
            var alloc = new _Simple();
            using (new AllocatorSwapper(alloc))
            {
                foreach (var _ in Enumerable.Range(1, 1).ToList()) { }
                foreach (var _ in new[] { 4 }.ToList()) { }
                foreach (var _ in new[] { 4 }.ToDictionary(x => x, x => x)) { }
                foreach (var _ in new[] { 4 }.SelectMany(x => x == 4 ? (BoxedEnumerable<int>)Enumerable.Empty<int>() : (BoxedEnumerable<int>)Enumerable.Empty<int>().DefaultIfEmpty(5))) { }
                foreach (var _ in new[] { 4 }.ToArray()) { }
                foreach (var _ in new[] { 4 }.ToHashSet()) { }
            }

            Assert.AreEqual(1, alloc.Dictionary);
            Assert.AreEqual(1, alloc.EmptyList);
            Assert.AreEqual(1, alloc.PopulatedList);
            Assert.AreEqual(1, alloc.Arrays);
            Assert.AreEqual(1, alloc.HashSets);
        }

        class _Resuse : IAllocator
        {
            class FreeEntry
            {
                public bool IsFree;
                public object Collection;
            }

            public int NewCalls = 0;

            public void EnumerableBoxed<T>() { }

            ThreadLocal<List<FreeEntry>> Dictionaries = new ThreadLocal<List<FreeEntry>>(() => new List<FreeEntry>());
            public Dictionary<TKey, TValue> GetEmptyDictionary<TKey, TValue>(int? desiredCapacity, IEqualityComparer<TKey> comparer)
            {
                var dicts = Dictionaries.Value;
                foreach (var entry in dicts)
                {
                    if (!entry.IsFree) continue;

                    if (entry.Collection is Dictionary<TKey, TValue>)
                    {
                        var ret = (Dictionary<TKey, TValue>)entry.Collection;
                        ret.Clear();
                        entry.IsFree = false;

                        return ret;
                    }
                }

                Dictionary<TKey, TValue> newRet;
                if (desiredCapacity.HasValue)
                {
                    Interlocked.Increment(ref NewCalls);
                    newRet = new Dictionary<TKey, TValue>(desiredCapacity.Value, comparer);
                }
                else
                {
                    Interlocked.Increment(ref NewCalls);
                    newRet = new Dictionary<TKey, TValue>(comparer);
                }

                dicts.Add(new FreeEntry { IsFree = false, Collection = newRet });

                return newRet;
            }

            public void FreeDictionary<TKey, TValue>(Dictionary<TKey, TValue> dict)
            {
                var dicts = Dictionaries.Value;
                foreach(var entry in dicts)
                {
                    if(entry.Collection == dict)
                    {
                        entry.IsFree = true;
                        return;
                    }
                }

                throw new Exception("Tried to free Dictionary that didn't come from allocator");
            }
            
            ThreadLocal<List<FreeEntry>> Lists = new ThreadLocal<List<FreeEntry>>(() => new List<FreeEntry>());
            public List<T> GetEmptyList<T>(int? desiredCapacity)
            {
                var lists = Lists.Value;
                foreach (var entry in lists)
                {
                    if (!entry.IsFree) continue;

                    if (entry.Collection is List<T>)
                    {
                        var ret = (List<T>)entry.Collection;
                        ret.Clear();
                        entry.IsFree = false;

                        return ret;
                    }
                }

                List<T> newRet;
                if (desiredCapacity.HasValue)
                {
                    Interlocked.Increment(ref NewCalls);
                    newRet = new List<T>(desiredCapacity.Value);
                }
                else
                {
                    Interlocked.Increment(ref NewCalls);
                    newRet = new List<T>();
                }

                lists.Add(new FreeEntry { IsFree = false, Collection = newRet });

                return newRet;
            }

            public List<T> GetPopulatedList<T>(IEnumerable<T> collection)
            {
                var lists = Lists.Value;
                foreach (var entry in lists)
                {
                    if (!entry.IsFree) continue;

                    if (entry.Collection is List<T>)
                    {
                        var ret = (List<T>)entry.Collection;
                        ret.Clear();
                        ret.AddRange(collection);
                        entry.IsFree = false;

                        return ret;
                    }
                }

                Interlocked.Increment(ref NewCalls);
                var newRet = new List<T>(collection);

                lists.Add(new FreeEntry { IsFree = false, Collection = newRet });

                return newRet;
            }

            public void FreeList<T>(List<T> list)
            {
                var lists = Lists.Value;
                foreach (var entry in lists)
                {
                    if (entry.Collection == list)
                    {
                        entry.IsFree = true;
                        return;
                    }
                }

                throw new Exception("Tried to free List that didn't come from allocator");
            }

            ThreadLocal<List<FreeEntry>> Arrays = new ThreadLocal<List<FreeEntry>>(() => new List<FreeEntry>());
            public T[] GetArray<T>(int length)
            {
                var arrs = Arrays.Value;
                foreach (var entry in arrs)
                {
                    if (!entry.IsFree) continue;

                    var typed = entry.Collection as T[];
                    if (typed != null && typed.Length == length)
                    {
                        entry.IsFree = false;

                        return typed;
                    }
                }

                Interlocked.Increment(ref NewCalls);
                var newRet = new T[length];
                arrs.Add(new FreeEntry { IsFree = false, Collection = newRet });

                return newRet;
            }

            public void ResizeArray<T>(ref T[] array, int length)
            {
                var newRet = GetArray<T>(length);

                if(array == null)
                {
                    array = newRet;
                    return;
                }

                var copyLength = Math.Min(length, array.Length);
                Array.Copy(array, newRet, copyLength);
                array = newRet;
            }

            public void FreeArray<T>(T[] arr)
            {
                var arrs = Arrays.Value;
                foreach (var entry in arrs)
                {
                    if (entry.Collection == arr)
                    {
                        entry.IsFree = true;
                        return;
                    }
                }

                throw new Exception("Tried to free array that didn't come from allocator");
            }

            ThreadLocal<List<FreeEntry>> HashSets = new ThreadLocal<List<FreeEntry>>(() => new List<FreeEntry>());
            public HashSet<T> GetEmptyHashSet<T>(IEqualityComparer<T> comparer)
            {
                comparer = comparer ?? EqualityComparer<T>.Default;

                var sets = HashSets.Value;
                foreach (var entry in sets)
                {
                    if (!entry.IsFree) continue;

                    if (entry.Collection is HashSet<T>)
                    {
                        var ret = (HashSet<T>)entry.Collection;

                        if (!object.ReferenceEquals(ret.Comparer, comparer)) continue;

                        ret.Clear();
                        entry.IsFree = false;

                        return ret;
                    }
                }

                Interlocked.Increment(ref NewCalls);
                var newRet = new HashSet<T>(comparer);

                sets.Add(new FreeEntry { IsFree = false, Collection = newRet });

                return newRet;
            }

            public void FreeHashSet<T>(HashSet<T> set)
            {
                var sets = HashSets.Value;
                foreach (var entry in sets)
                {
                    if (entry.Collection == set)
                    {
                        entry.IsFree = true;
                        return;
                    }
                }

                throw new Exception("Tried to free HashSet that didn't come from allocator");
            }
        }

        [TestMethod]
        public void Reuse()
        {
            var alloc = new _Resuse();
            using (new AllocatorSwapper(alloc))
            {
                var threads = new List<Thread>();
                for(var i = 0; i < 32; i++)
                {
                    var thread =
                        new Thread(
                            () =>
                            {
                                var random = new Random(Thread.CurrentThread.ManagedThreadId);

                                for (var x = 0; x < 1000; x++)
                                {
                                    Thread.Sleep(TimeSpan.FromMilliseconds(random.Next(100)));
                                    var list = Enumerable.Range(0, x).ToList();
                                    Assert.IsTrue(list.SequenceEqual(Enumerable.Range(0, x)));
                                    var dict = Enumerable.Range(0, x).ToDictionary(y => y, y => y * 2);
                                    foreach (var kv in dict)
                                    {
                                        Assert.AreEqual(kv.Key * 2, kv.Value);
                                    }
                                    Assert.AreEqual(x, dict.Count);
                                    alloc.FreeList(list);
                                    alloc.FreeDictionary(dict);

                                    // arr has to be constant size, unlike list and dictionary
                                    var arr = Enumerable.Range(0, 4).ToArray();
                                    Assert.IsTrue(arr.SequenceEqual(Enumerable.Range(0, 4)));
                                    alloc.FreeArray(arr);

                                    var hashset = Enumerable.Range(0, 10).ToHashSet();
                                    Assert.AreEqual(10, hashset.Count);
                                    Assert.IsTrue(Enumerable.Range(0, 10).All(y => hashset.Contains(y)));
                                    alloc.FreeHashSet(hashset);
                                }
                            }
                        );

                    threads.Add(thread);
                }

                threads.ForEach(t => t.Start());
                threads.ForEach(t => t.Join());
            }

            // one each for list, dictionary, hashset, and array * # of threads
            Assert.AreEqual(4 * 32, alloc.NewCalls);
        }
    }
}
