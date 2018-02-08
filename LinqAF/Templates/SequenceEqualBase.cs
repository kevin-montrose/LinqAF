using System;
using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class SequenceEqualBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ISequenceEqual<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public bool SequenceEqual(IEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryValue, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryValue, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual<TDictionaryKey>(Dictionary<TDictionaryKey, TItem>.ValueCollection second)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryKey, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual<TDictionaryKey>(Dictionary<TDictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryKey, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(HashSet<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(HashSet<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(LinkedList<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(List<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(List<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(Queue<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(Queue<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual<TDictionaryValue>(SortedDictionary<TItem, TDictionaryValue>.KeyCollection second)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryValue, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual<TDictionaryValue>(SortedDictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryValue, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual<TDictionaryKey>(SortedDictionary<TDictionaryKey, TItem>.ValueCollection second)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryKey, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual<TDictionaryKey>(SortedDictionary<TDictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TDictionaryKey, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(SortedSet<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(Stack<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(Stack<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(TItem[] second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, null);

        public bool SequenceEqual(TItem[] second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator>(RefThis(), second, comparer);

        public bool SequenceEqual(BoxedEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator> second)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TItem>
            where TIdentityBridger: struct, IStructBridger<TItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator>(RefThis(), ref second, null);


        public bool SequenceEqual<TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>(IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIdentityBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TItem>
            where TIdentityBridger : struct, IStructBridger<TItem, TIdentityBridgeType, TIdentityEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TCastInItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TItem, TCastInnerEnumerable, TCastInnerEnumerator> second)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TItem, TCastInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TCastInItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TItem, TCastInnerEnumerable, TCastInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, CastEnumerable<TCastInItem, TItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TItem, TCastInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> second)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TItem, TConcatFirstEnumerator, TConcatSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TItem, TConcatFirstEnumerator, TConcatSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TDefaultIfEmptyInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TDefaultIfEmptyInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TDefaultIfEmptyInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TDefaultIfEmptyInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(EmptyEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> second)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TOfTypeInItem, TItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TItem, TOfTypeInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TOfTypeInItem, TItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TItem, TOfTypeInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(RepeatEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TItem, TSelectInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TItem, TSelectInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TItem, TSelectInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TItem, TSelectInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridger: struct, IStructBridger<TItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridger: struct, IStructBridger<TItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridger: struct, IStructBridger<TItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridger: struct, IStructBridger<TItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TItem, TSkipInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TItem, TSkipInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TItem, TSkipInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TItem, TSkipInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TSkipInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TSkipInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TItem, TTakeInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TItem, TTakeInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TItem, TTakeInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TItem, TTakeInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TTakeInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TTakeInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TItem, TWhereInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TItem, TWhereInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TItem, TWhereInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TItem, TWhereInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>, SelectSelectEnumerator<TItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>, SelectSelectEnumerator<TItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>, SelectWhereEnumerator<TItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>, SelectWhereEnumerator<TItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TWherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>, WhereWhereEnumerator<TItem, TWhereInnerEnumerator, TWherePredicate>>(RefThis(), ref second, null);

        public bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TWherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>, WhereWhereEnumerator<TItem, TWhereInnerEnumerator, TWherePredicate>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TItem, TWhereInnerItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>, WhereSelectEnumerator<TItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>>(RefThis(), ref second, null);

        public bool SequenceEqual<TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TItem, TWhereInnerItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>, WhereSelectEnumerator<TItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TDistinctInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TDistinctInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TDistinctInnerEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TDistinctInnerEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(EmptyOrderedEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, EmptyOrderedEnumerable<TItem>, EmptyOrderedEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, EmptyOrderedEnumerable<TItem>, EmptyOrderedEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TGroupingKey>(GroupingEnumerable<TGroupingKey, TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupingKey, TItem>, GroupingEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual<TGroupingKey>(GroupingEnumerable<TGroupingKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupingKey, TItem>, GroupingEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(RefThis(), ref second, null);

        public bool SequenceEqual<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(OneItemDefaultEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(OneItemSpecificEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(RefThis(), ref second, null);

        public bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TSkipEnumerable, TSkipEnumerator>(SkipLastEnumerable<TItem, TSkipEnumerable, TSkipEnumerator> second)
            where TSkipEnumerable : struct, IStructEnumerable<TItem, TSkipEnumerator>
            where TSkipEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipLastEnumerable<TItem, TSkipEnumerable, TSkipEnumerator>, SkipLastEnumerator<TItem, TSkipEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TSkipEnumerable, TSkipEnumerator>(SkipLastEnumerable<TItem, TSkipEnumerable, TSkipEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipEnumerable : struct, IStructEnumerable<TItem, TSkipEnumerator>
            where TSkipEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, SkipLastEnumerable<TItem, TSkipEnumerable, TSkipEnumerator>, SkipLastEnumerator<TItem, TSkipEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TTakeEnumerable, TTakeEnumerator>(TakeLastEnumerable<TItem, TTakeEnumerable, TTakeEnumerator> second)
            where TTakeEnumerable : struct, IStructEnumerable<TItem, TTakeEnumerator>
            where TTakeEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeLastEnumerable<TItem, TTakeEnumerable, TTakeEnumerator>, TakeLastEnumerator<TItem, TTakeEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TTakeEnumerable, TTakeEnumerator>(TakeLastEnumerable<TItem, TTakeEnumerable, TTakeEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeEnumerable : struct, IStructEnumerable<TItem, TTakeEnumerator>
            where TTakeEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, TakeLastEnumerable<TItem, TTakeEnumerable, TTakeEnumerator>, TakeLastEnumerator<TItem, TTakeEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TAppendEnumerable, TAppendEnumerator>(AppendEnumerable<TItem, TAppendEnumerable, TAppendEnumerator> second)
            where TAppendEnumerable : struct, IStructEnumerable<TItem, TAppendEnumerator>
            where TAppendEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, AppendEnumerable<TItem, TAppendEnumerable, TAppendEnumerator>, AppendEnumerator<TItem, TAppendEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TAppendEnumerable, TAppendEnumerator>(AppendEnumerable<TItem, TAppendEnumerable, TAppendEnumerator> second, IEqualityComparer<TItem> comparer)
            where TAppendEnumerable : struct, IStructEnumerable<TItem, TAppendEnumerator>
            where TAppendEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, AppendEnumerable<TItem, TAppendEnumerable, TAppendEnumerator>, AppendEnumerator<TItem, TAppendEnumerator>>(RefThis(), ref second, comparer);

        public bool SequenceEqual<TPrependEnumerable, TPrependEnumerator>(PrependEnumerable<TItem, TPrependEnumerable, TPrependEnumerator> second)
            where TPrependEnumerable : struct, IStructEnumerable<TItem, TPrependEnumerator>
            where TPrependEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, PrependEnumerable<TItem, TPrependEnumerable, TPrependEnumerator>, PrependEnumerator<TItem, TPrependEnumerator>>(RefThis(), ref second, null);

        public bool SequenceEqual<TPrependEnumerable, TPrependEnumerator>(PrependEnumerable<TItem, TPrependEnumerable, TPrependEnumerator> second, IEqualityComparer<TItem> comparer)
            where TPrependEnumerable : struct, IStructEnumerable<TItem, TPrependEnumerator>
            where TPrependEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.SequenceEqual<TItem, TEnumerable, TEnumerator, PrependEnumerable<TItem, TPrependEnumerable, TPrependEnumerator>, PrependEnumerator<TItem, TPrependEnumerator>>(RefThis(), ref second, comparer);
    }
}