using LinqAF.Impl;
using System;

namespace LinqAF
{
    public static class Enumerable
    {
        internal const byte RangeSigil = 1;
        internal const byte ReverseRangeSigil = 1;
        internal const byte RepeatSigil = 1;
        internal const byte EmptySigil = 1;
        internal const byte EmptyOrderedSigil = 1;
        internal const byte EmptyComparerSigil = 1;
        internal const byte OneItemSigil = 1;
        
        public static RangeEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            long lastItem = start;
            lastItem += count;
            lastItem -= 1;

            if (lastItem > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return new RangeEnumerable<int>(RangeSigil, start, count);
        }

        public static EmptyEnumerable<TItem> Empty<TItem>() => EmptyCache<TItem>.Empty;

        public static RepeatEnumerable<TItem> Repeat<TItem>(TItem item, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            return new RepeatEnumerable<TItem>(RepeatSigil, item, count);
        }
    }
}
