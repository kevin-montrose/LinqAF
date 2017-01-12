namespace LinqAF
{
    public struct OfTypeEnumerator<TInItem, TOutItem, TInnerEnumerator>:
        IStructEnumerator<TOutItem>
        where TInnerEnumerator: struct, IStructEnumerator<TInItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        internal OfTypeEnumerator(ref TInnerEnumerator inner)
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
            while (Inner.MoveNext())
            {
                var item = Inner.Current;
                if(item is TOutItem)
                {
                    // operator explicitly boxes see:
                    // https://github.com/dotnet/corefx/blob/79e59d8b54dd0642547e8bb32303838e26ee0b06/src/System.Linq/src/System/Linq/Cast.cs#L24
                    Current = (TOutItem)(object)item;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TOutItem);
        }
    }

    public partial struct OfTypeEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TOutItem, OfTypeEnumerator<TInItem, TOutItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
    {
        TInnerEnumerable Inner;

        internal OfTypeEnumerable(ref TInnerEnumerable inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public OfTypeEnumerator<TInItem, TOutItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new OfTypeEnumerator<TInItem, TOutItem, TInnerEnumerator>(ref inner);
        }
    }
}
