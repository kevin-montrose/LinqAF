using LinqAF.Config;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static LookupDefaultEnumerable<TKey, TItem> ToLookup<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ToLookupImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector);
        }

        public static LookupSpecificEnumerable<TKey, TItem> ToLookup<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return ToLookupImpl<TItem, TKey, TEnumerable, TEnumerator>(ref source, keySelector, comparer);
        }

        public static LookupDefaultEnumerable<TKey, TElement> ToLookup<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return ToLookupImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector);
        }

        public static LookupSpecificEnumerable<TKey, TElement> ToLookup<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return ToLookupImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, comparer);
        }

        internal static LookupDefaultEnumerable<TKey, TItem> ToLookupImpl<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var e = source.GetEnumerator();
            try
            {
                return ToLookupImpl(ref e, keySelector);
            }
            finally
            {
                e.Dispose();
            }
        }

        internal static LookupDefaultEnumerable<TKey, TItem> ToLookupImpl<TItem, TKey, TEnumerator>(
            ref TEnumerator source,
            Func<TItem, TKey> keySelector
        )
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var hashtable = new LookupHashtable<TKey, TItem>();
            hashtable.Initialize();

            while (source.MoveNext())
            {
                var item = source.Current;
                var key = keySelector(item);
                hashtable.Add(key, item);
            }

            return new LookupDefaultEnumerable<TKey, TItem>(ref hashtable);
        }

        internal static LookupSpecificEnumerable<TKey, TItem> ToLookupImpl<TItem, TKey, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var e = source.GetEnumerator();
            try
            {
                return ToLookupImpl(ref e, keySelector, comparer);
            }
            finally
            {
                e.Dispose();
            }
        }

        internal static LookupSpecificEnumerable<TKey, TItem> ToLookupImpl<TItem, TKey, TEnumerator>(
            ref TEnumerator source,
            Func<TItem, TKey> keySelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? EqualityComparer<TKey>.Default;

            var hashtable = new LookupHashtable<TKey, TItem>();
            hashtable.Initialize();
            
            while (source.MoveNext())
            {
                var item = source.Current;
                var key = keySelector(item);
                hashtable.Add(key, item, comparer);
            }

            return new LookupSpecificEnumerable<TKey, TItem>(ref hashtable, comparer);
        }
        
        internal static LookupDefaultEnumerable<TKey, TElement> ToLookupImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var hashtable = new LookupHashtable<TKey, TElement>();
            hashtable.Initialize();
            
            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = elementSelector(item);
                hashtable.Add(key, element);
            }

            return new LookupDefaultEnumerable<TKey, TElement>(ref hashtable);
        }

        internal static LookupSpecificEnumerable<TKey, TElement> ToLookupImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? EqualityComparer<TKey>.Default;
            var hashtable = new LookupHashtable<TKey, TElement>();
            hashtable.Initialize();
            
            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = elementSelector(item);

                hashtable.Add(key, element, comparer);
            }

            return new LookupSpecificEnumerable<TKey, TElement>(ref hashtable, comparer);
        }
    }
}