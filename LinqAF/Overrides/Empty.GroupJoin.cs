using LinqAF.Impl;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqAF
{
    partial struct EmptyEnumerable<TItem>
    {
        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemDefaultOrderedEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemDefaultOrderedEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemSpecificOrderedEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemSpecificOrderedEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(BoxedEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(BoxedEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_CastInItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_CastInnerEnumerable, TGroupJoin_CastInnerEnumerator>(CastEnumerable<TGroupJoin_CastInItem, TGroupJoin_RightItem, TGroupJoin_CastInnerEnumerable, TGroupJoin_CastInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_CastInnerEnumerable : struct, IStructEnumerable<TGroupJoin_CastInItem, TGroupJoin_CastInnerEnumerator>
            where TGroupJoin_CastInnerEnumerator : struct, IStructEnumerator<TGroupJoin_CastInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_CastInItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_CastInnerEnumerable, TGroupJoin_CastInnerEnumerator>(CastEnumerable<TGroupJoin_CastInItem, TGroupJoin_RightItem, TGroupJoin_CastInnerEnumerable, TGroupJoin_CastInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_CastInnerEnumerable : struct, IStructEnumerable<TGroupJoin_CastInItem, TGroupJoin_CastInnerEnumerator>
            where TGroupJoin_CastInnerEnumerator : struct, IStructEnumerator<TGroupJoin_CastInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ConcatFirstEnumerable, TGroupJoin_ConcatFirstEnumerator, TGroupJoin_ConcatSecondEnumerable, TGroupJoin_ConcatSecondEnumerator>(ConcatEnumerable<TGroupJoin_RightItem, TGroupJoin_ConcatFirstEnumerable, TGroupJoin_ConcatFirstEnumerator, TGroupJoin_ConcatSecondEnumerable, TGroupJoin_ConcatSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_ConcatFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ConcatFirstEnumerator>
            where TGroupJoin_ConcatFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_ConcatSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ConcatSecondEnumerator>
            where TGroupJoin_ConcatSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ConcatFirstEnumerable, TGroupJoin_ConcatFirstEnumerator, TGroupJoin_ConcatSecondEnumerable, TGroupJoin_ConcatSecondEnumerator>(ConcatEnumerable<TGroupJoin_RightItem, TGroupJoin_ConcatFirstEnumerable, TGroupJoin_ConcatFirstEnumerator, TGroupJoin_ConcatSecondEnumerable, TGroupJoin_ConcatSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_ConcatFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ConcatFirstEnumerator>
            where TGroupJoin_ConcatFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_ConcatSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ConcatSecondEnumerator>
            where TGroupJoin_ConcatSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerator>
            where TGroupJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerator>
            where TGroupJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerator>
            where TGroupJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerable, TGroupJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DefaultIfEmptyInnerEnumerator>
            where TGroupJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerator>
            where TGroupJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerator>
            where TGroupJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerator>
            where TGroupJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerable, TGroupJoin_DistinctInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_DistinctInnerEnumerator>
            where TGroupJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator>(ExceptDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerator>
            where TGroupJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptSecondEnumerator>
            where TGroupJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator>(ExceptDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerator>
            where TGroupJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptSecondEnumerator>
            where TGroupJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator>(ExceptSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerator>
            where TGroupJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptSecondEnumerator>
            where TGroupJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator>(ExceptSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerable, TGroupJoin_ExceptFirstEnumerator, TGroupJoin_ExceptSecondEnumerable, TGroupJoin_ExceptSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptFirstEnumerator>
            where TGroupJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ExceptSecondEnumerator>
            where TGroupJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerator>
            where TGroupJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinLeftItem>
            where TGroupJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerator>
            where TGroupJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerator>
            where TGroupJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinLeftItem>
            where TGroupJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerator>
            where TGroupJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerator>
            where TGroupJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinLeftItem>
            where TGroupJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerator>
            where TGroupJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_GroupJoinKeyItem, TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerable, TGroupJoin_GroupJoinLeftEnumerator, TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerable, TGroupJoin_GroupJoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinLeftItem, TGroupJoin_GroupJoinLeftEnumerator>
            where TGroupJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinLeftItem>
            where TGroupJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_GroupJoinRightItem, TGroupJoin_GroupJoinRightEnumerator>
            where TGroupJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupedKey>(GroupedEnumerable<TGroupJoin_GroupedKey, TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupedKey>(GroupedEnumerable<TGroupJoin_GroupedKey, TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupedKey>(GroupingEnumerable<TGroupJoin_GroupedKey, TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupedKey>(GroupingEnumerable<TGroupJoin_GroupedKey, TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_RightItem, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupByCollectionDefaultEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_RightItem, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_RightItem, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupByCollectionSpecificEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_RightItem, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator>(IntersectDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerator>
            where TGroupJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectSecondEnumerator>
            where TGroupJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator>(IntersectDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerator>
            where TGroupJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectSecondEnumerator>
            where TGroupJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator>(IntersectSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerator>
            where TGroupJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectSecondEnumerator>
            where TGroupJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator>(IntersectSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerable, TGroupJoin_IntersectFirstEnumerator, TGroupJoin_IntersectSecondEnumerable, TGroupJoin_IntersectSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectFirstEnumerator>
            where TGroupJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_IntersectSecondEnumerator>
            where TGroupJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(TGroupJoin_RightItem[] inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(TGroupJoin_RightItem[] inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryValue>(Dictionary<TGroupJoin_RightItem, TGroupJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryValue>(Dictionary<TGroupJoin_RightItem, TGroupJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryKey>(Dictionary<TGroupJoin_DictionaryKey, TGroupJoin_RightItem>.ValueCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryKey>(Dictionary<TGroupJoin_DictionaryKey, TGroupJoin_RightItem>.ValueCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(IEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(IEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem>(IEnumerable inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<object, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, object>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem>(IEnumerable inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<object, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, object>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(LinkedList<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(LinkedList<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(List<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(List<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(Queue<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(Queue<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryValue>(SortedDictionary<TGroupJoin_RightItem, TGroupJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryValue>(SortedDictionary<TGroupJoin_RightItem, TGroupJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryKey>(SortedDictionary<TGroupJoin_DictionaryKey, TGroupJoin_RightItem>.ValueCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_DictionaryKey>(SortedDictionary<TGroupJoin_DictionaryKey, TGroupJoin_RightItem>.ValueCollection inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(SortedSet<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(SortedSet<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(Stack<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(Stack<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator>(JoinDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_JoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerator>
            where TGroupJoin_JoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_JoinLeftItem>
            where TGroupJoin_JoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerator>
            where TGroupJoin_JoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator>(JoinDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_JoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerator>
            where TGroupJoin_JoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_JoinLeftItem>
            where TGroupJoin_JoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerator>
            where TGroupJoin_JoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator>(JoinSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_JoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerator>
            where TGroupJoin_JoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_JoinLeftItem>
            where TGroupJoin_JoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerator>
            where TGroupJoin_JoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator>(JoinSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_JoinKeyItem, TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerable, TGroupJoin_JoinLeftEnumerator, TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerable, TGroupJoin_JoinRightEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_JoinLeftEnumerable : struct, IStructEnumerable<TGroupJoin_JoinLeftItem, TGroupJoin_JoinLeftEnumerator>
            where TGroupJoin_JoinLeftEnumerator : struct, IStructEnumerator<TGroupJoin_JoinLeftItem>
            where TGroupJoin_JoinRightEnumerable : struct, IStructEnumerable<TGroupJoin_JoinRightItem, TGroupJoin_JoinRightEnumerator>
            where TGroupJoin_JoinRightEnumerator : struct, IStructEnumerator<TGroupJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_OfTypeInItem, TGroupJoin_OfTypeInnerEnumerable, TGroupJoin_OfTypeInnerEnumerator>(OfTypeEnumerable<TGroupJoin_OfTypeInItem, TGroupJoin_RightItem, TGroupJoin_OfTypeInnerEnumerable, TGroupJoin_OfTypeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_OfTypeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_OfTypeInItem, TGroupJoin_OfTypeInnerEnumerator>
            where TGroupJoin_OfTypeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_OfTypeInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_OfTypeInItem, TGroupJoin_OfTypeInnerEnumerable, TGroupJoin_OfTypeInnerEnumerator>(OfTypeEnumerable<TGroupJoin_OfTypeInItem, TGroupJoin_RightItem, TGroupJoin_OfTypeInnerEnumerable, TGroupJoin_OfTypeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_OfTypeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_OfTypeInItem, TGroupJoin_OfTypeInnerEnumerator>
            where TGroupJoin_OfTypeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_OfTypeInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_OrderByKey, TGroupJoin_OrderByInnerEnumerable, TGroupJoin_OrderByInnerEnumerator, TGroupJoin_OrderByComparer>(OrderByEnumerable<TGroupJoin_RightItem, TGroupJoin_OrderByKey, TGroupJoin_OrderByInnerEnumerable, TGroupJoin_OrderByInnerEnumerator, TGroupJoin_OrderByComparer> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_OrderByInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_OrderByInnerEnumerator>
            where TGroupJoin_OrderByInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_OrderByComparer : struct, IStructComparer<TGroupJoin_RightItem, TGroupJoin_OrderByKey>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_OrderByKey, TGroupJoin_OrderByInnerEnumerable, TGroupJoin_OrderByInnerEnumerator, TGroupJoin_OrderByComparer>(OrderByEnumerable<TGroupJoin_RightItem, TGroupJoin_OrderByKey, TGroupJoin_OrderByInnerEnumerable, TGroupJoin_OrderByInnerEnumerator, TGroupJoin_OrderByComparer> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_OrderByInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_OrderByInnerEnumerator>
            where TGroupJoin_OrderByInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_OrderByComparer : struct, IStructComparer<TGroupJoin_RightItem, TGroupJoin_OrderByKey>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(RangeEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(RangeEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(RepeatEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(RepeatEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ReverseInnerEnumerable, TGroupJoin_ReverseInnerEnumerator>(ReverseEnumerable<TGroupJoin_RightItem, TGroupJoin_ReverseInnerEnumerable, TGroupJoin_ReverseInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_ReverseInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ReverseInnerEnumerator>
            where TGroupJoin_ReverseInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ReverseInnerEnumerable, TGroupJoin_ReverseInnerEnumerator>(ReverseEnumerable<TGroupJoin_RightItem, TGroupJoin_ReverseInnerEnumerable, TGroupJoin_ReverseInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_ReverseInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_ReverseInnerEnumerator>
            where TGroupJoin_ReverseInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator>(SelectEnumerable<TGroupJoin_SelectInItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator>(SelectEnumerable<TGroupJoin_SelectInItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator>(SelectIndexedEnumerable<TGroupJoin_SelectInItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator>(SelectIndexedEnumerable<TGroupJoin_SelectInItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyBridgeType, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyBridgeType : class
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_RightItem, TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyInnerEnumerable, TGroupJoin_SelectManyInnerEnumerator, TGroupJoin_SelectManyProjectedEnumerable, TGroupJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyInItem, TGroupJoin_SelectManyInnerEnumerator>
            where TGroupJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyInItem>
            where TGroupJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TGroupJoin_SelectManyCollectionItem, TGroupJoin_SelectManyProjectedEnumerator>
            where TGroupJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TGroupJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection>(SelectSelectEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInnerItem>
            where TGroupJoin_SelectProjection : struct, IStructProjection<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection>(SelectSelectEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInnerItem>
            where TGroupJoin_SelectProjection : struct, IStructProjection<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection, TGroupJoin_SelectPredicate>(SelectWhereEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection, TGroupJoin_SelectPredicate> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInnerItem>
            where TGroupJoin_SelectProjection : struct, IStructProjection<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem>
            where TGroupJoin_SelectPredicate : struct, IStructPredicate<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection, TGroupJoin_SelectPredicate>(SelectWhereEnumerable<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerable, TGroupJoin_SelectInnerEnumerator, TGroupJoin_SelectProjection, TGroupJoin_SelectPredicate> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SelectInnerEnumerable : struct, IStructEnumerable<TGroupJoin_SelectInnerItem, TGroupJoin_SelectInnerEnumerator>
            where TGroupJoin_SelectInnerEnumerator : struct, IStructEnumerator<TGroupJoin_SelectInnerItem>
            where TGroupJoin_SelectProjection : struct, IStructProjection<TGroupJoin_RightItem, TGroupJoin_SelectInnerItem>
            where TGroupJoin_SelectPredicate : struct, IStructPredicate<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator>(SkipEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SkipInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerator>
            where TGroupJoin_SkipInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator>(SkipEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SkipInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerator>
            where TGroupJoin_SkipInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator>(SkipWhileEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SkipInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerator>
            where TGroupJoin_SkipInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator>(SkipWhileEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SkipInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerator>
            where TGroupJoin_SkipInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator>(SkipWhileIndexedEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_SkipInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerator>
            where TGroupJoin_SkipInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator>(SkipWhileIndexedEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerable, TGroupJoin_SkipInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_SkipInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_SkipInnerEnumerator>
            where TGroupJoin_SkipInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator>(TakeEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_TakeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerator>
            where TGroupJoin_TakeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator>(TakeEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_TakeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerator>
            where TGroupJoin_TakeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator>(TakeWhileEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_TakeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerator>
            where TGroupJoin_TakeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator>(TakeWhileEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_TakeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerator>
            where TGroupJoin_TakeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator>(TakeWhileIndexedEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_TakeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerator>
            where TGroupJoin_TakeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator>(TakeWhileIndexedEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerable, TGroupJoin_TakeInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_TakeInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_TakeInnerEnumerator>
            where TGroupJoin_TakeInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator>(UnionDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_UnionFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerator>
            where TGroupJoin_UnionFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_UnionSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionSecondEnumerator>
            where TGroupJoin_UnionSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator>(UnionDefaultEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_UnionFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerator>
            where TGroupJoin_UnionFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_UnionSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionSecondEnumerator>
            where TGroupJoin_UnionSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator>(UnionSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_UnionFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerator>
            where TGroupJoin_UnionFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_UnionSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionSecondEnumerator>
            where TGroupJoin_UnionSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator>(UnionSpecificEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerable, TGroupJoin_UnionFirstEnumerator, TGroupJoin_UnionSecondEnumerable, TGroupJoin_UnionSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_UnionFirstEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionFirstEnumerator>
            where TGroupJoin_UnionFirstEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_UnionSecondEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_UnionSecondEnumerator>
            where TGroupJoin_UnionSecondEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator>(WhereEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator>(WhereEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator>(WhereIndexedEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator>(WhereIndexedEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate, TGroupJoin_WhereProjection>(WhereSelectEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate, TGroupJoin_WhereProjection> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_WhereInnerItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_WhereInnerItem>
            where TGroupJoin_WherePredicate : struct, IStructPredicate<TGroupJoin_WhereInnerItem>
            where TGroupJoin_WhereProjection : struct, IStructProjection<TGroupJoin_RightItem, TGroupJoin_WhereInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate, TGroupJoin_WhereProjection>(WhereSelectEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate, TGroupJoin_WhereProjection> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_WhereInnerItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_WhereInnerItem>
            where TGroupJoin_WherePredicate : struct, IStructPredicate<TGroupJoin_WhereInnerItem>
            where TGroupJoin_WhereProjection : struct, IStructProjection<TGroupJoin_RightItem, TGroupJoin_WhereInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate>(WhereWhereEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_WherePredicate : struct, IStructPredicate<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate>(WhereWhereEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerable, TGroupJoin_WhereInnerEnumerator, TGroupJoin_WherePredicate> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_WhereInnerEnumerable : struct, IStructEnumerable<TGroupJoin_RightItem, TGroupJoin_WhereInnerEnumerator>
            where TGroupJoin_WhereInnerEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
            where TGroupJoin_WherePredicate : struct, IStructPredicate<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ZipFirstItem, TGroupJoin_ZipSecondItem, TGroupJoin_ZipFirstEnumerable, TGroupJoin_ZipFirstEnumerator, TGroupJoin_ZipSecondEnumerable, TGroupJoin_ZipSecondEnumerator>(ZipEnumerable<TGroupJoin_RightItem, TGroupJoin_ZipFirstItem, TGroupJoin_ZipSecondItem, TGroupJoin_ZipFirstEnumerable, TGroupJoin_ZipFirstEnumerator, TGroupJoin_ZipSecondEnumerable, TGroupJoin_ZipSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_ZipFirstEnumerable : struct, IStructEnumerable<TGroupJoin_ZipFirstItem, TGroupJoin_ZipFirstEnumerator>
            where TGroupJoin_ZipFirstEnumerator : struct, IStructEnumerator<TGroupJoin_ZipFirstItem>
            where TGroupJoin_ZipSecondEnumerable : struct, IStructEnumerable<TGroupJoin_ZipSecondItem, TGroupJoin_ZipSecondEnumerator>
            where TGroupJoin_ZipSecondEnumerator : struct, IStructEnumerator<TGroupJoin_ZipSecondItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_ZipFirstItem, TGroupJoin_ZipSecondItem, TGroupJoin_ZipFirstEnumerable, TGroupJoin_ZipFirstEnumerator, TGroupJoin_ZipSecondEnumerable, TGroupJoin_ZipSecondEnumerator>(ZipEnumerable<TGroupJoin_RightItem, TGroupJoin_ZipFirstItem, TGroupJoin_ZipSecondItem, TGroupJoin_ZipFirstEnumerable, TGroupJoin_ZipFirstEnumerator, TGroupJoin_ZipSecondEnumerable, TGroupJoin_ZipSecondEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_ZipFirstEnumerable : struct, IStructEnumerable<TGroupJoin_ZipFirstItem, TGroupJoin_ZipFirstEnumerator>
            where TGroupJoin_ZipFirstEnumerator : struct, IStructEnumerator<TGroupJoin_ZipFirstItem>
            where TGroupJoin_ZipSecondEnumerable : struct, IStructEnumerable<TGroupJoin_ZipSecondItem, TGroupJoin_ZipSecondEnumerator>
            where TGroupJoin_ZipSecondEnumerator : struct, IStructEnumerator<TGroupJoin_ZipSecondItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_BridgeType, TGroupJoin_IdentityEnumerator>(IdentityEnumerable<TGroupJoin_RightItem, TGroupJoin_BridgeType, TGroupJoin_IdentityEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_BridgeType : class
            where TGroupJoin_IdentityEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem, TGroupJoin_BridgeType, TGroupJoin_IdentityEnumerator>(IdentityEnumerable<TGroupJoin_RightItem, TGroupJoin_BridgeType, TGroupJoin_IdentityEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_BridgeType : class
            where TGroupJoin_IdentityEnumerator : struct, IStructEnumerator<TGroupJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(ReverseRangeEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(ReverseRangeEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupByDefaultEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupByDefaultEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupBySpecificEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>>, TGroupJoin_OutItem> resultSelector)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator>(GroupBySpecificEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByKey, TGroupJoin_GroupByElement, TGroupJoin_GroupByEnumerable, TGroupJoin_GroupByEnumerator> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, GroupingEnumerable<TGroupJoin_GroupByKey, TGroupJoin_GroupByElement>>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
            where TGroupJoin_GroupByEnumerable : struct, IStructEnumerable<TGroupJoin_GroupByInItem, TGroupJoin_GroupByEnumerator>
            where TGroupJoin_GroupByEnumerator : struct, IStructEnumerator<TGroupJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>(Dictionary<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>(Dictionary<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>(SortedDictionary<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>(SortedDictionary<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, KeyValuePair<TGroupJoin_DictionaryKey, TGroupJoin_DictionaryValue>>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_LookupKey, TGroupJoin_LookupElement>(LookupEnumerable<TGroupJoin_LookupKey, TGroupJoin_LookupElement> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupJoin_LookupKey, TGroupJoin_LookupElement>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, GroupingEnumerable<TGroupJoin_LookupKey, TGroupJoin_LookupElement>>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_LookupKey, TGroupJoin_LookupElement>(LookupEnumerable<TGroupJoin_LookupKey, TGroupJoin_LookupElement> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TGroupJoin_LookupKey, TGroupJoin_LookupElement>, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, GroupingEnumerable<TGroupJoin_LookupKey, TGroupJoin_LookupElement>>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemDefaultEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemDefaultEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemSpecificEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TGroupJoin_OutItem> GroupJoin<TGroupJoin_OutItem, TGroupJoin_KeyItem, TGroupJoin_RightItem>(OneItemSpecificEnumerable<TGroupJoin_RightItem> inner, Func<TItem, TGroupJoin_KeyItem> outerKeySelector, Func<TGroupJoin_RightItem, TGroupJoin_KeyItem> innerKeySelector, Func<TItem, GroupedEnumerable<TGroupJoin_KeyItem, TGroupJoin_RightItem>, TGroupJoin_OutItem> resultSelector, IEqualityComparer<TGroupJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TGroupJoin_OutItem>.Empty;
        }
    }
}
