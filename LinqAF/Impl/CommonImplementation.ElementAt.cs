using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // Non-Default
        public static TItem ElementAt<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int index)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (index < 0) throw CommonImplementation.OutOfRange(nameof(index));

            return ElementAtImpl<TItem, TEnumerable, TEnumerator>(ref source, index);
        }

        internal static TItem ElementAtImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int index)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var curIndex = 0;
            foreach (var item in source)
            {
                if (curIndex == index) return item;
                curIndex++;
            }

            throw CommonImplementation.OutOfRange(nameof(index));
        }


        // Default
        public static TItem ElementAtOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int index)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (index < 0) return default(TItem);

            return ElementAtOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source, index);
        }

        internal static TItem ElementAtOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, int index)
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var curIndex = 0;
            foreach (var item in source)
            {
                if (curIndex == index) return item;
                curIndex++;
            }

            return default(TItem);
        }
    }
}
