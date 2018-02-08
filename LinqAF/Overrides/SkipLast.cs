using LinqAF.Impl;

namespace LinqAF
{
    public partial struct SkipLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public SkipLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> SkipLast(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if(count < 0)
            {
                count = 0;
            }

            var newCount = this.InnerCount + count;

            return new SkipLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref Inner, newCount);
        }
    }
}
