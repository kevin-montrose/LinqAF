using LinqAF.Impl;

namespace LinqAF
{
    public partial struct PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        // DefaultIfEmpty

        public PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        public PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        // First

        public TItem First()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this.FirstItem;
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this.FirstItem;
        }
    }
}
