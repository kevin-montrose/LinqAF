using LinqAF.Config;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static HashSet<TItem> ToHashSet<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return ToHashSetImpl<TItem, TEnumerable, TEnumerator>(ref source, null);
        }

        public static HashSet<TItem> ToHashSet<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return ToHashSetImpl<TItem, TEnumerable, TEnumerator>(ref source, comparer);
        }

        internal static HashSet<TItem> ToHashSetImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = Allocator.Current.GetEmptyHashSet<TItem>(comparer);
            foreach(var item in source)
            {
                ret.Add(item);
            }

            return ret;
        }
    }
}
