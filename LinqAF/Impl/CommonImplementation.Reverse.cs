using MiscUtil;
using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static ReverseEnumerable<TItem, TEnumerable, TEnumerator> Reverse<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return ReverseImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static ReverseEnumerable<TItem, TEnumerable, TEnumerator> ReverseImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new ReverseEnumerable<TItem, TEnumerable, TEnumerator>(ref source);
        }

        public static ReverseRangeEnumerable ReverseRange(ref RangeEnumerable source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            var newStart = (source.Start + source.InnerCount) - 1;

            return new ReverseRangeEnumerable(Enumerable.ReverseRangeSigil, newStart, source.InnerCount);
        }

        public static RangeEnumerable ReverseReverseRange(ref ReverseRangeEnumerable source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            // Range(start, count)
            // ReverseRange(start, count)
            // Range(1, 3) -> 1, 2, 3
            // ReverseRange(3, 3) -> 3, 2, 1
            
            var newStart = (source.Start - source.InnerCount) + 1;

            return new RangeEnumerable(Enumerable.RangeSigil, newStart, source.InnerCount);
        }
    }
}
