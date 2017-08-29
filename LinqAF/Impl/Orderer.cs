using LinqAF.Config;
using System;

namespace LinqAF.Impl
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    struct Orderer<TItem, TKey, TEnumerator, TComparer> :
        IDisposable
        where TEnumerator : struct, IStructEnumerator<TItem>
        where TComparer : struct, IStructComparer<TItem, TKey>
    {
        const int DEFAULT_SIZE = 8;

        public int SortedUpTo { get; private set; }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
        struct Part
        {
            public int Left;
            public int Right;

            public Part(int left, int right)
            {
                Left = left;
                Right = right;
            }
        }

        public int Length;
        OrderBySortItem<TItem, TKey>[] Items;
        StructStack<Part> Stack;

        public Orderer(ref TEnumerator e, ref TComparer comparer)
        {
            SortedUpTo = -1;
            Length = -1;
            Stack = default(StructStack<Part>);
            Items = null;

            Buffer(ref e, ref comparer);
        }

        public bool IsDefaultValue()
        {
            return Items == null;
        }

        public TItem ElementAt(int ix)
        {
            if (ix < 0 || ix >= Length) throw CommonImplementation.OutOfRange(nameof(ix));

            return Items[ix].Item;
        }

        public void Advance(ref TComparer comparer)
        {
            if (Stack.IsDefaultValue())
            {
                Stack.Push(new Part(0, Length - 1));
            }

            while (Stack.Count > 0)
            {
                var next = Stack.Pop();
                var lowerInclusiveIx = next.Left;
                var upperInclusiveIx = next.Right;

                if (upperInclusiveIx > lowerInclusiveIx)
                {
                    var pivot = lowerInclusiveIx + (upperInclusiveIx - lowerInclusiveIx) / 2;
                    var pivotPosition = Partition(Items, ref comparer, lowerInclusiveIx, upperInclusiveIx);
                    Stack.Push(new Part(pivotPosition + 1, upperInclusiveIx));
                    Stack.Push(new Part(lowerInclusiveIx, pivotPosition - 1));
                }
                else
                {
                    SortedUpTo = lowerInclusiveIx;
                    return;
                }
            }
        }

        static int Partition(OrderBySortItem<TItem, TKey>[] items, ref TComparer comparer, int lowerInclusiveIx, int upperInclusiveIx)
        {
            var pVal = items[upperInclusiveIx];
            var i = lowerInclusiveIx - 1;
            for (var j = lowerInclusiveIx; j < upperInclusiveIx; j++)
            {
                var itemsJ = items[j];
                var itemsJLessOrEqualPCompareKey = comparer.Compare(itemsJ.Key, pVal.Key);
                if (itemsJLessOrEqualPCompareKey == 0)
                {
                    itemsJLessOrEqualPCompareKey = itemsJ.Index - pVal.Index;
                }
                var itemsJLessOrEqualPVal = itemsJLessOrEqualPCompareKey < 0;
                if (itemsJLessOrEqualPVal)
                {
                    i++;
                    if (i != j)
                    {
                        var itemsI = items[i];
                        items[i] = itemsJ;
                        items[j] = itemsI;
                    }
                }
            }

            var itemsIPlusOne = items[i + 1];
            var itemsUpper = items[upperInclusiveIx];
            items[i + 1] = itemsUpper;
            items[upperInclusiveIx] = itemsIPlusOne;

            return i + 1;
        }

        void Buffer(ref TEnumerator e, ref TComparer comparer)
        {
            Items = Allocator.Current.GetArray<OrderBySortItem<TItem, TKey>>(DEFAULT_SIZE);
            Length = 0;

            while (e.MoveNext())
            {
                var item = e.Current;

                if (Length >= Items.Length)
                {
                    var nextSize = CommonImplementation.NextSize(Items.Length);
                    Allocator.Current.ResizeArray(ref Items, nextSize);
                }

                Items[Length] = new OrderBySortItem<TItem, TKey> { Item = item, Key = comparer.MakeKey(item), Index = Length };
                Length++;
            }
        }

        public void Dispose()
        {
            Items = null;
            Stack.Dispose();
        }
    }
}
