using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static Dictionary<TKey, TValue> ToDictionary<TItem, TKey, TValue, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return ToDictionaryImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, comparer);
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
            var ret = new Dictionary<TKey, TValue>(comparer);
            ToDictionaryLoopImpl<TItem, TKey, TValue, TEnumerable, TEnumerator>(ref source, ret, keySelector, elementSelector);

            return ret;
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