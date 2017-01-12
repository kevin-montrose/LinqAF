using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static GroupByDefaultEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator> GroupBy<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return GroupByImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector);
        }

        internal static GroupByDefaultEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator> GroupByImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new GroupByDefaultEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector);
        }

        public static GroupBySpecificEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator> GroupBy<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return GroupByImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, comparer ?? EqualityComparer<TKey>.Default);
        }

        internal static GroupBySpecificEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator> GroupByImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new GroupBySpecificEnumerable<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, comparer);
        }

        public static GroupByCollectionDefaultEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupBy<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return GroupByImpl<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, resultSelector);
        }

        internal static GroupByCollectionDefaultEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupByImpl<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new GroupByCollectionDefaultEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, resultSelector);
        }

        public static GroupByCollectionSpecificEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupBy<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return GroupByImpl<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, resultSelector, comparer ?? EqualityComparer<TKey>.Default);
        }

        internal static GroupByCollectionSpecificEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator> GroupByImpl<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new GroupByCollectionSpecificEnumerable<TItem, TKey, TElement, TResult, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, resultSelector, comparer);
        }
    }
}
