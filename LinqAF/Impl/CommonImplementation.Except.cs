using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static 
            ExceptDefaultEnumerable<
                TItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightEnumerable,
                TRightEnumerator
            > Except<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable first,
                ref TRightEnumerable second
            )
            where TLeftEnumerable: struct, IStructEnumerable<TItem, TLeftEnumerator>
            where TLeftEnumerator: struct, IStructEnumerator<TItem>
            where TRightEnumerable: struct, IStructEnumerable<TItem, TRightEnumerator>
            where TRightEnumerator: struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                ExceptImpl<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(
                    ref first,
                    ref second
                );
        }

        internal static
            ExceptDefaultEnumerable<
                TItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightEnumerable,
                TRightEnumerator
            > ExceptImpl<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable first,
                ref TRightEnumerable second
            )
            where TLeftEnumerable : struct, IStructEnumerable<TItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TItem>
            where TRightEnumerable : struct, IStructEnumerable<TItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TItem>
        {
            return new ExceptDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(ref first, ref second);
        }

        public static
            ExceptSpecificEnumerable<
                TItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightEnumerable,
                TRightEnumerator
            > Except<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable first,
                ref TRightEnumerable second,
                IEqualityComparer<TItem> comparer
            )
            where TLeftEnumerable : struct, IStructEnumerable<TItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TItem>
            where TRightEnumerable : struct, IStructEnumerable<TItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                ExceptImpl<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(
                    ref first,
                    ref second,
                    comparer
                );
        }

        internal static
            ExceptSpecificEnumerable<
                TItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightEnumerable,
                TRightEnumerator
            > ExceptImpl<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable first,
                ref TRightEnumerable second,
                IEqualityComparer<TItem> comparer
            )
            where TLeftEnumerable : struct, IStructEnumerable<TItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TItem>
            where TRightEnumerable : struct, IStructEnumerable<TItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? EqualityComparer<TItem>.Default;

            return new ExceptSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TRightEnumerable, TRightEnumerator>(ref first, ref second, comparer);
        }
    }
}
