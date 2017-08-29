using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IUnion<TItem, TLeftEnumerable, TLeftEnumerator>
        where TLeftEnumerable: struct, IStructEnumerable<TItem, TLeftEnumerator>
        where TLeftEnumerator: struct, IStructEnumerator<TItem>
    {
        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Union(BoxedEnumerable<TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Union(IEnumerable<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>,
            DictionaryKeysEnumerator<TItem, TDictionaryValue>
        > Union<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TUnion_DictionaryKey, TItem>, DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>,
            DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>
        > Union<TUnion_DictionaryKey>(Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>,
            HashSetEnumerator<TItem>
        > Union(HashSet<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>,
            LinkedListEnumerator<TItem>
        > Union(LinkedList<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>,
            ListEnumerator<TItem>
        > Union(List<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>,
            QueueEnumerator<TItem>
        > Union(Queue<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TUnion_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>>,
            SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>
        > Union<TUnion_DictionaryValue>(SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TUnion_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>,
            SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>
        > Union<TUnion_DictionaryKey>(SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>,
            SortedSetEnumerator<TItem>
        > Union(SortedSet<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>,
            StackEnumerator<TItem>
        > Union(Stack<TItem> second);
        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>,
            ArrayEnumerator<TItem>
        > Union(TItem[] second);

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>,
            TUnion_IdentityEnumerator
        > Union<TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>(IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator> second)
            where TUnion_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_IdentityBridger: struct, IStructBridger<TItem, TUnion_IdentityBridgeType, TUnion_IdentityEnumerator>
            where TUnion_IdentityBridgeType : class;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Union<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyEnumerable<TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Union(RangeEnumerable<TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Union(RepeatEnumerable<TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectIndexedEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>, SelectManyBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>(SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_Bridger: struct, IStructBridger<TItem, TUnion_BridgeType, TUnion_ProjectedEnumerator>
            where TUnion_BridgeType : class;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyBridgeType : class;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>
        > Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator> second
        )
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyBridgeType : class;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>
        > Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second
        )
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyBridgeType : class;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyEnumerator<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyCollectionEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Union<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, CastEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, OfTypeEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>, ZipEnumerator<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerator>> Union<TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>(ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator> second)
            where TUnion_ZipFirstEnumerable : struct, IStructEnumerable<TUnion_ZipFirstItem, TUnion_ZipFirstEnumerator>
            where TUnion_ZipFirstEnumerator : struct, IStructEnumerator<TUnion_ZipFirstItem>
            where TUnion_ZipSecondEnumerable : struct, IStructEnumerable<TUnion_ZipSecondItem, TUnion_ZipSecondEnumerator>
            where TUnion_ZipSecondEnumerator : struct, IStructEnumerator<TUnion_ZipSecondItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>, SelectSelectEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>(SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection> second)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>, SelectWhereEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>(SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate> second)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectPredicate : struct, IStructPredicate<TItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>, WhereWhereEnumerator<TItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>> Union<TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>(WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate> second)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>, WhereSelectEnumerator<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>> Union<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>(WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection> second)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TUnion_WhereInnerItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TUnion_WhereInnerItem>
            where TUnion_WhereProjection : struct, IStructProjection<TItem, TUnion_WhereInnerItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyOrderedEnumerable<TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Union<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second);
        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Union<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Union<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Union(ReverseRangeEnumerable<TItem> second);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Union<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
        > Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
        > Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
        > Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
        > Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
            JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        UnionDefaultEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
            JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Union(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Union(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>,
            DictionaryKeysEnumerator<TItem, TDictionaryValue>
        > Union<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TUnion_DictionaryKey, TItem>, DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>,
            DictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>
        > Union<TUnion_DictionaryKey>(Dictionary<TUnion_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>,
            HashSetEnumerator<TItem>
        > Union(HashSet<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>,
            LinkedListEnumerator<TItem>
        > Union(LinkedList<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>,
            ListEnumerator<TItem>
        > Union(List<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>,
            QueueEnumerator<TItem>
        > Union(Queue<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TUnion_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>>,
            SortedDictionaryKeysEnumerator<TItem, TUnion_DictionaryValue>
        > Union<TUnion_DictionaryValue>(SortedDictionary<TItem, TUnion_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TUnion_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>>,
            SortedDictionaryValuesEnumerator<TUnion_DictionaryKey, TItem>
        > Union<TUnion_DictionaryKey>(SortedDictionary<TUnion_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>,
            SortedSetEnumerator<TItem>
        > Union(SortedSet<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>,
            StackEnumerator<TItem>
        > Union(Stack<TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>,
            ArrayEnumerator<TItem>
        > Union(TItem[] second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>,
            TUnion_IdentityEnumerator
        > Union<TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator>(IdentityEnumerable<TItem, TUnion_IdentityBridgeType, TUnion_IdentityBridger, TUnion_IdentityEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_IdentityBridger: struct, IStructBridger<TItem, TUnion_IdentityBridgeType, TUnion_IdentityEnumerator>
            where TUnion_IdentityBridgeType : class;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Union<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Union(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Union(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SelectIndexedEnumerator<TUnion_SelectInItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_SelectInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SelectIndexedEnumerable<TUnion_SelectInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectInItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectInItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>, SelectManyBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator>(SelectManyBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_BridgeType, TUnion_Bridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_Bridger: struct, IStructBridger<TItem, TUnion_BridgeType, TUnion_ProjectedEnumerator>
            where TUnion_BridgeType : class;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyBridgeType : class;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>
        > Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_SelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyBridgeType : class;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>
        > Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyBridger, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>
            where TUnion_SelectManyBridger: struct, IStructBridger<TUnion_CollectionItem, TUnion_SelectManyBridgeType, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyBridgeType : class;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyEnumerator<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyEnumerable<TUnion_SelectManyInItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>, SelectManyCollectionEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator>(SelectManyCollectionEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator, TUnion_ProjectedEnumerable, TUnion_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_InnerEnumerator>
            where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_ProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_ProjectedEnumerator>
            where TUnion_ProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>> Union<TUnion_SelectManyInItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TUnion_SelectManyInItem, TItem, TUnion_CollectionItem, TUnion_SelectManyInnerEnumerable, TUnion_SelectManyInnerEnumerator, TUnion_SelectManyProjectedEnumerable, TUnion_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectManyInnerEnumerable : struct, IStructEnumerable<TUnion_SelectManyInItem, TUnion_SelectManyInnerEnumerator>
            where TUnion_SelectManyInnerEnumerator : struct, IStructEnumerator<TUnion_SelectManyInItem>
            where TUnion_SelectManyProjectedEnumerable : struct, IStructEnumerable<TUnion_CollectionItem, TUnion_SelectManyProjectedEnumerator>
            where TUnion_SelectManyProjectedEnumerator : struct, IStructEnumerator<TUnion_CollectionItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, WhereIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(WhereIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, TakeWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(TakeWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Union<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TUnion_InnerEnumerator>> Union<TUnion_InnerEnumerable, TUnion_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, CastEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(CastEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>, OfTypeEnumerator<TUnion_InItem, TItem, TUnion_InnerEnumerator>> Union<TUnion_InItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator>(OfTypeEnumerable<TUnion_InItem, TItem, TUnion_InnerEnumerable, TUnion_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
           where TUnion_InnerEnumerable : struct, IStructEnumerable<TUnion_InItem, TUnion_InnerEnumerator>
           where TUnion_InnerEnumerator : struct, IStructEnumerator<TUnion_InItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>, ZipEnumerator<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerator>> Union<TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator>(ZipEnumerable<TItem, TUnion_ZipFirstItem, TUnion_ZipSecondItem, TUnion_ZipFirstEnumerable, TUnion_ZipFirstEnumerator, TUnion_ZipSecondEnumerable, TUnion_ZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_ZipFirstEnumerable : struct, IStructEnumerable<TUnion_ZipFirstItem, TUnion_ZipFirstEnumerator>
            where TUnion_ZipFirstEnumerator : struct, IStructEnumerator<TUnion_ZipFirstItem>
            where TUnion_ZipSecondEnumerable : struct, IStructEnumerable<TUnion_ZipSecondItem, TUnion_ZipSecondEnumerator>
            where TUnion_ZipSecondEnumerator : struct, IStructEnumerator<TUnion_ZipSecondItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>, SelectSelectEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection>(SelectSelectEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>, SelectWhereEnumerator<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>> Union<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate>(SelectWhereEnumerable<TItem, TUnion_SelectInnerItem, TUnion_SelectInnerEnumerable, TUnion_SelectInnerEnumerator, TUnion_SelectProjection, TUnion_SelectPredicate> second, IEqualityComparer<TItem> comparer)
            where TUnion_SelectInnerEnumerable : struct, IStructEnumerable<TUnion_SelectInnerItem, TUnion_SelectInnerEnumerator>
            where TUnion_SelectInnerEnumerator : struct, IStructEnumerator<TUnion_SelectInnerItem>
            where TUnion_SelectPredicate : struct, IStructPredicate<TItem>
            where TUnion_SelectProjection : struct, IStructProjection<TItem, TUnion_SelectInnerItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>, WhereWhereEnumerator<TItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>> Union<TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate>(WhereWhereEnumerable<TItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate> second, IEqualityComparer<TItem> comparer)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>, WhereSelectEnumerator<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>> Union<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection>(WhereSelectEnumerable<TItem, TUnion_WhereInnerItem, TUnion_WhereInnerEnumerable, TUnion_WhereInnerEnumerator, TUnion_WherePredicate, TUnion_WhereProjection> second, IEqualityComparer<TItem> comparer)
            where TUnion_WhereInnerEnumerable : struct, IStructEnumerable<TUnion_WhereInnerItem, TUnion_WhereInnerEnumerator>
            where TUnion_WhereInnerEnumerator : struct, IStructEnumerator<TUnion_WhereInnerItem>
            where TUnion_WherePredicate : struct, IStructPredicate<TUnion_WhereInnerItem>
            where TUnion_WhereProjection : struct, IStructProjection<TItem, TUnion_WhereInnerItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TUnion_DistinctInnerEnumerator>> Union<TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TUnion_DistinctInnerEnumerable, TUnion_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnion_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TUnion_DistinctInnerEnumerator>
            where TUnion_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>> Union(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Union<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Union<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Union<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Union<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer);
        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Union<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Union<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>;

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Union(ReverseRangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Union<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
        > Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
        > Union<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
        > Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
        > Union<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
            JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        UnionSpecificEnumerable<
            TItem,
            TLeftEnumerable,
            TLeftEnumerator,
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
            JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Union<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Union(OneItemDefaultEnumerable<TItem> second);
        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Union(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Union(OneItemSpecificEnumerable<TItem> second);
        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Union(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Union(OneItemDefaultOrderedEnumerable<TItem> second);
        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Union(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        UnionDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Union(OneItemSpecificOrderedEnumerable<TItem> second);
        UnionSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Union(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
    }
}
