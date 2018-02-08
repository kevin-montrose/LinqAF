using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase
    {
        // Intersect

        public IntersectDefaultEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Intersect<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(first));
            var secondBridge = Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>
            >(RefLocal(firstBridge), RefLocal(secondBridge));
        }

        public IntersectDefaultEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, PlaceholderEnumerable<TItem>, PlaceholderEnumerator<TItem>> Intersect<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                PlaceholderEnumerable<TItem>,
                PlaceholderEnumerator<TItem>
            >(RefLocal(firstBridge), RefParam(second));
        }

        // Intersect specific

        public IntersectSpecificEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Intersect<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var firstBridge = Bridge(first, nameof(first));
            var secondBridge = Bridge(second, nameof(second));

            return CommonImplementation.IntersectImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>
            >(RefLocal(firstBridge), RefLocal(secondBridge), comparer);
        }

        public IntersectSpecificEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, PlaceholderEnumerable<TItem>, PlaceholderEnumerator<TItem>> Intersect<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                PlaceholderEnumerable<TItem>,
                PlaceholderEnumerator<TItem>
            >(RefLocal(firstBridge), RefParam(second), comparer);
        }

        // Intersect weird default

        [DoNotInject]
        public IntersectDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Intersect<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public IntersectDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Intersect<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public IntersectDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Intersect<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public IntersectDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Intersect<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second);
        }

        // Intersect weird specific
        [DoNotInject]
        public IntersectSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Intersect<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public IntersectSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Intersect<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public IntersectSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Intersect<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public IntersectSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Intersect<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        // RangeEnumerable

        [DoNotInject]
        public IntersectDefaultEnumerable<
            int,
            BuiltInEnumerable<int>,
            BuiltInEnumerator<int>,
            RangeEnumerable,
            RangeEnumerator
            > Intersect(
                BuiltInEnumerable<int> first,
                RangeEnumerable second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                int,
                BuiltInEnumerable<int>,
                BuiltInEnumerator<int>,
                RangeEnumerable,
                RangeEnumerator
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public IntersectSpecificEnumerable<
            int,
            BuiltInEnumerable<int>,
            BuiltInEnumerator<int>,
            RangeEnumerable,
            RangeEnumerator
            > Intersect(
                BuiltInEnumerable<int> first,
                RangeEnumerable second,
                IEqualityComparer<int> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                int,
                BuiltInEnumerable<int>,
                BuiltInEnumerator<int>,
                RangeEnumerable,
                RangeEnumerator
            >(RefLocal(firstBridge), ref second, comparer);
        }

        // ReverseRangeEnumerable

        [DoNotInject]
        public IntersectDefaultEnumerable<
            int,
            BuiltInEnumerable<int>,
            BuiltInEnumerator<int>,
            ReverseRangeEnumerable,
            ReverseRangeEnumerator
            > Intersect(
                BuiltInEnumerable<int> first,
                ReverseRangeEnumerable second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                int,
                BuiltInEnumerable<int>,
                BuiltInEnumerator<int>,
                ReverseRangeEnumerable,
                ReverseRangeEnumerator
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public IntersectSpecificEnumerable<
            int,
            BuiltInEnumerable<int>,
            BuiltInEnumerator<int>,
            ReverseRangeEnumerable,
            ReverseRangeEnumerator
            > Intersect(
                BuiltInEnumerable<int> first,
                ReverseRangeEnumerable second,
                IEqualityComparer<int> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.IntersectImpl<
                int,
                BuiltInEnumerable<int>,
                BuiltInEnumerator<int>,
                ReverseRangeEnumerable,
                ReverseRangeEnumerator
            >(RefLocal(firstBridge), ref second, comparer);
        }
    }
}