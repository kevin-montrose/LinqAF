using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IIntersect<TItem, TLeftEnumerable, TLeftEnumerator>
        where TLeftEnumerable : struct, IStructEnumerable<TItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TItem>
    {
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Intersect(BoxedEnumerable<TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Intersect(IEnumerable<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>,
                    DictionaryKeysEnumerator<TItem, TDictionaryValue>
                > Intersect<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TIntersect_DictionaryKey, TItem>, DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>,
                    DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>
                > Intersect<TIntersect_DictionaryKey>(Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>,
                    HashSetEnumerator<TItem>
                > Intersect(HashSet<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>,
                    LinkedListEnumerator<TItem>
                > Intersect(LinkedList<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>,
                    ListEnumerator<TItem>
                > Intersect(List<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>,
                    QueueEnumerator<TItem>
                > Intersect(Queue<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TIntersect_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>>,
                    SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>
                > Intersect<TIntersect_DictionaryValue>(SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TIntersect_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>,
                    SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>
                > Intersect<TIntersect_DictionaryKey>(SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>,
                    SortedSetEnumerator<TItem>
                > Intersect(SortedSet<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>,
                    StackEnumerator<TItem>
                > Intersect(Stack<TItem> second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>,
                    ArrayEnumerator<TItem>
                > Intersect(TItem[] second);
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityBridger, TIntersect_IdentityEnumerator>,
                    TIntersect_IdentityEnumerator
                > Intersect<TIntersect_IdentityBridgeType, TIntersect_IdentityBridger, TIntersect_IdentityEnumerator>(IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityBridger, TIntersect_IdentityEnumerator> second)
                    where TIntersect_IdentityEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_IdentityBridger: struct, IStructBridger<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator>
                    where TIntersect_IdentityBridgeType : class;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Intersect<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
                    where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
                    where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
                    where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
                    where TInnerRightEnumerator : struct, IStructEnumerator<TItem>;
        EmptyEnumerable<TItem> Intersect(EmptyEnumerable<TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Intersect(RangeEnumerable<TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Intersect(RepeatEnumerable<TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectIndexedEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>, SelectManyBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>(SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator> second)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_Bridger: struct, IStructBridger<TItem, TIntersect_BridgeType, TIntersect_ProjectedEnumerator>
                    where TIntersect_BridgeType : class;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second)
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_SelectManyBridger: struct, IStructBridger<TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyBridgeType : class;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>,
                    SelectManyCollectionBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>
                > Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(
                    SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second
                )
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
                    where TIntersect_SelectManyBridger: struct, IStructBridger<TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyBridgeType : class;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>,
                    SelectManyCollectionIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>
                > Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(
                    SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second
                )
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
                    where TIntersect_SelectManyBridger: struct,IStructBridger<TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyBridgeType : class;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_ProjectedEnumerator>
                    where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second)
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyCollectionEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_ProjectedEnumerator>
                    where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second)
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Intersect<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
                   where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
                   where TInnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, CastEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, OfTypeEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>, ZipEnumerator<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerator>> Intersect<TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>(ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator> second)
                    where TIntersect_ZipFirstEnumerable : struct, IStructEnumerable<TIntersect_ZipFirstItem, TIntersect_ZipFirstEnumerator>
                    where TIntersect_ZipFirstEnumerator : struct, IStructEnumerator<TIntersect_ZipFirstItem>
                    where TIntersect_ZipSecondEnumerable : struct, IStructEnumerable<TIntersect_ZipSecondItem, TIntersect_ZipSecondEnumerator>
                    where TIntersect_ZipSecondEnumerator : struct, IStructEnumerator<TIntersect_ZipSecondItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>, SelectSelectEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>(SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection> second)
                    where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
                    where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
                    where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>, SelectWhereEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>(SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate> second)
                    where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
                    where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
                    where TIntersect_SelectPredicate : struct, IStructPredicate<TItem>
                    where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>, WhereWhereEnumerator<TItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>> Intersect<TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>(WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate> second)
                    where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_WhereInnerEnumerator>
                    where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_WherePredicate : struct, IStructPredicate<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>, WhereSelectEnumerator<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>> Intersect<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>(WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection> second)
                    where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator>
                    where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TIntersect_WhereInnerItem>
                    where TIntersect_WherePredicate : struct, IStructPredicate<TIntersect_WhereInnerItem>
                    where TIntersect_WhereProjection : struct, IStructProjection<TItem, TIntersect_WhereInnerItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second)
                    where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
                    where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second)
                    where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
                    where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;
        EmptyEnumerable<TItem> Intersect(EmptyOrderedEnumerable<TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
                    where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
                    where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
                    where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
                    where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
                    where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
                    where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
                    where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
                    where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
                    where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
                    where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
                    where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
                    where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
                    where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
                    where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
                    where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
                    where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
                    where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
                    where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Intersect<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Intersect<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Intersect<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
                    where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
                    where TReverseEnumerator : struct, IStructEnumerator<TItem>;
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Intersect(ReverseRangeEnumerable<TItem> second);
        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Intersect<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
                    where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
                    where TOrderByEnumerator : struct, IStructEnumerator<TItem>
                    where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
                    GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
                > Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
                    GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
                )
                    where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
                    where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
                    where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
                    where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
                    GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
                > Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
                    GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
                )
                    where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
                    where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
                    where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
                    where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
                    GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
                > Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
                    GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
                )
                    where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
                    where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
                    GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
                > Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
                    GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
                )
                    where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
                    where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
                    JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
                > Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
                    JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
                )
                    where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
                    where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
                    where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
                    where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;
        IntersectDefaultEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
                    JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
                > Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
                    JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
                )
                    where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
                    where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
                    where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
                    where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Intersect(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Intersect(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>,
                    DictionaryKeysEnumerator<TItem, TDictionaryValue>
                > Intersect<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TIntersect_DictionaryKey, TItem>, DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>,
                    DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>
                > Intersect<TIntersect_DictionaryKey>(Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>,
                    HashSetEnumerator<TItem>
                > Intersect(HashSet<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>,
                    LinkedListEnumerator<TItem>
                > Intersect(LinkedList<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>,
                    ListEnumerator<TItem>
                > Intersect(List<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>,
                    QueueEnumerator<TItem>
                > Intersect(Queue<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TIntersect_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>>,
                    SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>
                > Intersect<TIntersect_DictionaryValue>(SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TIntersect_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>,
                    SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>
                > Intersect<TIntersect_DictionaryKey>(SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>,
                    SortedSetEnumerator<TItem>
                > Intersect(SortedSet<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>,
                    StackEnumerator<TItem>
                > Intersect(Stack<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>,
                    ArrayEnumerator<TItem>
                > Intersect(TItem[] second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityBridger, TIntersect_IdentityEnumerator>,
                    TIntersect_IdentityEnumerator
                > Intersect<TIntersect_IdentityBridgeType, TIntersect_IdentityBridger, TIntersect_IdentityEnumerator>(IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityBridger, TIntersect_IdentityEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_IdentityEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_IdentityBridger: struct, IStructBridger<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator>
                    where TIntersect_IdentityBridgeType : class;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Intersect<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
                    where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
                    where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
                    where TInnerRightEnumerator : struct, IStructEnumerator<TItem>;
        EmptyEnumerable<TItem> Intersect(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Intersect(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Intersect(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectIndexedEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>, SelectManyBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>(SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_Bridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_Bridger: struct, IStructBridger<TItem, TIntersect_BridgeType, TIntersect_ProjectedEnumerator>
                    where TIntersect_BridgeType : class;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_SelectManyBridger: struct, IStructBridger<TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyBridgeType : class;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>,
                    SelectManyCollectionBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>
                > Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(
                    SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
                    where TIntersect_SelectManyBridger: struct, IStructBridger<TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyBridgeType : class;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>,
                    SelectManyCollectionIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>
                > Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(
                    SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyBridger, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
                    where TIntersect_SelectManyBridger: struct, IStructBridger<TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyBridgeType : class;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_ProjectedEnumerator>
                    where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyCollectionEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
                    where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_ProjectedEnumerator>
                    where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
                    where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
                    where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_SelectManyProjectedEnumerator>
                    where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Intersect<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
                   where TInnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, CastEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, OfTypeEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
                   where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
                   where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>, ZipEnumerator<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerator>> Intersect<TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>(ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_ZipFirstEnumerable : struct, IStructEnumerable<TIntersect_ZipFirstItem, TIntersect_ZipFirstEnumerator>
                    where TIntersect_ZipFirstEnumerator : struct, IStructEnumerator<TIntersect_ZipFirstItem>
                    where TIntersect_ZipSecondEnumerable : struct, IStructEnumerable<TIntersect_ZipSecondItem, TIntersect_ZipSecondEnumerator>
                    where TIntersect_ZipSecondEnumerator : struct, IStructEnumerator<TIntersect_ZipSecondItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>, SelectSelectEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>(SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
                    where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
                    where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>, SelectWhereEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>(SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
                    where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
                    where TIntersect_SelectPredicate : struct, IStructPredicate<TItem>
                    where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>, WhereWhereEnumerator<TItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>> Intersect<TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>(WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_WhereInnerEnumerator>
                    where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersect_WherePredicate : struct, IStructPredicate<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>, WhereSelectEnumerator<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>> Intersect<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>(WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator>
                    where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TIntersect_WhereInnerItem>
                    where TIntersect_WherePredicate : struct, IStructPredicate<TIntersect_WhereInnerItem>
                    where TIntersect_WhereProjection : struct, IStructProjection<TItem, TIntersect_WhereInnerItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
                    where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
                    where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>;
        EmptyEnumerable<TItem>Intersect(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
                    where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
                    where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
                    where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
                    where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
                    where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
                    where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
                    where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
                    where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
                    where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
                    where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
                    where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
                    where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
                    where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Intersect<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Intersect<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Intersect<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
                    where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
                    where TReverseEnumerator : struct, IStructEnumerator<TItem>;
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Intersect(ReverseRangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Intersect<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
                    where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
                    where TOrderByEnumerator : struct, IStructEnumerator<TItem>
                    where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
                    GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
                > Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
                    GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
                    where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
                    where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
                    where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
                    GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
                > Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
                    GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
                    where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
                    where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
                    where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
                    GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
                > Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
                    GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
                    where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>,
                    GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>
                > Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
                    GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
                    where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
                    JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
                > Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
                    JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
                    where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
                    where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
                    where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;
        IntersectSpecificEnumerable<
                    TItem,
                    TLeftEnumerable,
                    TLeftEnumerator,
                    JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
                    JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
                > Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
                    JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
                    IEqualityComparer<TItem> comparer
                )
                    where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
                    where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
                    where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
                    where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Intersect(OneItemDefaultEnumerable<TItem> second);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Intersect(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Intersect(OneItemSpecificEnumerable<TItem> second);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Intersect(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Intersect(OneItemDefaultOrderedEnumerable<TItem> second);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Intersect(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        IntersectDefaultEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Intersect(OneItemSpecificOrderedEnumerable<TItem> second);
        IntersectSpecificEnumerable<TItem, TLeftEnumerable, TLeftEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Intersect(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
    }
}