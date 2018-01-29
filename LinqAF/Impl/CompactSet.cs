using LinqAF.Config;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LinqAF.Impl
{
    [StructLayout(LayoutKind.Auto)]
    struct CompactSetBucket<T>: IDisposable
    {
        uint UsedIndexes_SingleValue;
        internal int[] ValueIndexes;

        public enum Mode
        {
            Uninitialized,
            SingleValue,
            MultipleValue
        }

        public Mode CurrentMode
        {
            get
            {
                if (ValueIndexes == null && UsedIndexes_SingleValue == 0) return Mode.Uninitialized;
                if (ValueIndexes == null && UsedIndexes_SingleValue != 0) return Mode.SingleValue;
                if (ValueIndexes != null) return Mode.MultipleValue;

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
                    case Mode.MultipleValue: return (int)UsedIndexes_SingleValue;

                    default:
                    case Mode.Uninitialized:
                        throw CommonImplementation.UnexpectedState();
                }
            }
        }

        public int GetEntry(int entryIndex)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue: return (int)(UsedIndexes_SingleValue - 1);
                case Mode.MultipleValue: return ValueIndexes[entryIndex];

                default:
                case Mode.Uninitialized:
                    throw CommonImplementation.UnexpectedState();
            }
        }

        public bool IsDefaultValue() => ValueIndexes == null && UsedIndexes_SingleValue == 0;

        public void SetInitial(int index)
        {
            UsedIndexes_SingleValue = (uint)(index + 1);
        }

        public void Append(int index, int[] reuseArray)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    if(reuseArray == null)
                    {
                        ValueIndexes = Allocator.Current.GetArray<int>(2);
                    }
                    else
                    {
                        ValueIndexes = reuseArray;
                    }

                    ValueIndexes[0] = (int)(UsedIndexes_SingleValue - 1);
                    ValueIndexes[1] = index;

                    UsedIndexes_SingleValue = 2;
                    break;
                case Mode.MultipleValue:
                    if(UsedIndexes_SingleValue == ValueIndexes.Length)
                    {
                        Allocator.Current.ResizeArray(ref ValueIndexes, CommonImplementation.NextSize(ValueIndexes.Length));
                    }

                    ValueIndexes[UsedIndexes_SingleValue] = index;
                    UsedIndexes_SingleValue++;
                    break;

                default:
                case Mode.Uninitialized:
                    throw CommonImplementation.UnexpectedState();
            }
        }

        public bool Contains(T value, ref T[] values, IEqualityComparer<T> comparer, out int valueIndex)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    // just for containing values
                    {
                        var realIx = (int)(UsedIndexes_SingleValue - 1);
                        var containedVal = values[realIx];
                        if(CommonImplementation.GetHashCode(value, comparer) == CommonImplementation.GetHashCode(containedVal, comparer) && CommonImplementation.AreEqual(value, containedVal, comparer))
                        {
                            valueIndex = realIx;
                            return true;
                        }

                        valueIndex = -1;
                        return false;
                    }
                case Mode.MultipleValue:
                    for(var i = 0; i < UsedIndexes_SingleValue; i++)
                    {
                        var realIx = ValueIndexes[i];
                        var containedVal = values[realIx];
                        if(CommonImplementation.GetHashCode(value, comparer) == CommonImplementation.GetHashCode(containedVal, comparer) && CommonImplementation.AreEqual(value, containedVal, comparer))
                        {
                            valueIndex = realIx;
                            return true;
                        }
                    }

                    valueIndex = -1;
                    return false;

                case Mode.Uninitialized:
                    valueIndex = -1;
                    return false;

                default: throw CommonImplementation.UnexpectedState();
            }
        }

        public bool Contains(T value, ref T[] values, IComparer<T> comparer, out int valueIndex)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    // just for containing values
                    {
                        var realIx = (int)(UsedIndexes_SingleValue - 1);
                        var containedVal = values[realIx];
                        if (CommonImplementation.GetHashCode(value) == CommonImplementation.GetHashCode(containedVal) && CommonImplementation.AreEqual(value, containedVal, comparer))
                        {
                            valueIndex = realIx;
                            return true;
                        }

                        valueIndex = -1;
                        return false;
                    }
                case Mode.MultipleValue:
                    for (var i = 0; i < UsedIndexes_SingleValue; i++)
                    {
                        var realIx = ValueIndexes[i];
                        var containedVal = values[realIx];
                        if (CommonImplementation.GetHashCode(value) == CommonImplementation.GetHashCode(containedVal) && CommonImplementation.AreEqual(value, containedVal, comparer))
                        {
                            valueIndex = realIx;
                            return true;
                        }
                    }

                    valueIndex = -1;
                    return false;

                case Mode.Uninitialized:
                    valueIndex = -1;
                    return false;

                default: throw CommonImplementation.UnexpectedState();
            }
        }

        public bool Contains(T value, ref T[] values, out int valueIndex)
        {
            switch (CurrentMode)
            {
                case Mode.SingleValue:
                    // just for containing values
                    {
                        var realIx = (int)(UsedIndexes_SingleValue - 1);
                        var containedVal = values[realIx];
                        if (CommonImplementation.GetHashCode(value) == CommonImplementation.GetHashCode(containedVal) && CommonImplementation.AreEqual(value, containedVal))
                        {
                            valueIndex = realIx;
                            return true;
                        }

                        valueIndex = -1;
                        return false;
                    }
                case Mode.MultipleValue:
                    for (var i = 0; i < UsedIndexes_SingleValue; i++)
                    {
                        var realIx = ValueIndexes[i];
                        var containedVal = values[realIx];
                        if (CommonImplementation.GetHashCode(value) == CommonImplementation.GetHashCode(containedVal) && CommonImplementation.AreEqual(value, containedVal))
                        {
                            valueIndex = realIx;
                            return true;
                        }
                    }

                    valueIndex = -1;
                    return false;

                case Mode.Uninitialized:
                    valueIndex = -1;
                    return false;

                default: throw CommonImplementation.UnexpectedState();
            }
        }
        
        public void Reset()
        {
            UsedIndexes_SingleValue = 0;
            ValueIndexes = null;
        }

        public void Dispose()
        {
            UsedIndexes_SingleValue = 0;
            ValueIndexes = null;
        }
    }

    struct CompactSetIndexEnumerator<T>
    {
        public int Current => Buckets[BucketIndex].GetEntry(InnerIndex);

        private int BucketIndex;
        private int InnerIndex;
        CompactSetBucket<T>[] Buckets;
        public void Initialize(CompactSetBucket<T>[] buckets)
        {
            Buckets = buckets;
            BucketIndex = InnerIndex = -1;
        }

        public bool MoveNext()
        {
            return FindNextIndex(BucketIndex, InnerIndex, out BucketIndex, out InnerIndex);
        }

        bool FindNextIndex(int startAtBucketIx, int startAtInnerIndex, out int newBucketIx, out int newInnerIndex)
        {
            if (startAtBucketIx == -1 || startAtInnerIndex == Buckets[startAtBucketIx].NumEntries - 1)
            {
                newInnerIndex = 0;
                return FindNextBucket(startAtBucketIx, out newBucketIx);
            }

            newBucketIx = startAtBucketIx;
            newInnerIndex = startAtInnerIndex + 1;
            return true;
        }

        bool FindNextBucket(int oldBucketIx, out int newBucketIx)
        {
            newBucketIx = oldBucketIx + 1;

            while (newBucketIx < Buckets.Length && Buckets[newBucketIx].CurrentMode == CompactSetBucket<T>.Mode.Uninitialized)
            {
                newBucketIx++;
            }

            return newBucketIx < Buckets.Length;
        }
    }

    [StructLayout(LayoutKind.Auto)]
    struct CompactSet<T>: IDisposable
    {
        const int DEFAULT_SET_SIZE = 8;
        
        CompactSetBucket<T>[] Buckets;

        public bool IsDefaultValue() => Buckets == null;

        public void Initialize()
        {
            Buckets = Allocator.Current.GetArray<CompactSetBucket<T>>(DEFAULT_SET_SIZE);
        }

        public void Reset()
        {
            if (Buckets != null)
            {
                for (var i = 0; i < Buckets.Length; i++)
                {
                    if (Buckets[i].IsDefaultValue()) continue;
                    Buckets[i].Reset();
                }
            }

            Buckets = null;
        }

        public void Dispose()
        {
            if (Buckets != null)
            {
                for (var i = 0; i < Buckets.Length; i++)
                {
                    if (Buckets[i].IsDefaultValue()) continue;
                    Buckets[i].Dispose();
                }
            }
            
            Buckets = null;
        }

        public bool AddByIndex(int index, ref IndexedItemContainer<T> container, IEqualityComparer<T> comparer)
        {
            var value = container.Items[index];
            var hash = CommonImplementation.GetHashCode(value, comparer);
            var posHash = (hash & 0x7FFFFFFF);
            tryAgain:
            var bucketIx = posHash % Buckets.Length;

            // three cases
            //   #1 - bucket is empty
            //   #2 - bucket isn't empty, and contains the value
            //   #3 - bucket isn't empty, and doesn't contain the value

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                Buckets[bucketIx].SetInitial(index);
                return true;
            }
            else
            {
                int _;
                if (Buckets[bucketIx].Contains(value, ref container.Items, comparer, out _))
                {
                    // case #2
                    return false;
                }

                // case #3

                if (TryGrow(ref container, ref Buckets, comparer))
                {
                    goto tryAgain;
                }

                Buckets[bucketIx].Append(index, null);

                return true;
            }
        }

        public bool AddByIndex(int index, ref IndexedItemContainer<T> container)
        {
            var value = container.Items[index];
            var hash = CommonImplementation.GetHashCode(value);
            var posHash = (hash & 0x7FFFFFFF);
            tryAgain:
            var bucketIx = posHash % Buckets.Length;

            // three cases
            //   #1 - bucket is empty
            //   #2 - bucket isn't empty, and contains the value
            //   #3 - bucket isn't empty, and doesn't contain the value

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                Buckets[bucketIx].SetInitial(index);
                return true;
            }
            else
            {
                int _;
                if (Buckets[bucketIx].Contains(value, ref container.Items, out _))
                {
                    // case #2
                    return false;
                }

                // case #3

                if (TryGrow(ref container, ref Buckets))
                {
                    goto tryAgain;
                }

                Buckets[bucketIx].Append(index, null);

                return true;
            }
        }

        public bool Add(T value, ref IndexedItemContainer<T> container, IEqualityComparer<T> comparer)
        {
            var hash = CommonImplementation.GetHashCode(value, comparer);
            var posHash = (hash & 0x7FFFFFFF);
            tryAgain:
            var bucketIx = posHash % Buckets.Length;

            // three cases
            //   #1 - bucket is empty
            //   #2 - bucket isn't empty, and contains the value
            //   #3 - bucket isn't empty, and doesn't contain the value

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                var valueIndex = container.PlaceIn(value);
                Buckets[bucketIx].SetInitial(valueIndex);
                return true;
            }
            else
            {
                int _;
                if(Buckets[bucketIx].Contains(value, ref container.Items, comparer, out _))
                {
                    // case #2
                    return false;
                }

                // case #3

                if(TryGrow(ref container, ref Buckets, comparer))
                {
                    goto tryAgain;
                }

                var valueIndex = container.PlaceIn(value);
                Buckets[bucketIx].Append(valueIndex, null);

                return true;
            }
        }

        public bool Add(T value, ref IndexedItemContainer<T> container, IComparer<T> comparer)
        {
            var hash = CommonImplementation.GetHashCode(value);
            var posHash = (hash & 0x7FFFFFFF);
            tryAgain:
            var bucketIx = posHash % Buckets.Length;

            // three cases
            //   #1 - bucket is empty
            //   #2 - bucket isn't empty, and contains the value
            //   #3 - bucket isn't empty, and doesn't contain the value

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                var valueIndex = container.PlaceIn(value);
                Buckets[bucketIx].SetInitial(valueIndex);
                return true;
            }
            else
            {
                int _;
                if (Buckets[bucketIx].Contains(value, ref container.Items, comparer, out _))
                {
                    // case #2
                    return false;
                }

                // case #3

                if (TryGrow(ref container, ref Buckets, comparer))
                {
                    goto tryAgain;
                }

                var valueIndex = container.PlaceIn(value);
                Buckets[bucketIx].Append(valueIndex, null);

                return true;
            }
        }

        public bool Add(T value, ref IndexedItemContainer<T> container)
        {
            var hash = CommonImplementation.GetHashCode(value);
            var posHash = (hash & 0x7FFFFFFF);
            tryAgain:
            var bucketIx = posHash % Buckets.Length;

            // three cases
            //   #1 - bucket is empty
            //   #2 - bucket isn't empty, and contains the value
            //   #3 - bucket isn't empty, and doesn't contain the value

            if (Buckets[bucketIx].IsDefaultValue())
            {
                // case #1
                var valueIndex = container.PlaceIn(value);
                Buckets[bucketIx].SetInitial(valueIndex);
                return true;
            }
            else
            {
                int _;
                if (Buckets[bucketIx].Contains(value, ref container.Items, out _))
                {
                    // case #2
                    return false;
                }

                // case #3

                if (TryGrow(ref container, ref Buckets))
                {
                    goto tryAgain;
                }

                var valueIndex = container.PlaceIn(value);
                Buckets[bucketIx].Append(valueIndex, null);

                return true;
            }
        }

        public bool Contains(T value, ref IndexedItemContainer<T> container, IEqualityComparer<T> comparer, out int valueIndex)
        {
            var hash = CommonImplementation.GetHashCode(value, comparer);
            var posHash = (hash & 0x7FFFFFFF);
            var bucketIx = posHash % Buckets.Length;

            if (Buckets[bucketIx].IsDefaultValue())
            {
                valueIndex = -1;
                return false;
            }

            return Buckets[bucketIx].Contains(value, ref container.Items, comparer, out valueIndex);
        }

        public bool Contains(T value, ref IndexedItemContainer<T> container, IComparer<T> comparer, out int valueIndex)
        {
            var hash = CommonImplementation.GetHashCode(value);
            var posHash = (hash & 0x7FFFFFFF);
            var bucketIx = posHash % Buckets.Length;

            if (Buckets[bucketIx].IsDefaultValue())
            {
                valueIndex = -1;
                return false;
            }

            return Buckets[bucketIx].Contains(value, ref container.Items, comparer, out valueIndex);
        }

        public bool Contains(T value, ref IndexedItemContainer<T> container, out int valueIndex)
        {
            var hash = CommonImplementation.GetHashCode(value);
            var posHash = (hash & 0x7FFFFFFF);
            var bucketIx = posHash % Buckets.Length;

            if (Buckets[bucketIx].IsDefaultValue())
            {
                valueIndex = -1;
                return false;
            }

            return Buckets[bucketIx].Contains(value, ref container.Items, out valueIndex);
        }

        // not really an enumerator, but close enough
        public CompactSetIndexEnumerator<T> GetEnumerator()
        {
            var ret = new CompactSetIndexEnumerator<T>();
            ret.Initialize(Buckets);

            return ret;
        }

        static bool TryGrow(ref IndexedItemContainer<T> container, ref CompactSetBucket<T>[] buckets, IEqualityComparer<T> comparer)
        {
            var usedValues = container.UsedItems;
            
            if(usedValues != buckets.Length)
            {
                return false;
            }

            var oldBuckets = buckets;
            var newBuckets = Allocator.Current.GetArray<CompactSetBucket<T>>(CommonImplementation.NextSize(buckets.Length));

            int[] reuseableArray = null;

            for (var i = 0; i < oldBuckets.Length; i++)
            {
                var oldBucket = oldBuckets[i];
                if (oldBucket.IsDefaultValue()) continue;

                var numEntries = oldBucket.NumEntries;
                for (var j = 0; j < numEntries; j++)
                {
                    var valueIx = oldBucket.GetEntry(j);

                    var val = container.Items[valueIx];
                    var newBucketIx = (CommonImplementation.GetHashCode(val, comparer) & 0x7FFFFFFF) % newBuckets.Length;

                    if (newBuckets[newBucketIx].IsDefaultValue())
                    {
                        newBuckets[newBucketIx].SetInitial(valueIx);
                    }
                    else
                    {
                        newBuckets[newBucketIx].Append(valueIx, reuseableArray);
                        reuseableArray = null;
                    }
                }

                var nowAvailableArr = oldBucket.ValueIndexes;
                if (nowAvailableArr != null)
                {
                    if (reuseableArray == null)
                    {
                        reuseableArray = nowAvailableArr;
                    }
                    else
                    {
                        if(nowAvailableArr.Length > reuseableArray.Length)
                        {
                            reuseableArray = nowAvailableArr;
                        }
                    }
                }
            }

            buckets = newBuckets;
            return true;
        }

        static bool TryGrow(ref IndexedItemContainer<T> container, ref CompactSetBucket<T>[] buckets, IComparer<T> comparer)
        {
            var usedValues = container.UsedItems;

            if (usedValues != buckets.Length)
            {
                return false;
            }

            var oldBuckets = buckets;
            var newBuckets = Allocator.Current.GetArray<CompactSetBucket<T>>(CommonImplementation.NextSize(buckets.Length));

            int[] reuseableArray = null;

            for (var i = 0; i < oldBuckets.Length; i++)
            {
                var oldBucket = oldBuckets[i];
                if (oldBucket.IsDefaultValue()) continue;

                var numEntries = oldBucket.NumEntries;
                for (var j = 0; j < numEntries; j++)
                {
                    var valueIx = oldBucket.GetEntry(j);

                    var val = container.Items[valueIx];
                    var newBucketIx = (CommonImplementation.GetHashCode(val) & 0x7FFFFFFF) % newBuckets.Length;

                    if (newBuckets[newBucketIx].IsDefaultValue())
                    {
                        newBuckets[newBucketIx].SetInitial(valueIx);
                    }
                    else
                    {
                        newBuckets[newBucketIx].Append(valueIx, reuseableArray);
                        reuseableArray = null;
                    }
                }

                var nowAvailableArr = oldBucket.ValueIndexes;
                if (nowAvailableArr != null)
                {
                    if (reuseableArray == null)
                    {
                        reuseableArray = nowAvailableArr;
                    }
                    else
                    {
                        if (nowAvailableArr.Length > reuseableArray.Length)
                        {
                            reuseableArray = nowAvailableArr;
                        }
                    }
                }
            }

            buckets = newBuckets;
            return true;
        }

        static bool TryGrow(ref IndexedItemContainer<T> container, ref CompactSetBucket<T>[] buckets)
        {
            var usedValues = container.UsedItems;

            if (usedValues != buckets.Length)
            {
                return false;
            }

            var oldBuckets = buckets;
            var newBuckets = Allocator.Current.GetArray<CompactSetBucket<T>>(CommonImplementation.NextSize(buckets.Length));

            int[] reuseableArray = null;

            for (var i = 0; i < oldBuckets.Length; i++)
            {
                var oldBucket = oldBuckets[i];
                if (oldBucket.IsDefaultValue()) continue;

                var numEntries = oldBucket.NumEntries;
                for (var j = 0; j < numEntries; j++)
                {
                    var valueIx = oldBucket.GetEntry(j);

                    var val = container.Items[valueIx];
                    var newBucketIx = (CommonImplementation.GetHashCode(val) & 0x7FFFFFFF) % newBuckets.Length;

                    if (newBuckets[newBucketIx].IsDefaultValue())
                    {
                        newBuckets[newBucketIx].SetInitial(valueIx);
                    }
                    else
                    {
                        newBuckets[newBucketIx].Append(valueIx, reuseableArray);
                        reuseableArray = null;
                    }
                }

                var nowAvailableArr = oldBucket.ValueIndexes;
                if (nowAvailableArr != null)
                {
                    if (reuseableArray == null)
                    {
                        reuseableArray = nowAvailableArr;
                    }
                    else
                    {
                        if (nowAvailableArr.Length > reuseableArray.Length)
                        {
                            reuseableArray = nowAvailableArr;
                        }
                    }
                }
            }

            buckets = newBuckets;
            return true;
        }
    }
}