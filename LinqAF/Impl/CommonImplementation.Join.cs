using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static
            JoinDefaultEnumerable<
                TOutItem,
                TKeyItem,
                TLeftItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightItem,
                TRightEnumerable,
                TRightEnumerator
            > Join<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable outer,
                ref TRightEnumerable inner,
                Func<TLeftItem, TKeyItem> outerKeySelector,
                Func<TRightItem, TKeyItem> innerKeySelector,
                Func<TLeftItem, TRightItem, TOutItem> resultSelector
            )
            where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
            where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TRightItem>
        {
            if (outer.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                JoinImpl<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(
                    ref outer,
                    ref inner,
                    outerKeySelector,
                    innerKeySelector,
                    resultSelector
                );
        }

        internal static
            JoinDefaultEnumerable<
                TOutItem,
                TKeyItem,
                TLeftItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightItem,
                TRightEnumerable,
                TRightEnumerator
            > JoinImpl<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable outer,
                ref TRightEnumerable inner,
                Func<TLeftItem, TKeyItem> outerKeySelector,
                Func<TRightItem, TKeyItem> innerKeySelector,
                Func<TLeftItem, TRightItem, TOutItem> resultSelector
            )
            where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
            where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TRightItem>
        {
            return new JoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(ref outer, ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        public static
            JoinSpecificEnumerable<
                TOutItem,
                TKeyItem,
                TLeftItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightItem,
                TRightEnumerable,
                TRightEnumerator
            > Join<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable outer,
                ref TRightEnumerable inner,
                Func<TLeftItem, TKeyItem> outerKeySelector,
                Func<TRightItem, TKeyItem> innerKeySelector,
                Func<TLeftItem, TRightItem, TOutItem> resultSelector,
                IEqualityComparer<TKeyItem> comparer
            )
            where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
            where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TRightItem>
        {
            if (outer.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                JoinImpl<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(
                    ref outer,
                    ref inner,
                    outerKeySelector,
                    innerKeySelector,
                    resultSelector,
                    comparer
                );
        }

        internal static
            JoinSpecificEnumerable<
                TOutItem,
                TKeyItem,
                TLeftItem,
                TLeftEnumerable,
                TLeftEnumerator,
                TRightItem,
                TRightEnumerable,
                TRightEnumerator
            > JoinImpl<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(
                ref TLeftEnumerable outer,
                ref TRightEnumerable inner,
                Func<TLeftItem, TKeyItem> outerKeySelector,
                Func<TRightItem, TKeyItem> innerKeySelector,
                Func<TLeftItem, TRightItem, TOutItem> resultSelector,
                IEqualityComparer<TKeyItem> comparer
            )
            where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
            where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
            where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
            where TRightEnumerator : struct, IStructEnumerator<TRightItem>
        {
            return new JoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(ref outer, ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }
}
