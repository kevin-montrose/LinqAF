using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ExceptBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IExcept<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Except(BoxedEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Except(IEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Except<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TExcept_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TExcept_DictionaryKey, TItem>, DictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>>, DictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>> Except<TExcept_DictionaryKey>(Dictionary<TExcept_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Except(HashSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Except(LinkedList<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Except(List<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Except(Queue<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TExcept_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TExcept_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TExcept_DictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TExcept_DictionaryValue>> Except<TExcept_DictionaryValue>(SortedDictionary<TItem, TExcept_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TExcept_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TExcept_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>> Except<TExcept_DictionaryKey>(SortedDictionary<TExcept_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Except(SortedSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Except(Stack<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Except(TItem[] second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TExcept_IdentityBridgeType, TExcept_IdentityBridger, TExcept_IdentityEnumerator>, TExcept_IdentityEnumerator> Except<TExcept_IdentityBridgeType, TExcept_IdentityBridger, TExcept_IdentityEnumerator>(IdentityEnumerable<TItem, TExcept_IdentityBridgeType, TExcept_IdentityBridger, TExcept_IdentityEnumerator> second)
            where TExcept_IdentityBridgeType : class
            where TExcept_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_IdentityBridger: struct, IStructBridger<TItem, TExcept_IdentityBridgeType, TExcept_IdentityEnumerator>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Except<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Except(EmptyEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Except(RangeEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Except(RepeatEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SelectEnumerator<TExcept_SelectInItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_SelectInItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SelectEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectInItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SelectIndexedEnumerator<TExcept_SelectInItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_SelectInItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SelectIndexedEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectInItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator>, SelectManyBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator>(SelectManyBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator> second)
            where TExcept_BridgeType : class
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_Bridger: struct, IStructBridger<TItem, TExcept_BridgeType, TExcept_ProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator> second)
            where TExcept_SelectManyBridgeType : class
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_Bridger: struct, IStructBridger<TItem, TExcept_SelectManyBridgeType, TExcept_SelectManyProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator> second)
            where TExcept_SelectManyBridgeType : class
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
            where TExcept_Bridger: struct, IStructBridger<TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_SelectManyProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator> second)
            where TExcept_SelectManyBridgeType : class
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
            where TExcept_Bridger: struct, IStructBridger<TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_SelectManyProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TExcept_SelectManyInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>, SelectManyEnumerator<TExcept_SelectManyInItem, TItem, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>(SelectManyEnumerable<TExcept_SelectManyInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_ProjectedEnumerable : struct, IStructEnumerable<TItem, TExcept_ProjectedEnumerator>
            where TExcept_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TExcept_SelectManyInItem, TItem, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator> second)
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TExcept_SelectManyProjectedEnumerator>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>, SelectManyCollectionEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>(SelectManyCollectionEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_ProjectedEnumerable : struct, IStructEnumerable<TExcept_CollectionItem, TExcept_ProjectedEnumerator>
            where TExcept_ProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator> second)
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerable : struct, IStructEnumerable<TExcept_CollectionItem, TExcept_SelectManyProjectedEnumerator>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, WhereEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(WhereEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, WhereIndexedEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(WhereIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, TakeEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(TakeEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, TakeWhileEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(TakeWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Except<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SkipEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SkipEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SkipWhileEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SkipWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, CastEnumerator<TExcept_InItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_InItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(CastEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_InItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_InItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, OfTypeEnumerator<TExcept_InItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_InItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(OfTypeEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_InItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_InItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerable, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerable, TExcept_ZipSecondEnumerator>, ZipEnumerator<TItem, TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerator>> Except<TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerable, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerable, TExcept_ZipSecondEnumerator>(ZipEnumerable<TItem, TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerable, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerable, TExcept_ZipSecondEnumerator> second)
            where TExcept_ZipFirstEnumerable : struct, IStructEnumerable<TExcept_ZipFirstItem, TExcept_ZipFirstEnumerator>
            where TExcept_ZipFirstEnumerator : struct, IStructEnumerator<TExcept_ZipFirstItem>
            where TExcept_ZipSecondEnumerable : struct, IStructEnumerable<TExcept_ZipSecondItem, TExcept_ZipSecondEnumerator>
            where TExcept_ZipSecondEnumerator : struct, IStructEnumerator<TExcept_ZipSecondItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection>, SelectSelectEnumerator<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator, TExcept_SelectProjection>> Except<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection>(SelectSelectEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection> second)
            where TExcept_SelectInnerEnumerable : struct, IStructEnumerable<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator>
            where TExcept_SelectInnerEnumerator : struct, IStructEnumerator<TExcept_SelectInnerItem>
            where TExcept_SelectProjection : struct, IStructProjection<TItem, TExcept_SelectInnerItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate>, SelectWhereEnumerator<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate>> Except<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate>(SelectWhereEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate> second)
            where TExcept_SelectInnerEnumerable : struct, IStructEnumerable<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator>
            where TExcept_SelectInnerEnumerator : struct, IStructEnumerator<TExcept_SelectInnerItem>
            where TExcept_SelectProjection : struct, IStructProjection<TItem, TExcept_SelectInnerItem>
            where TExcept_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate>, WhereWhereEnumerator<TItem, TExcept_WhereInnerEnumerator, TExcept_WherePredicate>> Except<TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate>(WhereWhereEnumerable<TItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate> second)
            where TExcept_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TExcept_WhereInnerEnumerator>
            where TExcept_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TExcept_WhereInnerItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection>, WhereSelectEnumerator<TItem, TExcept_WhereInnerItem, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection>> Except<TExcept_WhereInnerItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection>(WhereSelectEnumerable<TItem, TExcept_WhereInnerItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection> second)
            where TExcept_WhereInnerEnumerable : struct, IStructEnumerable<TExcept_WhereInnerItem, TExcept_WhereInnerEnumerator>
            where TExcept_WhereInnerEnumerator : struct, IStructEnumerator<TExcept_WhereInnerItem>
            where TExcept_WherePredicate : struct, IStructPredicate<TExcept_WhereInnerItem>
            where TExcept_WhereProjection : struct, IStructProjection<TItem, TExcept_WhereInnerItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TExcept_DistinctInnerEnumerator>> Except<TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator> second)
            where TExcept_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TExcept_DistinctInnerEnumerator>
            where TExcept_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TExcept_DistinctInnerEnumerator>> Except<TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator> second)
            where TExcept_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TExcept_DistinctInnerEnumerator>
            where TExcept_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Except(EmptyOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            var empty = EmptyCache<TItem>.Empty;
            return CommonImplementation.ExceptImpl(RefThis(), ref empty);
        }

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, IntersectDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(IntersectDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, IntersectSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(IntersectSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Except<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Except<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Except<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Except<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Except<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Except(ReverseRangeEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Except<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Except<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Except<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Except<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Except<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Except<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Except<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Except(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Except(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Except<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TExcept_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TExcept_DictionaryKey, TItem>, DictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>>, DictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>> Except<TExcept_DictionaryKey>(Dictionary<TExcept_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Except(HashSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Except(LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Except(List<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Except(Queue<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TExcept_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TExcept_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TExcept_DictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TExcept_DictionaryValue>> Except<TExcept_DictionaryValue>(SortedDictionary<TItem, TExcept_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TExcept_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TExcept_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TExcept_DictionaryKey, TItem>> Except<TExcept_DictionaryKey>(SortedDictionary<TExcept_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Except(SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Except(Stack<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Except(TItem[] second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.ExceptImpl(RefThis(), ref bridge, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TExcept_IdentityBridgeType, TExcept_IdentityBridger, TExcept_IdentityEnumerator>, TExcept_IdentityEnumerator> Except<TExcept_IdentityBridgeType, TExcept_IdentityBridger, TExcept_IdentityEnumerator>(IdentityEnumerable<TItem, TExcept_IdentityBridgeType, TExcept_IdentityBridger, TExcept_IdentityEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_IdentityBridgeType : class
            where TExcept_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_IdentityBridger: struct, IStructBridger<TItem, TExcept_IdentityBridgeType, TExcept_IdentityEnumerator>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Except<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Except(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Except(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Except(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SelectEnumerator<TExcept_SelectInItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_SelectInItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SelectEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectInItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SelectIndexedEnumerator<TExcept_SelectInItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_SelectInItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SelectIndexedEnumerable<TExcept_SelectInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectInItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator>, SelectManyBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator>(SelectManyBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_BridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_BridgeType : class
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_Bridger: struct, IStructBridger<TItem, TExcept_BridgeType, TExcept_ProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectManyBridgeType : class
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_Bridger: struct, IStructBridger<TItem, TExcept_SelectManyBridgeType, TExcept_SelectManyProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectManyBridgeType : class
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
            where TExcept_Bridger: struct, IStructBridger<TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_SelectManyProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_Bridger, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectManyBridgeType : class
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
            where TExcept_Bridger: struct, IStructBridger<TExcept_CollectionItem, TExcept_SelectManyBridgeType, TExcept_SelectManyProjectedEnumerator>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TExcept_SelectManyInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>, SelectManyEnumerator<TExcept_SelectManyInItem, TItem, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>(SelectManyEnumerable<TExcept_SelectManyInItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_ProjectedEnumerable : struct, IStructEnumerable<TItem, TExcept_ProjectedEnumerator>
            where TExcept_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TExcept_SelectManyInItem, TItem, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TExcept_SelectManyProjectedEnumerator>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>, SelectManyCollectionEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator>(SelectManyCollectionEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator, TExcept_ProjectedEnumerable, TExcept_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_ProjectedEnumerable : struct, IStructEnumerable<TExcept_CollectionItem, TExcept_ProjectedEnumerator>
            where TExcept_ProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>> Except<TExcept_SelectManyInItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TExcept_SelectManyInItem, TItem, TExcept_CollectionItem, TExcept_SelectManyInnerEnumerable, TExcept_SelectManyInnerEnumerator, TExcept_SelectManyProjectedEnumerable, TExcept_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectManyInnerEnumerable : struct, IStructEnumerable<TExcept_SelectManyInItem, TExcept_SelectManyInnerEnumerator>
            where TExcept_SelectManyInnerEnumerator : struct, IStructEnumerator<TExcept_SelectManyInItem>
            where TExcept_SelectManyProjectedEnumerable : struct, IStructEnumerable<TExcept_CollectionItem, TExcept_SelectManyProjectedEnumerator>
            where TExcept_SelectManyProjectedEnumerator : struct, IStructEnumerator<TExcept_CollectionItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, WhereEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(WhereEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, WhereIndexedEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(WhereIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, TakeEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(TakeEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, TakeWhileEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(TakeWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Except<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SkipEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SkipEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SkipWhileEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SkipWhileEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TExcept_InnerEnumerator>> Except<TExcept_InnerEnumerable, TExcept_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, CastEnumerator<TExcept_InItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_InItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(CastEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_InItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_InItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>, OfTypeEnumerator<TExcept_InItem, TItem, TExcept_InnerEnumerator>> Except<TExcept_InItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator>(OfTypeEnumerable<TExcept_InItem, TItem, TExcept_InnerEnumerable, TExcept_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_InnerEnumerable : struct, IStructEnumerable<TExcept_InItem, TExcept_InnerEnumerator>
            where TExcept_InnerEnumerator : struct, IStructEnumerator<TExcept_InItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerable, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerable, TExcept_ZipSecondEnumerator>, ZipEnumerator<TItem, TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerator>> Except<TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerable, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerable, TExcept_ZipSecondEnumerator>(ZipEnumerable<TItem, TExcept_ZipFirstItem, TExcept_ZipSecondItem, TExcept_ZipFirstEnumerable, TExcept_ZipFirstEnumerator, TExcept_ZipSecondEnumerable, TExcept_ZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_ZipFirstEnumerable : struct, IStructEnumerable<TExcept_ZipFirstItem, TExcept_ZipFirstEnumerator>
            where TExcept_ZipFirstEnumerator : struct, IStructEnumerator<TExcept_ZipFirstItem>
            where TExcept_ZipSecondEnumerable : struct, IStructEnumerable<TExcept_ZipSecondItem, TExcept_ZipSecondEnumerator>
            where TExcept_ZipSecondEnumerator : struct, IStructEnumerator<TExcept_ZipSecondItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection>, SelectSelectEnumerator<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator, TExcept_SelectProjection>> Except<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection>(SelectSelectEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectInnerEnumerable : struct, IStructEnumerable<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator>
            where TExcept_SelectInnerEnumerator : struct, IStructEnumerator<TExcept_SelectInnerItem>
            where TExcept_SelectProjection : struct, IStructProjection<TItem, TExcept_SelectInnerItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate>, SelectWhereEnumerator<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate>> Except<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate>(SelectWhereEnumerable<TItem, TExcept_SelectInnerItem, TExcept_SelectInnerEnumerable, TExcept_SelectInnerEnumerator, TExcept_SelectProjection, TExcept_SelectPredicate> second, IEqualityComparer<TItem> comparer)
            where TExcept_SelectInnerEnumerable : struct, IStructEnumerable<TExcept_SelectInnerItem, TExcept_SelectInnerEnumerator>
            where TExcept_SelectInnerEnumerator : struct, IStructEnumerator<TExcept_SelectInnerItem>
            where TExcept_SelectProjection : struct, IStructProjection<TItem, TExcept_SelectInnerItem>
            where TExcept_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate>, WhereWhereEnumerator<TItem, TExcept_WhereInnerEnumerator, TExcept_WherePredicate>> Except<TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate>(WhereWhereEnumerable<TItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate> second, IEqualityComparer<TItem> comparer)
            where TExcept_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TExcept_WhereInnerEnumerator>
            where TExcept_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TExcept_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TExcept_WhereInnerItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection>, WhereSelectEnumerator<TItem, TExcept_WhereInnerItem, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection>> Except<TExcept_WhereInnerItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection>(WhereSelectEnumerable<TItem, TExcept_WhereInnerItem, TExcept_WhereInnerEnumerable, TExcept_WhereInnerEnumerator, TExcept_WherePredicate, TExcept_WhereProjection> second, IEqualityComparer<TItem> comparer)
            where TExcept_WhereInnerEnumerable : struct, IStructEnumerable<TExcept_WhereInnerItem, TExcept_WhereInnerEnumerator>
            where TExcept_WhereInnerEnumerator : struct, IStructEnumerator<TExcept_WhereInnerItem>
            where TExcept_WherePredicate : struct, IStructPredicate<TExcept_WhereInnerItem>
            where TExcept_WhereProjection : struct, IStructProjection<TItem, TExcept_WhereInnerItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TExcept_DistinctInnerEnumerator>> Except<TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TExcept_DistinctInnerEnumerator>
            where TExcept_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TExcept_DistinctInnerEnumerator>> Except<TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TExcept_DistinctInnerEnumerable, TExcept_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExcept_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TExcept_DistinctInnerEnumerator>
            where TExcept_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Except(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            var empty = EmptyCache<TItem>.Empty;
            return CommonImplementation.ExceptImpl(RefThis(), ref empty, comparer);
        }

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, IntersectDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(IntersectDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, IntersectSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Except<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(IntersectSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Except<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Except<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Except<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Except<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Except<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Except(ReverseRangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Except<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Except<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Except<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Except<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Except<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Except<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Except<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Except(OneItemDefaultEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Except(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Except(OneItemSpecificEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Except(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Except(OneItemDefaultOrderedEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Except(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);

        public ExceptDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Except(OneItemSpecificOrderedEnumerable<TItem> second)
        => CommonImplementation.Except(RefThis(), ref second);

        public ExceptSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Except(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Except(RefThis(), ref second, comparer);
    }
}