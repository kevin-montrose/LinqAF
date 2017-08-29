using System;
using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class ConcatBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IConcat<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Concat<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Concat(IEnumerable<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Concat(HashSet<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Concat(List<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Concat(Queue<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Concat(SortedSet<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Concat(TItem[] second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Concat(ReverseRangeEnumerable<TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Concat(Stack<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Concat(RepeatEnumerable<TItem> second)
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Concat(RangeEnumerable<TItem> second)
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>>(RefThis(), ref second);

        public TEnumerable Concat(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return This();
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Concat(LinkedList<TItem> second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>>(RefThis(), ref bridge);
        }

        public TEnumerable Concat(EmptyOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return This();
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Concat(BoxedEnumerable<TItem> second)
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Concat<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TDictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>> Concat<TDictionaryKey>(SortedDictionary<TDictionaryKey, TItem>.ValueCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TDictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>> Concat<TDictionaryValue>(SortedDictionary<TItem, TDictionaryValue>.KeyCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Concat<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TDictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TItem>, DictionaryValuesEnumerator<TDictionaryKey, TItem>>, DictionaryValuesEnumerator<TDictionaryKey, TItem>> Concat<TDictionaryKey>(Dictionary<TDictionaryKey, TItem>.ValueCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TDictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TItem>, DictionaryValuesEnumerator<TDictionaryKey, TItem>>, DictionaryValuesEnumerator<TDictionaryKey, TItem>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Concat<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second)
        {
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>(RefThis(), ref bridge);
        }

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipWhileEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(SkipWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipWhileEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, WhereEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, WhereEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>> Concat<TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>> Concat<TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Concat<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(TakeWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TInnerEnumerator>> Concat<TInnerEnumerable, TInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> Concat<TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator> second)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TItem>
            where TIdentityBridger: struct, IStructBridger<TItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TItem, TInnerEnumerable, TInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TItem, TInnerEnumerator>> Concat<TSelectInItem, TInnerEnumerable, TInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TItem, TInnerEnumerable, TInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TInItem, TItem, TInnerEnumerable, TInnerEnumerator>, CastEnumerator<TInItem, TItem, TInnerEnumerator>> Concat<TInItem, TInnerEnumerable, TInnerEnumerator>(CastEnumerable<TInItem, TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, CastEnumerable<TInItem, TItem, TInnerEnumerable, TInnerEnumerator>, CastEnumerator<TInItem, TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>> Concat<TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TInItem, TItem, TInnerEnumerable, TInnerEnumerator>, OfTypeEnumerator<TInItem, TItem, TInnerEnumerator>> Concat<TInItem, TInnerEnumerable, TInnerEnumerator>(OfTypeEnumerable<TInItem, TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TInItem, TItem, TInnerEnumerable, TInnerEnumerator>, OfTypeEnumerator<TInItem, TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TItem, TInnerEnumerable, TInnerEnumerator>, SelectEnumerator<TSelectInItem, TItem, TInnerEnumerator>> Concat<TSelectInItem, TInnerEnumerable, TInnerEnumerator>(SelectEnumerable<TSelectInItem, TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TItem, TInnerEnumerable, TInnerEnumerator>, SelectEnumerator<TSelectInItem, TItem, TInnerEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Concat<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Concat<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Concat<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Concat<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Concat<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Concat<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>> Concat<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Concat<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectManyInItem, TItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>> Concat<TSelectManyInItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectManyInItem, TItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> Concat<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> Concat<TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridger: struct, IStructBridger<TItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>> Concat<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>> Concat<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Concat<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Concat<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TItem, TBridgeType, TSelectManyBridger, TInnerEnumerator, TProjectedEnumerator>> Concat<TSelectManyInItem, TBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> second)
            where TBridgeType : class
            where TInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridger: struct, IStructBridger<TItem, TBridgeType, TProjectedEnumerator>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TItem, TBridgeType, TSelectManyBridger, TInnerEnumerator, TProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>> Concat<TSelectManyInItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> Concat<TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> Concat<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> Concat<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TInnerEnumerator, TSelectManyProjectedEnumerator>> Concat<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where  TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.Concat<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TInnerEnumerable, TInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Concat<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Concat<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Concat<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Concat<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Concat(OneItemDefaultEnumerable<TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Concat(OneItemSpecificEnumerable<TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Concat(OneItemDefaultOrderedEnumerable<TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);

        public ConcatEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Concat(OneItemSpecificOrderedEnumerable<TItem> second)
        => CommonImplementation.Concat(RefThis(), ref second);
    }
}
