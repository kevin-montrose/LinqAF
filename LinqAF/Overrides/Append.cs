using LinqAF.Impl;

namespace LinqAF
{
    public partial struct AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        // DefaultIfEmpty

        public AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        public AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        // Last

        public TItem Last()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this.LastItem;
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this.LastItem;
        }
    }
}
