namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct AppendEnumerator<TItem, TInnerEnumerator>:
        IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        bool InnerExhausted;
        bool LastYielded;
        TInnerEnumerator Inner;
        TItem LastItem;
        internal AppendEnumerator(ref TInnerEnumerator inner, TItem lastItem)
        {
            InnerExhausted = LastYielded = false;
            Current = default(TItem);
            Inner = inner;
            LastItem = lastItem;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public bool MoveNext()
        {
            if (!InnerExhausted)
            {
                if (Inner.MoveNext())
                {
                    Current = Inner.Current;
                    return true;
                }

                InnerExhausted = true;
            }

            if (!LastYielded)
            {
                Current = LastItem;
                LastYielded = true;

                return true;
            }

            return false;
        }

        public void Reset()
        {
            Current = default(TItem);
            InnerExhausted = LastYielded = false;
            Inner.Reset();
        }

        public void Dispose()
        {
            Current = default(TItem);
            InnerExhausted = LastYielded = false;
            Inner.Dispose();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, AppendEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        TItem LastItem;
        internal AppendEnumerable(ref TInnerEnumerable inner, TItem lastItem)
        {
            Inner = inner;
            LastItem = lastItem;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public AppendEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new AppendEnumerator<TItem, TInnerEnumerator>(ref e, LastItem);
        }
    }
}
