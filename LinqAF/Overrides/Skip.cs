using System;

namespace LinqAF
{
    public partial struct SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Skip(int count)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            if (count <= 0)
            {
                return this;
            }

            var newCount = SkipCount + count;

            return new SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref Inner, newCount);
        }
    }
}
