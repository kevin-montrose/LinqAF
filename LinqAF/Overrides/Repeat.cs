using LinqAF.Config;
using LinqAF.Impl;
using MiscUtil;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct RepeatEnumerable<TItem>
    {
        public bool Any()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return InnerCount > 0;
        }

        public int Count()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return InnerCount;
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return InnerCount;
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0) throw CommonImplementation.SequenceEmpty();

            return this.Item;
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0) return default(TItem);

            return this.Item;
        }

        public TItem Last()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0) throw CommonImplementation.SequenceEmpty();

            return this.Item;
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0) return default(TItem);

            return this.Item;
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (index < 0 || index >= InnerCount) throw CommonImplementation.OutOfRange(nameof(index));

            return Item;
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (index < 0 || index >= InnerCount) return default(TItem);

            return Item;
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return EqualityComparer<TItem>.Default.Equals(Item, value);
        }

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        {
            if (comparer == null) return Contains(value);
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return comparer.Equals(Item, value);
        }

        public TItem Max()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            return Item;
        }


        public TItem Min()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            return Item;
        }

        public bool SequenceEqual(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return InnerCount == 0;
        }

        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return InnerCount == 0;
        }

        public bool SequenceEqual(RepeatEnumerable<TItem> second) => SequenceEqual(second, null);

        public bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (InnerCount != second.InnerCount) return false;

            comparer = comparer ?? EqualityComparer<TItem>.Default;

            return comparer.Equals(Item, second.Item);
        }

        public TItem Single()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0)
            {
                throw CommonImplementation.SequenceEmpty();
            }

            if (InnerCount > 1)
            {
                throw CommonImplementation.MultipleElements();
            }

            return Item;
        }


        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0)
            {
                return default(TItem);
            }

            if (InnerCount > 1)
            {
                throw CommonImplementation.MultipleElements();
            }

            return Item;
        }

        public RepeatEnumerable<TItem> Skip(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (count < 0)
            {
                count = 0;
            }

            var newCount = InnerCount - count;
            if (newCount < 0)
            {
                newCount = 0;
            }

            return new RepeatEnumerable<TItem>(Enumerable.RepeatSigil, Item, newCount);
        }

        public RepeatEnumerable<TItem> Take(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (count < 0)
            {
                count = 0;
            }

            var newCount = Math.Min(InnerCount, count);

            return new RepeatEnumerable<TItem>(Enumerable.RepeatSigil, Item, newCount);
        }

        public RepeatEnumerable<TItem> Distinct()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (InnerCount == 0 || InnerCount == 1)
            {
                return this;
            }

            return new RepeatEnumerable<TItem>(Enumerable.RepeatSigil, Item, 1);
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetEmptyList<TItem>(this.InnerCount);
            for (var i = 0; i < this.InnerCount; i++)
            {
                ret.Add(this.Item);
            }

            return ret;
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetArray<TItem>(this.InnerCount);
            for (var i = 0; i < ret.Length; i++)
            {
                ret[i] = Item;
            }

            return ret;
        }

        public RepeatEnumerable<TItem> Reverse()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }
    }
}
