using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class IntersectBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IIntersect<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Intersect(IEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Intersect(LinkedList<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Intersect(HashSet<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Intersect(Stack<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Intersect(SortedSet<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Intersect(Queue<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Intersect(List<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Intersect(TItem[] second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));
            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Intersect(RepeatEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Intersect(RangeEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Intersect(ReverseRangeEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public EmptyEnumerable<TItem> Intersect(EmptyOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> Intersect(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return EmptyCache<TItem>.Empty;
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Intersect(BoxedEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, ReverseRangeEnumerable<TItem>, ReverseRangeEnumerator<TItem>> Intersect(ReverseRangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public EmptyEnumerable<TItem> Intersect(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return EmptyCache<TItem>.Empty;
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>> Intersect(LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>> Intersect(HashSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>> Intersect(IEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>> Intersect(Stack<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>> Intersect(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, RangeEnumerable<TItem>, RangeEnumerator<TItem>> Intersect(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public EmptyEnumerable<TItem> Intersect(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            if(second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            return EmptyCache<TItem>.Empty;
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TItem[], ArrayEnumerator<TItem>>, ArrayEnumerator<TItem>> Intersect(TItem[] second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>> Intersect(SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>> Intersect(Queue<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, List<TItem>, ListEnumerator<TItem>>, ListEnumerator<TItem>> Intersect(List<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>> Intersect(BoxedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>, DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>> Intersect<TIntersect_DictionaryKey>(Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>> Intersect<TIntersect_DictionaryKey>(SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>> Intersect<TIntersect_DictionaryValue>(SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Intersect<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Intersect<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Intersect<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>> Intersect<TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>, DictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>> Intersect<TIntersect_DictionaryKey>(Dictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TIntersect_DictionaryKey, TItem>> Intersect<TIntersect_DictionaryKey>(SortedDictionary<TIntersect_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TIntersect_DictionaryValue>> Intersect<TIntersect_DictionaryValue>(SortedDictionary<TItem, TIntersect_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>> Intersect<TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Intersect<TDictionaryValue>(Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl(RefThis(), ref bridge, comparer);
        }

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Intersect<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second)
            where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
            where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second)
            where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
            where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Intersect<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator>, TIntersect_IdentityEnumerator> Intersect<TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator>(IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator> second)
            where TIntersect_IdentityBridgeType : class
            where TIntersect_IdentityEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, TakeEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(TakeEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, WhereIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(WhereIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SkipWhileEnumerator<TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SkipWhileEnumerable<TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>> Intersect<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, IEqualityComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
            where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TIntersect_DistinctInnerEnumerator>> Intersect<TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TIntersect_DistinctInnerEnumerable, TIntersect_DistinctInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_DistinctInnerEnumerator>
            where TIntersect_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>> Intersect<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator>, TIntersect_IdentityEnumerator> Intersect<TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator>(IdentityEnumerable<TItem, TIntersect_IdentityBridgeType, TIntersect_IdentityEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_IdentityBridgeType : class
            where TIntersect_IdentityEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Intersect<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, OfTypeEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>, WhereWhereEnumerator<TItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>> Intersect<TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>(WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate> second)
            where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_WhereInnerEnumerator>
            where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TIntersect_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, CastEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectIndexedEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, OfTypeEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(OfTypeEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, CastEnumerator<TIntersect_InItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_InItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(CastEnumerable<TIntersect_InItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_InItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_InItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>, WhereWhereEnumerator<TItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>> Intersect<TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate>(WhereWhereEnumerable<TItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate> second, IEqualityComparer<TItem> comparer)
            where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TIntersect_WhereInnerEnumerator>
            where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TIntersect_WherePredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectIndexedEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectIndexedEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>> Intersect<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, IEqualityComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>, SelectEnumerator<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerator>> Intersect<TIntersect_SelectInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator>(SelectEnumerable<TIntersect_SelectInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>, SelectSelectEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>(SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection> second)
            where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
            where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
            where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Intersect<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> Intersect<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> Intersect<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>, SelectSelectEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection>(SelectSelectEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
            where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
            where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> Intersect<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>> Intersect<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second)
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_SelectManyProjectedEnumerator>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_ProjectedEnumerator>
            where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>, WhereSelectEnumerator<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>> Intersect<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>(WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection> second)
            where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator>
            where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TIntersect_WhereInnerItem>
            where TIntersect_WherePredicate : struct, IStructPredicate<TIntersect_WhereInnerItem>
            where TIntersect_WhereProjection : struct, IStructProjection<TItem, TIntersect_WhereInnerItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second)
            where TIntersect_SelectManyBridgeType : class
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>, SelectWhereEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>(SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate> second)
            where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
            where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
            where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>
            where TIntersect_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>, SelectManyBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_BridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>(SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator> second)
            where TIntersect_BridgeType : class
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectManyBridgeType : class
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_SelectManyProjectedEnumerator>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>, WhereSelectEnumerator<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>> Intersect<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection>(WhereSelectEnumerable<TItem, TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerable, TIntersect_WhereInnerEnumerator, TIntersect_WherePredicate, TIntersect_WhereProjection> second, IEqualityComparer<TItem> comparer)
            where TIntersect_WhereInnerEnumerable : struct, IStructEnumerable<TIntersect_WhereInnerItem, TIntersect_WhereInnerEnumerator>
            where TIntersect_WhereInnerEnumerator : struct, IStructEnumerator<TIntersect_WhereInnerItem>
            where TIntersect_WherePredicate : struct, IStructPredicate<TIntersect_WhereInnerItem>
            where TIntersect_WhereProjection : struct, IStructProjection<TItem, TIntersect_WhereInnerItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>> Intersect<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>, SelectWhereEnumerator<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>> Intersect<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate>(SelectWhereEnumerable<TItem, TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerable, TIntersect_SelectInnerEnumerator, TIntersect_SelectProjection, TIntersect_SelectPredicate> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectInnerItem, TIntersect_SelectInnerEnumerator>
            where TIntersect_SelectInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectInnerItem>
            where TIntersect_SelectProjection : struct, IStructProjection<TItem, TIntersect_SelectInnerItem>
            where TIntersect_SelectPredicate : struct, IStructPredicate<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TItem, TIntersect_ProjectedEnumerator>
            where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>, SelectManyBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_BridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator>(SelectManyBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_BridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_BridgeType : class
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second)
            where TIntersect_SelectManyBridgeType : class
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second)
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_SelectManyProjectedEnumerator>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>, ZipEnumerator<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerator>> Intersect<TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>(ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator> second)
            where TIntersect_ZipFirstEnumerable : struct, IStructEnumerable<TIntersect_ZipFirstItem, TIntersect_ZipFirstEnumerator>
            where TIntersect_ZipFirstEnumerator : struct, IStructEnumerator<TIntersect_ZipFirstItem>
            where TIntersect_ZipSecondEnumerable : struct, IStructEnumerable<TIntersect_ZipSecondItem, TIntersect_ZipSecondEnumerator>
            where TIntersect_ZipSecondEnumerator : struct, IStructEnumerator<TIntersect_ZipSecondItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyCollectionEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_ProjectedEnumerator>
            where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second)
            where TIntersect_SelectManyBridgeType : class
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerable, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_SelectManyProjectedEnumerator>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>, ZipEnumerator<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerator>> Intersect<TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator>(ZipEnumerable<TItem, TIntersect_ZipFirstItem, TIntersect_ZipSecondItem, TIntersect_ZipFirstEnumerable, TIntersect_ZipFirstEnumerator, TIntersect_ZipSecondEnumerable, TIntersect_ZipSecondEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_ZipFirstEnumerable : struct, IStructEnumerable<TIntersect_ZipFirstItem, TIntersect_ZipFirstEnumerator>
            where TIntersect_ZipFirstEnumerator : struct, IStructEnumerator<TIntersect_ZipFirstItem>
            where TIntersect_ZipSecondEnumerable : struct, IStructEnumerable<TIntersect_ZipSecondItem, TIntersect_ZipSecondEnumerator>
            where TIntersect_ZipSecondEnumerator : struct, IStructEnumerator<TIntersect_ZipSecondItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>, SelectManyCollectionEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator>(SelectManyCollectionEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_ProjectedEnumerable, TIntersect_ProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_ProjectedEnumerable : struct, IStructEnumerable<TIntersect_CollectionItem, TIntersect_ProjectedEnumerator>
            where TIntersect_ProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_SelectManyInnerEnumerable, TIntersect_SelectManyInnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectManyBridgeType : class
            where TIntersect_SelectManyInnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_SelectManyInnerEnumerator>
            where TIntersect_SelectManyInnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>> Intersect<TIntersect_SelectManyInItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TIntersect_SelectManyInItem, TItem, TIntersect_CollectionItem, TIntersect_SelectManyBridgeType, TIntersect_InnerEnumerable, TIntersect_InnerEnumerator, TIntersect_SelectManyProjectedEnumerator> second, IEqualityComparer<TItem> comparer)
            where TIntersect_SelectManyBridgeType : class
            where TIntersect_InnerEnumerable : struct, IStructEnumerable<TIntersect_SelectManyInItem, TIntersect_InnerEnumerator>
            where TIntersect_InnerEnumerator : struct, IStructEnumerator<TIntersect_SelectManyInItem>
            where TIntersect_SelectManyProjectedEnumerator : struct, IStructEnumerator<TIntersect_CollectionItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> Intersect<TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> Intersect<TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second, IEqualityComparer<TItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Intersect(OneItemDefaultEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>> Intersect(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Intersect(OneItemSpecificEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>> Intersect(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Intersect(OneItemDefaultOrderedEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>> Intersect(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);

        public IntersectDefaultEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Intersect(OneItemSpecificOrderedEnumerable<TItem> second)
        => CommonImplementation.Intersect(RefThis(), ref second);

        public IntersectSpecificEnumerable<TItem, TEnumerable, TEnumerator, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>> Intersect(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        => CommonImplementation.Intersect(RefThis(), ref second, comparer);
    }
}
