using LinqAF.Config;
using LinqAF.Impl;
using MiscUtil;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct ReverseRangeEnumerable<TItem>
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

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if(index >= InnerCount || index < 0)
            {
                throw CommonImplementation.OutOfRange(nameof(index));
            }

            return Operator.Subtract(Start, Operator.Convert<int, TItem>(index));
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (index >= InnerCount || index < 0)
            {
                return default(TItem);
            }

            return Operator.Subtract(Start, Operator.Convert<int, TItem>(index));
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var max = this.Start;
            var min = Operator.Subtract(max, Operator.Convert<int, TItem>(this.InnerCount));

            if (Operator.LessThan(value, min) || Operator.GreaterThanOrEqual(value, max)) return false;

            return true;
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            return this.Start;
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                return default(TItem);
            }

            return this.Start;
        }
        
        public TItem Last()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            // Range(1, 3) -> 1, 2, 3
            // ReverseRange(3, 3) -> 3, 2, 1
            // Last = (Start - InnerCount) + 1

            var ret = Operator.Add(Operator.Subtract(Start, Operator.Convert<int, TItem>(InnerCount)), RangeEnumerator<TItem>.One);
            return ret;
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                return default(TItem);
            }

            // Range(1, 3) -> 1, 2, 3
            // ReverseRange(3, 3) -> 3, 2, 1
            // Last = (Start - InnerCount) + 1

            var ret = Operator.Add(Operator.Subtract(Start, Operator.Convert<int, TItem>(InnerCount)), RangeEnumerator<TItem>.One);
            return ret;
        }

        public TItem Max() => First();


        public TItem Min() => Last();


        public bool SequenceEqual(EmptyEnumerable<TItem> second) => SequenceEqual(second, null);
        
        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return this.InnerCount == 0;
        }
        
        public bool SequenceEqual(ReverseRangeEnumerable<TItem> second) => SequenceEqual(second, null);

        public bool SequenceEqual(ReverseRangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (!Operator.Equal(this.InnerCount, second.InnerCount)) return false;

            comparer = comparer ?? EqualityComparer<TItem>.Default;

            return comparer.Equals(this.Start, second.Start);
        }

        public bool SequenceEqual(RepeatEnumerable<TItem> second) => SequenceEqual(second, null);

        public bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
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
            comparer = comparer ?? EqualityComparer<TItem>.Default;

            // only equal sequences if their single element is equal
            return comparer.Equals(this.Start, second.Item);
        }
        
        public TItem Single()
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

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (this.InnerCount == 0)
            {
                return default(TItem);
            }

            if (this.InnerCount > 1)
            {
                throw CommonImplementation.MultipleElements();
            }

            return this.Start;
        }
        
        public ReverseRangeEnumerable<TItem> Skip(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            // decrement start by count
            // decrement count by count

            var newStart = Operator.Subtract(Start, Operator.Convert<int, TItem>(count));
            var newCount = InnerCount - count;
            if (newCount < 0) newCount = 0;

            return new ReverseRangeEnumerable<TItem>(Enumerable.ReverseRangeSigil, newStart, newCount);
        }

        public ReverseRangeEnumerable<TItem> Take(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            // decrement count by count

            var newCount = Math.Min(InnerCount, count);
            if (newCount < 0) newCount = 0;

            return new ReverseRangeEnumerable<TItem>(Enumerable.ReverseRangeSigil, Start, newCount);
        }

        public ReverseRangeEnumerable<TItem> Distinct()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetEmptyList<TItem>(this.InnerCount);
            foreach(var item in this)
            {
                ret.Add(item);
            }

            return ret;
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetArray<TItem>(this.InnerCount);
            var ix = 0;
            foreach(var item in this)
            {
                ret[ix] = item;
                ix++;
            }

            return ret;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TItem>(this.InnerCount, null);
            foreach(var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
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

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TItem>(this.InnerCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
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

        public RangeEnumerable<TItem> Reverse()
        => CommonImplementation.ReverseReverseRange<TItem>(ref this);
    }
}
