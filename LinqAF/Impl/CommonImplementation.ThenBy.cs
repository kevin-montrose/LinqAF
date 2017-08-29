using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    DefaultAscending<TItem, TSecondKey>
                >
            > ThenBy<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ThenByImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(ref source, keySelector);
        }

        internal static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    DefaultAscending<TItem, TSecondKey>
                >
            > ThenByImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            var leftComparer = source.GetComparer();
            var rightComparer = new DefaultAscending<TItem, TSecondKey>(keySelector);
            var newComparer = new CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, DefaultAscending<TItem, TSecondKey>>(ref leftComparer, ref rightComparer);

            var inner = source.GetInnerEnumerable();

            return new OrderByEnumerable<TItem, CompoundKey<TFirstKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, DefaultAscending<TItem, TSecondKey>>>(ref inner, ref newComparer);
        }

        public static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    SingleComparerAscending<TItem, TSecondKey>
                >
            > ThenBy<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector,
                IComparer<TSecondKey> comparer
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ThenByImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(ref source, keySelector, comparer);
        }

        internal static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    SingleComparerAscending<TItem, TSecondKey>
                >
            > ThenByImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector,
                IComparer<TSecondKey> comparer
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            var leftComparer = source.GetComparer();
            var rightComparer = new SingleComparerAscending<TItem, TSecondKey>(keySelector, comparer ?? Comparer<TSecondKey>.Default);
            var newComparer = new CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, SingleComparerAscending<TItem, TSecondKey>>(ref leftComparer, ref rightComparer);

            var inner = source.GetInnerEnumerable();

            return new OrderByEnumerable<TItem, CompoundKey<TFirstKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, SingleComparerAscending<TItem, TSecondKey>>>(ref inner, ref newComparer);
        }

        public static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    DefaultDescending<TItem, TSecondKey>
                >
            > ThenByDescending<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ThenByDescendingImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(ref source, keySelector);
        }

        internal static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    DefaultDescending<TItem, TSecondKey>
                >
            > ThenByDescendingImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            var leftComparer = source.GetComparer();
            var rightComparer = new DefaultDescending<TItem, TSecondKey>(keySelector);
            var newComparer = new CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, DefaultDescending<TItem, TSecondKey>>(ref leftComparer, ref rightComparer);

            var inner = source.GetInnerEnumerable();

            return new OrderByEnumerable<TItem, CompoundKey<TFirstKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, DefaultDescending<TItem, TSecondKey>>>(ref inner, ref newComparer);
        }

        public static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    SingleComparerDescending<TItem, TSecondKey>
                >
            > ThenByDescending<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector,
                IComparer<TSecondKey> comparer
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ThenByDescendingImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(ref source, keySelector, comparer);
        }

        internal static
            OrderByEnumerable<
                TItem,
                CompoundKey<TFirstKey, TSecondKey>,
                TInnerEnumerable,
                TInnerEnumerator,
                CompoundComparer<
                    TItem,
                    TFirstKey,
                    TComparer,
                    TSecondKey,
                    SingleComparerDescending<TItem, TSecondKey>
                >
            > ThenByDescendingImpl<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector,
                IComparer<TSecondKey> comparer
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            var leftComparer = source.GetComparer();
            var rightComparer = new SingleComparerDescending<TItem, TSecondKey>(keySelector, comparer ?? Comparer<TSecondKey>.Default);
            var newComparer = new CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, SingleComparerDescending<TItem, TSecondKey>>(ref leftComparer, ref rightComparer);

            var inner = source.GetInnerEnumerable();

            return new OrderByEnumerable<TItem, CompoundKey<TFirstKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, SingleComparerDescending<TItem, TSecondKey>>>(ref inner, ref newComparer);
        }

        public static
            OrderByEnumerable<
                TItem, 
                CompoundKey<TFirstKey, TSecondKey>, 
                TInnerEnumerable, 
                TInnerEnumerator, 
                CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, ConfiguredComparer<TItem, TSecondKey>>
            > CreateOrderedEnumerable<TItem, TFirstKey, TComparer, TOuterEnumerable, TOuterEnumerator, TInnerEnumerable, TInnerEnumerator, TSecondKey>(
                ref TOuterEnumerable source,
                Func<TItem, TSecondKey> keySelector, 
                IComparer<TSecondKey> comparer, 
                bool descending
            )
            where TOuterEnumerable : struct, IStructEnumerable<TItem, TOuterEnumerator>, IHasComparer<TItem, TFirstKey, TComparer, TInnerEnumerable, TInnerEnumerator>
            where TOuterEnumerator : struct, IStructEnumerator<TItem>
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
            where TComparer : struct, IStructComparer<TItem, TFirstKey>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            comparer = comparer ?? Comparer<TSecondKey>.Default;

            var configured = new ConfiguredComparer<TItem, TSecondKey>(keySelector, comparer, descending);
            var first = source.GetComparer();
            
            var compound = new CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, ConfiguredComparer<TItem, TSecondKey>>(ref first, ref configured);

            var inner = source.GetInnerEnumerable();

            return
                new OrderByEnumerable<TItem, CompoundKey<TFirstKey, TSecondKey>, TInnerEnumerable, TInnerEnumerator, CompoundComparer<TItem, TFirstKey, TComparer, TSecondKey, ConfiguredComparer<TItem, TSecondKey>>>(
                    ref inner,
                    ref compound
                );
        }
    }
}
