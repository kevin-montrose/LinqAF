namespace LinqAF
{
    public struct CastEnumerator<TInItem, TOutItem, TInnerEnumerator>:
        IStructEnumerator<TOutItem>
        where TInnerEnumerator: struct, IStructEnumerator<TInItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        internal CastEnumerator(ref TInnerEnumerator inner)
        {
            Inner = inner;
            Current = default(TOutItem);
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (!Inner.MoveNext()) return false;

            // this operator explicitly boxes, see:
            // https://github.com/dotnet/corefx/blob/79e59d8b54dd0642547e8bb32303838e26ee0b06/src/System.Linq/src/System/Linq/Cast.cs#L49
            var curObj = (object)Inner.Current;
            Current = (TOutItem)curObj;

            return true;
        }

        public void Reset()
        {
            Inner.Reset();
        }
    }

    public partial struct CastEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>: 
        IStructEnumerable<TOutItem, CastEnumerator<TInItem, TOutItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TInItem>
    {
        TInnerEnumerable Inner;
        internal CastEnumerable(ref TInnerEnumerable inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public CastEnumerator<TInItem, TOutItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new CastEnumerator<TInItem, TOutItem, TInnerEnumerator>(ref inner);
        }
    }
}
