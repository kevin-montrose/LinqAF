using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface ISelectMany<TInItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TInItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TInItem>
    {
        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            IEnumerable<TOutItem>,
            IEnumerableBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, IEnumerable<TOutItem>> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            Dictionary<TOutItem, TDictionaryValue>.KeyCollection,
            DictionaryKeysBridger<TOutItem, TDictionaryValue>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryKeysEnumerator<TOutItem, TDictionaryValue>
        > SelectMany<TOutItem, TDictionaryValue>(Func<TInItem, Dictionary<TOutItem, TDictionaryValue>.KeyCollection> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            Dictionary<TDictionaryKey, TOutItem>.ValueCollection,
            DictionaryValuesBridger<TDictionaryKey, TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryValuesEnumerator<TDictionaryKey, TOutItem>
        > SelectMany<TOutItem, TDictionaryKey>(Func<TInItem, Dictionary<TDictionaryKey, TOutItem>.ValueCollection> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            HashSet<TOutItem>,
            HashSetBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            HashSetEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, HashSet<TOutItem>> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            LinkedList<TOutItem>,
            LinkedListBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            LinkedListEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, LinkedList<TOutItem>> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            List<TOutItem>,
            ListBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            ListEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, List<TOutItem>> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            Queue<TOutItem>,
            QueueBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            QueueEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, Queue<TOutItem>> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection,
            SortedDictionaryKeysBridger<TOutItem, TDictionaryValue>,
            TThisEnumerable,
            TThisEnumerator,
            SortedDictionaryKeysEnumerator<TOutItem, TDictionaryValue>
        > SelectMany<TOutItem, TDictionaryValue>(Func<TInItem, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection> selector);

        SelectManyBridgeEnumerable<
            TInItem,
            TOutItem,
            SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection,
            SortedDictionaryValuesBridger<TDictionaryKey, TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            SortedDictionaryValuesEnumerator<TDictionaryKey, TOutItem>
        > SelectMany<TOutItem, TDictionaryKey>(Func<TInItem, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection> selector);

        SelectManyBridgeEnumerable<
           TInItem,
           TOutItem,
           SortedSet<TOutItem>,
           SortedSetBridger<TOutItem>,
           TThisEnumerable,
           TThisEnumerator,
           SortedSetEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, SortedSet<TOutItem>> selector);

        SelectManyBridgeEnumerable<
          TInItem,
          TOutItem,
          Stack<TOutItem>,
          StackBridger<TOutItem>,
          TThisEnumerable,
          TThisEnumerator,
          StackEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, Stack<TOutItem>> selector);

        SelectManyBridgeEnumerable<
          TInItem,
          TOutItem,
          TOutItem[],
          ArrayBridger<TOutItem>,
          TThisEnumerable,
          TThisEnumerator,
          ArrayEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, TOutItem[]> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            BoxedEnumerable<TOutItem>,
            BoxedEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, BoxedEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultEnumerable<TOutItem>,
            OneItemDefaultEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, OneItemDefaultEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificEnumerable<TOutItem>,
            OneItemSpecificEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, OneItemSpecificEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultOrderedEnumerable<TOutItem>,
            OneItemDefaultOrderedEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, OneItemDefaultOrderedEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificOrderedEnumerable<TOutItem>,
            OneItemSpecificOrderedEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, OneItemSpecificOrderedEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>,
            TIdentityEnumerator
        > SelectMany<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(Func<TInItem, IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> selector)
            where TIdentityEnumerator : struct, IStructEnumerator<TOutItem>
            where TIdentityBridger: struct, IStructBridger<TOutItem, TIdentityBridgeType, TIdentityEnumerator>
            where TIdentityBridgeType : class;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TOutItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > SelectMany<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(Func<TInItem, ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> selector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TOutItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TOutItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TOutItem>;
        
        EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TInItem, EmptyEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            RangeEnumerable<TOutItem>,
            RangeEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, RangeEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            RepeatEnumerable<TOutItem>,
            RepeatEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, RepeatEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TInItem, SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectIndexedEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TInItem, SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TInItem, SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyIndexedBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TInItem, SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TInItem, SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyIndexedEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TInItem, SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereEnumerator<TOutItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TInItem, WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereIndexedEnumerator<TOutItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TInItem, WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptySpecificEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            TakeEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            TakeWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            TakeWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            SkipEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            SkipWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            SkipWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>
        > SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TInItem, CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            OfTypeEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>
        > SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TInItem, OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
            ZipEnumerator<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
        > SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
            Func<TInItem, ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> selector
        )
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyCollectionEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>,
            SelectSelectEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>
        > SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(
            Func<TInItem, SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> selector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>,
            SelectWhereEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>
        > SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(
            Func<TInItem, SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> selector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>,
            WhereWhereEnumerator<TOutItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>
        > SelectMany<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(
            Func<TInItem, WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> selector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>,
            WhereSelectEnumerator<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>
        > SelectMany<TSelectMany_WhereInnerItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(
            Func<TInItem, WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> selector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TOutItem, TSelectMany_WhereInnerItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctDefaultEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctSpecificEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TInItem, EmptyOrderedEnumerable<TOutItem>> selector);

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptDefaultEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptSpecificEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>,
            GroupedEnumerator<TOutItem>
        > SelectMany<TOutItem, TSelectMany_GroupedKey>(
            Func<TInItem, GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>> selector
        );

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>,
            GroupingEnumerator<TOutItem>
        > SelectMany<TOutItem, TSelectMany_GroupingKey>(
            Func<TInItem, GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>> selector
        );

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectDefaultEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectSpecificEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinDefaultEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinSpecificEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector
        );

        SelectManyEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector
        );

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>,
            OrderByEnumerator<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>
        > SelectMany<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>(
            Func<TInItem, OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>> selector
        )
            where TSelectMany_OrderByEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_OrderByEnumerator>
            where TSelectMany_OrderByEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TOutItem, TSelectMany_OrderByKey>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>,
            ReverseEnumerator<TOutItem>
        > SelectMany<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(
            Func<TInItem, ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> selector
        )
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseRangeEnumerable<TOutItem>,
            ReverseRangeEnumerator<TOutItem>
        > SelectMany<TOutItem>(
            Func<TInItem, ReverseRangeEnumerable<TOutItem>> selector
        );

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionDefaultEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionSpecificEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        // indexed options

        SelectManyIndexedBridgeEnumerable<
           TInItem,
           TOutItem,
           IEnumerable<TOutItem>,
           IEnumerableBridger<TOutItem>,
           TThisEnumerable,
           TThisEnumerator,
           IdentityEnumerator<TOutItem>
       > SelectMany<TOutItem>(Func<TInItem, int, IEnumerable<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            Dictionary<TOutItem, TDictionaryValue>.KeyCollection,
            DictionaryKeysBridger<TOutItem, TDictionaryValue>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryKeysEnumerator<TOutItem, TDictionaryValue>
        > SelectMany<TOutItem, TDictionaryValue>(Func<TInItem, int, Dictionary<TOutItem, TDictionaryValue>.KeyCollection> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            Dictionary<TDictionaryKey, TOutItem>.ValueCollection,
            DictionaryValuesBridger<TDictionaryKey, TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryValuesEnumerator<TDictionaryKey, TOutItem>
        > SelectMany<TOutItem, TDictionaryKey>(Func<TInItem, int, Dictionary<TDictionaryKey, TOutItem>.ValueCollection> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            HashSet<TOutItem>,
            HashSetBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            HashSetEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, HashSet<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            LinkedList<TOutItem>,
            LinkedListBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            LinkedListEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, LinkedList<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            List<TOutItem>,
            ListBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            ListEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, List<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            Queue<TOutItem>,
            QueueBridger<TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            QueueEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, Queue<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection,
            SortedDictionaryKeysBridger<TOutItem, TDictionaryValue>,
            TThisEnumerable,
            TThisEnumerator,
            SortedDictionaryKeysEnumerator<TOutItem, TDictionaryValue>
        > SelectMany<TOutItem, TDictionaryValue>(Func<TInItem, int, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection> selector);

        SelectManyIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection,
            SortedDictionaryValuesBridger<TDictionaryKey, TOutItem>,
            TThisEnumerable,
            TThisEnumerator,
            SortedDictionaryValuesEnumerator<TDictionaryKey, TOutItem>
        > SelectMany<TOutItem, TDictionaryKey>(Func<TInItem, int, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection> selector);

        SelectManyIndexedBridgeEnumerable<
           TInItem,
           TOutItem,
           SortedSet<TOutItem>,
           SortedSetBridger<TOutItem>,
           TThisEnumerable,
           TThisEnumerator,
           SortedSetEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, SortedSet<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
          TInItem,
          TOutItem,
          Stack<TOutItem>,
          StackBridger<TOutItem>,
          TThisEnumerable,
          TThisEnumerator,
          StackEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, Stack<TOutItem>> selector);

        SelectManyIndexedBridgeEnumerable<
          TInItem,
          TOutItem,
          TOutItem[],
          ArrayBridger<TOutItem>,
          TThisEnumerable,
          TThisEnumerator,
          ArrayEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, TOutItem[]> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            BoxedEnumerable<TOutItem>,
            BoxedEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, BoxedEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultEnumerable<TOutItem>,
            OneItemDefaultEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, OneItemDefaultEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificEnumerable<TOutItem>,
            OneItemSpecificEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, OneItemSpecificEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultOrderedEnumerable<TOutItem>,
            OneItemDefaultOrderedEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, OneItemDefaultOrderedEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificOrderedEnumerable<TOutItem>,
            OneItemSpecificOrderedEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, OneItemSpecificOrderedEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>,
            TIdentityEnumerator
        > SelectMany<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(Func<TInItem, int, IdentityEnumerable<TOutItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> selector)
            where TIdentityEnumerator : struct, IStructEnumerator<TOutItem>
            where TIdentityBridger: struct, IStructBridger<TOutItem, TIdentityBridgeType, TIdentityEnumerator>
            where TIdentityBridgeType : class;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TOutItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > SelectMany<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(Func<TInItem, int, ConcatEnumerable<TOutItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> selector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TOutItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TOutItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TInItem, int, EmptyEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            RangeEnumerable<TOutItem>,
            RangeEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, RangeEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            RepeatEnumerable<TOutItem>,
            RepeatEnumerator<TOutItem>
        > SelectMany<TOutItem>(Func<TInItem, int, RepeatEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TInItem, int, SelectEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectIndexedEnumerator<TSelectInItem, TOutItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(Func<TInItem, int, SelectIndexedEnumerable<TSelectInItem, TOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> selector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;


        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TInItem, int, SelectManyBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyIndexedBridgeEnumerator<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(Func<TInItem, int, SelectManyIndexedBridgeEnumerable<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectManyBridger: struct, IStructBridger<TOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TInItem, int, SelectManyEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyIndexedEnumerator<TSelectInItem, TOutItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(Func<TInItem, int, SelectManyIndexedEnumerable<TSelectInItem, TOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> selector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TOutItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereEnumerator<TOutItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TInItem, int, WhereEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereIndexedEnumerator<TOutItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(Func<TInItem, int, WhereIndexedEnumerable<TOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> selector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, DefaultIfEmptyDefaultEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptySpecificEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, DefaultIfEmptySpecificEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            TakeEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, TakeEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            TakeWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, TakeWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            TakeWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, TakeWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            SkipEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, SkipEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            SkipWhileEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, SkipWhileEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            SkipWhileIndexedEnumerator<TOutItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(Func<TInItem, int, SkipWhileIndexedEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> selector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TOutItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>
        > SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TInItem, int, CastEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            OfTypeEnumerator<TCastInItem, TOutItem, TCastInnerEnumerator>
        > SelectMany<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(Func<TInItem, int, OfTypeEnumerable<TCastInItem, TOutItem, TCastInnerEnumerable, TCastInnerEnumerator>> selector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
            ZipEnumerator<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
        > SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
            Func<TInItem, int, ZipEnumerable<TOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> selector
        )
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyCollectionEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyCollectionEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> selector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>,
            SelectSelectEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>
        > SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(
            Func<TInItem, int, SelectSelectEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> selector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>,
            SelectWhereEnumerator<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>
        > SelectMany<TSelectMany_SelectInnerItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(
            Func<TInItem, int, SelectWhereEnumerable<TOutItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> selector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TOutItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>,
            WhereWhereEnumerator<TOutItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>
        > SelectMany<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(
            Func<TInItem, int, WhereWhereEnumerable<TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> selector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>,
            WhereSelectEnumerator<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>
        > SelectMany<TSelectMany_WhereInnerItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(
            Func<TInItem, int, WhereSelectEnumerable<TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> selector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TOutItem, TSelectMany_WhereInnerItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctDefaultEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, int, DistinctDefaultEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctSpecificEnumerator<TOutItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, int, DistinctSpecificEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> selector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TOutItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem>(Func<TInItem, int, EmptyOrderedEnumerable<TOutItem>> selector);

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptDefaultEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, int, ExceptDefaultEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptSpecificEnumerator<TOutItem, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, int, ExceptSpecificEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> selector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptFirstEnumerator>
            where TSelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TOutItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> selector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>,
            GroupedEnumerator<TOutItem>
        > SelectMany<TOutItem, TSelectMany_GroupedKey>(
            Func<TInItem, int, GroupedEnumerable<TSelectMany_GroupedKey, TOutItem>> selector
        );

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>,
            GroupingEnumerator<TOutItem>
        > SelectMany<TOutItem, TSelectMany_GroupingKey>(
            Func<TInItem, int, GroupingEnumerable<TSelectMany_GroupingKey, TOutItem>> selector
        );

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, int, GroupJoinDefaultEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, int, GroupJoinSpecificEnumerable<TOutItem, TSelectMany_GroupJoinKey, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> selector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectDefaultEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, int, IntersectDefaultEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectSpecificEnumerator<TOutItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, int, IntersectSpecificEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> selector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinDefaultEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, int, JoinDefaultEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinSpecificEnumerator<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, int, JoinSpecificEnumerable<TOutItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> selector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, int, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector
        );

        SelectManyIndexedEnumerable<
            TInItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, int, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> selector
        );

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>,
            OrderByEnumerator<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>
        > SelectMany<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>(
            Func<TInItem, int, OrderByEnumerable<TOutItem, TSelectMany_OrderByKey, TSelectMany_OrderByEnumerable, TSelectMany_OrderByEnumerator, TSelectMany_OrderByComparer>> selector
        )
            where TSelectMany_OrderByEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_OrderByEnumerator>
            where TSelectMany_OrderByEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TOutItem, TSelectMany_OrderByKey>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>,
            ReverseEnumerator<TOutItem>
        > SelectMany<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(
            Func<TInItem, int, ReverseEnumerable<TOutItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> selector
        )
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseRangeEnumerable<TOutItem>,
            ReverseRangeEnumerator<TOutItem>
        > SelectMany<TOutItem>(
            Func<TInItem, int, ReverseRangeEnumerable<TOutItem>> selector
        );

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionDefaultEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, int, UnionDefaultEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        SelectManyIndexedEnumerable<
            TInItem,
            TOutItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionSpecificEnumerator<TOutItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, int, UnionSpecificEnumerable<TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> selector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TOutItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TOutItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TOutItem>;

        // collections

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            IEnumerable<TCollectionItem>,
            IEnumerableBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, IEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection,
            DictionaryKeysBridger<TCollectionItem, TDictionaryValue>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>
        > SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(
            Func<TInItem, Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection,
            DictionaryValuesBridger<TDictionaryKey, TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(
            Func<TInItem, Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            HashSet<TCollectionItem>,
            HashSetBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            HashSetEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, HashSet<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            LinkedList<TCollectionItem>,
            LinkedListBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            LinkedListEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, LinkedList<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            List<TCollectionItem>,
            ListBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            ListEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, List<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Queue<TCollectionItem>,
            QueueBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            QueueEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, Queue<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection,
           SortedDictionaryKeysBridger<TCollectionItem, TDictionaryValue>,
           TThisEnumerable,
           TThisEnumerator,
           SortedDictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>
       > SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(
           Func<TInItem, SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector,
           Func<TInItem, TCollectionItem, TOutItem> resultSelector
       );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection,
            SortedDictionaryValuesBridger<TDictionaryKey, TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            SortedDictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(
            Func<TInItem, SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            SortedSet<TCollectionItem>,
            SortedSetBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            SortedSetEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, SortedSet<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Stack<TCollectionItem>,
            StackBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            StackEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, Stack<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TCollectionItem[],
            ArrayBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            ArrayEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, TCollectionItem[]> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            BoxedEnumerable<TCollectionItem>,
            BoxedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, BoxedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultEnumerable<TCollectionItem>,
            OneItemDefaultEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, OneItemDefaultEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificEnumerable<TCollectionItem>,
            OneItemSpecificEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, OneItemSpecificEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultOrderedEnumerable<TCollectionItem>,
            OneItemDefaultOrderedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, OneItemDefaultOrderedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificOrderedEnumerable<TCollectionItem>,
            OneItemSpecificOrderedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, OneItemSpecificOrderedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>,
            TIdentityEnumerator
        > SelectMany<TOutItem, TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(
            Func<TInItem, IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TIdentityEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TIdentityBridger: struct, IStructBridger<TCollectionItem, TIdentityBridgeType, TIdentityEnumerator>
            where TIdentityBridgeType : class;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TCollectionItem, TCastInnerEnumerator>
        > SelectMany<TCastInItem, TOutItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>(
            Func<TInItem, CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TCollectionItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(
            Func<TInItem, ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TConcatFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            Func<TInItem, DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptySpecificEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            Func<TInItem, DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, EmptyEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>,
            OfTypeEnumerator<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerator>
        > SelectMany<TOfTypeInItem, TOutItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(
            Func<TInItem, OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            RangeEnumerable<TCollectionItem>,
            RangeEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, RangeEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            RepeatEnumerable<TCollectionItem>,
            RepeatEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, RepeatEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
            Func<TInItem, SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
            Func<TInItem, SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyIndexedBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TSelectManyBridgeType, TSelectManyBridger, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(
            Func<TInItem, SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(
            Func<TInItem, SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           TThisEnumerable,
           TThisEnumerator,
           SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
           SelectManyCollectionEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           Func<TInItem, SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector,
           Func<TInItem, TCollectionItem, TOutItem> resultSelector
       )
           where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
           where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
           where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
           where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyCollectionEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           TThisEnumerable,
           TThisEnumerator,
           SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
           SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           Func<TInItem, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector,
           Func<TInItem, TCollectionItem, TOutItem> resultSelector
       )
           where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
           where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
           where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
           where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipEnumerator<TCollectionItem, TSkipInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            Func<TInItem, SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipWhileEnumerator<TCollectionItem, TSkipInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            Func<TInItem, SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipWhileIndexedEnumerator<TCollectionItem, TSkipInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            Func<TInItem, SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeEnumerator<TCollectionItem, TTakeInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            Func<TInItem, TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeWhileEnumerator<TCollectionItem, TTakeInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            Func<TInItem, TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeWhileIndexedEnumerator<TCollectionItem, TTakeInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            Func<TInItem, TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereEnumerator<TCollectionItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            Func<TInItem, WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereIndexedEnumerator<TCollectionItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            Func<TInItem, WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
            ZipEnumerator<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
        > SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TCollectionItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
            Func<TInItem, ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>,
            SelectSelectEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>
        > SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(
            Func<TInItem, SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>,
            SelectWhereEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>
        > SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(
            Func<TInItem, SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>,
            WhereWhereEnumerator<TCollectionItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(
            Func<TInItem, WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>,
            WhereSelectEnumerator<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(
            Func<TInItem, WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TCollectionItem, TSelectMany_WhereInnerItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctDefaultEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctSpecificEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, EmptyOrderedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptDefaultEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptSpecificEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectDefaultEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectSpecificEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionDefaultEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionSpecificEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>,
            GroupedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(
            Func<TInItem, GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>,
            GroupingEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(
            Func<TInItem, GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinDefaultEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinSpecificEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector
        );

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>,
            OrderByEnumerator<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>(
            Func<TInItem, OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_OrderByInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_OrderByInnerEnumerator>
            where TSelectMany_OrderByInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TCollectionItem, TSelectMany_OrderByKey>;

        SelectManyCollectionEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>,
            ReverseEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(
            Func<TInItem, ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           TThisEnumerable,
           TThisEnumerator,
           ReverseRangeEnumerable<TCollectionItem>,
           ReverseRangeEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, ReverseRangeEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

       // Collections indexed

       SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            IEnumerable<TCollectionItem>,
            IEnumerableBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, IEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection,
            DictionaryKeysBridger<TCollectionItem, TDictionaryValue>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>
        > SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(
            Func<TInItem, int, Dictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection,
            DictionaryValuesBridger<TDictionaryKey, TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            DictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(
            Func<TInItem, int, Dictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            HashSet<TCollectionItem>,
            HashSetBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            HashSetEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, HashSet<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            LinkedList<TCollectionItem>,
            LinkedListBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            LinkedListEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, LinkedList<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            List<TCollectionItem>,
            ListBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            ListEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, List<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Queue<TCollectionItem>,
            QueueBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            QueueEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, Queue<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection,
           SortedDictionaryKeysBridger<TCollectionItem, TDictionaryValue>,
           TThisEnumerable,
           TThisEnumerator,
           SortedDictionaryKeysEnumerator<TCollectionItem, TDictionaryValue>
       > SelectMany<TOutItem, TCollectionItem, TDictionaryValue>(
           Func<TInItem, int, SortedDictionary<TCollectionItem, TDictionaryValue>.KeyCollection> collectionSelector,
           Func<TInItem, TCollectionItem, TOutItem> resultSelector
       );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection,
            SortedDictionaryValuesBridger<TDictionaryKey, TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            SortedDictionaryValuesEnumerator<TDictionaryKey, TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TDictionaryKey>(
            Func<TInItem, int, SortedDictionary<TDictionaryKey, TCollectionItem>.ValueCollection> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            SortedSet<TCollectionItem>,
            SortedSetBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            SortedSetEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, SortedSet<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            Stack<TCollectionItem>,
            StackBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            StackEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, Stack<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedBridgeEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TCollectionItem[],
            ArrayBridger<TCollectionItem>,
            TThisEnumerable,
            TThisEnumerator,
            ArrayEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, TCollectionItem[]> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            BoxedEnumerable<TCollectionItem>,
            BoxedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, BoxedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultEnumerable<TCollectionItem>,
            OneItemDefaultEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, OneItemDefaultEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificEnumerable<TCollectionItem>,
            OneItemSpecificEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, OneItemSpecificEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultOrderedEnumerable<TCollectionItem>,
            OneItemDefaultOrderedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, OneItemDefaultOrderedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificOrderedEnumerable<TCollectionItem>,
            OneItemSpecificOrderedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, OneItemSpecificOrderedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>,
            TIdentityEnumerator
        > SelectMany<TOutItem, TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(
            Func<TInItem, int, IdentityEnumerable<TCollectionItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TIdentityEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TIdentityBridger: struct, IStructBridger<TCollectionItem, TIdentityBridgeType, TIdentityEnumerator>
            where TIdentityBridgeType : class;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TCollectionItem, TCastInnerEnumerator>
        > SelectMany<TCastInItem, TOutItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>(
            Func<TInItem, int, CastEnumerable<TCastInItem, TCollectionItem, TCastInnerEnumerable, TCastInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TCollectionItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(
            Func<TInItem, int, ConcatEnumerable<TCollectionItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TConcatFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            Func<TInItem, int, DefaultIfEmptyDefaultEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptySpecificEnumerator<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            Func<TInItem, int, DefaultIfEmptySpecificEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, EmptyEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>,
            OfTypeEnumerator<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerator>
        > SelectMany<TOfTypeInItem, TOutItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(
            Func<TInItem, int, OfTypeEnumerable<TOfTypeInItem, TCollectionItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            RangeEnumerable<TCollectionItem>,
            RangeEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, RangeEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            RepeatEnumerable<TCollectionItem>,
            RepeatEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, RepeatEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
            Func<TInItem, int, SelectEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectInnerEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
            Func<TInItem, int, SelectIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectInnerEnumerable, TSelectInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyIndexedBridgeEnumerator<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyIndexedBridgeEnumerable<TSelectInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(
            Func<TInItem, int, SelectManyEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>,
            SelectManyIndexedEnumerator<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>
        > SelectMany<TSelectInItem, TOutItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>(
            Func<TInItem, int, SelectManyIndexedEnumerable<TSelectInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyInnerProjectedEnumerable, TSelectManyInnerProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
            where TSelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyInnerProjectedEnumerator>
            where TSelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            Func<TInItem, int, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        SelectManyCollectionIndexedEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           TThisEnumerable,
           TThisEnumerator,
           SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
           SelectManyCollectionEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           Func<TInItem, int, SelectManyCollectionEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector,
           Func<TInItem, TCollectionItem, TOutItem> resultSelector
       )
           where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
           where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
           where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
           where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           TThisEnumerable,
           TThisEnumerator,
           SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
           SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > SelectMany<TSelectManyInItem, TOutItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           Func<TInItem, int, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TCollectionItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> collectionSelector,
           Func<TInItem, TCollectionItem, TOutItem> resultSelector
       )
           where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
           where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
           where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
           where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipEnumerator<TCollectionItem, TSkipInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            Func<TInItem, int, SkipEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipWhileEnumerator<TCollectionItem, TSkipInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            Func<TInItem, int, SkipWhileEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipWhileIndexedEnumerator<TCollectionItem, TSkipInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            Func<TInItem, int, SkipWhileIndexedEnumerable<TCollectionItem, TSkipInnerEnumerable, TSkipInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeEnumerator<TCollectionItem, TTakeInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            Func<TInItem, int, TakeEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeWhileEnumerator<TCollectionItem, TTakeInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            Func<TInItem, int, TakeWhileEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeWhileIndexedEnumerator<TCollectionItem, TTakeInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            Func<TInItem, int, TakeWhileIndexedEnumerable<TCollectionItem, TTakeInnerEnumerable, TTakeInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereEnumerator<TCollectionItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            Func<TInItem, int, WhereEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereIndexedEnumerator<TCollectionItem, TWhereInnerEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            Func<TInItem, int, WhereIndexedEnumerable<TCollectionItem, TWhereInnerEnumerable, TWhereInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
            ZipEnumerator<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
        > SelectMany<TZipFirstItem, TZipSecondItem, TOutItem, TCollectionItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
            Func<TInItem, int, ZipEnumerable<TCollectionItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>,
            SelectSelectEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>
        > SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>(
            Func<TInItem, int, SelectSelectEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>,
            SelectWhereEnumerator<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>
        > SelectMany<TSelectMany_SelectInnerItem, TCollectionItem, TOutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>(
            Func<TInItem, int, SelectWhereEnumerable<TCollectionItem, TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator, TSelectMany_SelectProjection, TSelectMany_SelectPredicate>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInnerItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInnerItem>
            where TSelectMany_SelectProjection : struct, IStructProjection<TCollectionItem, TSelectMany_SelectInnerItem>
            where TSelectMany_SelectPredicate : struct, IStructPredicate<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>,
            WhereWhereEnumerator<TCollectionItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>(
            Func<TInItem, int, WhereWhereEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>,
            WhereSelectEnumerator<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>(
            Func<TInItem, int, WhereSelectEnumerable<TCollectionItem, TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator, TSelectMany_WherePredicate, TSelectMany_WhereProjection>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_WhereInnerItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_WhereInnerItem>
            where TSelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_WhereInnerItem>
            where TSelectMany_WhereProjection : struct, IStructProjection<TCollectionItem, TSelectMany_WhereInnerItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctDefaultEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, int, DistinctDefaultEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>,
            DistinctSpecificEnumerator<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>(
            Func<TInItem, int, DistinctSpecificEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerable, TSelectMany_DistinctInnerEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_DistinctInnerEnumerator>
            where TSelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TCollectionItem>;

        EmptyEnumerable<TOutItem> SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, EmptyOrderedEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptDefaultEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, int, ExceptDefaultEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>,
            ExceptSpecificEnumerator<TCollectionItem, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>(
            Func<TInItem, int, ExceptSpecificEnumerable<TCollectionItem, TSelectMany_ExceptFirstEnumerable, TSelectMany_ExceptionFirstEnumerator, TSelectMany_ExceptSecondEnumerable, TSelectMany_ExceptSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptionFirstEnumerator>
            where TSelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ExceptSecondEnumerator>
            where TSelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectDefaultEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, int, IntersectDefaultEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>,
            IntersectSpecificEnumerator<TCollectionItem, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>(
            Func<TInItem, int, IntersectSpecificEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerable, TSelectMany_IntersectFirstEnumerator, TSelectMany_IntersectSecondEnumerable, TSelectMany_IntersectSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectFirstEnumerator>
            where TSelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_IntersectSecondEnumerator>
            where TSelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionDefaultEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, int, UnionDefaultEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>,
            UnionSpecificEnumerator<TCollectionItem, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerator>
        > SelectMany<TCollectionItem, TOutItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>(
            Func<TInItem, int, UnionSpecificEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerable, TSelectMany_UnionFirstEnumerator, TSelectMany_UnionSecondEnumerable, TSelectMany_UnionSecondEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionFirstEnumerator>
            where TSelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_UnionSecondEnumerator>
            where TSelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupByDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupBySpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupBySpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_GroupByKey, TSelectMany_GroupByElement>, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>,
            GroupedEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(
            Func<TInItem, int, GroupedEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>,
            GroupingEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupedKey>(
            Func<TInItem, int, GroupingEnumerable<TSelectMany_GroupedKey, TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupByCollectionDefaultEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerator>
        > SelectMany<TOutItem, TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>(
            Func<TInItem, int, GroupByCollectionSpecificEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByKey, TSelectMany_GroupByElement, TCollectionItem, TSelectMany_GroupByEnumerable, TSelectMany_GroupByEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_GroupByInItem, TSelectMany_GroupByEnumerator>
            where TSelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_GroupByInItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, int, GroupJoinDefaultEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>(
            Func<TInItem, int, GroupJoinSpecificEnumerable<TCollectionItem, TSelectMany_GroupJoinKeyItem, TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerable, TSelectMany_GroupJoinLeftEnumerator, TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerable, TSelectMany_GroupJoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinLeftItem, TSelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinLeftItem>
            where TSelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_GroupJoinRightItem, TSelectMany_GroupJoinRightEnumerator>
            where TSelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_GroupJoinRightItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinDefaultEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, int, JoinDefaultEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>,
            JoinSpecificEnumerator<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>(
            Func<TInItem, int, JoinSpecificEnumerable<TCollectionItem, TSelectMany_JoinKeyItem, TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerable, TSelectMany_JoinLeftEnumerator, TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerable, TSelectMany_JoinRightEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_JoinLeftItem, TSelectMany_JoinLeftEnumerator>
            where TSelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_JoinLeftItem>
            where TSelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_JoinRightItem, TSelectMany_JoinRightEnumerator>
            where TSelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_JoinRightItem>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupDefaultEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, int, LookupDefaultEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>,
            LookupSpecificEnumerator<TSelectMany_LookupKey, TSelectMany_LookupElement>
        > SelectMany<TOutItem, TSelectMany_LookupKey, TSelectMany_LookupElement>(
            Func<TInItem, int, LookupSpecificEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>> collectionSelector,
            Func<TInItem, GroupingEnumerable<TSelectMany_LookupKey, TSelectMany_LookupElement>, TOutItem> resultSelector
        );

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>,
            OrderByEnumerator<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>(
            Func<TInItem, int, OrderByEnumerable<TCollectionItem, TSelectMany_OrderByKey, TSelectMany_OrderByInnerEnumerable, TSelectMany_OrderByInnerEnumerator, TSelectMany_OrderByComparer>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_OrderByInnerEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_OrderByInnerEnumerator>
            where TSelectMany_OrderByInnerEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectMany_OrderByComparer : struct, IStructComparer<TCollectionItem, TSelectMany_OrderByKey>;

        SelectManyCollectionIndexedEnumerable<
            TInItem,
            TOutItem,
            TCollectionItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>,
            ReverseEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>(
            Func<TInItem, int, ReverseEnumerable<TCollectionItem, TSelectMany_ReverseEnumerable, TSelectMany_ReverseEnumerator>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        )
            where TSelectMany_ReverseEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectMany_ReverseEnumerator>
            where TSelectMany_ReverseEnumerator : struct, IStructEnumerator<TCollectionItem>;

        SelectManyCollectionIndexedEnumerable<
           TInItem,
           TOutItem,
           TCollectionItem,
           TThisEnumerable,
           TThisEnumerator,
           ReverseRangeEnumerable<TCollectionItem>,
           ReverseRangeEnumerator<TCollectionItem>
        > SelectMany<TOutItem, TCollectionItem>(
            Func<TInItem, int, ReverseRangeEnumerable<TCollectionItem>> collectionSelector,
            Func<TInItem, TCollectionItem, TOutItem> resultSelector
        );
    }
}