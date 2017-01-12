using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase:
        ExtensionMethodsBase
    {
        // SelectMany

        public SelectManyEnumerable<TGenInItem, TGenOutItem, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, PlaceholderEnumerable<TGenOutItem>, PlaceholderEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, PlaceholderEnumerable<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    PlaceholderEnumerable<TGenOutItem>,
                    PlaceholderEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        // SelectManyIndexed

        public SelectManyIndexedEnumerable<TGenInItem, TGenOutItem, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, PlaceholderEnumerable<TGenOutItem>, PlaceholderEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, PlaceholderEnumerable<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    PlaceholderEnumerable<TGenOutItem>,
                    PlaceholderEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        // SelectManyCollection

        public SelectManyCollectionEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, PlaceholderEnumerable<TGenCollectionItem>, PlaceholderEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, PlaceholderEnumerable<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    PlaceholderEnumerable<TGenCollectionItem>,
                    PlaceholderEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        // SelectManyCollectionIndexed

        public SelectManyCollectionIndexedEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, PlaceholderEnumerable<TGenCollectionItem>, PlaceholderEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, PlaceholderEnumerable<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    PlaceholderEnumerable<TGenCollectionItem>,
                    PlaceholderEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        // SelectMany (bridges)

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, TGenOutItem[], ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, TGenOutItem[]> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenOutItem[],
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.Array);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, IEnumerable<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, IEnumerable<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    IEnumerable<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.IEnumerable);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem, TGenDictionaryValue>.DictionaryKeys);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenDictionaryKey, TGenOutItem>.DictionaryValues);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, HashSet<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, HashSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    HashSet<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.HashSet);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, LinkedList<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, LinkedList<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    LinkedList<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.LinkedList);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, List<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, List<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    List<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.List);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Queue<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Queue<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Queue<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.Queue);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem, TGenDictionaryValue>.SortedDictionaryKeys);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenDictionaryKey, TGenOutItem>.SortedDictionaryValues);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, SortedSet<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedSet<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.SortedSet);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Stack<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Stack<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Stack<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.Stack);
        }

        // SelectManyIndexed (bridges)

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenOutItem[], ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, TGenOutItem[]> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenOutItem[],
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.Array);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, IEnumerable<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, IEnumerable<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    IEnumerable<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.IEnumerable);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem, TGenDictionaryValue>.DictionaryKeys);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenDictionaryKey, TGenOutItem>.DictionaryValues);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, HashSet<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, HashSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    HashSet<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.HashSet);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, LinkedList<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, LinkedList<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    LinkedList<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.LinkedList);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, List<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, List<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    List<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.List);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Queue<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Queue<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Queue<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.Queue);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem, TGenDictionaryValue>.SortedDictionaryKeys);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenDictionaryKey, TGenOutItem>.SortedDictionaryValues);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, SortedSet<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedSet<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.SortedSet);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Stack<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Stack<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Stack<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector, IdentityMaps<TGenOutItem>.Stack);
        }

        // SelectManyCollection (bridges)

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, TGenCollectionItem[], ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, TGenCollectionItem[]> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    TGenCollectionItem[],
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.Array);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, IEnumerable<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, IEnumerable<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    IEnumerable<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.IEnumerable);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem, TGenDictionaryValue>.DictionaryKeys);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenDictionaryKey, TGenCollectionItem>.DictionaryValues);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, HashSet<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, HashSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    HashSet<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.HashSet);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, LinkedList<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, LinkedList<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    LinkedList<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.LinkedList);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, List<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, List<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    List<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.List);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Queue<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Queue<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Queue<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.Queue);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem, TGenDictionaryValue>.SortedDictionaryKeys);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenDictionaryKey, TGenCollectionItem>.SortedDictionaryValues);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedSet<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedSet<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.SortedSet);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Stack<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Stack<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Stack<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.Stack);
        }

        // SelectManyCollectionIndexed (bridges)

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, TGenCollectionItem[], ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, TGenCollectionItem[]> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    TGenCollectionItem[],
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.Array);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, IEnumerable<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, IEnumerable<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    IEnumerable<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.IEnumerable);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem, TGenDictionaryValue>.DictionaryKeys);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenDictionaryKey, TGenCollectionItem>.DictionaryValues);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, HashSet<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, HashSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    HashSet<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.HashSet);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, LinkedList<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, LinkedList<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    LinkedList<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.LinkedList);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, List<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, List<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    List<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.List);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Queue<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Queue<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Queue<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.Queue);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem, TGenDictionaryValue>.SortedDictionaryKeys);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenDictionaryKey, TGenCollectionItem>.SortedDictionaryValues);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedSet<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedSet<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.SortedSet);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Stack<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Stack<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Stack<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector, IdentityMaps<TGenCollectionItem>.Stack);
        }

        // SelectMany Weird
        public 
            SelectManyEnumerable<
                TGenInItem, 
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>, 
                ConstrainedBuiltInEnumerable<TGenInItem>, 
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>, 
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TGenInItem> source, 
                Func<TGenInItem, GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> selector
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> selector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, LookupEnumerable<TGenLookupKey, TGenLookupElement>> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), selector);
        }

        // SelectManyIndexed Weird
        public
            SelectManyIndexedEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> selector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyIndexedEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> selector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyIndexedEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, LookupEnumerable<TGenLookupKey, TGenLookupElement>> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), selector);
        }

        // SelectManyCollection Weird
        public
            SelectManyCollectionEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>, TGenResultItem> resultSelector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>, TGenResultItem> resultSelector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, LookupEnumerable<TGenLookupKey, TGenLookupElement>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGenResultItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        // SelectManyCollectionIndexed Weird
        public
            SelectManyCollectionIndexedEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>, TGenResultItem> resultSelector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionIndexedEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
            > SelectMany<TGenInItem, TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>, TGenResultItem> resultSelector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenGroupByKeyItem, TGenGroupByElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKeyItem, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionIndexedEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, LookupEnumerable<TGenLookupKey, TGenLookupElement>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGenResultItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }
    }
}
