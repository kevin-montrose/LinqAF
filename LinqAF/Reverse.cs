using System;

namespace LinqAF
{
    public struct ReverseEnumerator<TItem> : IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TItem[] Inner;
        int Index;
        internal ReverseEnumerator(TItem[] inner)
        {
            Inner = inner;
            Index = inner.Length - 1;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return
                Inner == null;
        }

        public void Dispose()
        {
            Inner = null;
        }

        public bool MoveNext()
        {
            if(Index < 0)
            {
                return false;
            }

            Current = Inner[Index];
            Index--;
            return true;
        }

        public void Reset()
        {
            Index = Inner.Length - 1;
            Current = default(TItem);
        }
    }

    public partial struct ReverseEnumerable<TItem, TEnumerable, TEnumerator>:
        IStructEnumerable<TItem, ReverseEnumerator<TItem>>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        TEnumerable Inner;
        internal ReverseEnumerable(ref TEnumerable inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public ReverseEnumerator<TItem> GetEnumerator()
        {
            // we _have_ to buffer this, but not until the enumerator is actually called
            var arr = Impl.CommonImplementation.ToArrayImpl<TItem, TEnumerable, TEnumerator>(ref Inner);
            return new ReverseEnumerator<TItem>(arr);
        }
    }
}
