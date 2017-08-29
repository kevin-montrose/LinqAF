using System;
using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class SelectManyBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ISelectMany<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public SelectManyBridgeEnumerable<TItem, TOutItem, HashSet<TOutItem>, HashSetBridger<TOutItem>, TEnumerable, TEnumerator, HashSetEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, HashSet<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, LinkedList<TOutItem>, LinkedListBridger<TOutItem>, TEnumerable, TEnumerator, LinkedListEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, LinkedList<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, Queue<TOutItem>, QueueBridger<TOutItem>, TEnumerable, TEnumerator, QueueEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, Queue<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, List<TOutItem>, ListBridger<TOutItem>, TEnumerable, TEnumerator, ListEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, List<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, Stack<TOutItem>, StackBridger<TOutItem>, TEnumerable, TEnumerator, StackEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, Stack<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, BoxedEnumerable<TOutItem>, BoxedEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, BoxedEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, TOutItem[], ArrayBridger<TOutItem>, TEnumerable, TEnumerator, ArrayEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, TOutItem[]> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, IEnumerable<TOutItem>, IEnumerableBridger<TOutItem>, TEnumerable, TEnumerator, IdentityEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, IEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, List<TOutItem>, ListBridger<TOutItem>, TEnumerable, TEnumerator, ListEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, List<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, LinkedList<TOutItem>,  LinkedListBridger<TOutItem>, TEnumerable, TEnumerator, LinkedListEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, LinkedList<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, HashSet<TOutItem>, HashSetBridger<TOutItem>, TEnumerable, TEnumerator, HashSetEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, HashSet<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TOutItem>, ReverseRangeEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, ReverseRangeEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, RangeEnumerable<TOutItem>, RangeEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, RangeEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TItem, int, EmptyOrderedEnumerable<TOutItem>> selector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, RepeatEnumerable<TOutItem>, RepeatEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, RepeatEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TOutItem>, ReverseRangeEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, ReverseRangeEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TItem, EmptyEnumerable<TOutItem>> selector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, SortedSet<TOutItem>, SortedSetBridger<TOutItem>, TEnumerable, TEnumerator, SortedSetEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, SortedSet<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TItem, EmptyOrderedEnumerable<TOutItem>> selector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, Queue<TOutItem>, QueueBridger<TOutItem>, TEnumerable, TEnumerator, QueueEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, Queue<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, Stack<TOutItem>, StackBridger<TOutItem>, TEnumerable, TEnumerator, StackEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, Stack<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, BoxedEnumerable<TOutItem>, BoxedEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, BoxedEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, RepeatEnumerable<TOutItem>, RepeatEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, RepeatEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, RangeEnumerable<TOutItem>, RangeEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, RangeEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TItem, int, EmptyEnumerable<TOutItem>> selector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, TOutItem[], ArrayBridger<TOutItem>, TEnumerable, TEnumerator, ArrayEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, TOutItem[]> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, SortedSet<TOutItem>, SortedSetBridger<TOutItem>, TEnumerable, TEnumerator, SortedSetEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, SortedSet<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, IEnumerable<TOutItem>, IEnumerableBridger<TOutItem>, TEnumerable, TEnumerator, IdentityEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, IEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>, GroupingEnumerator<TOutItem>> SelectMany<TOutItem, TSelectMany_GroupingKey>(Func<TItem, GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, Dictionary<TDictionaryKey, TOutItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TOutItem>, TEnumerable, TEnumerator, DictionaryValuesEnumerator<TDictionaryKey, TOutItem>> SelectMany<TOutItem, TDictionaryKey>(Func<TItem, int, Dictionary<TDictionaryKey, TOutItem>.ValueCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>, GroupedEnumerator<TOutItem>> SelectMany<TOutItem, TSelectMany_GroupedKey>(Func<TItem, int, GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>, GroupingEnumerator<TOutItem>> SelectMany<TOutItem, TSelectMany_GroupingKey>(Func<TItem, int, GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, int, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, int, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, Dictionary<TOutItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TOutItem, TDictionaryValue>, TEnumerable, TEnumerator, DictionaryKeysEnumerator<TOutItem, TDictionaryValue>> SelectMany<TOutItem, TDictionaryValue>(Func<TItem, int, Dictionary<TOutItem, TDictionaryValue>.KeyCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TOutItem>, TEnumerable, TEnumerator, SortedDictionaryValuesEnumerator<TDictionaryKey, TOutItem>> SelectMany<TOutItem, TDictionaryKey>(Func<TItem, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TOutItem, TDictionaryValue>, TEnumerable, TEnumerator, SortedDictionaryKeysEnumerator<TOutItem, TDictionaryValue>> SelectMany<TOutItem, TDictionaryValue>(Func<TItem, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>, GroupedEnumerator<TOutItem>> SelectMany<TOutItem, TSelectMany_GroupedKey>(Func<TItem, GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, Dictionary<TDictionaryKey, TOutItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TOutItem>, TEnumerable, TEnumerator, DictionaryValuesEnumerator<TDictionaryKey, TOutItem>> SelectMany<TOutItem, TDictionaryKey>(Func<TItem, Dictionary<TDictionaryKey, TOutItem>.ValueCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TOutItem>, TEnumerable, TEnumerator, SortedDictionaryValuesEnumerator<TDictionaryKey, TOutItem>> SelectMany<TOutItem, TDictionaryKey>(Func<TItem, int, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedBridgeEnumerable<TItem, TOutItem, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TOutItem, TDictionaryValue>, TEnumerable, TEnumerator, SortedDictionaryKeysEnumerator<TOutItem, TDictionaryValue>> SelectMany<TOutItem, TDictionaryValue>(Func<TItem, int, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyBridgeEnumerable<TItem, TOutItem, Dictionary<TOutItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TOutItem, TDictionaryValue>, TEnumerable, TEnumerator, DictionaryKeysEnumerator<TOutItem, TDictionaryValue>> SelectMany<TOutItem, TDictionaryValue>(Func<TItem, Dictionary<TOutItem, TDictionaryValue>.KeyCollection> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, LinkedList<TCollectionItem>, LinkedListBridger<TCollectionItem>, TEnumerable, TEnumerator, LinkedListEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, LinkedList<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, Queue<TCollectionItem>, QueueBridger<TCollectionItem>, TEnumerable, TEnumerator, QueueEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, Queue<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, Stack<TCollectionItem>, StackBridger<TCollectionItem>, TEnumerable, TEnumerator, StackEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, Stack<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(Func<TItem, EmptyOrderedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, BoxedEnumerable<TCollectionItem>, BoxedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, BoxedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(Func<TItem, EmptyEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, RangeEnumerable<TCollectionItem>, RangeEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, RangeEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, RepeatEnumerable<TCollectionItem>, RepeatEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, RepeatEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, IEnumerable<TCollectionItem>, IEnumerableBridger<TCollectionItem>, TEnumerable, TEnumerator, IdentityEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, IEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, Queue<TCollectionItem>, QueueBridger<TCollectionItem>, TEnumerable, TEnumerator, QueueEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, Queue<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, Stack<TCollectionItem>, StackBridger<TCollectionItem>, TEnumerable, TEnumerator, StackEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, Stack<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, BoxedEnumerable<TCollectionItem>, BoxedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, BoxedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, EmptyEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, RepeatEnumerable<TCollectionItem>, RepeatEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, RepeatEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, RangeEnumerable<TCollectionItem>, RangeEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, RangeEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, TCollectionItem[], ArrayBridger<TCollectionItem>, TEnumerable, TEnumerator, ArrayEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, TCollectionItem[]> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, SortedSet<TCollectionItem>, SortedSetBridger<TCollectionItem>, TEnumerable, TEnumerator, SortedSetEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, SortedSet<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, List<TCollectionItem>, ListBridger<TCollectionItem>, TEnumerable, TEnumerator, ListEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, List<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, LinkedList<TCollectionItem>, LinkedListBridger<TCollectionItem>, TEnumerable, TEnumerator, LinkedListEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, LinkedList<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, HashSet<TCollectionItem>, HashSetBridger<TCollectionItem>, TEnumerable, TEnumerator, HashSetEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, HashSet<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, EmptyOrderedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.EmptySelectMany<TItem, TOutItem, TEnumerable, TEnumerator>(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TCollectionItem>, ReverseRangeEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, ReverseRangeEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, TCollectionItem[], ArrayBridger<TCollectionItem>, TEnumerable, TEnumerator, ArrayEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, TCollectionItem[]> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, SortedSet<TCollectionItem>, SortedSetBridger<TCollectionItem>, TEnumerable, TEnumerator, SortedSetEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, SortedSet<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, List<TCollectionItem>, ListBridger<TCollectionItem>, TEnumerable, TEnumerator, ListEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, List<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TCollectionItem>, ReverseRangeEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, ReverseRangeEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, HashSet<TCollectionItem>, HashSetBridger<TCollectionItem>, TEnumerable, TEnumerator, HashSetEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, HashSet<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, IEnumerable<TCollectionItem>, IEnumerableBridger<TCollectionItem>, TEnumerable, TEnumerator, IdentityEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, IEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, SkipWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TOutItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TOutItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, TakeEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> SelectMany<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(Func<TItem, int, IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> selector)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TOutItem>
            where TIdentityBridger: struct, IStructBridger<TOutItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TOutItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, int, WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, TakeEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, TakeWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, SkipWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, SkipWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, SkipEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, TakeWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TOutItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, int, WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, TakeWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, TakeWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, SkipWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>, ReverseEnumerator<TOutItem>> SelectMany<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(Func<TItem, int, ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> selector)
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>, ReverseEnumerator<TOutItem>> SelectMany<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(Func<TItem, ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> selector)
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, SkipEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> SelectMany<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(Func<TItem, IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> selector)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TOutItem>
            where TIdentityBridger: struct, IStructBridger<TOutItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>, GroupingEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(Func<TItem, int, GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>, GroupingEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(Func<TItem, GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, int, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, int, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>, GroupedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(Func<TItem, GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TCollectionItem>, TEnumerable, TEnumerator, SortedDictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(Func<TItem, int, SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TCollectionItem, TDictionaryValue>, TEnumerable, TEnumerator, SortedDictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>> SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(Func<TItem, int, SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TCollectionItem>, TEnumerable, TEnumerator, SortedDictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(Func<TItem, SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TEnumerable, TEnumerator, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>> SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(Func<TItem, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TCollectionItem>, TEnumerable, TEnumerator, DictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(Func<TItem, int, Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>, GroupedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(Func<TItem, int, GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedBridgeEnumerable<TItem, TOutItem, TCollectionItem, Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TCollectionItem, TDictionaryValue>, TEnumerable, TEnumerator, DictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>> SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(Func<TItem, int, Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TCollectionItem, TDictionaryValue>, TEnumerable, TEnumerator, SortedDictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>> SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(Func<TItem, SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TCollectionItem>, TEnumerable, TEnumerator, DictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(Func<TItem, Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionBridgeEnumerable<TItem, TOutItem, TCollectionItem, Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TCollectionItem, TDictionaryValue>, TEnumerable, TEnumerator, DictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>> SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(Func<TItem, Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>> SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TItem, CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>> SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TItem, int, CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>, OfTypeEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>> SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TItem, int, OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, int, SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, int, SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>, OrderByEnumerator<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>> SelectMany<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>(Func<TItem, int, OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>> selector)
            where TSelectMany_OrderByEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_OrderByEnumerator>
            where TSelectMany_OrderByEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TOutItem, TSelectMany_OrderByKey>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>, OfTypeEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>> SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TItem, OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>, OrderByEnumerator<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>> SelectMany<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>(Func<TItem, OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>> selector)
            where TSelectMany_OrderByEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_OrderByEnumerator>
            where TSelectMany_OrderByEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TOutItem, TSelectMany_OrderByKey>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>, WhereWhereEnumerator<TOutItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> SelectMany<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(Func<TItem, int, WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>, WhereWhereEnumerator<TOutItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> SelectMany<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(Func<TItem, WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TCollectionItem, TTakeInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(Func<TItem, int, TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TCollectionItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, int, WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TCollectionItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, int, WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TCollectionItem, TTakeInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(Func<TItem, int, TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TCollectionItem, TTakeInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(Func<TItem, int, TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TCollectionItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TCollectionItem, TSkipInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(Func<TItem, SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TCollectionItem, TSkipInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(Func<TItem, SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>, ReverseEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(Func<TItem, int, ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> SelectMany<TOutItem, TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(Func<TItem, int, IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TIdentityBridger: struct, IStructBridger<TCollectionItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TCollectionItem, TTakeInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(Func<TItem, TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TCollectionItem, TTakeInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(Func<TItem, TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TCollectionItem, TWhereInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TItem, WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TCollectionItem, TTakeInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(Func<TItem, TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TCollectionItem, TSkipInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(Func<TItem, int, SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TCollectionItem, TSkipInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(Func<TItem, int, SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TCollectionItem, TSkipInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(Func<TItem, int, SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TCollectionItem, TSkipInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(Func<TItem, SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>, ReverseEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(Func<TItem, ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>> SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> SelectMany<TOutItem, TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(Func<TItem, IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TIdentityBridger: struct, IStructBridger<TCollectionItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectDefaultEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionDefaultEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionSpecificEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectSpecificEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionSpecificEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptDefaultEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptSpecificEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectDefaultEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionDefaultEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectSpecificEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TOutItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> SelectMany<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(Func<TItem, int, ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> selector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TOutItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TOutItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>, SelectSelectEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(Func<TItem, int, SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptDefaultEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptSpecificEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>, SelectSelectEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(Func<TItem, SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TOutItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> SelectMany<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(Func<TItem, ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> selector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TOutItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TOutItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>, OrderByEnumerator<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>> SelectMany<TOutItem, TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>(Func<TItem, OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_OrderByInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_OrderByInnerEnumerator>
            where TSelectMany_OrderByInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TCollectionItem, TSelectMany_OrderByKey>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>, WhereWhereEnumerator<TCollectionItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(Func<TItem, WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerator>> SelectMany<TOfTypeInItem, TOutItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(Func<TItem, OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>, OrderByEnumerator<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>> SelectMany<TOutItem, TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>(Func<TItem, int, OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_OrderByInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_OrderByInnerEnumerator>
            where TSelectMany_OrderByInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TCollectionItem, TSelectMany_OrderByKey>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>, WhereWhereEnumerator<TCollectionItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(Func<TItem, int, WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, int, SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TItem, int, SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TCollectionItem, TCastInnerEnumerator>> SelectMany<TCastInItem, TOutItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TItem, int, CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerator>> SelectMany<TOfTypeInItem, TOutItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(Func<TItem, int, OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TCollectionItem, TCastInnerEnumerator>> SelectMany<TCastInItem, TOutItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TItem, CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>, SelectWhereEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(Func<TItem, SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>, SelectWhereEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(Func<TItem, int, SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>, WhereSelectEnumerator<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> SelectMany<TSelectMany_WhereInnerItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(Func<TItem, WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TOutItem, TSelectMany_WhereInnerItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>, WhereSelectEnumerator<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> SelectMany<TSelectMany_WhereInnerItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(Func<TItem, int, WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TOutItem, TSelectMany_WhereInnerItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionSpecificEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TCollectionItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> SelectMany<TOutItem, TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(Func<TItem, int, ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>, SelectSelectEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(Func<TItem, SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptDefaultEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptSpecificEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectDefaultEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>, SelectSelectEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(Func<TItem, int, SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectSpecificEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptDefaultEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>, ExceptSpecificEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectDefaultEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionDefaultEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionSpecificEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TEnumerable, TEnumerator, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>, IntersectSpecificEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>, UnionDefaultEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>> SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(Func<TItem, UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TCollectionItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> SelectMany<TOutItem, TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(Func<TItem, ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(Func<TItem, int, ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> selector)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(Func<TItem, ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> selector)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>, WhereSelectEnumerator<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(Func<TItem, WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TCollectionItem, TSelectMany_WhereInnerItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>, GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>> SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>, WhereSelectEnumerator<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(Func<TItem, int, WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TCollectionItem, TSelectMany_WhereInnerItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>, SelectWhereEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(Func<TItem, int, SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>, SelectWhereEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(Func<TItem, SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>, SelectManyEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinDefaultEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, int, JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinSpecificEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, int, JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinDefaultEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinSpecificEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TCollectionItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(Func<TItem, int, ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TCollectionItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(Func<TItem, ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinDefaultEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, int, JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinSpecificEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, int, JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinDefaultEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>, JoinSpecificEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(Func<TItem, JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>> SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TOutItem>, OneItemDefaultEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, OneItemDefaultEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TOutItem>, OneItemSpecificEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, OneItemSpecificEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TOutItem>, OneItemDefaultEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, OneItemDefaultEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TOutItem>, OneItemSpecificEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, OneItemSpecificEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TCollectionItem>, OneItemDefaultEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, OneItemDefaultEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TCollectionItem>, OneItemSpecificEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, OneItemSpecificEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TCollectionItem>, OneItemDefaultEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, OneItemDefaultEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TCollectionItem>, OneItemSpecificEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, OneItemSpecificEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TOutItem>, OneItemDefaultOrderedEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, OneItemDefaultOrderedEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TOutItem>, OneItemSpecificOrderedEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, OneItemSpecificOrderedEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TOutItem>, OneItemDefaultOrderedEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, OneItemDefaultOrderedEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyIndexedEnumerable<TItem, TOutItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TOutItem>, OneItemSpecificOrderedEnumerator<TOutItem>> SelectMany<TOutItem>(Func<TItem, int, OneItemSpecificOrderedEnumerable<TOutItem>> selector)
        => CommonImplementation.SelectMany(RefThis(), selector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TCollectionItem>, OneItemDefaultOrderedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, OneItemDefaultOrderedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TCollectionItem>, OneItemSpecificOrderedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, OneItemSpecificOrderedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TCollectionItem>, OneItemDefaultOrderedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, OneItemDefaultOrderedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);

        public SelectManyCollectionIndexedEnumerable<TItem, TOutItem, TCollectionItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TCollectionItem>, OneItemSpecificOrderedEnumerator<TCollectionItem>> SelectMany<TOutItem, TCollectionItem>(Func<TItem, int, OneItemSpecificOrderedEnumerable<TCollectionItem>> collectionSelector, Func<TItem, TCollectionItem, TOutItem> resultSelector)
        => CommonImplementation.SelectMany(RefThis(), collectionSelector, resultSelector);
    }
}