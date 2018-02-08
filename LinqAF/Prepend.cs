namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct PrependEnumerator<TItem, TInnerEnumerator>:
        IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }
        
        bool FirstYielded;
        TInnerEnumerator Inner;
        TItem FirstItem;
        internal PrependEnumerator(TItem firstItem, ref TInnerEnumerator inner)
        {
            Current = default(TItem);
            FirstYielded = false;
            FirstItem = firstItem;
            Inner = inner;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public bool MoveNext()
        {
            if (!FirstYielded)
            {
                Current = FirstItem;
                FirstYielded = true;
                return true;
            }

            if (Inner.MoveNext())
            {
                Current = Inner.Current;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Current = default(TItem);
            FirstYielded = false;
            Inner.Reset();
        }

        public void Dispose()
        {
            Current = default(TItem);
            FirstYielded = false;
            Inner.Dispose();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, PrependEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TItem FirstItem;
        TInnerEnumerable Inner;

        internal PrependEnumerable(TItem firstItem, ref TInnerEnumerable inner)
        {
            FirstItem = firstItem;
            Inner = inner;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public PrependEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new PrependEnumerator<TItem, TInnerEnumerator>(FirstItem, ref e);
        }
    }
}
