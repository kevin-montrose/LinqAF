namespace LinqAF
{
    public struct OneItemDefaultEnumerator<TItem> :
        IStructEnumerator<TItem>
    {
        public TItem Current => default(TItem);

        byte Sigil;
        bool Ended;
        internal OneItemDefaultEnumerator(byte sigil)
        {
            Sigil = sigil;
            Ended = false;
        }

        public void Dispose() { }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public bool MoveNext()
        {
            if (Ended) return false;

            // Current doesn't need to be set, it's hard wired
            Ended = true;
            return true;
        }

        public void Reset()
        {
            // Current doesn't need to be reset
            Ended = false;
        }
    }

    public partial struct OneItemDefaultEnumerable<TItem>:
        IStructEnumerable<TItem, OneItemDefaultEnumerator<TItem>>
    {
        internal static readonly OneItemDefaultEnumerable<TItem> Instance = new OneItemDefaultEnumerable<TItem>(Enumerable.OneItemSigil);

        byte Sigil;
        private OneItemDefaultEnumerable(byte sigil)
        {
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public OneItemDefaultEnumerator<TItem> GetEnumerator() => new OneItemDefaultEnumerator<TItem>(Enumerable.OneItemSigil);
    }

    public struct OneItemSpecificEnumerator<TItem> :
        IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        byte Sigil;
        TItem Item;
        bool Ended;
        internal OneItemSpecificEnumerator(byte sigil, TItem item)
        {
            Current = default(TItem);
            Item = item;
            Sigil = sigil;
            Ended = false;
        }

        public void Dispose()
        {
            Item = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public bool MoveNext()
        {
            if (Ended) return false;

            Current = Item;
            Ended = true;
            return true;
        }

        public void Reset()
        {
            Current = default(TItem);
            Ended = false;
        }
    }

    public partial struct OneItemSpecificEnumerable<TItem> :
        IStructEnumerable<TItem, OneItemSpecificEnumerator<TItem>>
    {
        internal TItem Item;
        byte Sigil;
        internal OneItemSpecificEnumerable(byte sigil, TItem item)
        {
            Item = item;
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public OneItemSpecificEnumerator<TItem> GetEnumerator() => new OneItemSpecificEnumerator<TItem>(Enumerable.OneItemSigil, Item);
    }
}
