using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IJoin<TLeftItem, TLeftEnumerable, TLeftEnumerator>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
    {
        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            BoxedEnumerable<TRightItem>,
            BoxedEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(BoxedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector);

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            BoxedEnumerable<TRightItem>,
            BoxedEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(BoxedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer);

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TRightItem, TCastInnerEnumerator>
        > Join<TOutItem, TCastInItem, TKeyItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>(
            CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector, 
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>,
            CastEnumerator<TCastInItem, TRightItem, TCastInnerEnumerator>
        > Join<TOutItem, TCastInItem, TKeyItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator>(
            CastEnumerable<TCastInItem, TRightItem, TCastInnerEnumerable, TCastInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TCastInnerEnumerable : struct, IStructEnumerable<TCastInItem, TCastInnerEnumerator>
            where TCastInnerEnumerator : struct, IStructEnumerator<TCastInItem>;

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TRightItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(
            ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TConcatFirstEnumerable : struct, IStructEnumerable<TRightItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TRightItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>,
            ConcatEnumerator<TRightItem, TConcatFirstEnumerator, TConcatSecondEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator>(
            ConcatEnumerable<TRightItem, TConcatFirstEnumerable, TConcatFirstEnumerator, TConcatSecondEnumerable, TConcatSecondEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TConcatFirstEnumerable : struct, IStructEnumerable<TRightItem, TConcatFirstEnumerator>
            where TConcatFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TConcatSecondEnumerable : struct, IStructEnumerable<TRightItem, TConcatSecondEnumerator>
            where TConcatSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptyDefaultEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            DefaultIfEmptyDefaultEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
           DefaultIfEmptySpecificEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
           DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
           where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
           where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>,
            DefaultIfEmptySpecificEnumerator<TRightItem, TDefaultIfEmptyInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator>(
            DefaultIfEmptySpecificEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerable, TDefaultIfEmptyInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TDefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TRightItem, TDefaultIfEmptyInnerEnumerator>
            where TDefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>,
           DistinctDefaultEnumerator<TRightItem, TDistinctInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(
           DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
           where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
           where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>,
           DistinctDefaultEnumerator<TRightItem, TDistinctInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(
           DistinctDefaultEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
           where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
           where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>,
           DistinctSpecificEnumerator<TRightItem, TDistinctInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(
           DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
           where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
           where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>,
           DistinctSpecificEnumerator<TRightItem, TDistinctInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator>(
           DistinctSpecificEnumerable<TRightItem, TDistinctInnerEnumerable, TDistinctInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
           where TDistinctInnerEnumerable : struct, IStructEnumerable<TRightItem, TDistinctInnerEnumerator>
           where TDistinctInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        EmptyEnumerable<TOutItem> Join<TOutItem, TRightItem, TKeyItem>(
            EmptyEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        EmptyEnumerable<TOutItem> Join<TOutItem, TRightItem, TKeyItem>(
            EmptyEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        EmptyEnumerable<TOutItem> Join<TOutItem, TRightItem, TKeyItem>(
            EmptyOrderedEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        EmptyEnumerable<TOutItem> Join<TOutItem, TRightItem, TKeyItem>(
            EmptyOrderedEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>,
           ExceptDefaultEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(
           ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
           Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>,
           ExceptDefaultEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(
           ExceptDefaultEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>,
           ExceptSpecificEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(
           ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>,
           ExceptSpecificEnumerator<TRightItem, TExceptFirstEnumerator, TExceptSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(
           ExceptSpecificEnumerable<TRightItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TExceptFirstEnumerable : struct, IStructEnumerable<TRightItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TRightItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
           GroupJoinDefaultEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
           GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
           GroupJoinDefaultEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
           GroupJoinDefaultEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
           GroupJoinSpecificEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
           GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>,
           GroupJoinSpecificEnumerator<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
           GroupJoinSpecificEnumerable<TRightItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupedEnumerable<TGroupedKey, TRightItem>,
           GroupedEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupedKey>(
           GroupedEnumerable<TGroupedKey, TRightItem> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupedEnumerable<TGroupedKey, TRightItem>,
           GroupedEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupedKey>(
           GroupedEnumerable<TGroupedKey, TRightItem> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupingEnumerable<TGroupedKey, TRightItem>,
           GroupingEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupedKey>(
           GroupingEnumerable<TGroupedKey, TRightItem> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupingEnumerable<TGroupedKey, TRightItem>,
           GroupingEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupedKey>(
           GroupingEnumerable<TGroupedKey, TRightItem> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           GroupingEnumerable<TGroupByKey, TGroupByElement>,
           GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>,
           GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector,
           Func<TLeftItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           GroupingEnumerable<TGroupByKey, TGroupByElement>,
           GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>,
           GroupByDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupByDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector,
           Func<TLeftItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           GroupingEnumerable<TGroupByKey, TGroupByElement>,
           GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>,
           GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector,
            Func<TLeftItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           GroupingEnumerable<TGroupByKey, TGroupByElement>,
           GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>,
           GroupBySpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupBySpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<GroupingEnumerable<TGroupByKey, TGroupByElement>, TKeyItem> innerKeySelector,
            Func<TLeftItem, GroupingEnumerable<TGroupByKey, TGroupByElement>, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>,
           GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>,
           GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>,
           GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator>,
           GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
           GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TRightItem, TGroupByEnumerable, TGroupByEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>,
           IntersectDefaultEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(
           IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>,
           IntersectDefaultEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(
           IntersectDefaultEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>,
           IntersectSpecificEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(
           IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>,
           IntersectSpecificEnumerator<TRightItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(
           IntersectSpecificEnumerable<TRightItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TRightItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TRightItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, TRightItem[], ArrayEnumerator<TRightItem>>,
           ArrayEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            TRightItem[] inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, TRightItem[], ArrayEnumerator<TRightItem>>,
           ArrayEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            TRightItem[] inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Dictionary<TRightItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TRightItem, TDictionaryValue>>,
           DictionaryKeysEnumerator<TRightItem, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(
            Dictionary<TRightItem, TDictionaryValue>.KeyCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Dictionary<TRightItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TRightItem, TDictionaryValue>>,
           DictionaryKeysEnumerator<TRightItem, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(
            Dictionary<TRightItem, TDictionaryValue>.KeyCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Dictionary<TDictionaryKey, TRightItem>.ValueCollection, DictionaryValuesEnumerator<TDictionaryKey, TRightItem>>,
           DictionaryValuesEnumerator<TDictionaryKey, TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(
            Dictionary<TDictionaryKey, TRightItem>.ValueCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Dictionary<TDictionaryKey, TRightItem>.ValueCollection, DictionaryValuesEnumerator<TDictionaryKey, TRightItem>>,
           DictionaryValuesEnumerator<TDictionaryKey, TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(
            Dictionary<TDictionaryKey, TRightItem>.ValueCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           KeyValuePair<TDictionaryKey, TDictionaryValue>,
           IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, Dictionary<TDictionaryKey, TDictionaryValue>, DictionaryEnumerator<TDictionaryKey, TDictionaryValue>>,
           DictionaryEnumerator<TDictionaryKey, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(
            Dictionary<TDictionaryKey, TDictionaryValue> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector,
            Func<TLeftItem, KeyValuePair<TDictionaryKey, TDictionaryValue>, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           KeyValuePair<TDictionaryKey, TDictionaryValue>,
           IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, Dictionary<TDictionaryKey, TDictionaryValue>, DictionaryEnumerator<TDictionaryKey, TDictionaryValue>>,
           DictionaryEnumerator<TDictionaryKey, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(
            Dictionary<TDictionaryKey, TDictionaryValue> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector,
            Func<TLeftItem, KeyValuePair<TDictionaryKey, TDictionaryValue>, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, IEnumerable<TRightItem>, IdentityEnumerator<TRightItem>>,
           IdentityEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            IEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, IEnumerable<TRightItem>, IdentityEnumerator<TRightItem>>,
           IdentityEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            IEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           object,
           IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>,
           IdentityEnumerator
        > Join<TOutItem, TKeyItem>(
            System.Collections.IEnumerable inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<object, TKeyItem> innerKeySelector,
            Func<TLeftItem, object, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           object,
           IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>,
           IdentityEnumerator
        > Join<TOutItem, TKeyItem>(
            System.Collections.IEnumerable inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<object, TKeyItem> innerKeySelector,
            Func<TLeftItem, object, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, LinkedList<TRightItem>, LinkedListEnumerator<TRightItem>>,
           LinkedListEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            LinkedList<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, LinkedList<TRightItem>, LinkedListEnumerator<TRightItem>>,
           LinkedListEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            LinkedList<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, List<TRightItem>, ListEnumerator<TRightItem>>,
           ListEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            List<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, List<TRightItem>, ListEnumerator<TRightItem>>,
           ListEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            List<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Queue<TRightItem>, QueueEnumerator<TRightItem>>,
           QueueEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            Queue<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Queue<TRightItem>, QueueEnumerator<TRightItem>>,
           QueueEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            Queue<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>>,
           SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(
            SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>>,
           SortedDictionaryKeysEnumerator<TRightItem, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryValue>(
            SortedDictionary<TRightItem, TDictionaryValue>.KeyCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection, SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>>,
           SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(
            SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection, SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>>,
           SortedDictionaryValuesEnumerator<TDictionaryKey, TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem, TDictionaryKey>(
            SortedDictionary<TDictionaryKey, TRightItem>.ValueCollection inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           KeyValuePair<TDictionaryKey, TDictionaryValue>,
           IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, SortedDictionary<TDictionaryKey, TDictionaryValue>, SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>>,
           SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(
            SortedDictionary<TDictionaryKey, TDictionaryValue> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector,
            Func<TLeftItem, KeyValuePair<TDictionaryKey, TDictionaryValue>, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           KeyValuePair<TDictionaryKey, TDictionaryValue>,
           IdentityEnumerable<KeyValuePair<TDictionaryKey, TDictionaryValue>, SortedDictionary<TDictionaryKey, TDictionaryValue>, SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>>,
           SortedDictionaryEnumerator<TDictionaryKey, TDictionaryValue>
        > Join<TOutItem, TKeyItem, TDictionaryKey, TDictionaryValue>(
            SortedDictionary<TDictionaryKey, TDictionaryValue> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<KeyValuePair<TDictionaryKey, TDictionaryValue>, TKeyItem> innerKeySelector,
            Func<TLeftItem, KeyValuePair<TDictionaryKey, TDictionaryValue>, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, SortedSet<TRightItem>, SortedSetEnumerator<TRightItem>>,
           SortedSetEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            SortedSet<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, SortedSet<TRightItem>, SortedSetEnumerator<TRightItem>>,
           SortedSetEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            SortedSet<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Stack<TRightItem>, StackEnumerator<TRightItem>>,
           StackEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            Stack<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           IdentityEnumerable<TRightItem, Stack<TRightItem>, StackEnumerator<TRightItem>>,
           StackEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            Stack<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
           JoinDefaultEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
           JoinDefaultEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
           JoinSpecificEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>,
           JoinSpecificEnumerator<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TRightItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           GroupingEnumerable<TLookupKey, TLookupElement>,
           LookupEnumerable<TLookupKey, TLookupElement>,
           LookupEnumerator<TLookupKey, TLookupElement>
        > Join<TOutItem, TKeyItem, TLookupKey, TLookupElement>(
            LookupEnumerable<TLookupKey, TLookupElement> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<GroupingEnumerable<TLookupKey, TLookupElement>, TKeyItem> innerKeySelector,
            Func<TLeftItem, GroupingEnumerable<TLookupKey, TLookupElement>, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           GroupingEnumerable<TLookupKey, TLookupElement>,
           LookupEnumerable<TLookupKey, TLookupElement>,
           LookupEnumerator<TLookupKey, TLookupElement>
        > Join<TOutItem, TKeyItem, TLookupKey, TLookupElement>(
            LookupEnumerable<TLookupKey, TLookupElement> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<GroupingEnumerable<TLookupKey, TLookupElement>, TKeyItem> innerKeySelector,
            Func<TLeftItem, GroupingEnumerable<TLookupKey, TLookupElement>, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>,
           OfTypeEnumerator<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(
            OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>,
           OfTypeEnumerator<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TOfTypeInItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator>(
            OfTypeEnumerable<TOfTypeInItem, TRightItem, TOfTypeInnerEnumerable, TOfTypeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TOfTypeInnerEnumerable : struct, IStructEnumerable<TOfTypeInItem, TOfTypeInnerEnumerator>
            where TOfTypeInnerEnumerator : struct, IStructEnumerator<TOfTypeInItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>,
           OrderByEnumerator<TRightItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>
        > Join<TOutItem, TKeyItem, TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>(
            OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TOrderByInnerEnumerable : struct, IStructEnumerable<TRightItem, TOrderByInnerEnumerator>
            where TOrderByInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TOrderByComparer : struct, IStructComparer<TRightItem, TOrderByKey>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>,
           OrderByEnumerator<TRightItem, TOrderByKey, TOrderByInnerEnumerator, TOrderByComparer>
        > Join<TOutItem, TKeyItem, TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer>(
            OrderByEnumerable<TRightItem, TOrderByKey, TOrderByInnerEnumerable, TOrderByInnerEnumerator, TOrderByComparer> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TOrderByInnerEnumerable : struct, IStructEnumerable<TRightItem, TOrderByInnerEnumerator>
            where TOrderByInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TOrderByComparer : struct, IStructComparer<TRightItem, TOrderByKey>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           RangeEnumerable<TRightItem>,
           RangeEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            RangeEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           RangeEnumerable<TRightItem>,
           RangeEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            RangeEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          RepeatEnumerable<TRightItem>,
          RepeatEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem>(
           RepeatEnumerable<TRightItem> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       );

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          RepeatEnumerable<TRightItem>,
          RepeatEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem>(
           RepeatEnumerable<TRightItem> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       );

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>,
          ReverseEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>(
           ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TReverseInnerEnumerable : struct, IStructEnumerable<TRightItem, TReverseInnerEnumerator>
            where TReverseInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>,
          ReverseEnumerator<TRightItem>
       > Join<TOutItem, TKeyItem, TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator>(
           ReverseEnumerable<TRightItem, TReverseInnerEnumerable, TReverseInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TReverseInnerEnumerable : struct, IStructEnumerable<TRightItem, TReverseInnerEnumerator>
            where TReverseInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
          SelectEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
           SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        JoinSpecificEnumerable<
         TOutItem,
         TKeyItem,
         TLeftItem,
         TLeftEnumerable,
         TLeftEnumerator,
         TRightItem,
         SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
         SelectEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>
      > Join<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
          SelectEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner,
          Func<TLeftItem, TKeyItem> outerKeySelector,
          Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
          IEqualityComparer<TKeyItem> comparer
      )
           where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
           where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
          SelectIndexedEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
           SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator>,
          SelectIndexedEnumerator<TSelectInItem, TRightItem, TSelectInnerEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectInItem, TSelectInnerEnumerable, TSelectInnerEnumerator>(
           SelectIndexedEnumerable<TSelectInItem, TRightItem, TSelectInnerEnumerable, TSelectInnerEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridgeType : class;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridgeType : class;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridgeType : class;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>
            where TSelectManyBridgeType : class;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridgeType : class;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyCollectionBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyCollectionBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridgeType : class;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
          SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
           SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
            where TSelectManyBridgeType : class;

        JoinSpecificEnumerable<
         TOutItem,
         TKeyItem,
         TLeftItem,
         TLeftEnumerable,
         TLeftEnumerator,
         TRightItem,
         SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>,
         SelectManyCollectionIndexedBridgeEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>
      > Join<TOutItem, TKeyItem, TRightItem, TSelectManyCollectionItem, TSelectManyInItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator>(
          SelectManyCollectionIndexedBridgeEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyBridgeType, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerator> inner,
          Func<TLeftItem, TKeyItem> outerKeySelector,
          Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
          IEqualityComparer<TKeyItem> comparer
      )
           where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
           where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
           where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>
           where TSelectManyBridgeType : class;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TRightItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyCollectionEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyCollectionEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyCollectionEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>,
          SelectManyCollectionIndexedEnumerator<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TSelectManyInItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator>(
           SelectManyCollectionIndexedEnumerable<TSelectManyInItem, TRightItem, TSelectManyCollectionItem, TSelectManyInnerEnumerable, TSelectManyInnerEnumerator, TSelectManyProjectedEnumerable, TSelectManyProjectedEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TSelectManyInnerEnumerable : struct, IStructEnumerable<TSelectManyInItem, TSelectManyInnerEnumerator>
            where TSelectManyInnerEnumerator : struct, IStructEnumerator<TSelectManyInItem>
            where TSelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectManyCollectionItem, TSelectManyProjectedEnumerator>
            where TSelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectManyCollectionItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>,
          SelectSelectEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>
        > Join<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(
            SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>,
          SelectSelectEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection>
        > Join<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection>(
            SelectSelectEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>,
          SelectWhereEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>
        > Join<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(
            SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>,
          SelectWhereEnumerator<TRightItem, TSelectInnerItem, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>
        > Join<TOutItem, TKeyItem, TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate>(
            SelectWhereEnumerable<TRightItem, TSelectInnerItem, TSelectInnerEnumerable, TSelectInnerEnumerator, TSelectProjection, TSelectPredicate> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TSelectInnerEnumerable : struct, IStructEnumerable<TSelectInnerItem, TSelectInnerEnumerator>
            where TSelectInnerEnumerator : struct, IStructEnumerator<TSelectInnerItem>
            where TSelectProjection : struct, IStructProjection<TRightItem, TSelectInnerItem>
            where TSelectPredicate : struct, IStructPredicate<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
          SkipEnumerator<TRightItem, TSkipInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
          SkipEnumerator<TRightItem, TSkipInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
          SkipWhileEnumerator<TRightItem, TSkipInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
          SkipWhileEnumerator<TRightItem, TSkipInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipWhileEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
          SkipWhileIndexedEnumerator<TRightItem, TSkipInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>,
          SkipWhileIndexedEnumerator<TRightItem, TSkipInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator>(
            SkipWhileIndexedEnumerable<TRightItem, TSkipInnerEnumerable, TSkipInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TSkipInnerEnumerable : struct, IStructEnumerable<TRightItem, TSkipInnerEnumerator>
            where TSkipInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
          TakeEnumerator<TRightItem, TTakeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
          TakeEnumerator<TRightItem, TTakeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
          TakeWhileEnumerator<TRightItem, TTakeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
          TakeWhileEnumerator<TRightItem, TTakeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeWhileEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
          TakeWhileIndexedEnumerator<TRightItem, TTakeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>,
          TakeWhileIndexedEnumerator<TRightItem, TTakeInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator>(
            TakeWhileIndexedEnumerable<TRightItem, TTakeInnerEnumerable, TTakeInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TTakeInnerEnumerable : struct, IStructEnumerable<TRightItem, TTakeInnerEnumerator>
            where TTakeInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>,
           UnionDefaultEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(
           UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>,
           UnionDefaultEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(
           UnionDefaultEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>,
           UnionSpecificEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(
           UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
       )
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>,
           UnionSpecificEnumerator<TRightItem, TUnionFirstEnumerator, TUnionSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(
           UnionSpecificEnumerable<TRightItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
            where TUnionFirstEnumerable : struct, IStructEnumerable<TRightItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TRightItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TRightItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
           WhereEnumerator<TRightItem, TWhereInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
           WhereEnumerator<TRightItem, TWhereInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            WhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
           WhereIndexedEnumerator<TRightItem, TWhereInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>,
           WhereIndexedEnumerator<TRightItem, TWhereInnerEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator>(
            WhereIndexedEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>,
           WhereSelectEnumerator<TRightItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(
            WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
            where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
            where TWhereProjection : struct, IStructProjection<TRightItem, TWhereInnerItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>,
          WhereSelectEnumerator<TRightItem, TWhereInnerItem, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>
       > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection>(
           WhereSelectEnumerable<TRightItem, TWhereInnerItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate, TWhereProjection> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
           where TWhereInnerEnumerable : struct, IStructEnumerable<TWhereInnerItem, TWhereInnerEnumerator>
           where TWhereInnerEnumerator : struct, IStructEnumerator<TWhereInnerItem>
           where TWherePredicate : struct, IStructPredicate<TWhereInnerItem>
           where TWhereProjection : struct, IStructProjection<TRightItem, TWhereInnerItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>,
           WhereWhereEnumerator<TRightItem, TWhereInnerEnumerator, TWherePredicate>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(
            WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TWherePredicate : struct, IStructPredicate<TRightItem>;

        JoinSpecificEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>,
           WhereWhereEnumerator<TRightItem, TWhereInnerEnumerator, TWherePredicate>
        > Join<TOutItem, TKeyItem, TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate>(
            WhereWhereEnumerable<TRightItem, TWhereInnerEnumerable, TWhereInnerEnumerator, TWherePredicate> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TWhereInnerEnumerable : struct, IStructEnumerable<TRightItem, TWhereInnerEnumerator>
            where TWhereInnerEnumerator : struct, IStructEnumerator<TRightItem>
            where TWherePredicate : struct, IStructPredicate<TRightItem>;

        JoinDefaultEnumerable<
           TOutItem,
           TKeyItem,
           TLeftItem,
           TLeftEnumerable,
           TLeftEnumerator,
           TRightItem,
           ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
           ZipEnumerator<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
        > Join<TOutItem, TKeyItem, TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
            ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
            where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
            where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
            where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>,
          ZipEnumerator<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerator, TZipSecondEnumerator>
       > Join<TOutItem, TKeyItem, TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator>(
           ZipEnumerable<TRightItem, TZipFirstItem, TZipSecondItem, TZipFirstEnumerable, TZipFirstEnumerator, TZipSecondEnumerable, TZipSecondEnumerator> inner,
           Func<TLeftItem, TKeyItem> outerKeySelector,
           Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
           IEqualityComparer<TKeyItem> comparer
       )
           where TZipFirstEnumerable : struct, IStructEnumerable<TZipFirstItem, TZipFirstEnumerator>
           where TZipFirstEnumerator : struct, IStructEnumerator<TZipFirstItem>
           where TZipSecondEnumerable : struct, IStructEnumerable<TZipSecondItem, TZipSecondEnumerator>
           where TZipSecondEnumerator : struct, IStructEnumerator<TZipSecondItem>;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          IdentityEnumerable<TRightItem, TBridgeType, TIdentityEnumerator>,
          TIdentityEnumerator
        > Join<TOutItem, TKeyItem, TRightItem, TBridgeType, TIdentityEnumerator>(
            IdentityEnumerable<TRightItem, TBridgeType, TIdentityEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
            where TIdentityEnumerator : struct, IStructEnumerator<TRightItem>
            where TBridgeType : class;

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          IdentityEnumerable<TRightItem, TBridgeType, TIdentityEnumerator>,
          TIdentityEnumerator
        > Join<TOutItem, TKeyItem, TRightItem, TBridgeType, TIdentityEnumerator>(
            IdentityEnumerable<TRightItem, TBridgeType, TIdentityEnumerator> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
            where TIdentityEnumerator : struct, IStructEnumerator<TRightItem>
            where TBridgeType : class;

        JoinDefaultEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          ReverseRangeEnumerable<TRightItem>,
          ReverseRangeEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            ReverseRangeEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        );

        JoinSpecificEnumerable<
          TOutItem,
          TKeyItem,
          TLeftItem,
          TLeftEnumerable,
          TLeftEnumerator,
          TRightItem,
          ReverseRangeEnumerable<TRightItem>,
          ReverseRangeEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(
            ReverseRangeEnumerable<TRightItem> inner,
            Func<TLeftItem, TKeyItem> outerKeySelector,
            Func<TRightItem, TKeyItem> innerKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        );

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemDefaultEnumerable<TRightItem>,
            OneItemDefaultEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemDefaultEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector);

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemDefaultEnumerable<TRightItem>,
            OneItemDefaultEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemDefaultEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer);

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemSpecificEnumerable<TRightItem>,
            OneItemSpecificEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemSpecificEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector);

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemSpecificEnumerable<TRightItem>,
            OneItemSpecificEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemSpecificEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer);

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemDefaultOrderedEnumerable<TRightItem>,
            OneItemDefaultOrderedEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemDefaultOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector);

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemDefaultOrderedEnumerable<TRightItem>,
            OneItemDefaultOrderedEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemDefaultOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer);

        JoinDefaultEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemSpecificOrderedEnumerable<TRightItem>,
            OneItemSpecificOrderedEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemSpecificOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector);

        JoinSpecificEnumerable<
            TOutItem,
            TKeyItem,
            TLeftItem,
            TLeftEnumerable,
            TLeftEnumerator,
            TRightItem,
            OneItemSpecificOrderedEnumerable<TRightItem>,
            OneItemSpecificOrderedEnumerator<TRightItem>
        > Join<TOutItem, TKeyItem, TRightItem>(OneItemSpecificOrderedEnumerable<TRightItem> inner, Func<TLeftItem, TKeyItem> outerKeySelector, Func<TRightItem, TKeyItem> innerKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer);
    }
}