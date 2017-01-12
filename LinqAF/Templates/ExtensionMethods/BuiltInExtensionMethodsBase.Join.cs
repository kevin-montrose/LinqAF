using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase :
        ExtensionMethodsBase
    {
        // Join

        public
            JoinDefaultEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                TJoinRightItem,
                BuiltInEnumerable<TJoinRightItem>,
                BuiltInEnumerator<TJoinRightItem>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TJoinRightItem>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                BuiltInEnumerable<TJoinRightItem> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<TJoinRightItem, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, TJoinRightItem, TOutItem> resultSelector
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            var innerBridge = Bridge(inner, nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    TJoinRightItem,
                    BuiltInEnumerable<TJoinRightItem>,
                    BuiltInEnumerator<TJoinRightItem>
                >(RefLocal(outerBridge), RefLocal(innerBridge), outerKeySelector, innerKeySelector, resultSelector);
        }

        public
            JoinDefaultEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                TJoinRightItem,
                PlaceholderEnumerable<TJoinRightItem>,
                PlaceholderEnumerator<TJoinRightItem>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TJoinRightItem>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                PlaceholderEnumerable<TJoinRightItem> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<TJoinRightItem, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, TJoinRightItem, TOutItem> resultSelector
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    TJoinRightItem,
                    PlaceholderEnumerable<TJoinRightItem>,
                    PlaceholderEnumerator<TJoinRightItem>
                >(RefLocal(outerBridge), RefParam(inner), outerKeySelector, innerKeySelector, resultSelector);
        }

        public
            JoinSpecificEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                TJoinRightItem,
                BuiltInEnumerable<TJoinRightItem>,
                BuiltInEnumerator<TJoinRightItem>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TJoinRightItem>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                BuiltInEnumerable<TJoinRightItem> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<TJoinRightItem, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, TJoinRightItem, TOutItem> resultSelector,
                IEqualityComparer<TJoinKeyItem> comparer
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            var innerBridge = Bridge(inner, nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    TJoinRightItem,
                    BuiltInEnumerable<TJoinRightItem>,
                    BuiltInEnumerator<TJoinRightItem>
                >(RefLocal(outerBridge), RefLocal(innerBridge), outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public
            JoinSpecificEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                TJoinRightItem,
                PlaceholderEnumerable<TJoinRightItem>,
                PlaceholderEnumerator<TJoinRightItem>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TJoinRightItem>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                PlaceholderEnumerable<TJoinRightItem> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<TJoinRightItem, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, TJoinRightItem, TOutItem> resultSelector,
                IEqualityComparer<TJoinKeyItem> comparer
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    TJoinRightItem,
                    PlaceholderEnumerable<TJoinRightItem>,
                    PlaceholderEnumerator<TJoinRightItem>
                >(RefLocal(outerBridge), RefParam(inner), outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        // Join Weird, default

        [DoNotInject]
        public
            JoinDefaultEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TOutItem> resultSelector
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        [DoNotInject]
        public
            JoinDefaultEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TOutItem> resultSelector
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        [DoNotInject]
        public
            JoinDefaultEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TOutItem> resultSelector
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        // Join Specific, default

        [DoNotInject]
        public
            JoinSpecificEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TOutItem> resultSelector,
                IEqualityComparer<TJoinKeyItem> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        [DoNotInject]
        public
            JoinSpecificEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TOutItem> resultSelector,
                IEqualityComparer<TJoinKeyItem> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        [DoNotInject]
        public
            JoinSpecificEnumerable<
                TOutItem,
                TJoinKeyItem,
                TJoinLeftItem,
                BuiltInEnumerable<TJoinLeftItem>,
                BuiltInEnumerator<TJoinLeftItem>,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Join<TOutItem, TJoinKeyItem, TJoinLeftItem, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TJoinLeftItem> outer,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> inner,
                Func<TJoinLeftItem, TJoinKeyItem> outerKeySelector,
                Func<GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TJoinKeyItem> innerKeySelector,
                Func<TJoinLeftItem, GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TOutItem> resultSelector,
                IEqualityComparer<TJoinKeyItem> comparer
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));

            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.JoinImpl<
                    TOutItem,
                    TJoinKeyItem,
                    TJoinLeftItem,
                    BuiltInEnumerable<TJoinLeftItem>,
                    BuiltInEnumerator<TJoinLeftItem>,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }
}