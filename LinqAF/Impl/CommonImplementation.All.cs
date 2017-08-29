using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        /*public static bool All<TItem, TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    Dictionary<TItem, TDictionaryValue>.KeyCollection,
                    DictionaryKeysEnumerator<TItem, TDictionaryValue>
                >,
                DictionaryKeysEnumerator<TItem, TDictionaryValue>
            >(ref bridge, predicate);
        }

        public static bool All<TItem, TDictionaryKey>(Dictionary<TDictionaryKey, TItem>.ValueCollection source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    Dictionary<TDictionaryKey, TItem>.ValueCollection,
                    DictionaryValuesEnumerator<TDictionaryKey, TItem>
                >,
                DictionaryValuesEnumerator<TDictionaryKey, TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem>(HashSet<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    HashSet<TItem>,
                    HashSetEnumerator<TItem>
                >,
                HashSetEnumerator<TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem>(LinkedList<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    LinkedList<TItem>,
                    LinkedListEnumerator<TItem>
                >,
                LinkedListEnumerator<TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem>(List<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    List<TItem>,
                    ListEnumerator<TItem>
                >,
                ListEnumerator<TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem>(Queue<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    Queue<TItem>,
                    QueueEnumerator<TItem>
                >,
                QueueEnumerator<TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem, TDictionaryValue>(SortedDictionary<TItem, TDictionaryValue>.KeyCollection source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    SortedDictionary<TItem, TDictionaryValue>.KeyCollection,
                    SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>
                >,
                SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>
            >(ref bridge, predicate);
        }

        public static bool All<TItem, TDictionaryKey>(SortedDictionary<TDictionaryKey, TItem>.ValueCollection source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    SortedDictionary<TDictionaryKey, TItem>.ValueCollection,
                    SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>
                >,
                SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem>(SortedSet<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    SortedSet<TItem>,
                    SortedSetEnumerator<TItem>
                >,
                SortedSetEnumerator<TItem>
            >(ref bridge, predicate);
        }

        public static bool All<TItem>(Stack<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<
                TItem,
                IdentityEnumerable<
                    TItem,
                    Stack<TItem>,
                    StackEnumerator<TItem>
                >,
                StackEnumerator<TItem>
            >(ref bridge, predicate);
        }*/

        public static bool All<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return AllImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static bool AllImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            foreach (var item in source)
            {
                if (!predicate(item)) return false;
            }

            return true;
        }
    }
}
