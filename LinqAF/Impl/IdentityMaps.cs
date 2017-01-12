using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static class IdentyMapsNonGeneric
    {
        internal static Func<System.Collections.IEnumerable, IdentityEnumerator> IEnumerable = e => new IdentityEnumerator(e);
    }

    static class IdentityMaps<TItem>
    {
        internal static Func<IEnumerable<TItem>, IdentityEnumerator<TItem>> IEnumerable = e => new IdentityEnumerator<TItem>(e);
        internal static Func<HashSet<TItem>, HashSetEnumerator<TItem>> HashSet = e => new HashSetEnumerator<TItem>(e);
        internal static Func<LinkedList<TItem>, LinkedListEnumerator<TItem>> LinkedList = e => new LinkedListEnumerator<TItem>(e);
        internal static Func<List<TItem>, ListEnumerator<TItem>> List = e => new ListEnumerator<TItem>(e);
        internal static Func<SortedSet<TItem>, SortedSetEnumerator<TItem>> SortedSet = e => new SortedSetEnumerator<TItem>(e);
        internal static Func<Stack<TItem>, StackEnumerator<TItem>> Stack = e => new StackEnumerator<TItem>(e);
        internal static Func<Queue<TItem>, QueueEnumerator<TItem>> Queue = e => new QueueEnumerator<TItem>(e);
        internal static Func<TItem[], ArrayEnumerator<TItem>> Array = e => new ArrayEnumerator<TItem>(e);
    }

    static class IdentityMaps<TKey, TValue>
    {
        internal static Func<Dictionary<TKey, TValue>, DictionaryEnumerator<TKey, TValue>> Dictionary = e => new DictionaryEnumerator<TKey, TValue>(e);
        internal static Func<Dictionary<TKey, TValue>.KeyCollection, DictionaryKeysEnumerator<TKey, TValue>> DictionaryKeys = e => new DictionaryKeysEnumerator<TKey, TValue>(e);
        internal static Func<Dictionary<TKey, TValue>.ValueCollection, DictionaryValuesEnumerator<TKey, TValue>> DictionaryValues = e => new DictionaryValuesEnumerator<TKey, TValue>(e);
        internal static Func<SortedDictionary<TKey, TValue>, SortedDictionaryEnumerator<TKey, TValue>> SortedDictionary = e => new SortedDictionaryEnumerator<TKey, TValue>(e);
        internal static Func<SortedDictionary<TKey, TValue>.KeyCollection, SortedDictionaryKeysEnumerator<TKey, TValue>> SortedDictionaryKeys = e => new SortedDictionaryKeysEnumerator<TKey, TValue>(e);
        internal static Func<SortedDictionary<TKey, TValue>.ValueCollection, SortedDictionaryValuesEnumerator<TKey, TValue>> SortedDictionaryValues = e => new SortedDictionaryValuesEnumerator<TKey, TValue>(e);
    }
}