using System;
using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class ZipBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IZip<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, BoxedEnumerable<TInnerItem>, BoxedEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(BoxedEnumerable<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, BoxedEnumerable<TInnerItem>, BoxedEnumerator<TInnerItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, IEnumerable<TInnerItem>, IdentityEnumerator<TInnerItem>>, IdentityEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(IEnumerable<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, Dictionary<TInnerItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TInnerItem, TDictionaryValue>>, DictionaryKeysEnumerator<TInnerItem, TDictionaryValue>> Zip<TInnerItem, TOutItem, TDictionaryValue>(Dictionary<TInnerItem, TDictionaryValue>.KeyCollection second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, TDictionaryValue>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, Dictionary<TDictionaryKey, TInnerItem>.ValueCollection, DictionaryValuesEnumerator<TDictionaryKey, TInnerItem>>, DictionaryValuesEnumerator<TDictionaryKey, TInnerItem>> Zip<TInnerItem, TOutItem, TDictionaryKey>(Dictionary<TDictionaryKey, TInnerItem>.ValueCollection second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, TDictionaryKey>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, HashSet<TInnerItem>, HashSetEnumerator<TInnerItem>>, HashSetEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(HashSet<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, LinkedList<TInnerItem>, LinkedListEnumerator<TInnerItem>>, LinkedListEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(LinkedList<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, List<TInnerItem>, ListEnumerator<TInnerItem>>, ListEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(List<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, Queue<TInnerItem>, QueueEnumerator<TInnerItem>>, QueueEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(Queue<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, SortedDictionary<TInnerItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TInnerItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TInnerItem, TDictionaryValue>> Zip<TInnerItem, TOutItem, TDictionaryValue>(SortedDictionary<TInnerItem, TDictionaryValue>.KeyCollection second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, TDictionaryValue>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, SortedDictionary<TDictionaryKey, TInnerItem>.ValueCollection, SortedDictionaryValuesEnumerator<TDictionaryKey, TInnerItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TInnerItem>> Zip<TInnerItem, TOutItem, TDictionaryKey>(SortedDictionary<TDictionaryKey, TInnerItem>.ValueCollection second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, TDictionaryKey>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, SortedSet<TInnerItem>, SortedSetEnumerator<TInnerItem>>, SortedSetEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(SortedSet<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, Stack<TInnerItem>, StackEnumerator<TInnerItem>>, StackEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(Stack<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, IdentityEnumerable<TInnerItem, TInnerItem[], ArrayEnumerator<TInnerItem>>, ArrayEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(TInnerItem[] second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator>(RefThis(), second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TIdentityItem, TEnumerable, TEnumerator, IdentityEnumerable<TIdentityItem, TIdentityBridgeType, TIdentityEnumerator>, TIdentityEnumerator> Zip<TOutItem, TIdentityItem, TIdentityBridgeType, TIdentityEnumerator>(IdentityEnumerable<TIdentityItem, TIdentityBridgeType, TIdentityEnumerator> second, Func<TItem, TIdentityItem, TOutItem> resultSelector)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TIdentityItem>
        => CommonImplementation.Zip<TOutItem, TItem, TIdentityItem, TEnumerable, TEnumerator, IdentityEnumerable<TIdentityItem, TIdentityBridgeType, TIdentityEnumerator>, TIdentityEnumerator>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TCastOutItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TCastOutItem, TCastInnerEnumerator>> Zip<TOutItem, TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator> second, Func<TItem, TCastOutItem, TOutItem> resultSelector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.Zip<TOutItem, TItem, TCastOutItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TCastOutItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TCastOutItem, TCastInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TConcatItem, TEnumerable, TEnumerator, ConcatEnumerable<TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TConcatItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> Zip<TOutItem, TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> second, Func<TItem, TConcatItem, TOutItem> resultSelector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TConcatItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TConcatItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TConcatItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TConcatItem>
        => CommonImplementation.Zip<TOutItem, TItem, TConcatItem, TEnumerable, TEnumerator, ConcatEnumerable<TConcatItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TConcatItem, TConcatFirstEnumerator, TConcatSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TDefaultItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TDefaultItem, TDefaultInnerEnumerator>> Zip<TOutItem, TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator> second, Func<TItem, TDefaultItem, TOutItem> resultSelector)
            where TDefaultInnerEnumerable : struct, IStructEnumerable<TDefaultItem, TDefaultInnerEnumerator>
            where TDefaultInnerEnumerator : struct, IStructEnumerator<TDefaultItem>
        => CommonImplementation.Zip<TOutItem, TItem, TDefaultItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TDefaultItem, TDefaultInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TDefaultItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TDefaultItem, TDefaultInnerEnumerator>> Zip<TOutItem, TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator> second, Func<TItem, TDefaultItem, TOutItem> resultSelector)
            where TDefaultInnerEnumerable : struct, IStructEnumerable<TDefaultItem, TDefaultInnerEnumerator>
            where TDefaultInnerEnumerator : struct, IStructEnumerator<TDefaultItem>
        => CommonImplementation.Zip<TOutItem, TItem, TDefaultItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TDefaultItem, TDefaultInnerEnumerable, TDefaultInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TDefaultItem, TDefaultInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public EmptyEnumerable<TOutItem> Zip<TOutItem, TEmptyItem>(EmptyEnumerable<TEmptyItem> second, Func<TItem, TEmptyItem, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        public ZipEnumerable<TOutItem, TItem, TOfTypeOutItem, TEnumerable, TEnumerator, OfTypeEnumerable<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerator>> Zip<TOutItem, TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> second, Func<TItem, TOfTypeOutItem, TOutItem> resultSelector)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.Zip<TOutItem, TItem, TOfTypeOutItem, TEnumerable, TEnumerator, OfTypeEnumerable<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TOfTypeOutItem, TOfTypeInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TRangeItem, TEnumerable, TEnumerator, RangeEnumerable<TRangeItem>, RangeEnumerator<TRangeItem>> Zip<TOutItem, TRangeItem>(RangeEnumerable<TRangeItem> second, Func<TItem, TRangeItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TRangeItem, TEnumerable, TEnumerator, RangeEnumerable<TRangeItem>, RangeEnumerator<TRangeItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TRangeItem, TEnumerable, TEnumerator, RepeatEnumerable<TRangeItem>, RepeatEnumerator<TRangeItem>> Zip<TOutItem, TRangeItem>(RepeatEnumerable<TRangeItem> second, Func<TItem, TRangeItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TRangeItem, TEnumerable, TEnumerator, RepeatEnumerable<TRangeItem>, RepeatEnumerator<TRangeItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TSelectOutItem, TSelectInnerEnumerator>> Zip<TOutItem, TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second, Func<TItem, TSelectOutItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TSelectOutItem, TSelectInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TSelectOutItem, TSelectInnerEnumerator>> Zip<TOutItem, TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second, Func<TItem, TSelectOutItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TSelectOutItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TSelectOutItem, TSelectInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyOutItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyOutItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> Zip<TOutItem, TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, Func<TItem, TSelectManyOutItem, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectManyOutItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TSelectManyOutItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSkipOutItem, TEnumerable, TEnumerator, SkipEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TSkipOutItem, TSkipInnerEnumerator>> Zip<TOutItem, TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, Func<TItem, TSkipOutItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TSkipOutItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TSkipOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSkipOutItem, TEnumerable, TEnumerator, SkipEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TSkipOutItem, TSkipInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSkipOutItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TSkipOutItem, TSkipInnerEnumerator>> Zip<TOutItem, TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, Func<TItem, TSkipOutItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TSkipOutItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TSkipOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSkipOutItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TSkipOutItem, TSkipInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSkipOutItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TSkipOutItem, TSkipInnerEnumerator>> Zip<TOutItem, TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, Func<TItem, TSkipOutItem, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TSkipOutItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TSkipOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSkipOutItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TSkipOutItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TSkipOutItem, TSkipInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TTakeOutItem, TEnumerable, TEnumerator, TakeEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TTakeOutItem, TTakeInnerEnumerator>> Zip<TOutItem, TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, Func<TItem, TTakeOutItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TTakeOutItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TTakeOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TTakeOutItem, TEnumerable, TEnumerator, TakeEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TTakeOutItem, TTakeInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TTakeOutItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TTakeOutItem, TTakeInnerEnumerator>> Zip<TOutItem, TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, Func<TItem, TTakeOutItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TTakeOutItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TTakeOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TTakeOutItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TTakeOutItem, TTakeInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TTakeOutItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TTakeOutItem, TTakeInnerEnumerator>> Zip<TOutItem, TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, Func<TItem, TTakeOutItem, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TTakeOutItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TTakeOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TTakeOutItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TTakeOutItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TTakeOutItem, TTakeInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TWhereOutItem, TWhereInnerEnumerator>> Zip<TOutItem, TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second, Func<TItem, TWhereOutItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TWhereOutItem, TWhereInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TWhereOutItem, TWhereInnerEnumerator>> Zip<TOutItem, TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second, Func<TItem, TWhereOutItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TWhereOutItem, TWhereInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TZipOutItem, TEnumerable, TEnumerator, ZipEnumerable<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> Zip<TOutItem, TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second, Func<TItem, TZipOutItem, TOutItem> resultSelector)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.Zip<TOutItem, TItem, TZipOutItem, TEnumerable, TEnumerator, ZipEnumerable<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TZipOutItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>, SelectSelectEnumerator<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>> Zip<TOutItem, TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> second, Func<TItem, TSelectOutItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TSelectOutItem, TSelectInnerItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>, SelectSelectEnumerator<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>, SelectWhereEnumerator<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>> Zip<TOutItem, TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> second, Func<TItem, TSelectOutItem, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TSelectOutItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TSelectOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TSelectOutItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>, SelectWhereEnumerator<TSelectOutItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>, WhereWhereEnumerator<TWhereOutItem, TWhereInnerEnumerator, TWherePredicate>> Zip<TOutItem, TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> second, Func<TItem, TWhereOutItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereOutItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereOutItem>
            where TWherePredicate : struct, IStructPredicate<TWhereOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TWhereOutItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>, WhereWhereEnumerator<TWhereOutItem, TWhereInnerEnumerator, TWherePredicate>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>, WhereSelectEnumerator<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>> Zip<TOutItem, TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> second, Func<TItem, TWhereOutItem, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TWhereOutItem, TWhereInnerItem>
        => CommonImplementation.Zip<TOutItem, TItem, TWhereOutItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>, WhereSelectEnumerator<TWhereOutItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TDistinctOutItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctDefaultEnumerator<TDistinctOutItem, TDistinctInnerEnumerator>> Zip<TOutItem, TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second, Func<TItem, TDistinctOutItem, TOutItem> resultSelector)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TDistinctOutItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TDistinctOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TDistinctOutItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctDefaultEnumerator<TDistinctOutItem, TDistinctInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TDistinctOutItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctSpecificEnumerator<TDistinctOutItem, TDistinctInnerEnumerator>> Zip<TOutItem, TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second, Func<TItem, TDistinctOutItem, TOutItem> resultSelector)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TDistinctOutItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TDistinctOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TDistinctOutItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TDistinctOutItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctSpecificEnumerator<TDistinctOutItem, TDistinctInnerEnumerator>>(RefThis(), ref second, resultSelector);

        public EmptyEnumerable<TOutItem> Zip<TOutItem, TEmptyOrderedItem>(EmptyOrderedEnumerable<TEmptyOrderedItem> second, Func<TItem, TEmptyOrderedItem, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        public ZipEnumerable<TOutItem, TItem, TExceptOutItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TExceptOutItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Zip<TOutItem, TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, Func<TItem, TExceptOutItem, TOutItem> resultSelector)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TExceptOutItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TExceptOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TExceptOutItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TExceptOutItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TExceptOutItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TExceptOutItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Zip<TOutItem, TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, Func<TItem, TExceptOutItem, TOutItem> resultSelector)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TExceptOutItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TExceptOutItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TExceptOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TExceptOutItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TExceptOutItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TExceptOutItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TIntersectOutItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TIntersectOutItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Zip<TOutItem, TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, Func<TItem, TIntersectOutItem, TOutItem> resultSelector)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TIntersectOutItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TIntersectOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TIntersectOutItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TIntersectOutItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TIntersectOutItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TIntersectOutItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Zip<TOutItem, TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, Func<TItem, TIntersectOutItem, TOutItem> resultSelector)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TIntersectOutItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TIntersectOutItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TIntersectOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TIntersectOutItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TIntersectOutItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TIntersectOutItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TUnionOutItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TUnionOutItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Zip<TOutItem, TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, Func<TItem, TUnionOutItem, TOutItem> resultSelector)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TUnionOutItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TUnionOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TUnionOutItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TUnionOutItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TUnionOutItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TUnionOutItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Zip<TOutItem, TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, Func<TItem, TUnionOutItem, TOutItem> resultSelector)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TUnionOutItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TUnionOutItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TUnionOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TUnionOutItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TUnionOutItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TUnionOutItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TGroupedOutItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TGroupedOutItem>, GroupedEnumerator<TGroupedOutItem>> Zip<TOutItem, TGroupedKey, TGroupedOutItem>(GroupedEnumerable<TGroupedKey, TGroupedOutItem> second, Func<TItem, TGroupedOutItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TGroupedOutItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TGroupedOutItem>, GroupedEnumerator<TGroupedOutItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TGroupedOutItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TGroupedOutItem>, GroupingEnumerator<TGroupedOutItem>> Zip<TOutItem, TGroupedKey, TGroupedOutItem>(GroupingEnumerable<TGroupedKey, TGroupedOutItem> second, Func<TItem, TGroupedOutItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TGroupedOutItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TGroupedOutItem>, GroupingEnumerator<TGroupedOutItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TGroupJoinOutItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Zip<TOutItem, TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, Func<TItem, TGroupJoinOutItem, TOutItem> resultSelector)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Zip<TOutItem, TItem, TGroupJoinOutItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TGroupJoinOutItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Zip<TOutItem, TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, Func<TItem, TGroupJoinOutItem, TOutItem> resultSelector)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Zip<TOutItem, TItem, TGroupJoinOutItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TGroupJoinOutItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TJoinOutItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Zip<TOutItem, TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, Func<TItem, TJoinOutItem, TOutItem> resultSelector)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Zip<TOutItem, TItem, TJoinOutItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TJoinOutItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Zip<TOutItem, TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, Func<TItem, TJoinOutItem, TOutItem> resultSelector)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Zip<TOutItem, TItem, TJoinOutItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TJoinOutItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TOrderByOutItem, TEnumerable, TEnumerator, OrderByEnumerable<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>, OrderByEnumerator<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>> Zip<TOutItem, TOrderByKey, TOrderByOutItem, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>(OrderByEnumerable<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer> second, Func<TItem, TOrderByOutItem, TOutItem> resultSelector)
            where TOrderByInnerEnumerable : struct, IStructEnumerable<TOrderByOutItem, TOrderByInnerEnumerator>
            where TOrderByInnerEnumerator : struct, IStructEnumerator<TOrderByOutItem>
            where TOrderByComparer : struct, IStructComparer<TOrderByOutItem, TOrderByKey>
        => CommonImplementation.Zip<TOutItem, TItem, TOrderByOutItem, TEnumerable, TEnumerator, OrderByEnumerable<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>, OrderByEnumerator<TOrderByOutItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TReverseOutItem, TEnumerable, TEnumerator, ReverseEnumerable<TReverseOutItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TReverseOutItem>> Zip<TOutItem, TReverseOutItem, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TReverseOutItem, TReverseEnumerable, TReverseEnumerator> second, Func<TItem, TReverseOutItem, TOutItem> resultSelector)
            where TReverseEnumerable : struct, IStructEnumerable<TReverseOutItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TReverseOutItem>
        => CommonImplementation.Zip<TOutItem, TItem, TReverseOutItem, TEnumerable, TEnumerator, ReverseEnumerable<TReverseOutItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TReverseOutItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TReverseRangeOutItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TReverseRangeOutItem>, ReverseRangeEnumerator<TReverseRangeOutItem>> Zip<TOutItem, TReverseRangeOutItem>(ReverseRangeEnumerable<TReverseRangeOutItem> second, Func<TItem, TReverseRangeOutItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TReverseRangeOutItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TReverseRangeOutItem>, ReverseRangeEnumerator<TReverseRangeOutItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TGroupByOutItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerator>> Zip<TOutItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator> second, Func<TItem, TGroupByOutItem, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Zip<TOutItem, TItem, TGroupByOutItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TGroupByOutItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerator>> Zip<TOutItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator> second, Func<TItem, TGroupByOutItem, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Zip<TOutItem, TItem, TGroupByOutItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByOutItem, TGroupByEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TEnumerable, TEnumerator, GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>> Zip<TOutItem, TGroupByKey, TGroupByElement, TGroupByInItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> second, Func<TItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Zip<TOutItem, TItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TEnumerable, TEnumerator, GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TEnumerable, TEnumerator, GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>> Zip<TOutItem, TGroupByKey, TGroupByElement, TGroupByInItem, TGroupByEnumerable, TGroupByEnumerator>(GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> second, Func<TItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Zip<TOutItem, TItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TEnumerable, TEnumerator, GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, GroupingEnumerable<TLookupKey, TLookupElement>, TEnumerable, TEnumerator, LookupEnumerable<TLookupKey, TLookupElement>, LookupEnumerator<TLookupKey, TLookupElement>> Zip<TOutItem, TLookupKey, TLookupElement>(LookupEnumerable<TLookupKey, TLookupElement> second, Func<TItem, GroupingEnumerable<TLookupKey, TLookupElement>, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, GroupingEnumerable<TLookupKey, TLookupElement>, TEnumerable, TEnumerator, LookupEnumerable<TLookupKey, TLookupElement>, LookupEnumerator<TLookupKey, TLookupElement>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TInnerItem>, OneItemDefaultEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(OneItemDefaultEnumerable<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TInnerItem>, OneItemDefaultEnumerator<TInnerItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TInnerItem>, OneItemSpecificEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(OneItemSpecificEnumerable<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TInnerItem>, OneItemSpecificEnumerator<TInnerItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TInnerItem>, OneItemDefaultOrderedEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(OneItemDefaultOrderedEnumerable<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TInnerItem>, OneItemDefaultOrderedEnumerator<TInnerItem>>(RefThis(), ref second, resultSelector);

        public ZipEnumerable<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TInnerItem>, OneItemSpecificOrderedEnumerator<TInnerItem>> Zip<TInnerItem, TOutItem>(OneItemSpecificOrderedEnumerable<TInnerItem> second, Func<TItem, TInnerItem, TOutItem> resultSelector)
        => CommonImplementation.Zip<TOutItem, TItem, TInnerItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TInnerItem>, OneItemSpecificOrderedEnumerator<TInnerItem>>(RefThis(), ref second, resultSelector);
    }
}
