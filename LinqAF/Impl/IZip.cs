using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Impl
{
    internal interface IZip<TThisItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TThisItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TThisItem>
    {
        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            BoxedEnumerable<TInnerItem>,
            BoxedEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            BoxedEnumerable<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, IEnumerable<TInnerItem>, IEnumerableBridger<TInnerItem>, IdentityEnumerator<TInnerItem>>,
            IdentityEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            IEnumerable<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, Dictionary<TInnerItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TInnerItem, TDictionaryValue>, DictionaryKeysEnumerator<TInnerItem, TDictionaryValue>>,
            DictionaryKeysEnumerator<TInnerItem, TDictionaryValue>
        > Zip<TInnerItem, TOutItem, TDictionaryValue>(
            Dictionary<TInnerItem, TDictionaryValue>.KeyCollection second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, Dictionary<TDictionaryKey, TInnerItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TInnerItem>, DictionaryValuesEnumerator<TDictionaryKey, TInnerItem>>,
            DictionaryValuesEnumerator<TDictionaryKey, TInnerItem>
        > Zip<TInnerItem, TOutItem, TDictionaryKey>(
            Dictionary<TDictionaryKey, TInnerItem>.ValueCollection second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, HashSet<TInnerItem>, HashSetBridger<TInnerItem>, HashSetEnumerator<TInnerItem>>,
            HashSetEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            HashSet<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, LinkedList<TInnerItem>, LinkedListBridger<TInnerItem>, LinkedListEnumerator<TInnerItem>>,
            LinkedListEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            LinkedList<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, List<TInnerItem>, ListBridger<TInnerItem>, ListEnumerator<TInnerItem>>,
            ListEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            List<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, Queue<TInnerItem>, QueueBridger<TInnerItem>, QueueEnumerator<TInnerItem>>,
            QueueEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            Queue<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, SortedDictionary<TInnerItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TInnerItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TInnerItem, TDictionaryValue>>,
            SortedDictionaryKeysEnumerator<TInnerItem, TDictionaryValue>
        > Zip<TInnerItem, TOutItem, TDictionaryValue>(
            SortedDictionary<TInnerItem, TDictionaryValue>.KeyCollection second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, SortedDictionary<TDictionaryKey, TInnerItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TInnerItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TInnerItem>>,
            SortedDictionaryValuesEnumerator<TDictionaryKey, TInnerItem>
        > Zip<TInnerItem, TOutItem, TDictionaryKey>(
            SortedDictionary<TDictionaryKey, TInnerItem>.ValueCollection second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, SortedSet<TInnerItem>, SortedSetBridger<TInnerItem>, SortedSetEnumerator<TInnerItem>>,
            SortedSetEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            SortedSet<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, Stack<TInnerItem>, StackBridger<TInnerItem>, StackEnumerator<TInnerItem>>,
            StackEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            Stack<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TInnerItem, TInnerItem[], ArrayBridger<TInnerItem>, ArrayEnumerator<TInnerItem>>,
            ArrayEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            TInnerItem[] second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TIdentityItem,
            TThisEnumerable,
            TThisEnumerator,
            IdentityEnumerable<TIdentityItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>,
            TIdentityEnumerator
        > Zip<TOutItem, TIdentityItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(
            IdentityEnumerable<TIdentityItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator> second,
            Func<TThisItem, TIdentityItem, TOutItem> resultSelector
        )
            where TIdentityEnumerator : struct, IStructEnumerator<TIdentityItem>
            where TIdentityBridger: struct, IStructBridger<TIdentityItem, TIdentityBridgeType, TIdentityEnumerator>
            where TIdentityBridgeType : class;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TCastOutItem,
            TThisEnumerable,
            TThisEnumerator,
            CastEnumerable<TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TCastOutItem, TCastInnerEnumerator>
        > Zip<TOutItem, TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(
            CastEnumerable<TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator> second,
            Func<TThisItem, TCastOutItem, TOutItem> resultSelector
        )
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TConcatItem,
            TThisEnumerable,
            TThisEnumerator,
            ConcatEnumerable<TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TConcatItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > Zip<TOutItem, TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(
            ConcatEnumerable<TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> second,
            Func<TThisItem, TConcatItem, TOutItem> resultSelector
        )
            where TConcatFirstEnumerable : struct, IStructEnumerable<TConcatItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TConcatItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TConcatItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TConcatItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TDefaultItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptyDefaultEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TDefaultItem, TDefaultInnerEnumerator>
        > Zip<TOutItem, TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>(
            DefaultIfEmptyDefaultEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator> second,
            Func<TThisItem, TDefaultItem, TOutItem> resultSelector
        )
            where TDefaultInnerEnumerable : struct, IStructEnumerable<TDefaultItem, TDefaultInnerEnumerator>
            where TDefaultInnerEnumerator : struct, IStructEnumerator<TDefaultItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TDefaultItem,
            TThisEnumerable,
            TThisEnumerator,
            DefaultIfEmptySpecificEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>,
            DefaultIfEmptySpecificEnumerator<TDefaultItem, TDefaultInnerEnumerator>
        > Zip<TOutItem, TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>(
            DefaultIfEmptySpecificEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator> second,
            Func<TThisItem, TDefaultItem, TOutItem> resultSelector
        )
            where TDefaultInnerEnumerable : struct, IStructEnumerable<TDefaultItem, TDefaultInnerEnumerator>
            where TDefaultInnerEnumerator : struct, IStructEnumerator<TDefaultItem>;

        EmptyEnumerable<TOutItem> Zip<TOutItem, TEmptyItem>(EmptyEnumerable<TEmptyItem> second, Func<TThisItem, TEmptyItem, TOutItem> resultSelector);

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TOfTypeOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OfTypeEnumerable<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>,
            OfTypeEnumerator<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerator>
        > Zip<TOutItem, TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(
            OfTypeEnumerable<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> second,
            Func<TThisItem, TOfTypeOutItem, TOutItem> resultSelector
        )
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TRangeItem,
            TThisEnumerable,
            TThisEnumerator,
            RangeEnumerable<TRangeItem>,
            RangeEnumerator<TRangeItem>
        > Zip<TOutItem, TRangeItem>(RangeEnumerable<TRangeItem> second, Func<TThisItem, TRangeItem, TOutItem> resultSelector);

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TRangeItem,
            TThisEnumerable,
            TThisEnumerator,
            RepeatEnumerable<TRangeItem>,
            RepeatEnumerator<TRangeItem>
        > Zip<TOutItem, TRangeItem>(RepeatEnumerable<TRangeItem> second, Func<TThisItem, TRangeItem, TOutItem> resultSelector);

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectEnumerator<TSelectInItem, TSelectOutItem, TSelectInnerEnumerator>
        > Zip<TOutItem, TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
            SelectEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second,
            Func<TThisItem, TSelectOutItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable: struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator: struct, IStructEnumerator<TSelectInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectIndexedEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
            SelectIndexedEnumerator<TSelectInItem, TSelectOutItem, TSelectInnerEnumerator>
        > Zip<TOutItem, TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
            SelectIndexedEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second,
            Func<TThisItem, TSelectOutItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyOutItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyOutItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyIndexedEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyOutItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
            where TSelectManyBridgeType : class;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyCollectionEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyCollectionEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectManyOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
            SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
        > Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second,
            Func<TThisItem, TSelectManyOutItem, TOutItem> resultSelector
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSkipOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipEnumerator<TSkipOutItem, TSkipInnerEnumerator>
       > Zip<TOutItem, TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second,
            Func<TThisItem, TSkipOutItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TSkipOutItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TSkipOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSkipOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipWhileEnumerator<TSkipOutItem, TSkipInnerEnumerator>
       > Zip<TOutItem, TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipWhileEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second,
            Func<TThisItem, TSkipOutItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TSkipOutItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TSkipOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSkipOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SkipWhileIndexedEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
            SkipWhileIndexedEnumerator<TSkipOutItem, TSkipInnerEnumerator>
       > Zip<TOutItem, TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipWhileIndexedEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second,
            Func<TThisItem, TSkipOutItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TSkipOutItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TSkipOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TTakeOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeEnumerator<TTakeOutItem, TTakeInnerEnumerator>
       > Zip<TOutItem, TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second,
            Func<TThisItem, TTakeOutItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TTakeOutItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TTakeOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TTakeOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeWhileEnumerator<TTakeOutItem, TTakeInnerEnumerator>
       > Zip<TOutItem, TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeWhileEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second,
            Func<TThisItem, TTakeOutItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TTakeOutItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TTakeOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TTakeOutItem,
            TThisEnumerable,
            TThisEnumerator,
            TakeWhileIndexedEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
            TakeWhileIndexedEnumerator<TTakeOutItem, TTakeInnerEnumerator>
       > Zip<TOutItem, TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeWhileIndexedEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second,
            Func<TThisItem, TTakeOutItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TTakeOutItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TTakeOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TWhereOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereEnumerator<TWhereOutItem, TWhereInnerEnumerator>
        > Zip<TOutItem, TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            WhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second,
            Func<TThisItem, TWhereOutItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TWhereOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereIndexedEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
            WhereIndexedEnumerator<TWhereOutItem, TWhereInnerEnumerator>
        > Zip<TOutItem, TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            WhereIndexedEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second,
            Func<TThisItem, TWhereOutItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TZipOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ZipEnumerable<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
            ZipEnumerator<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
        > Zip<TOutItem, TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
            ZipEnumerable<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second,
            Func<TThisItem, TZipOutItem, TOutItem> resultSelector
        )
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectSelectEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>,
            SelectSelectEnumerator<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>
        > Zip<TOutItem, TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(
            SelectSelectEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> second,
            Func<TThisItem, TSelectOutItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TSelectOutItem, TSelectInnerItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TSelectOutItem,
            TThisEnumerable,
            TThisEnumerator,
            SelectWhereEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>,
            SelectWhereEnumerator<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>
        > Zip<TOutItem, TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(
            SelectWhereEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> second,
            Func<TThisItem, TSelectOutItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TSelectOutItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TSelectOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TWhereOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereWhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>,
            WhereWhereEnumerator<TWhereOutItem, TWhereInnerEnumerator, TWherePredicate>
        > Zip<TOutItem, TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(
            WhereWhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> second,
            Func<TThisItem, TWhereOutItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereOutItem>
            where TWherePredicate : struct, IStructPredicate<TWhereOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TWhereOutItem,
            TThisEnumerable,
            TThisEnumerator,
            WhereSelectEnumerable<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>,
            WhereSelectEnumerator<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>
        > Zip<TOutItem, TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(
            WhereSelectEnumerable<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> second,
            Func<TThisItem, TWhereOutItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TWhereOutItem, TWhereInnerItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TDistinctOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctDefaultEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>,
            DistinctDefaultEnumerator<TDistinctOutItem, TDistinctInnerEnumerator>
        > Zip<TOutItem, TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(
            DistinctDefaultEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second,
            Func<TThisItem, TDistinctOutItem, TOutItem> resultSelector
        )
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TDistinctOutItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TDistinctOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TDistinctOutItem,
            TThisEnumerable,
            TThisEnumerator,
            DistinctSpecificEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>,
            DistinctSpecificEnumerator<TDistinctOutItem, TDistinctInnerEnumerator>
        > Zip<TOutItem, TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(
            DistinctSpecificEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second,
            Func<TThisItem, TDistinctOutItem, TOutItem> resultSelector
        )
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TDistinctOutItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TDistinctOutItem>;

        EmptyEnumerable<TOutItem> Zip<TOutItem, TEmptyOrderedItem>(EmptyOrderedEnumerable<TEmptyOrderedItem> second, Func<TThisItem, TEmptyOrderedItem, TOutItem> resultSelector);

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TExceptOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptDefaultEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>,
            ExceptDefaultEnumerator<TExceptOutItem, TExceptFirstEnumerator, TExceptSecondEnumerator>
        > Zip<TOutItem, TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(
            ExceptDefaultEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second,
            Func<TThisItem, TExceptOutItem, TOutItem> resultSelector
        )
            where TExceptFirstEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TExceptOutItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TExceptOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TExceptOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ExceptSpecificEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>,
            ExceptSpecificEnumerator<TExceptOutItem, TExceptFirstEnumerator, TExceptSecondEnumerator>
        > Zip<TOutItem, TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(
            ExceptSpecificEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second,
            Func<TThisItem, TExceptOutItem, TOutItem> resultSelector
        )
            where TExceptFirstEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TExceptOutItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TExceptOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TIntersectOutItem,
            TThisEnumerable,
            TThisEnumerator,
            IntersectDefaultEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>,
            IntersectDefaultEnumerator<TIntersectOutItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>
        > Zip<TOutItem, TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(
            IntersectDefaultEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second,
            Func<TThisItem, TIntersectOutItem, TOutItem> resultSelector
        )
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TIntersectOutItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TIntersectOutItem>;

        ZipEnumerable<
           TOutItem,
           TThisItem,
           TIntersectOutItem,
           TThisEnumerable,
           TThisEnumerator,
           IntersectSpecificEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>,
           IntersectSpecificEnumerator<TIntersectOutItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>
       > Zip<TOutItem, TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(
           IntersectSpecificEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second,
           Func<TThisItem, TIntersectOutItem, TOutItem> resultSelector
       )
           where TIntersectFirstEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectFirstEnumerator>
           where TIntersectFirstEnumerator : struct, IStructEnumerator<TIntersectOutItem>
           where TIntersectSecondEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectSecondEnumerator>
           where TIntersectSecondEnumerator : struct, IStructEnumerator<TIntersectOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TUnionOutItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionDefaultEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>,
            UnionDefaultEnumerator<TUnionOutItem, TUnionFirstEnumerator, TUnionSecondEnumerator>
        > Zip<TOutItem, TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(
            UnionDefaultEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second,
            Func<TThisItem, TUnionOutItem, TOutItem> resultSelector
        )
            where TUnionFirstEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TUnionOutItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TUnionOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TUnionOutItem,
            TThisEnumerable,
            TThisEnumerator,
            UnionSpecificEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>,
            UnionSpecificEnumerator<TUnionOutItem, TUnionFirstEnumerator, TUnionSecondEnumerator>
        > Zip<TOutItem, TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(
            UnionSpecificEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second,
            Func<TThisItem, TUnionOutItem, TOutItem> resultSelector
        )
            where TUnionFirstEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TUnionOutItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TUnionOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            GroupingEnumerable<TGroupByKey, TGroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>
        > Zip<TOutItem, TGroupByKey, TGroupByElement, TGroupByInItem, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> second,
            Func<TThisItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            GroupingEnumerable<TGroupByKey, TGroupByElement>,
            TThisEnumerable,
            TThisEnumerator,
            GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>,
            GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>
        > Zip<TOutItem, TGroupByKey, TGroupByElement, TGroupByInItem, TGroupByEnumerable, TGroupByEnumerator>(
            GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> second,
            Func<TThisItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            GroupingEnumerable<TLookupKey, TLookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupDefaultEnumerable<TLookupKey, TLookupElement>,
            LookupDefaultEnumerator<TLookupKey, TLookupElement>
        > Zip<TOutItem, TLookupKey, TLookupElement>(
            LookupDefaultEnumerable<TLookupKey, TLookupElement> second,
            Func<TThisItem, GroupingEnumerable<TLookupKey, TLookupElement>, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            GroupingEnumerable<TLookupKey, TLookupElement>,
            TThisEnumerable,
            TThisEnumerator,
            LookupSpecificEnumerable<TLookupKey, TLookupElement>,
            LookupSpecificEnumerator<TLookupKey, TLookupElement>
        > Zip<TOutItem, TLookupKey, TLookupElement>(
            LookupSpecificEnumerable<TLookupKey, TLookupElement> second,
            Func<TThisItem, GroupingEnumerable<TLookupKey, TLookupElement>, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TGroupedOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupedEnumerable<TGroupedKey, TGroupedOutItem>,
            GroupedEnumerator<TGroupedOutItem>
        > Zip<TOutItem, TGroupedKey, TGroupedOutItem>(
            GroupedEnumerable<TGroupedKey, TGroupedOutItem> second,
            Func<TThisItem, TGroupedOutItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TGroupedOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupingEnumerable<TGroupedKey, TGroupedOutItem>,
            GroupingEnumerator<TGroupedOutItem>
        > Zip<TOutItem, TGroupedKey, TGroupedOutItem>(
            GroupingEnumerable<TGroupedKey, TGroupedOutItem> second,
            Func<TThisItem, TGroupedOutItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TGroupJoinOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinDefaultEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
            GroupJoinDefaultEnumerator<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
        > Zip<TOutItem, TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinDefaultEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
            Func<TThisItem, TGroupJoinOutItem, TOutItem> resultSelector
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TGroupJoinOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupJoinSpecificEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
            GroupJoinSpecificEnumerator<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
        > Zip<TOutItem, TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinSpecificEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
            Func<TThisItem, TGroupJoinOutItem, TOutItem> resultSelector
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TJoinOutItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinDefaultEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
            JoinDefaultEnumerator<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Zip<TOutItem, TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
            Func<TThisItem, TJoinOutItem, TOutItem> resultSelector
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TJoinOutItem,
            TThisEnumerable,
            TThisEnumerator,
            JoinSpecificEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
            JoinSpecificEnumerator<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Zip<TOutItem, TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
            Func<TThisItem, TJoinOutItem, TOutItem> resultSelector
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TOrderByOutItem,
            TThisEnumerable,
            TThisEnumerator,
            OrderByEnumerable<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>,
            OrderByEnumerator<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>
        > Zip<TOutItem, TOrderByKey, TOrderByOutItem, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>(
            OrderByEnumerable<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer> second,
            Func<TThisItem, TOrderByOutItem, TOutItem> resultSelector
        )
            where TOrderByInnerEnumerable : struct, IStructEnumerable<TOrderByOutItem, TOrderByInnerEnumerator>
            where TOrderByInnerEnumerator : struct, IStructEnumerator<TOrderByOutItem>
            where TOrderByComparer : struct, IStructComparer<TOrderByOutItem, TOrderByKey>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TReverseOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseEnumerable<TReverseOutItem, TReverseEnumerable, TReverseEnumerator>,
            ReverseEnumerator<TReverseOutItem>
        > Zip<TOutItem, TReverseOutItem, TReverseEnumerable, TReverseEnumerator>(
            ReverseEnumerable<TReverseOutItem, TReverseEnumerable, TReverseEnumerator> second,
            Func<TThisItem, TReverseOutItem, TOutItem> resultSelector
        )
            where TReverseEnumerable : struct, IStructEnumerable<TReverseOutItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TReverseOutItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TReverseRangeOutItem,
            TThisEnumerable,
            TThisEnumerator,
            ReverseRangeEnumerable<TReverseRangeOutItem>,
            ReverseRangeEnumerator<TReverseRangeOutItem>
        > Zip<TOutItem, TReverseRangeOutItem>(
            ReverseRangeEnumerable<TReverseRangeOutItem> second,
            Func<TThisItem, TReverseRangeOutItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TGroupByOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerator>
        > Zip<TOutItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator> second,
            Func<TThisItem, TGroupByOutItem, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TGroupByOutItem,
            TThisEnumerable,
            TThisEnumerator,
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>,
            GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerator>
        > Zip<TOutItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator> second,
            Func<TThisItem, TGroupByOutItem, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultEnumerable<TInnerItem>,
            OneItemDefaultEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            OneItemDefaultEnumerable<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificEnumerable<TInnerItem>,
            OneItemSpecificEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            OneItemSpecificEnumerable<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemDefaultOrderedEnumerable<TInnerItem>,
            OneItemDefaultOrderedEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            OneItemDefaultOrderedEnumerable<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );

        ZipEnumerable<
            TOutItem,
            TThisItem,
            TInnerItem,
            TThisEnumerable,
            TThisEnumerator,
            OneItemSpecificOrderedEnumerable<TInnerItem>,
            OneItemSpecificOrderedEnumerator<TInnerItem>
        > Zip<TInnerItem, TOutItem>(
            OneItemSpecificOrderedEnumerable<TInnerItem> second,
            Func<TThisItem, TInnerItem, TOutItem> resultSelector
        );
    }
}