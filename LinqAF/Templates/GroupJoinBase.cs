using LinqAF.Impl;
using System;
using System.Collections.Generic;
using System.Collections;

namespace LinqAF
{
    abstract class GroupJoinBase<TLeftItem, TLeftEnumerable, TLeftEnumerator> :
        TemplateBase,
        IGroupJoin<TLeftItem, TLeftEnumerable, TLeftEnumerator>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
    {
        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, BoxedEnumerable<TRightItem>, BoxedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(BoxedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, BoxedEnumerable<TRightItem>, BoxedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(BoxedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TRightItem, TCastInnerEnumerator>> GroupJoin<TOutItem, TCastInItem, TKeyItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>, CastEnumerator<TCastInItem, TRightItem, TCastInnerEnumerator>> GroupJoin<TOutItem, TCastInItem, TKeyItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>(CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TRightItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TRightItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TRightItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>, ConcatEnumerator<TRightItem, TConcatFirstEnumerator, TConcatSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TConcatFirstEnumerable : struct, IStructEnumerable<TRightItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TRightItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctDefaultEnumerator<TRightItem, TDistinctInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctDefaultEnumerator<TRightItem, TDistinctInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctSpecificEnumerator<TRightItem, TDistinctInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>, DistinctSpecificEnumerator<TRightItem, TDistinctInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
            where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        
        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupedEnumerable<TGroupedKey, TRightItem>, GroupedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupedKey>(GroupedEnumerable<TGroupedKey, TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupedEnumerable<TGroupedKey, TRightItem>, GroupedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupedKey>(GroupedEnumerable<TGroupedKey, TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupingEnumerable<TGroupedKey, TRightItem>, GroupingEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupedKey>(GroupingEnumerable<TGroupedKey, TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupingEnumerable<TGroupedKey, TRightItem>, GroupingEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupedKey>(GroupingEnumerable<TGroupedKey, TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, TRightItem[], ArrayBridger<TRightItem>, ArrayEnumerator<TRightItem>>, ArrayEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(TRightItem[] inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, TRightItem[], ArrayBridger<TRightItem>, ArrayEnumerator<TRightItem>>, ArrayEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(TRightItem[] inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Dictionary<TRightItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TRightItem, TDictionaryValue>, DictionaryKeysEnumerator<TRightItem, TDictionaryValue>>, DictionaryKeysEnumerator<TRightItem, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(Dictionary<TRightItem, TDictionaryValue>.KeyCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Dictionary<TRightItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TRightItem, TDictionaryValue>, DictionaryKeysEnumerator<TRightItem, TDictionaryValue>>, DictionaryKeysEnumerator<TRightItem, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(Dictionary<TRightItem, TDictionaryValue>.KeyCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Dictionary<TDictionaryKey, TRightItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TRightItem>, DictionaryValuesEnumerator<TDictionaryKey, TRightItem>>, DictionaryValuesEnumerator<TDictionaryKey, TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(Dictionary<TDictionaryKey, TRightItem>.ValueCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Dictionary<TDictionaryKey, TRightItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TRightItem>, DictionaryValuesEnumerator<TDictionaryKey, TRightItem>>, DictionaryValuesEnumerator<TDictionaryKey, TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(Dictionary<TDictionaryKey, TRightItem>.ValueCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, IEnumerable<TRightItem>, IEnumerableBridger<TRightItem>, IdentityEnumerator<TRightItem>>, IdentityEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(IEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, IEnumerable<TRightItem>, IEnumerableBridger<TRightItem>, IdentityEnumerator<TRightItem>>, IdentityEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(IEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, object, IdentityEnumerable<object, IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator> GroupJoin<TOutItem, TKeyItem>(IEnumerable inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<object, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, object>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, object, IdentityEnumerable<object, IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator> GroupJoin<TOutItem, TKeyItem>(IEnumerable inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<object, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, object>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, LinkedList<TRightItem>, LinkedListBridger<TRightItem>, LinkedListEnumerator<TRightItem>>, LinkedListEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(LinkedList<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, LinkedList<TRightItem>, LinkedListBridger<TRightItem>, LinkedListEnumerator<TRightItem>>, LinkedListEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(LinkedList<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, List<TRightItem>, ListBridger<TRightItem>, ListEnumerator<TRightItem>>, ListEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(List<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, List<TRightItem>, ListBridger<TRightItem>, ListEnumerator<TRightItem>>, ListEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(List<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Queue<TRightItem>, QueueBridger<TRightItem>, QueueEnumerator<TRightItem>>, QueueEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(Queue<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Queue<TRightItem>, QueueBridger<TRightItem>, QueueEnumerator<TRightItem>>, QueueEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(Queue<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TRightItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TRightItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TRightItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TRightItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, SortedSet<TRightItem>, SortedSetBridger<TRightItem>, SortedSetEnumerator<TRightItem>>, SortedSetEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(SortedSet<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, SortedSet<TRightItem>, SortedSetBridger<TRightItem>, SortedSetEnumerator<TRightItem>>, SortedSetEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(SortedSet<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Stack<TRightItem>, StackBridger<TRightItem>, StackEnumerator<TRightItem>>, StackEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(Stack<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, Stack<TRightItem>, StackBridger<TRightItem>, StackEnumerator<TRightItem>>, StackEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(Stack<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>, OfTypeEnumerator<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>, OrderByEnumerator<TRightItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>> GroupJoin<TOutItem, TKeyItem, TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>(OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TOrderByInnerEnumerable : struct, IStructEnumerable<TRightItem, TOrderByInnerEnumerator>
            where TOrderByInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TOrderByComparer : struct, IStructComparer<TRightItem, TOrderByKey>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>, OrderByEnumerator<TRightItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>> GroupJoin<TOutItem, TKeyItem, TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>(OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TOrderByInnerEnumerable : struct, IStructEnumerable<TRightItem, TOrderByInnerEnumerator>
            where TOrderByInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TOrderByComparer : struct, IStructComparer<TRightItem, TOrderByKey>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, RangeEnumerable<TRightItem>, RangeEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(RangeEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, RangeEnumerable<TRightItem>, RangeEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(RangeEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, RepeatEnumerable<TRightItem>, RepeatEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(RepeatEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, RepeatEnumerable<TRightItem>, RepeatEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(RepeatEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>, ReverseEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>(ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TReverseInnerEnumerable : struct, IStructEnumerable<TRightItem, TReverseInnerEnumerator>
            where TReverseInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>, ReverseEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>(ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TReverseInnerEnumerable : struct, IStructEnumerable<TRightItem, TReverseInnerEnumerator>
            where TReverseInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>, SelectIndexedEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridger: struct, IStructBridger<TRightItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridger: struct, IStructBridger<TRightItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridger: struct, IStructBridger<TRightItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridger: struct, IStructBridger<TRightItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyBridger, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyBridgeType : class
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridger: struct, IStructBridger<TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyProjectedEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>, SelectSelectEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>, SelectSelectEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>, SelectWhereEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>, SelectWhereEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TRightItem, TSkipInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipEnumerator<TRightItem, TSkipInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TRightItem, TSkipInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileEnumerator<TRightItem, TSkipInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TRightItem, TSkipInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>, SkipWhileIndexedEnumerator<TRightItem, TSkipInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TRightItem, TTakeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeEnumerator<TRightItem, TTakeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TRightItem, TTakeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileEnumerator<TRightItem, TTakeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TRightItem, TTakeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>, TakeWhileIndexedEnumerator<TRightItem, TTakeInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TRightItem, TWhereInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereEnumerator<TRightItem, TWhereInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TRightItem, TWhereInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>, WhereIndexedEnumerator<TRightItem, TWhereInnerEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>, WhereSelectEnumerator<TRightItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TRightItem, TWhereInnerItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>, WhereSelectEnumerator<TRightItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TRightItem, TWhereInnerItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>, WhereWhereEnumerator<TRightItem, TWhereInnerEnumerator, TWherePredicate>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TWherePredicate : struct, IStructPredicate<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>, WhereWhereEnumerator<TRightItem, TWhereInnerEnumerator, TWherePredicate>> GroupJoin<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TWherePredicate : struct, IStructPredicate<TRightItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>, ZipEnumerator<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>> GroupJoin<TOutItem, TKeyItem, TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, TBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> GroupJoin<TOutItem, TKeyItem, TRightItem, TBridgeType, TIdentityBridger, TIdentityEnumerator>(IdentityEnumerable<TRightItem, TBridgeType, TIdentityBridger, TIdentityEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
            where TBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TRightItem>
            where TIdentityBridger: struct, IStructBridger<TRightItem, TBridgeType, TIdentityEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, IdentityEnumerable<TRightItem, TBridgeType, TIdentityBridger, TIdentityEnumerator>, TIdentityEnumerator> GroupJoin<TOutItem, TKeyItem, TRightItem, TBridgeType, TIdentityBridger, TIdentityEnumerator>(IdentityEnumerable<TRightItem, TBridgeType, TIdentityBridger, TIdentityEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TBridgeType : class
            where TIdentityEnumerator : struct, IStructEnumerator<TRightItem>
            where TIdentityBridger : struct, IStructBridger<TRightItem, TBridgeType, TIdentityEnumerator>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ReverseRangeEnumerable<TRightItem>, ReverseRangeEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(ReverseRangeEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, ReverseRangeEnumerable<TRightItem>, ReverseRangeEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(ReverseRangeEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupByKey, TGroupByElement>, GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TGroupByKey, TGroupByElement>>, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupByKey, TGroupByElement>, GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TGroupByKey, TGroupByElement>>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupByKey, TGroupByElement>, GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TGroupByKey, TGroupByElement>>, TOutItem> resultSelector)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TGroupByKey, TGroupByElement>, GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>, GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>> GroupJoin<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TGroupByKey, TGroupByElement>>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, KeyValuePair<TDictionaryKey, TDictionaryValue>, IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, Dictionary<TDictionaryKey, TDictionaryValue>, DictionaryBridger<TDictionaryKey, TDictionaryValue>, DictionaryEnumerator<TDictionaryKey, TDictionaryValue>>, DictionaryEnumerator<TDictionaryKey, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(Dictionary<TDictionaryKey, TDictionaryValue> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, KeyValuePair<TDictionaryKey, TDictionaryValue>>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, KeyValuePair<TDictionaryKey, TDictionaryValue>, IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, Dictionary<TDictionaryKey, TDictionaryValue>, DictionaryBridger<TDictionaryKey, TDictionaryValue>, DictionaryEnumerator<TDictionaryKey, TDictionaryValue>>, DictionaryEnumerator<TDictionaryKey, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(Dictionary<TDictionaryKey, TDictionaryValue> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, KeyValuePair<TDictionaryKey, TDictionaryValue>>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, KeyValuePair<TDictionaryKey, TDictionaryValue>, IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, SortedDictionary<TDictionaryKey, TDictionaryValue>, SortedDictionaryBridger<TDictionaryKey, TDictionaryValue>, SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>>, SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(SortedDictionary<TDictionaryKey, TDictionaryValue> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, KeyValuePair<TDictionaryKey, TDictionaryValue>>, TOutItem> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector);
        }

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, KeyValuePair<TDictionaryKey, TDictionaryValue>, IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, SortedDictionary<TDictionaryKey, TDictionaryValue>, SortedDictionaryBridger<TDictionaryKey, TDictionaryValue>, SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>>, SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>> GroupJoin<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(SortedDictionary<TDictionaryKey, TDictionaryValue> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, KeyValuePair<TDictionaryKey, TDictionaryValue>>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("outer");
            var bridge = CommonImplementation.Bridge(inner, nameof(inner));
            if (outerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(outerKeySelector));
            if (innerKeySelector == null) throw CommonImplementation.ArgumentNull(nameof(innerKeySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupJoinImpl(RefThis(), ref bridge, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TLookupKey, TLookupElement>, LookupDefaultEnumerable<TLookupKey, TLookupElement>, LookupDefaultEnumerator<TLookupKey, TLookupElement>> GroupJoin<TOutItem, TKeyItem, TLookupKey, TLookupElement>(LookupDefaultEnumerable<TLookupKey, TLookupElement> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TLookupKey, TLookupElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TLookupKey, TLookupElement>>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TLookupKey, TLookupElement>, LookupSpecificEnumerable<TLookupKey, TLookupElement>, LookupSpecificEnumerator<TLookupKey, TLookupElement>> GroupJoin<TOutItem, TKeyItem, TLookupKey, TLookupElement>(LookupSpecificEnumerable<TLookupKey, TLookupElement> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TLookupKey, TLookupElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TLookupKey, TLookupElement>>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);
        
        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TLookupKey, TLookupElement>, LookupDefaultEnumerable<TLookupKey, TLookupElement>, LookupDefaultEnumerator<TLookupKey, TLookupElement>> GroupJoin<TOutItem, TKeyItem, TLookupKey, TLookupElement>(LookupDefaultEnumerable<TLookupKey, TLookupElement> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TLookupKey, TLookupElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TLookupKey, TLookupElement>>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, GroupingEnumerable<TLookupKey, TLookupElement>, LookupSpecificEnumerable<TLookupKey, TLookupElement>, LookupSpecificEnumerator<TLookupKey, TLookupElement>> GroupJoin<TOutItem, TKeyItem, TLookupKey, TLookupElement>(LookupSpecificEnumerable<TLookupKey, TLookupElement> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<GroupingEnumerable<TLookupKey, TLookupElement>, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, GroupingEnumerable<TLookupKey, TLookupElement>>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemDefaultEnumerable<TRightItem>, OneItemDefaultEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemDefaultEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemDefaultEnumerable<TRightItem>, OneItemDefaultEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemDefaultEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemSpecificEnumerable<TRightItem>, OneItemSpecificEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemSpecificEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemSpecificEnumerable<TRightItem>, OneItemSpecificEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemSpecificEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemDefaultOrderedEnumerable<TRightItem>, OneItemDefaultOrderedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemDefaultOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemDefaultOrderedEnumerable<TRightItem>, OneItemDefaultOrderedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemDefaultOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemSpecificOrderedEnumerable<TRightItem>, OneItemSpecificOrderedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemSpecificOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, OneItemSpecificOrderedEnumerable<TRightItem>, OneItemSpecificOrderedEnumerator<TRightItem>> GroupJoin<TOutItem, TKeyItem, TRightItem>(OneItemSpecificOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, EmptyEnumerable<TRightItem>, EmptyEnumerator<TRightItem>> GroupJoin<TOutItem, TRightItem, TKeyItem>(EmptyEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, EmptyEnumerable<TRightItem>, EmptyEnumerator<TRightItem>> GroupJoin<TOutItem, TRightItem, TKeyItem>(EmptyEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        public GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, EmptyOrderedEnumerable<TRightItem>, EmptyOrderedEnumerator<TRightItem>> GroupJoin<TOutItem, TRightItem, TKeyItem>(EmptyOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector);

        public GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, EmptyOrderedEnumerable<TRightItem>, EmptyOrderedEnumerator<TRightItem>> GroupJoin<TOutItem, TRightItem, TKeyItem>(EmptyOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        => CommonImplementation.GroupJoin(RefThis(), ref inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
    }
}
