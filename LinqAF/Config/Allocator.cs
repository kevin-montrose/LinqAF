using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF.Config
{
    /// <summary>
    /// Used to configure the instance of IAllocator responsible
    /// for any allocations necessary during enumerable or
    /// enumerator creation or opperation.
    /// 
    /// Inflight enumerables may use a mix of Allocators if
    /// the current allocator is changing between their creation
    /// and their use.
    /// </summary>
    public static class Allocator
    {
        /// <summary>
        /// Returns the currently configured IAllocator.
        /// </summary>
        public static IAllocator Current { get; private set; } = new DefaultAllocator();

        /// <summary>
        /// Sets the allocator to use.
        /// </summary>
        public static void SetAllocator(IAllocator allocator)
        {
            var newAllocator = allocator;
            if (newAllocator == null)
            {
                newAllocator = new DefaultAllocator();
            }

            Current = newAllocator;
        }
    }

    /// <summary>
    /// Services requests for allocations for LinqAF provided
    /// Enumerables and Enumerators.
    /// </summary>
    public interface IAllocator
    {
        /// <summary>
        /// Must return an array of the given length.
        /// 
        /// The elements of the returned array may contain any value.
        /// </summary>
        T[] GetArray<T>(int length);
        
        /// <summary>
        /// Must resize the given array so that is of the given length.
        /// 
        /// Values in the passed array must be preserved in the final array.
        /// 
        /// The new elements of the final array may contain any value.
        /// 
        /// The passed array can be null.
        /// The passed length can be larger or smaller than the passed array's length.
        /// The passed length will always be >= 0
        /// </summary>
        void ResizeArray<T>(ref T[] array, int length);
        
        /// <summary>
        /// Must return an empty list.
        /// 
        /// Ideally the list is of at least the desired capacity, if any is provided.
        /// </summary>
        List<T> GetEmptyList<T>(int? desiredCapacity);

        /// <summary>
        /// Must return a list containing all the elements in collection, in order.
        /// 
        /// The passed IEnumerable will not be null.
        /// </summary>
        List<T> GetPopulatedList<T>(IEnumerable<T> collection);

        /// <summary>
        /// Must return an empty dictionary with the desired comparer (or the default comparer, if comparer is null).
        /// 
        /// Ideally the dictionary is of at least the desired capacity, if any is provided.
        /// 
        /// The passed IEqualityComparer may be null.
        /// </summary>
        Dictionary<TKey, TValue> GetEmptyDictionary<TKey, TValue>(int? desiredCapacity, IEqualityComparer<TKey> comparer);

        /// <summary>
        /// Must return an empty hashset with the desired comparer (or the default comparer, if comparer is null).
        /// 
        /// The passed IEqualityComparer may be null.
        /// </summary>
        HashSet<TItem> GetEmptyHashSet<TItem>(IEqualityComparer<TItem> comparer);

        /// <summary>
        /// Invoked when an enumerable is boxed.
        /// </summary>
        void EnumerableBoxed<T>();
    }
}
