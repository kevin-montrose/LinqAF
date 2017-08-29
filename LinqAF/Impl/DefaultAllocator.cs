using System;
using System.Collections.Generic;
using LinqAF.Config;

namespace LinqAF.Impl
{
    class DefaultAllocator : IAllocator
    {
        public void EnumerableBoxed<T>() { }

        public T[] GetArray<T>(int length) => new T[length];
        
        public void ResizeArray<T>(ref T[] array, int length) => Array.Resize(ref array, length);

        public Dictionary<TKey, TValue> GetEmptyDictionary<TKey, TValue>(int? desiredCapacity, IEqualityComparer<TKey> comparer)
        {
            if (desiredCapacity == null) return new Dictionary<TKey, TValue>(comparer);

            return new Dictionary<TKey, TValue>(desiredCapacity.Value, comparer);
        }

        public List<T> GetEmptyList<T>(int? desiredCapacity)
        {
            if (desiredCapacity == null) return new List<T>();

            return new List<T>(desiredCapacity.Value);
        }

        public List<T> GetPopulatedList<T>(IEnumerable<T> collection) => new List<T>(collection);
    }
}
