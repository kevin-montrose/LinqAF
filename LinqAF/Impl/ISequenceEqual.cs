using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAF.Impl
{
    interface ISequenceEqual<TItem>
    {
        bool SequenceEqual(IEnumerable<TItem> second);
        bool SequenceEqual(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second);
        bool SequenceEqual<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual<TDictionaryKey>(Dictionary<TDictionaryKey, TItem>.ValueCollection second);
        bool SequenceEqual<TDictionaryKey>(Dictionary<TDictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(HashSet<TItem> second);
        bool SequenceEqual(HashSet<TItem> second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(LinkedList<TItem> second);
        bool SequenceEqual(LinkedList<TItem> second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(List<TItem> second);
        bool SequenceEqual(List<TItem> second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(Queue<TItem> second);
        bool SequenceEqual(Queue<TItem> second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual<TDictionaryValue>(SortedDictionary<TItem, TDictionaryValue>.KeyCollection second);
        bool SequenceEqual<TDictionaryValue>(SortedDictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual<TDictionaryKey>(SortedDictionary<TDictionaryKey, TItem>.ValueCollection second);
        bool SequenceEqual<TDictionaryKey>(SortedDictionary<TDictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(SortedSet<TItem> second);
        bool SequenceEqual(SortedSet<TItem> second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(Stack<TItem> second);
        bool SequenceEqual(Stack<TItem> second, IEqualityComparer<TItem> comparer);
        bool SequenceEqual(TItem[] second);
        bool SequenceEqual(TItem[] second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual(BoxedEnumerable<TItem> second);
        bool SequenceEqual(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual<TIdentityBridgeType, TIdentityEnumerator>(IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityEnumerator> second)
            where TIdentityEnumerator : struct, IStructEnumerator<TItem>
            where TIdentityBridgeType : class;
        bool SequenceEqual<TIdentityBridgeType, TIdentityEnumerator>(IdentityEnumerable<TItem, TIdentityBridgeType, TIdentityEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIdentityEnumerator : struct, IStructEnumerator<TItem>
            where TIdentityBridgeType : class;
        
        bool SequenceEqual<TCastInItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TItem, TCastInnerEnumerable, TCastInnerEnumerator> second)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;
        bool SequenceEqual<TCastInItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TItem, TCastInnerEnumerable, TCastInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        bool SequenceEqual<TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> second)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual(EmptyEnumerable<TItem> second);
        bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual<TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> second)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        bool SequenceEqual<TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        bool SequenceEqual(RangeEnumerable<TItem> second);
        bool SequenceEqual(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual(RepeatEnumerable<TItem> second);
        bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        bool SequenceEqual<TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TItem, TSelectInnerEnumerable, TSelectInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second, 
            IEqualityComparer<TItem> comparer
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem> 
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TSelectManyBridgeType : class;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyCollectionEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        bool SequenceEqual<TSelectManyInItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TItem, TCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> second,
            IEqualityComparer<TItem> comparer
        )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>;

        bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TSkipInnerEnumerable, TSkipInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TTakeInnerEnumerable, TTakeInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        bool SequenceEqual<TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>;

        bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>;

        bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> second)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TItem>;

        bool SequenceEqual<TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> second, IEqualityComparer<TItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TItem>;

        bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TWherePredicate : struct, IStructPredicate<TItem>;

        bool SequenceEqual<TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TWherePredicate : struct, IStructPredicate<TItem>;

        bool SequenceEqual<TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> second)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TItem, TWhereInnerItem>;

        bool SequenceEqual<TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> second, IEqualityComparer<TItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TItem, TWhereInnerItem>;

        bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual(EmptyOrderedEnumerable<TItem> second);
        bool SequenceEqual(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second);
        bool SequenceEqual<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual<TGroupingKey>(GroupingEnumerable<TGroupingKey, TItem> second);
        bool SequenceEqual<TGroupingKey>(GroupingEnumerable<TGroupingKey, TItem> second, IEqualityComparer<TItem> comparer);

        // GroupBy skipped intentionally

        bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;
        bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;
        bool SequenceEqual<TGroupByInItem, TGroupByKey, TGroupByItem, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByItem, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;
        bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;
        bool SequenceEqual<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        bool SequenceEqual<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        // Intentionally skipping Lookup

        bool SequenceEqual<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>;
        bool SequenceEqual<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>;

        bool SequenceEqual<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>;
        bool SequenceEqual<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>;

        bool SequenceEqual(ReverseRangeEnumerable<TItem> second);
        bool SequenceEqual(ReverseRangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual(OneItemDefaultEnumerable<TItem> second);
        bool SequenceEqual(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual(OneItemSpecificEnumerable<TItem> second);
        bool SequenceEqual(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second);
        bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);

        bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second);
        bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer);
    }
}
