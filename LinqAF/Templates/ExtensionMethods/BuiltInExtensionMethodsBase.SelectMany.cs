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
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

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
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

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
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, TGenOutItem[], ArrayBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, TGenOutItem[]> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenOutItem[],
                    ArrayBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, IEnumerable<TGenOutItem>, IEnumerableBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, IEnumerable<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    IEnumerable<TGenOutItem>,
                    IEnumerableBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, DictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    DictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, DictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    DictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, HashSet<TGenOutItem>, HashSetBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, HashSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    HashSet<TGenOutItem>,
                    HashSetBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, LinkedList<TGenOutItem>, LinkedListBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, LinkedList<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    LinkedList<TGenOutItem>,
                    LinkedListBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, List<TGenOutItem>, ListBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, List<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    List<TGenOutItem>,
                     ListBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Queue<TGenOutItem>, QueueBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Queue<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Queue<TGenOutItem>,
                     QueueBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    SortedDictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, SortedDictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                     SortedDictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, SortedSet<TGenOutItem>, SortedSetBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedSet<TGenOutItem>,
                     SortedSetBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyBridgeEnumerable<TGenInItem, TGenOutItem, Stack<TGenOutItem>, StackBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Stack<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Stack<TGenOutItem>,
                     StackBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        // SelectManyIndexed (bridges)

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenOutItem[], ArrayBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, TGenOutItem[]> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenOutItem[],
                    ArrayBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, IEnumerable<TGenOutItem>, IEnumerableBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, IEnumerable<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    IEnumerable<TGenOutItem>,
                     IEnumerableBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, DictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                     DictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, DictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Dictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    DictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, HashSet<TGenOutItem>, HashSetBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, HashSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    HashSet<TGenOutItem>,
                     HashSetBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, LinkedList<TGenOutItem>, LinkedListBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, LinkedList<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    LinkedList<TGenOutItem>,
                    LinkedListBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, List<TGenOutItem>, ListBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, List<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    List<TGenOutItem>,
                     ListBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Queue<TGenOutItem>, QueueBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Queue<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Queue<TGenOutItem>,
                     QueueBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenOutItem, TGenDictionaryValue>.KeyCollection,
                    SortedDictionaryKeysBridger<TGenOutItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenOutItem, TGenDictionaryValue>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection, SortedDictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>> SelectMany<TGenInItem, TGenOutItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedDictionary<TGenDictionaryKey, TGenOutItem>.ValueCollection,
                    SortedDictionaryValuesBridger<TGenDictionaryKey, TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, SortedSet<TGenOutItem>, SortedSetBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedSet<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    SortedSet<TGenOutItem>,
                     SortedSetBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        public SelectManyIndexedBridgeEnumerable<TGenInItem, TGenOutItem, Stack<TGenOutItem>, StackBridger<TGenOutItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenOutItem>> SelectMany<TGenInItem, TGenOutItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Stack<TGenOutItem>> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    Stack<TGenOutItem>,
                    StackBridger<TGenOutItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenOutItem>
                >(RefLocal(bridge), selector);
        }

        // SelectManyCollection (bridges)

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, TGenCollectionItem[], ArrayBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, TGenCollectionItem[]> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    TGenCollectionItem[],
                    ArrayBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, IEnumerable<TGenCollectionItem>, IEnumerableBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, IEnumerable<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    IEnumerable<TGenCollectionItem>,
                     IEnumerableBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, DictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                     DictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, DictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    DictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, HashSet<TGenCollectionItem>, HashSetBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, HashSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    HashSet<TGenCollectionItem>,
                     HashSetBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, LinkedList<TGenCollectionItem>, LinkedListBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, LinkedList<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    LinkedList<TGenCollectionItem>,
                     LinkedListBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, List<TGenCollectionItem>, ListBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, List<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    List<TGenCollectionItem>,
                     ListBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Queue<TGenCollectionItem>, QueueBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Queue<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Queue<TGenCollectionItem>,
                     QueueBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    SortedDictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, SortedDictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    SortedDictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedSet<TGenCollectionItem>, SortedSetBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, SortedSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedSet<TGenCollectionItem>,
                     SortedSetBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Stack<TGenCollectionItem>, StackBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, Stack<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Stack<TGenCollectionItem>,
                     StackBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        // SelectManyCollectionIndexed (bridges)

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, TGenCollectionItem[], ArrayBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ArrayEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, TGenCollectionItem[]> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    TGenCollectionItem[],
                     ArrayBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ArrayEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, IEnumerable<TGenCollectionItem>, IEnumerableBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, IdentityEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, IEnumerable<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    IEnumerable<TGenCollectionItem>,
                    IEnumerableBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    IdentityEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, DictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    DictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, DictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Dictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    DictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    DictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, HashSet<TGenCollectionItem>, HashSetBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, HashSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, HashSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    HashSet<TGenCollectionItem>,
                     HashSetBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    HashSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, LinkedList<TGenCollectionItem>, LinkedListBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, LinkedListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, LinkedList<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    LinkedList<TGenCollectionItem>,
                     LinkedListBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LinkedListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, List<TGenCollectionItem>, ListBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, ListEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, List<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    List<TGenCollectionItem>,
                    ListBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ListEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Queue<TGenCollectionItem>, QueueBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, QueueEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Queue<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Queue<TGenCollectionItem>,
                     QueueBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    QueueEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryValue>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenCollectionItem, TGenDictionaryValue>.KeyCollection,
                    SortedDictionaryKeysBridger<TGenCollectionItem, TGenDictionaryValue>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryKeysEnumerator<TGenCollectionItem, TGenDictionaryValue>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection, SortedDictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem, TGenDictionaryKey>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedDictionary<TGenDictionaryKey, TGenCollectionItem>.ValueCollection,
                    SortedDictionaryValuesBridger<TGenDictionaryKey, TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedDictionaryValuesEnumerator<TGenDictionaryKey, TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, SortedSet<TGenCollectionItem>, SortedSetBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, SortedSetEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, SortedSet<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    SortedSet<TGenCollectionItem>,
                     SortedSetBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    SortedSetEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TGenInItem, TGenOutItem, TGenCollectionItem, Stack<TGenCollectionItem>, StackBridger<TGenCollectionItem>, ConstrainedBuiltInEnumerable<TGenInItem>, ConstrainedBuiltInEnumerator<TGenInItem>, StackEnumerator<TGenCollectionItem>> SelectMany<TGenInItem, TGenOutItem, TGenCollectionItem>(BuiltInEnumerable<TGenInItem> source, Func<TGenInItem, int, Stack<TGenCollectionItem>> collectionSelector, Func<TGenInItem, TGenCollectionItem, TGenOutItem> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    TGenCollectionItem,
                    Stack<TGenCollectionItem>,
                     StackBridger<TGenCollectionItem>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    StackEnumerator<TGenCollectionItem>
                >(RefLocal(bridge), collectionSelector, resultSelector);
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
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

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
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

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
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
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
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

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
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

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
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyIndexedEnumerable<
                TGenInItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
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
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGenResultItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionEnumerable<
                TGenInItem,
                TGenResultItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGenResultItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
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
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

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
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement, TGenResultItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>> collectionSelector,
                Func<TGenInItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGenResultItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
           SelectManyCollectionIndexedEnumerable<
               TGenInItem,
               TGenResultItem,
               GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
               ConstrainedBuiltInEnumerable<TGenInItem>,
               ConstrainedBuiltInEnumerator<TGenInItem>,
               LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
               LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
           > SelectMany<TGenInItem, TGenLookupKey, TGenLookupElement, TGenResultItem>(
               BuiltInEnumerable<TGenInItem> source,
               Func<TGenInItem, int, LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>> collectionSelector,
               Func<TGenInItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGenResultItem> resultSelector
           )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenResultItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        // RangeEnumerable

        public 
            SelectManyEnumerable<
                TGenInItem, 
                int, 
                ConstrainedBuiltInEnumerable<TGenInItem>, 
                ConstrainedBuiltInEnumerator<TGenInItem>, 
                RangeEnumerable, 
                RangeEnumerator
                > 
            SelectMany<TGenInItem>(
                BuiltInEnumerable<TGenInItem> source, 
                Func<TGenInItem, RangeEnumerable> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    RangeEnumerable,
                    RangeEnumerator
                >(RefLocal(bridge), selector);
        }

        public
           SelectManyIndexedEnumerable<
               TGenInItem,
               int,
               ConstrainedBuiltInEnumerable<TGenInItem>,
               ConstrainedBuiltInEnumerator<TGenInItem>,
               RangeEnumerable,
               RangeEnumerator
               >
           SelectMany<TGenInItem>(
               BuiltInEnumerable<TGenInItem> source,
               Func<TGenInItem, int, RangeEnumerable> selector
           )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    RangeEnumerable,
                    RangeEnumerator
                >(RefLocal(bridge), selector);
        }

        public 
            SelectManyCollectionEnumerable<
                TGenInItem, 
                TGenOutItem, 
                int, 
                ConstrainedBuiltInEnumerable<TGenInItem>, 
                ConstrainedBuiltInEnumerator<TGenInItem>,
                RangeEnumerable, 
                RangeEnumerator
            > SelectMany<TGenInItem, TGenOutItem>(
                BuiltInEnumerable<TGenInItem> source, 
                Func<TGenInItem, RangeEnumerable> collectionSelector, 
                Func<TGenInItem, int, TGenOutItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    RangeEnumerable,
                    RangeEnumerator
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionIndexedEnumerable<
                TGenInItem,
                TGenOutItem,
                int,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                RangeEnumerable,
                RangeEnumerator
            > SelectMany<TGenInItem, TGenOutItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, RangeEnumerable> collectionSelector,
                Func<TGenInItem, int, TGenOutItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    RangeEnumerable,
                    RangeEnumerator
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        // ReverseRangeEnumerable

        public
            SelectManyEnumerable<
                TGenInItem,
                int,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                ReverseRangeEnumerable,
                ReverseRangeEnumerator
                >
            SelectMany<TGenInItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, ReverseRangeEnumerable> selector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(RefLocal(bridge), selector);
        }

        public
           SelectManyIndexedEnumerable<
               TGenInItem,
               int,
               ConstrainedBuiltInEnumerable<TGenInItem>,
               ConstrainedBuiltInEnumerator<TGenInItem>,
               ReverseRangeEnumerable,
               ReverseRangeEnumerator
               >
           SelectMany<TGenInItem>(
               BuiltInEnumerable<TGenInItem> source,
               Func<TGenInItem, int, ReverseRangeEnumerable> selector
           )
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(RefLocal(bridge), selector);
        }

        public
            SelectManyCollectionEnumerable<
                TGenInItem,
                TGenOutItem,
                int,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                ReverseRangeEnumerable,
                ReverseRangeEnumerator
            > SelectMany<TGenInItem, TGenOutItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, ReverseRangeEnumerable> collectionSelector,
                Func<TGenInItem, int, TGenOutItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }

        public
            SelectManyCollectionIndexedEnumerable<
                TGenInItem,
                TGenOutItem,
                int,
                ConstrainedBuiltInEnumerable<TGenInItem>,
                ConstrainedBuiltInEnumerator<TGenInItem>,
                ReverseRangeEnumerable,
                ReverseRangeEnumerator
            > SelectMany<TGenInItem, TGenOutItem>(
                BuiltInEnumerable<TGenInItem> source,
                Func<TGenInItem, int, ReverseRangeEnumerable> collectionSelector,
                Func<TGenInItem, int, TGenOutItem> resultSelector
            )
        {
            var bridge = Bridge(source, nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.SelectMany<
                    TGenInItem,
                    TGenOutItem,
                    int,
                    ConstrainedBuiltInEnumerable<TGenInItem>,
                    ConstrainedBuiltInEnumerator<TGenInItem>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(RefLocal(bridge), collectionSelector, resultSelector);
        }
    }
}
