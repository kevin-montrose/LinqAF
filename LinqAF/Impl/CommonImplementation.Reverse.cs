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
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return ReverseImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static ReverseEnumerable<TItem, TEnumerable, TEnumerator> ReverseImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new ReverseEnumerable<TItem, TEnumerable, TEnumerator>(ref source);
        }

        public static ReverseRangeEnumerable<TItem> ReverseRange<TItem>(ref RangeEnumerable<TItem> source)
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            var newStart = Operator.Subtract(Operator.Add(source.Start, Operator.Convert<int, TItem>(source.InnerCount)), RangeEnumerator<TItem>.One);

            return new ReverseRangeEnumerable<TItem>(Enumerable.ReverseRangeSigil, newStart, source.InnerCount);
        }

        public static RangeEnumerable<TItem> ReverseReverseRange<TItem>(ref ReverseRangeEnumerable<TItem> source)
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            // Range(start, count)
            // ReverseRange(start, count)
            // Range(1, 3) -> 1, 2, 3
            // ReverseRange(3, 3) -> 3, 2, 1
            
            var newStart = Operator.Add(Operator.Subtract(source.Start, Operator.Convert<int, TItem>(source.InnerCount)), RangeEnumerator<TItem>.One);

            return new RangeEnumerable<TItem>(Enumerable.RangeSigil, newStart, source.InnerCount);
        }
    }
}
