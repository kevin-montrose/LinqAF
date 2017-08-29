using LinqAF.Config;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LinqAF.Impl
{
    [StructLayout(LayoutKind.Auto)]
    struct SlimGrouping<TKey, TElement>
    {
        public TKey Key;
        internal uint UsedIndexes_SingleIndex;
        internal int[] ElementIndexes;
        
        internal void SetInitial(TKey key, TElement element, ref IndexedItemContainer<TElement> elements)
        {
            Key = key;
            var firstIndex = elements.PlaceIn(element);

            UsedIndexes_SingleIndex = ((uint)firstIndex) + 1;
        }

        internal void Add(TElement element, ref IndexedItemContainer<TElement> elements)
        {
            var nextIndex = elements.PlaceIn(element);

            if (ElementIndexes == null)
            {
                ElementIndexes = Allocator.Current.GetArray<int>(2);
                ElementIndexes[0] = (int)(UsedIndexes_SingleIndex - 1);
                UsedIndexes_SingleIndex = 1;
            }
            else
            {
                if (ElementIndexes.Length == UsedIndexes_SingleIndex)
                {
                    Allocator.Current.ResizeArray(ref ElementIndexes, CommonImplementation.NextSize(ElementIndexes.Length));
                }
            }

            ElementIndexes[UsedIndexes_SingleIndex] = nextIndex;
            UsedIndexes_SingleIndex++;
        }
    }

    /// <summary>
    /// A hashtable implementation for lookup.
    /// 
    /// Has an internal array that grows whenever the # of distinct groupings == it's inner array count.
    /// Has an internal array that contains the traversal order.
    /// 
    /// Inner array is a series of buckets.
    /// Each bucket contains groupings that all have keys that have the same hash % inner array size.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    internal struct LookupHashtable<TKey, TElement>
    {
        const int DEFAULT_HASHTABLE_SIZE = 8;

        public int Count => AllocatedGroupings;
        
        // number of GROUPINGs (things that have a distinct TKey associated with them)
        //  this is also the index of the next Groupings & TraversalIndexes entries to use
        int AllocatedGroupings;

        // we append to this list constantly, so we can resize and get quick copies of these
        //   note that we _don't_ need to re-organize this ever, just grow it
        SlimGrouping<TKey, TElement>[] Groupings;

        // a hashtable, where each bucket contains a list of indexes into Groupings
        //   these have to re-organized when we Grow()
        LookupBucket<TKey, TElement>[] Buckets;

        IndexedItemContainer<TElement> Container;

        internal LookupHashtable(int size, SlimGrouping<TKey, TElement>[] groupings, LookupBucket<TKey, TElement>[] buckets)
        {
            AllocatedGroupings = size;
            Groupings = groupings;
            Buckets = buckets;
            Container = new IndexedItemContainer<TElement>();
        }

#if DEBUG
        internal string ToStringDebug(IEqualityComparer<TKey> comparer)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Count: {Count}");
            for(var i = 0; i < AllocatedGroupings; i++)
            {
                var grouping = Groupings[i];
                sb.AppendLine($"Key: {grouping.Key}");
                var hash = CommonImplementation.GetHashCode(grouping.Key, comparer);
                sb.AppendLine($"  Hash: {hash}");
                var expectedIx = (hash & 0x7FFFFFFF) % Buckets.Length;
                sb.AppendLine($"  Expected Bucket Index: {expectedIx}");
            }

            sb.AppendLine();

            for (var i =0; i < Buckets.Length; i++)
            {
                sb.AppendLine($"Bucket Index: {i}");
                var bucket = Buckets[i];
                if (bucket.IsDefaultValue())
                {
                    sb.AppendLine("  None");
                    continue;
                }
                var numEntries = bucket.NumEntries;
                sb.AppendLine($"  Num Entries: {numEntries}");
                for(var j = 0; j < numEntries; j++)
                {
                    sb.AppendLine($"    Entry {j}");
                    var grpIx = bucket.GetGroupingIndex(j);
                    sb.AppendLine($"      Grouping Index: {grpIx}");
                    var grouping = Groupings[grpIx];
                    sb.AppendLine($"      Grouping Key: {grouping.Key}");
                    var hash = CommonImplementation.GetHashCode(grouping.Key, comparer);
                    sb.AppendLine($"      Hash: {hash}");
                    var expectedIx = (hash & 0x7FFFFFFF) % Buckets.Length;
                    sb.AppendLine($"      Expected Bucket Index: {expectedIx}");
                }
            }

            return sb.ToString();
        }
#endif

        public void Initialize()
        {
            AllocatedGroupings = 0;
            Groupings = Allocator.Current.GetArray<SlimGrouping<TKey, TElement>>(DEFAULT_HASHTABLE_SIZE);
            Buckets = Allocator.Current.GetArray<LookupBucket<TKey, TElement>>(DEFAULT_HASHTABLE_SIZE);
            Container.Initialize();
        }
        
        public void Add(TKey key,TElement element)
        {
            var hash = CommonImplementation.GetHashCode(key);

            tryAgain:
            var bucketIx = (hash & 0x7FFFFFFF) % Buckets.Length;

            // three cases
            // 1 - bucket is not in use, so initialize the bucket with a new grouping
            //     a. add a new grouping to Groupings
            //     b. initialize the bucket with the index of the new grouping
            //     c. increment allocated groupings
            // 2 - the bucket is in use, and the element doesn't belong to any existing grouping
            //     a. check to see if it's time to grow
            //        * if so, grow and then retry Add(), which may change the case we're in
            //     b. add a new grouping to Groupings
            //     c. add the index of the new grouping to the bucket
            //     d. increment allocated groupings
            // 3 - the bucket is in use, and the element does belong to an existing grouping
            //     a. add the element to the existing grouping

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                var newGroupingIx = AddNewGrouping(key, element, ref Container, ref Groupings, ref AllocatedGroupings);
                Buckets[bucketIx].SetInitial(newGroupingIx);
            }
            else
            {
                int inGroupingIx;
                if (Buckets[bucketIx].Contains(hash, key, ref Groupings, out inGroupingIx))
                {
                    // case #3
                    Groupings[inGroupingIx].Add(element, ref Container);
                }
                else
                {
                    // case #2
                    if (AllocatedGroupings == Buckets.Length)
                    {
                        Grow(ref Buckets, ref Groupings);
                        // we can't proceed any further, because Buckets() has changed, so try again
                        goto tryAgain;
                    }

                    var newGroupingIx = AddNewGrouping(key, element, ref Container, ref Groupings, ref AllocatedGroupings);
                    Buckets[bucketIx].Append(newGroupingIx, null);
                }
            }
        }

        public void Add(
            TKey key,
            TElement element,
            IEqualityComparer<TKey> comparer
        )
        {
            var hash = CommonImplementation.GetHashCode(key, comparer);

            tryAgain:
            var bucketIx = (hash & 0x7FFFFFFF) % Buckets.Length;

            // three cases
            // 1 - bucket is not in use, so initialize the bucket with a new grouping
            //     a. add a new grouping to Groupings
            //     b. initialize the bucket with the index of the new grouping
            //     c. increment allocated groupings
            // 2 - the bucket is in use, and the element doesn't belong to any existing grouping
            //     a. check to see if it's time to grow
            //        * if so, grow and then retry Add(), which may change the case we're in
            //     b. add a new grouping to Groupings
            //     c. add the index of the new grouping to the bucket
            //     d. increment allocated groupings
            // 3 - the bucket is in use, and the element does belong to an existing grouping
            //     a. add the element to the existing grouping

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                var newGroupingIx = AddNewGrouping(key, element, ref Container, ref Groupings, ref AllocatedGroupings);
                Buckets[bucketIx].SetInitial(newGroupingIx);
            }
            else
            {
                int inGroupingIx;
                if(Buckets[bucketIx].Contains(hash, key, comparer, ref Groupings, out inGroupingIx))
                {
                    // case #3
                    Groupings[inGroupingIx].Add(element, ref Container);
                }
                else
                {
                    // case #2
                    if(AllocatedGroupings == Buckets.Length)
                    {
                        Grow(ref Buckets, ref Groupings, comparer);
                        // we can't proceed any further, because Buckets() has changed, so try again
                        goto tryAgain;
                    }

                    var newGroupingIx = AddNewGrouping(key, element, ref Container, ref Groupings, ref AllocatedGroupings);
                    Buckets[bucketIx].Append(newGroupingIx, null);
                }
            }
        }

        // creates a new GroupingEnumerable in groupings, updates the # of allocated groupings
        //   and returns the index of the new grouping inserted into groupings
        //
        // resizes groupings if needed
        static int AddNewGrouping(
            TKey key, 
            TElement element, 
            ref IndexedItemContainer<TElement> container,
            ref SlimGrouping<TKey, TElement>[] groupings, 
            ref int allocatedGroupings)
        {
            if (groupings.Length == allocatedGroupings)
            {
                var nextSize = CommonImplementation.NextSize(groupings.Length);
                Allocator.Current.ResizeArray(ref groupings, nextSize);
            }

            var ret = allocatedGroupings;
            groupings[allocatedGroupings].SetInitial(key, element, ref container);
            
            allocatedGroupings++;
            return ret;
        }

        // increases the size of buckets, and moves the different indexes around depending on 
        //   how the distributions of hashes has changed
        static void Grow(ref LookupBucket<TKey, TElement>[] buckets, ref SlimGrouping<TKey, TElement>[] groupings, IEqualityComparer<TKey> comparer)
        {
            var oldBuckets = buckets;
            var newBuckets = Allocator.Current.GetArray<LookupBucket<TKey, TElement>>(CommonImplementation.NextSize(groupings.Length));

            // keep track of a recent array we've freed up, just to
            //   save some bytes
            int[] reusableArray = null;

            for (var i = 0; i < oldBuckets.Length; i++)
            {
                // nothing to do for this bucket
                if (oldBuckets[i].IsDefaultValue()) continue;

                // we need to take each grouping we've inserted, and see if it should be moved to a different
                //   bucket in the new schema
                var oldBucket = oldBuckets[i];
                var numEntries = oldBucket.NumEntries;

                for (var j = 0; j < numEntries; j++)
                {
                    var groupingIndex = oldBucket.GetGroupingIndex(j);
                    var hash = CommonImplementation.GetHashCode(groupings[groupingIndex].Key, comparer);
                    var newBucketIndex = (hash & 0x7FFFFFFF) % newBuckets.Length;
                    if (newBuckets[newBucketIndex].IsDefaultValue())
                    {
                        newBuckets[newBucketIndex].SetInitial(groupingIndex);
                    }
                    else
                    {
                        newBuckets[newBucketIndex].Append(groupingIndex, reusableArray);
                        reusableArray = null;
                    }
                }

                var oldBucketGroupingIndexes = oldBucket.GroupingIndexes;
                if (oldBucketGroupingIndexes != null)
                {
                    if (reusableArray == null)
                    {
                        reusableArray = oldBucketGroupingIndexes;
                    }
                    else
                    {
                        if (reusableArray.Length < oldBucketGroupingIndexes.Length)
                        {
                            reusableArray = oldBucketGroupingIndexes;
                        }
                    }
                }
            }

            buckets = newBuckets;
        }

        // increases the size of buckets, and moves the different indexes around depending on 
        //   how the distributions of hashes has changed
        static void Grow(ref LookupBucket<TKey, TElement>[] buckets, ref SlimGrouping<TKey, TElement>[] groupings)
        {
            var oldBuckets = buckets;
            var newBuckets = Allocator.Current.GetArray<LookupBucket<TKey, TElement>>(CommonImplementation.NextSize(groupings.Length));

            // keep track of a recent array we've freed up, just to
            //   save some bytes
            int[] reusableArray = null;

            for (var i = 0; i < oldBuckets.Length; i++)
            {
                // nothing to do for this bucket
                if (oldBuckets[i].IsDefaultValue()) continue;

                // we need to take each grouping we've inserted, and see if it should be moved to a different
                //   bucket in the new schema
                var oldBucket = oldBuckets[i];
                var numEntries = oldBucket.NumEntries;

                for (var j = 0; j < numEntries; j++)
                {
                    var groupingIndex = oldBucket.GetGroupingIndex(j);
                    var hash = CommonImplementation.GetHashCode(groupings[groupingIndex].Key);
                    var newBucketIndex = (hash & 0x7FFFFFFF) % newBuckets.Length;
                    if (newBuckets[newBucketIndex].IsDefaultValue())
                    {
                        newBuckets[newBucketIndex].SetInitial(groupingIndex);
                    }
                    else
                    {
                        newBuckets[newBucketIndex].Append(groupingIndex, reusableArray);
                        reusableArray = null;
                    }
                }

                var oldBucketGroupingIndexes = oldBucket.GroupingIndexes;
                if (oldBucketGroupingIndexes != null)
                {
                    if (reusableArray == null)
                    {
                        reusableArray = oldBucketGroupingIndexes;
                    }
                    else
                    {
                        if (reusableArray.Length < oldBucketGroupingIndexes.Length)
                        {
                            reusableArray = oldBucketGroupingIndexes;
                        }
                    }
                }
            }

            buckets = newBuckets;
        }

        public bool Contains(TKey key, IEqualityComparer<TKey> comparer)
        {
            var hash = CommonImplementation.GetHashCode(key, comparer);
            var bucketIndex = (hash & 0x7FFFFFFF) % Buckets.Length;

            if (Buckets[bucketIndex].IsDefaultValue()) return false;

            int _;
            return Buckets[bucketIndex].Contains(hash, key, comparer, ref Groupings, out _);
        }

        public bool Contains(TKey key)
        {
            var hash = CommonImplementation.GetHashCode(key);
            var bucketIndex = (hash & 0x7FFFFFFF) % Buckets.Length;

            if (Buckets[bucketIndex].IsDefaultValue()) return false;

            int _;
            return Buckets[bucketIndex].Contains(hash, key, ref Groupings, out _);
        }

        public GroupingEnumerable<TKey, TElement> GetGrouping(TKey key, IEqualityComparer<TKey> comparer)
        {
            var hash = CommonImplementation.GetHashCode(key, comparer);
            var bucketIndex = (hash & 0x7FFFFFFF) % Buckets.Length;

            int groupingIndex;
            if (Buckets[bucketIndex].IsDefaultValue() || !Buckets[bucketIndex].Contains(hash, key, comparer, ref Groupings, out groupingIndex))
            {
                return EmptyCache<TKey, TElement>.EmptyGrouping;
            }

            var ret = Groupings[groupingIndex];
            return new GroupingEnumerable<TKey, TElement>(ret.Key, ret.UsedIndexes_SingleIndex, ret.ElementIndexes, ref Container);
        }

        public GroupingEnumerable<TKey, TElement> GetGrouping(TKey key)
        {
            var hash = CommonImplementation.GetHashCode(key);
            var bucketIndex = (hash & 0x7FFFFFFF) % Buckets.Length;

            int groupingIndex;
            if (Buckets[bucketIndex].IsDefaultValue() || !Buckets[bucketIndex].Contains(hash, key, ref Groupings, out groupingIndex))
            {
                return EmptyCache<TKey, TElement>.EmptyGrouping;
            }

            var ret = Groupings[groupingIndex];
            return new GroupingEnumerable<TKey, TElement>(ret.Key, ret.UsedIndexes_SingleIndex, ret.ElementIndexes, ref Container);
        }

        public GroupingEnumerable<TKey, TElement> GetAtIndex(int index)
        {
            var ret = Groupings[index];
            return new GroupingEnumerable<TKey, TElement>(ret.Key, ret.UsedIndexes_SingleIndex, ret.ElementIndexes, ref Container);
        }

        public bool IsDefaultValue() => Buckets == null;
    }

    /// <summary>
    /// Contains a set of groupings that all have the same hash modulo the current container size.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    internal struct LookupBucket<TKey, TElement>
    {
        /// <summary>
        /// This value can be one of two things
        ///    1. If GroupingIndexes is non-null, it's the number of entries used in the grouping index
        ///    2. If GroupingIndex is null, it's the index + 1 into a GroupingEnumerable[] of a single value
        /// </summary>
        uint UsedIndexes_SingleValueIndex;

        internal int[] GroupingIndexes;

        enum Mode : byte
        {
            Uninitialized,
            SingleValue,
            MultipleValue
        }

        Mode CurrentMode
        {
            get
            {
                if (UsedIndexes_SingleValueIndex == 0 && GroupingIndexes == null) return Mode.Uninitialized;
                if (GroupingIndexes == null && UsedIndexes_SingleValueIndex != 0) return Mode.SingleValue;
                if (GroupingIndexes != null) return Mode.MultipleValue;

                throw CommonImplementation.UnexpectedState();
            }
        }

        public int NumEntries
        {
            get
            {
                switch (CurrentMode)
                {
                    case Mode.SingleValue: return 1;
                    case Mode.MultipleValue: return (int)UsedIndexes_SingleValueIndex;

                    default:
                    case Mode.Uninitialized:
                        return 0;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInitial(int index)
        {
            UsedIndexes_SingleValueIndex = ((uint)index) + 1;
        }

        public bool Contains(
            int hash,
            TKey key,
            IEqualityComparer<TKey> comparer,
            ref SlimGrouping<TKey, TElement>[] groupings,
            out int groupingIndex
        )
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    var trueIndex = (int)(UsedIndexes_SingleValueIndex - 1);
                    if (CommonImplementation.GetHashCode(groupings[trueIndex].Key, comparer) == hash && CommonImplementation.AreEqual(key, groupings[trueIndex].Key, comparer))
                    {
                        groupingIndex = trueIndex;
                        return true;
                    }
                    groupingIndex = -1;
                    return false;

                case Mode.MultipleValue:
                    for (var i = 0; i < UsedIndexes_SingleValueIndex; i++)
                    {
                        var ix = GroupingIndexes[i];

                        if (CommonImplementation.GetHashCode(groupings[ix].Key, comparer) == hash && CommonImplementation.AreEqual(key, groupings[ix].Key, comparer))
                        {
                            groupingIndex = ix;
                            return true;
                        }
                    }

                    groupingIndex = -1;
                    return false;

                default:
                case Mode.Uninitialized:
                    throw CommonImplementation.UnexpectedState();
            }
        }

        public bool Contains(
            int hash,
            TKey key,
            ref SlimGrouping<TKey, TElement>[] groupings,
            out int groupingIndex
        )
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    var trueIndex = (int)(UsedIndexes_SingleValueIndex - 1);
                    if (CommonImplementation.GetHashCode(groupings[trueIndex].Key) == hash && CommonImplementation.AreEqual(key, groupings[trueIndex].Key))
                    {
                        groupingIndex = trueIndex;
                        return true;
                    }
                    groupingIndex = -1;
                    return false;

                case Mode.MultipleValue:
                    for (var i = 0; i < UsedIndexes_SingleValueIndex; i++)
                    {
                        var ix = GroupingIndexes[i];

                        if (CommonImplementation.GetHashCode(groupings[ix].Key) == hash && CommonImplementation.AreEqual(key, groupings[ix].Key))
                        {
                            groupingIndex = ix;
                            return true;
                        }
                    }

                    groupingIndex = -1;
                    return false;

                default:
                case Mode.Uninitialized:
                    throw CommonImplementation.UnexpectedState();
            }
        }

        public void Append(int groupingIndex, int[] reuseArray)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    if (reuseArray != null)
                    {
                        GroupingIndexes = reuseArray;
                    }
                    else
                    {
                        GroupingIndexes = Allocator.Current.GetArray<int>(2);
                    }

                    GroupingIndexes[0] = (int)(UsedIndexes_SingleValueIndex - 1);
                    GroupingIndexes[1] = groupingIndex;
                    UsedIndexes_SingleValueIndex = 2;
                    break;

                case Mode.MultipleValue:
                    if (UsedIndexes_SingleValueIndex == GroupingIndexes.Length)
                    {
                        Allocator.Current.ResizeArray(ref GroupingIndexes, CommonImplementation.NextSize(GroupingIndexes.Length));
                    }

                    GroupingIndexes[UsedIndexes_SingleValueIndex] = groupingIndex;
                    UsedIndexes_SingleValueIndex++;
                    break;

                default:
                case Mode.Uninitialized:
                    throw CommonImplementation.UnexpectedState();
            }
        }

        public bool IsDefaultValue() => GroupingIndexes == null && UsedIndexes_SingleValueIndex == 0;

        public int GetGroupingIndex(int ix)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    return (int)(UsedIndexes_SingleValueIndex - 1);
                case Mode.MultipleValue:
                    return GroupingIndexes[ix];

                default:
                case Mode.Uninitialized:
                    throw CommonImplementation.UnexpectedState();
            }
        }
    }
}
