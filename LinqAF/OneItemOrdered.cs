using LinqAF.Impl;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct OneItemDefaultOrderedEnumerator<TItem> :
        IStructEnumerator<TItem>
    {
        public TItem Current => default(TItem);

        byte Sigil;
        bool Finished;
        internal OneItemDefaultOrderedEnumerator(byte sigil)
        {
            Sigil = sigil;
            Finished = false;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (Finished) return false;

            Finished = true;
            return true;
        }

        public void Reset()
        {
            Finished = false;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct OneItemDefaultOrderedEnumerable<TItem> :
        IStructEnumerable<TItem, OneItemDefaultOrderedEnumerator<TItem>>,
        IHasComparer<TItem, object, EmptyComparer<TItem>, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>
    {
        internal static readonly OneItemDefaultOrderedEnumerable<TItem> Instance = new OneItemDefaultOrderedEnumerable<TItem>(Enumerable.OneItemSigil);

        byte Sigil;
        private OneItemDefaultOrderedEnumerable(byte sigil)
        {
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public OneItemDefaultOrderedEnumerator<TItem> GetEnumerator() => new OneItemDefaultOrderedEnumerator<TItem>(Sigil);

        EmptyComparer<TItem> IHasComparer<TItem, object, EmptyComparer<TItem>, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>.GetComparer() => EmptyCache<TItem>.EmptyComparer;

        OneItemDefaultEnumerable<TItem> IHasComparer<TItem, object, EmptyComparer<TItem>, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>.GetInnerEnumerable() => OneItemDefaultEnumerable<TItem>.Instance;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct OneItemSpecificOrderedEnumerator<TItem> :
        IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        byte Sigil;
        bool Finished;
        TItem Item;
        internal OneItemSpecificOrderedEnumerator(byte sigil, TItem item)
        {
            Sigil = sigil;
            Current = default(TItem);
            Finished = false;
            Item = item;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (Finished) return false;

            Current = Item;
            Finished = true;
            return true;
        }

        public void Reset()
        {
            Finished = false;
            Current = default(TItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct OneItemSpecificOrderedEnumerable<TItem> :
        IStructEnumerable<TItem, OneItemSpecificOrderedEnumerator<TItem>>,
        IHasComparer<TItem, object, EmptyComparer<TItem>, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>
    {
        byte Sigil;
        internal TItem Item;
        internal OneItemSpecificOrderedEnumerable(byte sigil, TItem item)
        {
            Sigil = sigil;
            Item = item;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public OneItemSpecificOrderedEnumerator<TItem> GetEnumerator() => new OneItemSpecificOrderedEnumerator<TItem>(Sigil, Item);

        EmptyComparer<TItem> IHasComparer<TItem, object, EmptyComparer<TItem>, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>.GetComparer() => EmptyCache<TItem>.EmptyComparer;

        OneItemSpecificEnumerable<TItem> IHasComparer<TItem, object, EmptyComparer<TItem>, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>.GetInnerEnumerable() => new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
    }
}
