using LinqAF.Impl;
using MiscUtil;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct RangeEnumerable<TItem>
    {
        public int Count()
        {
            if(IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return InnerCount;
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return InnerCount;
        }

        public bool Any()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return InnerCount >= 0;
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            
            if(index >= InnerCount || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return Operator.Add(Start, Operator.Convert<int, TItem>(index));
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (index >= InnerCount || index < 0)
            {
                return default(TItem);
            }

            return Operator.Add(Start, Operator.Convert<int, TItem>(index));
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            var min = this.Start;
            var max = Operator.Add(min, Operator.Convert<int, TItem>(this.InnerCount));

            if (Operator.LessThan(value, min) || Operator.GreaterThanOrEqual(value, max)) return false;

            return true;
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (this.InnerCount == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }

            return this.Start;
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (this.InnerCount == 0)
            {
                return default(TItem);
            }

            return this.Start;
        }
        
        public TItem Last()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (this.InnerCount == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }

            var ret = Operator.Add(this.Start, Operator.Convert<int, TItem>(this.InnerCount - 1));

            return ret;
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (this.InnerCount == 0)
            {
                return default(TItem);
            }

            var ret = Operator.Add(this.Start, Operator.Convert<int, TItem>(this.InnerCount - 1));

            return ret;
        }

        public TItem Max() => Last();


        public TItem Min() => First();


        public bool SequenceEqual(EmptyEnumerable<TItem> second) => SequenceEqual(second, null);
        
        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return this.InnerCount == 0;
        }
        
        public bool SequenceEqual(RangeEnumerable<TItem> second) => SequenceEqual(second, null);

        public bool SequenceEqual(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            if (!Operator.Equal(this.InnerCount, second.InnerCount)) return false;

            comparer = comparer ?? EqualityComparer<TItem>.Default;

            return comparer.Equals(this.Start, second.Start);
        }

        public bool SequenceEqual(RepeatEnumerable<TItem> second) => SequenceEqual(second, null);

        public bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

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
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (this.InnerCount == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }

            if(this.InnerCount > 1)
            {
                throw new InvalidOperationException("Sequence contained multiple elements");
            }

            return this.Start;
        }

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (this.InnerCount == 0)
            {
                return default(TItem);
            }

            if (this.InnerCount > 1)
            {
                throw new InvalidOperationException("Sequence contained multiple elements");
            }

            return this.Start;
        }

        public RangeEnumerable<TItem> Skip(int count)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (count < 0)
            {
                count = 0;
            }

            var tCount = Operator.Convert<int, TItem>(count);

            var newStart = Operator.Add(this.Start, tCount);
            var newCount = this.InnerCount - count;

            if (newCount < 0)
            {
                newCount = 0;
            }

            return new RangeEnumerable<TItem>(Enumerable.RangeSigil, newStart, newCount);
        }

        public RangeEnumerable<TItem> Take(int count)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (count < 0)
            {
                count = 0;
            }

            if (count > this.InnerCount)
            {
                count = this.InnerCount;
            }

            var tCount = Operator.Convert<int, TItem>(count);
            var newCount = count;

            if (newCount < 0)
            {
                newCount = 0;
            }

            return new RangeEnumerable<TItem>(Enumerable.RangeSigil, this.Start, newCount);
        }

        public RangeEnumerable<TItem> Distinct()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            var ret = new List<TItem>(this.InnerCount);
            foreach(var item in this)
            {
                ret.Add(item);
            }

            return ret;
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            var ret = new TItem[this.InnerCount];
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
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var ret = new Dictionary<TToDictionary_Key, TItem>(this.InnerCount);
            foreach(var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var ret = new Dictionary<TToDictionary_Key, TToDictionary_Value>(this.InnerCount);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), elementSelector(item));
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var ret = new Dictionary<TToDictionary_Key, TItem>(this.InnerCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var ret = new Dictionary<TToDictionary_Key, TToDictionary_Value>(this.InnerCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), elementSelector(item));
            }
            return ret;
        }

        public ReverseRangeEnumerable<TItem> Reverse()
        => CommonImplementation.ReverseRange<TItem>(ref this);
    }
}
