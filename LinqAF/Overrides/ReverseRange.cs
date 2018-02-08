using LinqAF.Config;
using LinqAF.Impl;
using MiscUtil;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct ReverseRangeEnumerable
    {
        public int Count()
        {
            if(IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return InnerCount;
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return InnerCount;
        }

        public bool Any()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return InnerCount >= 0;
        }

        public int ElementAt(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if(index >= InnerCount || index < 0)
            {
                throw CommonImplementation.OutOfRange(nameof(index));
            }

            return Start - index;
        }

        public int ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (index >= InnerCount || index < 0)
            {
                return 0;
            }

            return Start -index;
        }

        public bool Contains(int value)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            // 3 2 1
            // length = 3
            // max = 3
            // min = 3 - 3 + 1 = 1

            // value = 0
            //   value < min
            // value = 3
            //   value > max

            var max = this.Start;
            var min = max - this.InnerCount + 1;

            if (value < min || value > max) return false;

            return true;
        }

        public int First()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            return this.Start;
        }

        public int FirstOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                return 0;
            }

            return this.Start;
        }
        
        public int Last()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            // Range(1, 3) -> 1, 2, 3
            // ReverseRange(3, 3) -> 3, 2, 1
            // Last = (Start - InnerCount) + 1

            var ret = (Start - InnerCount) + 1;
            return ret;
        }

        public int LastOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                return 0;
            }

            // Range(1, 3) -> 1, 2, 3
            // ReverseRange(3, 3) -> 3, 2, 1
            // Last = (Start - InnerCount) + 1

            var ret = (Start - InnerCount) + 1;
            return ret;
        }

        public int Max() => First();


        public int Min() => Last();


        public bool SequenceEqual(EmptyEnumerable<int> second) => SequenceEqual(second, null);
        
        public bool SequenceEqual(EmptyEnumerable<int> second, IEqualityComparer<int> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return this.InnerCount == 0;
        }
        
        public bool SequenceEqual(RepeatEnumerable<int> second) => SequenceEqual(second, null);

        public bool SequenceEqual(RepeatEnumerable<int> second, IEqualityComparer<int> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (this.InnerCount == 0 && second.InnerCount == 0)
            {
                return true;
            }

            if(this.InnerCount > 1 || second.InnerCount > 1)
            {
                return false;
            }

            // must be exactly one now item
            comparer = comparer ?? EqualityComparer<int>.Default;

            // only equal sequences if their single element is equal
            return comparer.Equals(this.Start, second.Item);
        }
        
        public int Single()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            if(this.InnerCount > 1)
            {
                throw CommonImplementation.MultipleElements();
            }

            return this.Start;
        }

        public int SingleOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                return 0;
            }

            if (this.InnerCount > 1)
            {
                throw CommonImplementation.MultipleElements();
            }

            return this.Start;
        }
        
        public ReverseRangeEnumerable Skip(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            // decrement start by count
            // decrement count by count

            var newStart = Start -count;
            var newCount = InnerCount - count;
            if (newCount < 0) newCount = 0;

            return new ReverseRangeEnumerable(Enumerable.ReverseRangeSigil, newStart, newCount);
        }

        public ReverseRangeEnumerable Take(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            // decrement count by count

            var newCount = Math.Min(InnerCount, count);
            if (newCount < 0) newCount = 0;

            return new ReverseRangeEnumerable(Enumerable.ReverseRangeSigil, Start, newCount);
        }

        public ReverseRangeEnumerable Distinct()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        public List<int> ToList()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetEmptyList<int>(this.InnerCount);
            foreach(var item in this)
            {
                ret.Add(item);
            }

            return ret;
        }

        public int[] ToArray()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetArray<int>(this.InnerCount);
            var ix = 0;
            foreach(var item in this)
            {
                ret[ix] = item;
                ix++;
            }

            return ret;
        }

        public Dictionary<TToDictionary_Key, int> ToDictionary<TToDictionary_Key>(Func<int, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, int>(this.InnerCount, null);
            foreach(var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<int, TToDictionary_Key> keySelector, Func<int, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TToDictionary_Value>(this.InnerCount, null);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), elementSelector(item));
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, int> ToDictionary<TToDictionary_Key>(Func<int, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, int>(this.InnerCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<int, TToDictionary_Key> keySelector, Func<int, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TToDictionary_Value>(this.InnerCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), elementSelector(item));
            }
            return ret;
        }

        public RangeEnumerable Reverse()
        => CommonImplementation.ReverseReverseRange(ref this);
    }
}
