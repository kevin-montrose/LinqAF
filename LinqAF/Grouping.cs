using LinqAF.Impl;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct GroupingEnumerator<TElement> :
        IStructEnumerator<TElement>
    {
        public TElement Current { get; private set; }

        TElement[] Elements;
        int[] Indexes;
        uint Count_SingleValue;
        int Index;

        internal GroupingEnumerator(TElement[] elements, int[] indexes, uint count)
        {
            Index = 0;
            Elements = elements;
            Indexes = indexes;
            Count_SingleValue = count;
            Current = default(TElement);
        }

        public bool IsDefaultValue() => Elements == null;

        public void Dispose()
        {
            Elements = null;
            Indexes = null;
            Count_SingleValue = 0;
            Current = default(TElement);
        }

        public bool MoveNext()
        {
            if(Indexes == null)
            {
                // handle the single value case
                if (Index > 0) return false;

                var ix = (int)(Count_SingleValue - 1);
                Current = Elements[ix];
                Index++;
                return true;
            }

            if (Index == Count_SingleValue)
            {
                return false;
            }

            var nextIndex = Indexes[Index];
            Current = Elements[nextIndex];
            Index++;

            return true;
        }

        public void Reset()
        {
            Index = 0;
            Current = default(TElement);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct GroupingEnumerable<TKey, TElement> :
        IStructEnumerable<TElement, GroupingEnumerator<TElement>>
    {
        public TKey Key { get; private set; }

        TElement[] Elements;
        uint UsedIndexes_SingleIndex;
        int[] ElementIndexes;

        internal GroupingEnumerable(TKey key, uint count_singleIndex, int[] indexes, ref IndexedItemContainer<TElement> container)
        {
            Key = key;
            UsedIndexes_SingleIndex = count_singleIndex;
            ElementIndexes = indexes;
            Elements = container.Items;
        }

        public bool IsDefaultValue() => Elements == null;

        public GroupingEnumerator<TElement> GetEnumerator()
        {
            return new GroupingEnumerator<TElement>(Elements, ElementIndexes, UsedIndexes_SingleIndex);
        }

        public override bool Equals(object obj) => false;
        public override int GetHashCode()
        {
            var ret = 23 + Key.GetHashCode();
            ret *= 17;
            ret += (int)UsedIndexes_SingleIndex;

            if (ElementIndexes == null)
            {
                ret *= 17;
                ret += Elements[UsedIndexes_SingleIndex - 1].GetHashCode();
            }
            else
            {
                for(var i = 0; i < UsedIndexes_SingleIndex; i++)
                {
                    ret *= 17;
                    ret += Elements[ElementIndexes[i]].GetHashCode();
                }
            }

            return ret;
        }
    }
}