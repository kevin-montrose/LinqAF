using System;
using System.Collections.Generic;

namespace LinqAF
{
    public interface IStructComparer<TItem, TCompositeKey> : IComparer<TCompositeKey>
    {
        bool IsDefaultValue();
        TCompositeKey MakeKey(TItem item);
    }

    public struct DefaultAscending<TItem, TCompareOn> : IStructComparer<TItem, TCompareOn>
    {
        Func<TItem, TCompareOn> Selector;

        internal DefaultAscending(Func<TItem, TCompareOn> selector)
        {
            Selector = selector;
        }

        public bool IsDefaultValue()
        {
            return
                Selector == null;
        }

        public TCompareOn MakeKey(TItem item) => Selector(item);

        public int Compare(TCompareOn x, TCompareOn y)
        {
            return Comparer<TCompareOn>.Default.Compare(x, y);
        }
    }

    public struct DefaultDescending<TItem, TCompareOn> : IStructComparer<TItem, TCompareOn>
    {
        Func<TItem, TCompareOn> Selector;

        internal DefaultDescending(Func<TItem, TCompareOn> selector)
        {
            Selector = selector;
        }

        public bool IsDefaultValue()
        {
            return
                Selector == null;
        }

        public TCompareOn MakeKey(TItem item) => Selector(item);

        public int Compare(TCompareOn x, TCompareOn y)
        {
            return Comparer<TCompareOn>.Default.Compare(y, x);
        }
    }

    public struct SingleComparerAscending<TItem, TCompareOn> : IStructComparer<TItem, TCompareOn>
    {
        Func<TItem, TCompareOn> Selector;
        IComparer<TCompareOn> Inner;

        internal SingleComparerAscending(Func<TItem, TCompareOn> selector, IComparer<TCompareOn> inner)
        {
            Selector = selector;
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return
                Selector == null &&
                Inner == null;
        }

        public TCompareOn MakeKey(TItem item) => Selector(item);

        public int Compare(TCompareOn x, TCompareOn y)
        {
            return Inner.Compare(x, y);
        }
    }

    public struct SingleComparerDescending<TItem, TCompareOn> : IStructComparer<TItem, TCompareOn>
    {
        Func<TItem, TCompareOn> Selector;
        IComparer<TCompareOn> Inner;

        internal SingleComparerDescending(Func<TItem, TCompareOn> selector, IComparer<TCompareOn> inner)
        {
            Selector = selector;
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return
                Selector == null &&
                Inner == null;
        }

        public TCompareOn MakeKey(TItem item) => Selector(item);

        public int Compare(TCompareOn x, TCompareOn y)
        {
            return Inner.Compare(y, x);
        }
    }

    public struct CompoundKey<TFirstKey, TSecondKey>
    {
        public TFirstKey FirstKey;
        public TSecondKey SecondKey;

        public CompoundKey(TFirstKey first, TSecondKey second)
        {
            FirstKey = first;
            SecondKey = second;
        }
    }

    public struct CompoundComparer<TItem, TFirstKey, TFirstComparer, TSecondKey, TSecondComparer> :
        IStructComparer<TItem, CompoundKey<TFirstKey, TSecondKey>>
        where TFirstComparer : struct, IStructComparer<TItem, TFirstKey>
        where TSecondComparer : struct, IStructComparer<TItem, TSecondKey>
    {
        TFirstComparer First;
        TSecondComparer Second;

        internal CompoundComparer(ref TFirstComparer first, ref TSecondComparer second)
        {
            First = first;
            Second = second;
        }

        public bool IsDefaultValue()
        {
            return First.IsDefaultValue() && Second.IsDefaultValue();
        }

        public CompoundKey<TFirstKey, TSecondKey> MakeKey(TItem item)
        {
            var first = First.MakeKey(item);
            var second = Second.MakeKey(item);

            return new CompoundKey<TFirstKey, TSecondKey>(first, second);
        }

        public int Compare(CompoundKey<TFirstKey, TSecondKey> x, CompoundKey<TFirstKey, TSecondKey> y)
        {
            var firstComparison = First.Compare(x.FirstKey, y.FirstKey);
            if (firstComparison != 0) return firstComparison;

            return Second.Compare(x.SecondKey, y.SecondKey);
        }
    }

    public struct EmptyComparer<TItem> :
        IStructComparer<TItem, object>
    {
        byte Sigil;
        internal EmptyComparer(byte sigil)
        {
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public object MakeKey(TItem item)
        {
            throw new InvalidOperationException($"Called {nameof(MakeKey)} on {nameof(EmptyComparer<TItem>)}");
        }

        public int Compare(object x, object y)
        {
            throw new InvalidOperationException($"Called {nameof(Compare)} on {nameof(EmptyComparer<TItem>)}");
        }
    }
}