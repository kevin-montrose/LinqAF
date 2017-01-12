using LinqAF.Impl;

namespace LinqAF
{
    public struct BoxedEnumerator<TItem> : IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }
        IBoxedEnumerator<TItem> Inner;

        internal BoxedEnumerator(IBoxedEnumerator<TItem> inner)
        {
            Inner = inner;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return Inner == null || Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (!Inner.MoveNext()) return false;

            Current = Inner.Current;
            return true;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TItem);
        }
    }

    public partial struct BoxedEnumerable<TItem>: IStructEnumerable<TItem, BoxedEnumerator<TItem>>
    {
        IBoxedEnumerable<TItem> Inner;

        internal BoxedEnumerable(IBoxedEnumerable<TItem> inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner == null || Inner.IsDefaultValue();
        }

        public BoxedEnumerator<TItem> GetEnumerator() => new BoxedEnumerator<TItem>(Inner.GetEnumerator());
    }
}
