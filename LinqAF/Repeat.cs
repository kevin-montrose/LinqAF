namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct RepeatEnumerator<TItem>: IStructEnumerator<TItem>
    {
        byte Sigil;
        TItem Item;
        int Count;
        int Index;

        public TItem Current { get; private set; }

        internal RepeatEnumerator(byte sigil, TItem item, int count)
        {
            Sigil = sigil;
            Item = item;
            Count = count;
            Index = 0;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public bool MoveNext()
        {
            if(Index < Count)
            {
                Current = Item;
                Index++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = default(TItem);
        }

        public void Dispose()
        {
            Item = default(TItem);
        }

    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct RepeatEnumerable<TItem>: IStructEnumerable<TItem, RepeatEnumerator<TItem>>
    {
        byte Sigil;
        internal TItem Item;
        internal int InnerCount;
        internal RepeatEnumerable(byte sigil, TItem item, int count)
        {
            Sigil = sigil;
            Item = item;
            InnerCount = count;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }
        
        public RepeatEnumerator<TItem> GetEnumerator() => new RepeatEnumerator<TItem>(Sigil, Item, InnerCount);
    }
}
