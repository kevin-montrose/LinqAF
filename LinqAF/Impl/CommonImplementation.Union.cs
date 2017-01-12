using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static UnionDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> Union<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable: struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator: struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return UnionImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second);
        }

        internal static UnionDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> UnionImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator: struct, IStructEnumerator<TItem>
        {
            return new UnionDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second);
        }

        public static UnionSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> Union<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return UnionImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, comparer);
        }

        internal static UnionSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> UnionImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            return new UnionSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, comparer);
        }
    }
}
