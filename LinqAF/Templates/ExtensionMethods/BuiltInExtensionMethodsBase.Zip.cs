using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase :
        ExtensionMethodsBase
    {
        // Zip
        public ZipEnumerable<TOutItem, TFirstItem, TSecondItem, BuiltInEnumerable<TFirstItem>, BuiltInEnumerator<TFirstItem>, BuiltInEnumerable<TSecondItem>, BuiltInEnumerator<TSecondItem>> Zip<TOutItem, TFirstItem, TSecondItem>(BuiltInEnumerable<TFirstItem> first, BuiltInEnumerable<TSecondItem> second, Func<TFirstItem, TSecondItem, TOutItem> resultSelector)
        {
            var firstBridged = Bridge(first, nameof(first));
            var secondBridged = Bridge(second, nameof(second));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.ZipImpl<
                    TOutItem,
                    TFirstItem,
                    TSecondItem,
                    BuiltInEnumerable<TFirstItem>,
                    BuiltInEnumerator<TFirstItem>,
                    BuiltInEnumerable<TSecondItem>,
                    BuiltInEnumerator<TSecondItem>
                >(RefLocal(firstBridged), RefLocal(secondBridged), resultSelector);
        }

        // HACK: the "Gen" part of each generic parameter is necessary to avoid some name collisions in code gen
        public ZipEnumerable<TGenOutItem, TGenFirstItem, TGenSecondItem, BuiltInEnumerable<TGenFirstItem>, BuiltInEnumerator<TGenFirstItem>, PlaceholderEnumerable<TGenSecondItem>, PlaceholderEnumerator<TGenSecondItem>> Zip<TGenOutItem, TGenFirstItem, TGenSecondItem>(BuiltInEnumerable<TGenFirstItem> first, PlaceholderEnumerable<TGenSecondItem> second, Func<TGenFirstItem, TGenSecondItem, TGenOutItem> resultSelector)
        {
            var firstBridged = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.ZipImpl<
                    TGenOutItem,
                    TGenFirstItem,
                    TGenSecondItem,
                    BuiltInEnumerable<TGenFirstItem>,
                    BuiltInEnumerator<TGenFirstItem>,
                    PlaceholderEnumerable<TGenSecondItem>,
                    PlaceholderEnumerator<TGenSecondItem>
                >(RefLocal(firstBridged), RefParam(second), resultSelector);
        }

        // Zip Weird

        [DoNotInject]
        public 
            ZipEnumerable<
                TOutItem, 
                TFirstItem, 
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<TFirstItem>, 
                BuiltInEnumerator<TFirstItem>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Zip<TOutItem, TFirstItem, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TFirstItem> first, 
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second, 
                Func<TFirstItem, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TOutItem> resultSelector
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridged = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.ZipImpl<
                    TOutItem,
                    TFirstItem,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    BuiltInEnumerable<TFirstItem>,
                    BuiltInEnumerator<TFirstItem>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(firstBridged), ref second, resultSelector);
        }

        [DoNotInject]
        public
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                BuiltInEnumerable<TFirstItem>,
                BuiltInEnumerator<TFirstItem>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Zip<TOutItem, TFirstItem, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TFirstItem> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                Func<TFirstItem, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TOutItem> resultSelector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var firstBridged = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.ZipImpl<
                    TOutItem,
                    TFirstItem,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    BuiltInEnumerable<TFirstItem>,
                    BuiltInEnumerator<TFirstItem>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(firstBridged), ref second, resultSelector);
        }

        [DoNotInject]
        public
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                BuiltInEnumerable<TFirstItem>,
                BuiltInEnumerator<TFirstItem>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Zip<TOutItem, TFirstItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TFirstItem> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second,
                Func<TFirstItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TOutItem> resultSelector
            )
        {
            var firstBridged = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.ZipImpl<
                    TOutItem,
                    TFirstItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    BuiltInEnumerable<TFirstItem>,
                    BuiltInEnumerator<TFirstItem>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(firstBridged), ref second, resultSelector);
        }

        [DoNotInject]
        public
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                BuiltInEnumerable<TFirstItem>,
                BuiltInEnumerator<TFirstItem>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Zip<TOutItem, TFirstItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TFirstItem> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second,
                Func<TFirstItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TOutItem> resultSelector
            )
        {
            var firstBridged = Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return
                CommonImplementation.ZipImpl<
                    TOutItem,
                    TFirstItem,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    BuiltInEnumerable<TFirstItem>,
                    BuiltInEnumerator<TFirstItem>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(firstBridged), ref second, resultSelector);
        }
    }
}