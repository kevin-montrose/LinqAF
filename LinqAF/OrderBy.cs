using LinqAF.Impl;
using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct OrderByEnumerator<TItem, TKey, TInnerEnumerator, TComparer> :
        IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
        where TComparer : struct, IStructComparer<TItem, TKey>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        TComparer Comparer;

        int ToYield;
        Orderer<TItem, TKey, TInnerEnumerator, TComparer> Orderer;

        internal OrderByEnumerator(ref TInnerEnumerator inner, ref TComparer comparer)
        {
            Inner = inner;
            Comparer = comparer;
            Current = default(TItem);

            ToYield = 0;
            Orderer = default(Orderer<TItem, TKey, TInnerEnumerator, TComparer>);
        }

        public bool IsDefaultValue()
        {
            return Comparer.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
            Orderer.Dispose();
        }

        public bool MoveNext()
        {
            if (Orderer.IsDefaultValue())
            {
                Orderer = new Orderer<TItem, TKey, TInnerEnumerator, TComparer>(ref Inner, ref Comparer);
            }

            if (ToYield >= Orderer.Length) return false;

            while (Orderer.SortedUpTo < ToYield)
            {
                Orderer.Advance(ref Comparer);
            }

            var toYield = ToYield;
            Current = Orderer.ElementAt(toYield);
            ToYield++;

            return true;
        }

        public void Reset()
        {
            ToYield = 0;
            Current = default(TItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct OrderByEnumerable<TItem, TKey, TInnerEnumerable, TInnerEnumerator, TComparer>:
        IStructEnumerable<TItem, OrderByEnumerator<TItem, TKey, TInnerEnumerator, TComparer>>,
        IHasComparer<TItem, TKey, TComparer, TInnerEnumerable, TInnerEnumerator>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
        where TComparer: struct, IStructComparer<TItem, TKey>
    {
        TInnerEnumerable Inner;
        TComparer Comparer;
        internal OrderByEnumerable(ref TInnerEnumerable inner, ref TComparer comparer)
        {
            Inner = inner;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer.IsDefaultValue();
        }

        public OrderByEnumerator<TItem, TKey, TInnerEnumerator, TComparer> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new OrderByEnumerator<TItem, TKey, TInnerEnumerator, TComparer>(ref e, ref Comparer);
        }

        TComparer IHasComparer<TItem, TKey, TComparer, TInnerEnumerable, TInnerEnumerator>.GetComparer() => Comparer;

        TInnerEnumerable IHasComparer<TItem, TKey, TComparer, TInnerEnumerable, TInnerEnumerator>.GetInnerEnumerable() => Inner;
    }
}
