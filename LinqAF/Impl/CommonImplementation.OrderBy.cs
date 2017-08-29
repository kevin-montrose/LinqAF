using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultAscending<TItem, TKey>> OrderBy<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return OrderByImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector);
        }

        internal static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultAscending<TItem, TKey>> OrderByImpl<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector)
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var bridge = new DefaultAscending<TItem, TKey>(keySelector);

            return new OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultAscending<TItem, TKey>>(ref source, ref bridge);
        }

        public static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerAscending<TItem, TKey>> OrderBy<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return OrderByImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector, comparer);
        }

        internal static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerAscending<TItem, TKey>> OrderByImpl<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? Comparer<TKey>.Default;
            var bridge = new SingleComparerAscending<TItem, TKey>(keySelector, comparer);

            return new OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerAscending<TItem, TKey>>(ref source, ref bridge);
        }

        public static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultDescending<TItem, TKey>> OrderByDescending<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return OrderByDescendingImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector);
        }

        internal static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultDescending<TItem, TKey>> OrderByDescendingImpl<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector)
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var bridge = new DefaultDescending<TItem, TKey>(keySelector);

            return new OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, DefaultDescending<TItem, TKey>>(ref source, ref bridge);
        }

        public static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerDescending<TItem, TKey>> OrderByDescending<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return OrderByDescendingImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector, comparer);
        }

        internal static OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerDescending<TItem, TKey>> OrderByDescendingImpl<TItem, TKey, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? Comparer<TKey>.Default;
            var bridge = new SingleComparerDescending<TItem, TKey>(keySelector, comparer);

            return new OrderByEnumerable<TItem, TKey, TEnumerable, TEnumerator, SingleComparerDescending<TItem, TKey>>(ref source, ref bridge);
        }
    }
}
