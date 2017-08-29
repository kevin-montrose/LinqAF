using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase
    {
        // Union

        public UnionDefaultEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Union<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(first));
            var secondBridge = Bridge(second, nameof(second));

            return CommonImplementation.UnionImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>
            >(RefLocal(firstBridge), RefLocal(secondBridge));
        }

        public UnionDefaultEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, PlaceholderEnumerable<TItem>, PlaceholderEnumerator<TItem>> Union<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                PlaceholderEnumerable<TItem>,
                PlaceholderEnumerator<TItem>
            >(RefLocal(firstBridge), RefParam(second));
        }

        // Union specific

        public UnionSpecificEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Union<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var firstBridge = Bridge(first, nameof(first));
            var secondBridge = Bridge(second, nameof(second));

            return CommonImplementation.UnionImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>
            >(RefLocal(firstBridge), RefLocal(secondBridge), comparer);
        }

        public UnionSpecificEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, PlaceholderEnumerable<TItem>, PlaceholderEnumerator<TItem>> Union<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                TItem,
                BuiltInEnumerable<TItem>,
                BuiltInEnumerator<TItem>,
                PlaceholderEnumerable<TItem>,
                PlaceholderEnumerator<TItem>
            >(RefLocal(firstBridge), RefParam(second), comparer);
        }

        // Union weird default

        [DoNotInject]
        public UnionDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, 
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>, 
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first, 
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public UnionDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public UnionDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Union<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public UnionDefaultEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Union<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second);
        }

        // Union weird specific

        [DoNotInject]
        public UnionSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public UnionSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
            GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public UnionSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Union<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupDefaultEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupDefaultEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public UnionSpecificEnumerable<
            GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
            BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
            LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
            LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            > Union<TGenGroupByKey, TGenGroupByElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement> second,
                IEqualityComparer<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return CommonImplementation.UnionImpl<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                LookupSpecificEnumerable<TGenGroupByKey, TGenGroupByElement>,
                LookupSpecificEnumerator<TGenGroupByKey, TGenGroupByElement>
            >(RefLocal(firstBridge), ref second, comparer);
        }
    }
}