using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase
    {

        // GroupJoin

        public
            GroupJoinDefaultEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                TInner,
                BuiltInEnumerable<TInner>,
                BuiltInEnumerator<TInner>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TInner>(
                BuiltInEnumerable<TOuter> outer,
                BuiltInEnumerable<TInner> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<TInner, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, TInner>, TResult> resultSelector
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            var innerBridge = Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    TInner,
                    BuiltInEnumerable<TInner>,
                    BuiltInEnumerator<TInner>
                >(RefLocal(outerBridge), RefLocal(innerBridge), outerKeySelector, innerKeySelector, resultSelector);
        }

        public
            GroupJoinDefaultEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                TInner,
                PlaceholderEnumerable<TInner>,
                PlaceholderEnumerator<TInner>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TInner>(
                BuiltInEnumerable<TOuter> outer,
                PlaceholderEnumerable<TInner> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<TInner, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, TInner>, TResult> resultSelector
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    TInner,
                    PlaceholderEnumerable<TInner>,
                    PlaceholderEnumerator<TInner>
                >(RefLocal(outerBridge), RefParam(inner), outerKeySelector, innerKeySelector, resultSelector);
        }

        public
            GroupJoinSpecificEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                TInner,
                BuiltInEnumerable<TInner>,
                BuiltInEnumerator<TInner>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TInner>(
                BuiltInEnumerable<TOuter> outer,
                BuiltInEnumerable<TInner> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<TInner, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, TInner>, TResult> resultSelector,
                IEqualityComparer<TGroupJoinKey> comparer
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            var innerBridge = Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    TInner,
                    BuiltInEnumerable<TInner>,
                    BuiltInEnumerator<TInner>
                >(RefLocal(outerBridge), RefLocal(innerBridge), outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public
            GroupJoinSpecificEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                TInner,
                PlaceholderEnumerable<TInner>,
                PlaceholderEnumerator<TInner>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TInner>(
                BuiltInEnumerable<TOuter> outer,
                PlaceholderEnumerable<TInner> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<TInner, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, TInner>, TResult> resultSelector,
                IEqualityComparer<TGroupJoinKey> comparer
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    TInner,
                    PlaceholderEnumerable<TInner>,
                    PlaceholderEnumerator<TInner>
                >(RefLocal(outerBridge), RefParam(inner), outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        // GroupJoin Weird, default

        [DoNotInject]
        public
            GroupJoinDefaultEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TOuter> outer,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>, TResult> resultSelector
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        [DoNotInject]
        public
            GroupJoinDefaultEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TOuter> outer,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>, TResult> resultSelector
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
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        [DoNotInject]
        public
            GroupJoinDefaultEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TOuter> outer,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, GroupingEnumerable<TGenLookupKey, TGenLookupElement>>, TResult> resultSelector
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        // GroupJoin Weird, specific

        [DoNotInject]
        public
            GroupJoinSpecificEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TOuter> outer,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>, TResult> resultSelector,
                IEqualityComparer<TGroupJoinKey> comparer
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
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        [DoNotInject]
        public
            GroupJoinSpecificEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                BuiltInEnumerable<TOuter> outer,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>>, TResult> resultSelector,
                IEqualityComparer<TGroupJoinKey> comparer
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
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    GroupingEnumerable<TGenGroupByKey, TGenGroupByElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenGroupByKey, TGenGroupByElement, TGenGroupByEnumerator>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        [DoNotInject]
        public
            GroupJoinSpecificEnumerable<
                TResult,
                TGroupJoinKey,
                TOuter,
                BuiltInEnumerable<TOuter>,
                BuiltInEnumerator<TOuter>,
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > GroupJoin<TResult, TGroupJoinKey, TOuter, TGenLookupKey, TGenLookupElement>(
                BuiltInEnumerable<TOuter> outer,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> inner,
                Func<TOuter, TGroupJoinKey> outerKeySelector,
                Func<GroupingEnumerable<TGenLookupKey, TGenLookupElement>, TGroupJoinKey> innerKeySelector,
                Func<TOuter, GroupedEnumerable<TGroupJoinKey, GroupingEnumerable<TGenLookupKey, TGenLookupElement>>, TResult> resultSelector,
                IEqualityComparer<TGroupJoinKey> comparer
            )
        {
            var outerBridge = Bridge(outer, nameof(outer));
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                CommonImplementation.GroupJoinImpl<
                    TResult,
                    TGroupJoinKey,
                    TOuter,
                    BuiltInEnumerable<TOuter>,
                    BuiltInEnumerator<TOuter>,
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(RefLocal(outerBridge), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }
}