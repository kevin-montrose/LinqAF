using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class UnionBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IUnion<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Union<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Union<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Union(IEnumerable<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Union(HashSet<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Union(List<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Union(Queue<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Union(SortedSet<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Union(TItem[] second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }
        
        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Union(Stack<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Union(RepeatEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);
        
        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Union(LinkedList<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }
        
        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Union(BoxedEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Union(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Union(LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Union(Queue<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Union(TItem[] second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Union(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);
        
        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Union(Stack<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Union(SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Union(List<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Union(HashSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Union(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Union<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TUnion_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>> Union<TUnion_DictionaryKey>(SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TUnion_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>> Union<TUnion_DictionaryValue>(SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Union<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TUnion_DictionaryKey, TItem>, DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>, DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>> Union<TUnion_DictionaryKey>(Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Union<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TUnion_DictionaryKey, TItem>, DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>, DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>> Union<TUnion_DictionaryKey>(Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TUnion_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>> Union<TUnion_DictionaryKey>(SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Union<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Union<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TUnion_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>> Union<TUnion_DictionaryValue>(SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Union<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Union(RefThis(), ref bridge, comparer);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Union<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Union<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>, TUnion_IdentityEnumerator> Union<TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>(IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator> second)
            where TUnion_IdentityBridgeType : class
            where TUnion_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_IdentityBridger: struct, IStructBridger<TItem, TUnion_IdentityBridgeType, TUnion_IdentityEnumerator>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Union<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Union<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>, TUnion_IdentityEnumerator> Union<TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>(IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_IdentityBridgeType : class
            where TUnion_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_IdentityBridger: struct, IStructBridger<TItem, TUnion_IdentityBridgeType, TUnion_IdentityEnumerator>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectIndexedEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, CastEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>, WhereWhereEnumerator<TItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>> Union<TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>(WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate> second)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, OfTypeEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, OfTypeEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, CastEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>, WhereWhereEnumerator<TItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>> Union<TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>(WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate> second, IEqualityComparer<TItem> comparer)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectIndexedEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>, SelectSelectEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>(SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection> second)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Union<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>, SelectSelectEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>(SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Union<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyEnumerator<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyBridgeType : class
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>, SelectWhereEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>(SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate> second)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>
            where TUnion_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>, WhereSelectEnumerator<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>> Union<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>(WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection> second)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TUnion_WhereInnerItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TUnion_WhereInnerItem>
            where TUnion_WhereProjection : struct, IStructProjection<TItem, TUnion_WhereInnerItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>, SelectManyBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>(SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator> second)
            where TUnion_BridgeType : class
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_Bridger: struct, IStructBridger<TItem, TUnion_BridgeType, TUnion_ProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>, SelectWhereEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>(SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>
            where TUnion_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>, WhereSelectEnumerator<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>> Union<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>(WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection> second, IEqualityComparer<TItem> comparer)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TUnion_WhereInnerItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TUnion_WhereInnerItem>
            where TUnion_WhereProjection : struct, IStructProjection<TItem, TUnion_WhereInnerItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyBridgeType : class
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyEnumerator<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>, SelectManyBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>(SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_BridgeType : class
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_Bridger: struct, IStructBridger<TItem, TUnion_BridgeType, TUnion_ProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyCollectionEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>, ZipEnumerator<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerator>> Union<TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>(ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator> second)
            where TUnion_ZipFirstEnumerable : struct, IStructEnumerable<TUnion_ZipFirstItem, TUnion_ZipFirstEnumerator>
            where TUnion_ZipFirstEnumerator : struct, IStructEnumerator<TUnion_ZipFirstItem>
            where TUnion_ZipSecondEnumerable : struct, IStructEnumerable<TUnion_ZipSecondItem, TUnion_ZipSecondEnumerator>
            where TUnion_ZipSecondEnumerator : struct, IStructEnumerator<TUnion_ZipSecondItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyBridgeType : class
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyBridgeType : class
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyCollectionEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>, ZipEnumerator<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerator>> Union<TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>(ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_ZipFirstEnumerable : struct, IStructEnumerable<TUnion_ZipFirstItem, TUnion_ZipFirstEnumerator>
            where TUnion_ZipFirstEnumerator : struct, IStructEnumerator<TUnion_ZipFirstItem>
            where TUnion_ZipSecondEnumerable : struct, IStructEnumerable<TUnion_ZipSecondItem, TUnion_ZipSecondEnumerator>
            where TUnion_ZipSecondEnumerator : struct, IStructEnumerator<TUnion_ZipSecondItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyBridgeType : class
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyBridgeType : class
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            var empty = EmptyCache<TItem>.Empty;
            return CommonImplementation.UnionImpl(RefThis(), ref empty);
        }

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            var empty = EmptyCache<TItem>.Empty;
            return CommonImplementation.UnionImpl(RefThis(), ref empty, comparer);
        }

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Union(OneItemDefaultEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Union(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Union(OneItemSpecificEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Union(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Union(OneItemDefaultOrderedEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Union(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Union(OneItemSpecificOrderedEnumerable<TItem> second)
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Union(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipLastEnumerable<TItem, TSkipLastInnerEnumerable, TSkipLastInnerEnumerator>, SkipLastEnumerator<TItem, TSkipLastInnerEnumerator>> Union<TSkipLastInnerEnumerable, TSkipLastInnerEnumerator>(SkipLastEnumerable<TItem, TSkipLastInnerEnumerable, TSkipLastInnerEnumerator> second)
            where TSkipLastInnerEnumerable : struct, IStructEnumerable<TItem, TSkipLastInnerEnumerator>
            where TSkipLastInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeLastEnumerable<TItem, TTakeLastInnerEnumerable, TTakeLastInnerEnumerator>, TakeLastEnumerator<TItem, TTakeLastInnerEnumerator>> Union<TTakeLastInnerEnumerable, TTakeLastInnerEnumerator>(TakeLastEnumerable<TItem, TTakeLastInnerEnumerable, TTakeLastInnerEnumerator> second)
            where TTakeLastInnerEnumerable : struct, IStructEnumerable<TItem, TTakeLastInnerEnumerator>
            where TTakeLastInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipLastEnumerable<TItem, TSkipLastInnerEnumerable, TSkipLastInnerEnumerator>, SkipLastEnumerator<TItem, TSkipLastInnerEnumerator>> Union<TSkipLastInnerEnumerable, TSkipLastInnerEnumerator>(SkipLastEnumerable<TItem, TSkipLastInnerEnumerable, TSkipLastInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipLastInnerEnumerable : struct, IStructEnumerable<TItem, TSkipLastInnerEnumerator>
            where TSkipLastInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeLastEnumerable<TItem, TTakeLastInnerEnumerable, TTakeLastInnerEnumerator>, TakeLastEnumerator<TItem, TTakeLastInnerEnumerator>> Union<TTakeLastInnerEnumerable, TTakeLastInnerEnumerator>(TakeLastEnumerable<TItem, TTakeLastInnerEnumerable, TTakeLastInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeLastInnerEnumerable : struct, IStructEnumerable<TItem, TTakeLastInnerEnumerator>
            where TTakeLastInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, AppendEnumerable<TItem, TAppendInnerEnumerable, TAppendInnerEnumerator>, AppendEnumerator<TItem, TAppendInnerEnumerator>> Union<TAppendInnerEnumerable, TAppendInnerEnumerator>(AppendEnumerable<TItem, TAppendInnerEnumerable, TAppendInnerEnumerator> second)
            where TAppendInnerEnumerable : struct, IStructEnumerable<TItem, TAppendInnerEnumerator>
            where TAppendInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, AppendEnumerable<TItem, TAppendInnerEnumerable, TAppendInnerEnumerator>, AppendEnumerator<TItem, TAppendInnerEnumerator>> Union<TAppendInnerEnumerable, TAppendInnerEnumerator>(AppendEnumerable<TItem, TAppendInnerEnumerable, TAppendInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TAppendInnerEnumerable : struct, IStructEnumerable<TItem, TAppendInnerEnumerator>
            where TAppendInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);

        public UnionDefaultEnumerable<TItem, TEnumerable, TEnumerator, PrependEnumerable<TItem, TPrependInnerEnumerable, TPrependInnerEnumerator>, PrependEnumerator<TItem, TPrependInnerEnumerator>> Union<TPrependInnerEnumerable, TPrependInnerEnumerator>(PrependEnumerable<TItem, TPrependInnerEnumerable, TPrependInnerEnumerator> second)
            where TPrependInnerEnumerable : struct, IStructEnumerable<TItem, TPrependInnerEnumerator>
            where TPrependInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second);

        public UnionSpecificEnumerable<TItem, TEnumerable, TEnumerator, PrependEnumerable<TItem, TPrependInnerEnumerable, TPrependInnerEnumerator>, PrependEnumerator<TItem, TPrependInnerEnumerator>> Union<TPrependInnerEnumerable, TPrependInnerEnumerator>(PrependEnumerable<TItem, TPrependInnerEnumerable, TPrependInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TPrependInnerEnumerable : struct, IStructEnumerable<TItem, TPrependInnerEnumerator>
            where TPrependInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Union(RefThis(), ref second, comparer);
    }
}
