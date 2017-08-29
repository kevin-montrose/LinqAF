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
            if (outer.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(outer));
            if (inner.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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
            if (outer.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(outer));
            if (inner.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));
            
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
            comparer = comparer ?? EqualityComparer<TKeyItem>.Default;

            return new JoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>(ref outer, ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }
}
