using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase
    {
        // Concat

        public ConcatEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Concat<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(first));
            var secondBridge = Bridge(second, nameof(second));

            return
                CommonImplementation.ConcatImpl<
                    TItem,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>
                >(RefLocal(firstBridge), RefLocal(secondBridge));
        }

        public ConcatEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, PlaceholderEnumerable<TItem>, PlaceholderEnumerator<TItem>> Concat<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.ConcatImpl<
                    TItem,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>,
                    PlaceholderEnumerable<TItem>,
                    PlaceholderEnumerator<TItem>
                >(RefLocal(firstBridge), RefParam(second));
        }

        // Concat, weird
        [DoNotInject]
        public 
            ConcatEnumerable<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, 
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>, 
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Concat<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first, 
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.ConcatImpl<
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(firstBridge), ref second);
        }
        [DoNotInject]
        public
            ConcatEnumerable<
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Concat<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.ConcatImpl<
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public
            ConcatEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Concat<TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.ConcatImpl<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public
            ConcatEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                BuiltInEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                BuiltInEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Concat<TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.ConcatImpl<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(firstBridge), ref second);
        }
    }
}