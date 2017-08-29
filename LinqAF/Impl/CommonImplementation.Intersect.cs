using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static IntersectDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> Intersect<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable: struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator: struct, IStructEnumerator<TItem>
            where TSecondEnumerable: struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator: struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return IntersectImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second);
        }

        internal static IntersectDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> IntersectImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            return new IntersectDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second);
        }

        public static IntersectSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> Intersect<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return IntersectImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, comparer);
        }

        internal static IntersectSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> IntersectImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? EqualityComparer<TItem>.Default;

            return new IntersectSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, comparer);
        }
    }
}
