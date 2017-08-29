using LinqAF.Config;
using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static Dictionary<TKey, TItem> ToDictionary<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ToDictionaryImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector);
        }

        public static Dictionary<TKey, TItem> ToDictionary<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ToDictionaryImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector, comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TItem, TKey, TValue, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return ToDictionaryImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TItem, TKey, TValue, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return ToDictionaryImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, comparer);
        }

        internal static Dictionary<TKey, TItem> ToDictionaryImpl<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = Allocator.Current.GetEmptyDictionary<TKey, TItem>(null, null);
            ToDictionaryLoopImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, ret, keySelector);

            return ret;
        }

        internal static Dictionary<TKey, TItem> ToDictionaryImpl<TItem, TKey, TEnumerable, TEnumerator>(
           ref TEnumerable source,
           Func<TItem, TKey> keySelector,
           IEqualityComparer<TKey> comparer
       )
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = Allocator.Current.GetEmptyDictionary<TKey, TItem>(null, comparer);
            ToDictionaryLoopImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, ret, keySelector);

            return ret;
        }

        internal static Dictionary<TKey, TValue> ToDictionaryImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = Allocator.Current.GetEmptyDictionary<TKey, TValue>(null, null);
            ToDictionaryLoopImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(ref source, ret, keySelector, elementSelector);

            return ret;
        }

        internal static Dictionary<TKey, TValue> ToDictionaryImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = Allocator.Current.GetEmptyDictionary<TKey, TValue>(null, comparer);
            ToDictionaryLoopImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(ref source, ret, keySelector, elementSelector);

            return ret;
        }

        internal static void ToDictionaryLoopImpl<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Dictionary<TKey, TItem> ret,
            Func<TItem, TKey> keySelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = item;

                ret.Add(key, element);
            }
        }

        internal static void ToDictionaryLoopImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Dictionary<TKey, TValue> ret,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = elementSelector(item);

                ret.Add(key, element);
            }
        }
    }
}