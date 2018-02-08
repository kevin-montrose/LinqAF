using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    partial struct EmptyOrderedEnumerable<TItem>
    {
        public OneItemDefaultOrderedEnumerable<TItem> Concat(OneItemDefaultOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public OneItemSpecificOrderedEnumerable<TItem> Concat(OneItemSpecificOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public OrderByEnumerable<TItem, TConcat_OrderByKey, TConcat_OrderByEnumerable, TConcat_OrderByEnumerator, TConcat_OrderByComparer> Concat<TConcat_OrderByKey, TConcat_OrderByEnumerable, TConcat_OrderByEnumerator, TConcat_OrderByComparer>(OrderByEnumerable<TItem, TConcat_OrderByKey, TConcat_OrderByEnumerable, TConcat_OrderByEnumerator, TConcat_OrderByComparer> second)
            where TConcat_OrderByEnumerable : struct, IStructEnumerable<TItem, TConcat_OrderByEnumerator>
            where TConcat_OrderByEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_OrderByComparer : struct, IStructComparer<TItem, TConcat_OrderByKey>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public GroupingEnumerable<TConcat_GroupedKey, TItem> Concat<TConcat_GroupedKey>(GroupingEnumerable<TConcat_GroupedKey, TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public GroupedEnumerable<TConcat_GroupedKey, TItem> Concat<TConcat_GroupedKey>(GroupedEnumerable<TConcat_GroupedKey, TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public WhereIndexedEnumerable<TItem, TConcat_Concat_InnerEnumerable, TConcat_Concat_InnerEnumerator> Concat<TConcat_Concat_InnerEnumerable, TConcat_Concat_InnerEnumerator>(WhereIndexedEnumerable<TItem, TConcat_Concat_InnerEnumerable, TConcat_Concat_InnerEnumerator> second)
            where TConcat_Concat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_Concat_InnerEnumerator>
            where TConcat_Concat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public ReverseEnumerable<TItem, TConcat_ReverseEnumerable, TConcat_ReverseEnumerator> Concat<TConcat_ReverseEnumerable, TConcat_ReverseEnumerator>(ReverseEnumerable<TItem, TConcat_ReverseEnumerable, TConcat_ReverseEnumerator> second)
            where TConcat_ReverseEnumerable : struct, IStructEnumerable<TItem, TConcat_ReverseEnumerator>
            where TConcat_ReverseEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public ExceptDefaultEnumerable<TItem, TConcat_ExceptFirstEnumerable, TConcat_ExceptFirstEnumerator, TConcat_ExceptSecondEnumerable, TConcat_ExceptSecondEnumerator> Concat<TConcat_ExceptFirstEnumerable, TConcat_ExceptFirstEnumerator, TConcat_ExceptSecondEnumerable, TConcat_ExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TConcat_ExceptFirstEnumerable, TConcat_ExceptFirstEnumerator, TConcat_ExceptSecondEnumerable, TConcat_ExceptSecondEnumerator> second)
            where TConcat_ExceptFirstEnumerable : struct, IStructEnumerable<TItem, TConcat_ExceptFirstEnumerator>
            where TConcat_ExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_ExceptSecondEnumerable : struct, IStructEnumerable<TItem, TConcat_ExceptSecondEnumerator>
            where TConcat_ExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public ExceptSpecificEnumerable<TItem, TConcat_ExceptFirstEnumerable, TConcat_ExceptFirstEnumerator, TConcat_ExceptSecondEnumerable, TConcat_ExceptSecondEnumerator> Concat<TConcat_ExceptFirstEnumerable, TConcat_ExceptFirstEnumerator, TConcat_ExceptSecondEnumerable, TConcat_ExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TConcat_ExceptFirstEnumerable, TConcat_ExceptFirstEnumerator, TConcat_ExceptSecondEnumerable, TConcat_ExceptSecondEnumerator> second)
            where TConcat_ExceptFirstEnumerable : struct, IStructEnumerable<TItem, TConcat_ExceptFirstEnumerator>
            where TConcat_ExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_ExceptSecondEnumerable : struct, IStructEnumerable<TItem, TConcat_ExceptSecondEnumerator>
            where TConcat_ExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public IntersectDefaultEnumerable<TItem, TConcat_IntersectFirstEnumerable, TConcat_IntersectFirstEnumerator, TConcat_IntersectSecondEnumerable, TConcat_IntersectSecondEnumerator> Concat<TConcat_IntersectFirstEnumerable, TConcat_IntersectFirstEnumerator, TConcat_IntersectSecondEnumerable, TConcat_IntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TConcat_IntersectFirstEnumerable, TConcat_IntersectFirstEnumerator, TConcat_IntersectSecondEnumerable, TConcat_IntersectSecondEnumerator> second)
            where TConcat_IntersectFirstEnumerable : struct, IStructEnumerable<TItem, TConcat_IntersectFirstEnumerator>
            where TConcat_IntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IntersectSecondEnumerable : struct, IStructEnumerable<TItem, TConcat_IntersectSecondEnumerator>
            where TConcat_IntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public UnionDefaultEnumerable<TItem, TConcat_UnionFirstEnumerable, TConcat_UnionFirstEnumerator, TConcat_UnionSecondEnumerable, TConcat_UnionSecondEnumerator> Concat<TConcat_UnionFirstEnumerable, TConcat_UnionFirstEnumerator, TConcat_UnionSecondEnumerable, TConcat_UnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TConcat_UnionFirstEnumerable, TConcat_UnionFirstEnumerator, TConcat_UnionSecondEnumerable, TConcat_UnionSecondEnumerator> second)
            where TConcat_UnionFirstEnumerable : struct, IStructEnumerable<TItem, TConcat_UnionFirstEnumerator>
            where TConcat_UnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_UnionSecondEnumerable : struct, IStructEnumerable<TItem, TConcat_UnionSecondEnumerator>
            where TConcat_UnionSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public UnionSpecificEnumerable<TItem, TConcat_UnionFirstEnumerable, TConcat_UnionFirstEnumerator, TConcat_UnionSecondEnumerable, TConcat_UnionSecondEnumerator> Concat<TConcat_UnionFirstEnumerable, TConcat_UnionFirstEnumerator, TConcat_UnionSecondEnumerable, TConcat_UnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TConcat_UnionFirstEnumerable, TConcat_UnionFirstEnumerator, TConcat_UnionSecondEnumerable, TConcat_UnionSecondEnumerator> second)
            where TConcat_UnionFirstEnumerable : struct, IStructEnumerable<TItem, TConcat_UnionFirstEnumerator>
            where TConcat_UnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_UnionSecondEnumerable : struct, IStructEnumerable<TItem, TConcat_UnionSecondEnumerator>
            where TConcat_UnionSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public IntersectSpecificEnumerable<TItem, TConcat_IntersectFirstEnumerable, TConcat_IntersectFirstEnumerator, TConcat_IntersectSecondEnumerable, TConcat_IntersectSecondEnumerator> Concat<TConcat_IntersectFirstEnumerable, TConcat_IntersectFirstEnumerator, TConcat_IntersectSecondEnumerable, TConcat_IntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TConcat_IntersectFirstEnumerable, TConcat_IntersectFirstEnumerator, TConcat_IntersectSecondEnumerable, TConcat_IntersectSecondEnumerator> second)
            where TConcat_IntersectFirstEnumerable : struct, IStructEnumerable<TItem, TConcat_IntersectFirstEnumerator>
            where TConcat_IntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IntersectSecondEnumerable : struct, IStructEnumerable<TItem, TConcat_IntersectSecondEnumerator>
            where TConcat_IntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public GroupByCollectionDefaultEnumerable<TConcat_GroupByInItem, TConcat_GroupByKey, TConcat_GroupByElement, TItem, TConcat_GroupByEnumerable, TConcat_GroupByEnumerator> Concat<TConcat_GroupByInItem, TConcat_GroupByKey, TConcat_GroupByElement, TConcat_GroupByEnumerable, TConcat_GroupByEnumerator>(GroupByCollectionDefaultEnumerable<TConcat_GroupByInItem, TConcat_GroupByKey, TConcat_GroupByElement, TItem, TConcat_GroupByEnumerable, TConcat_GroupByEnumerator> second)
            where TConcat_GroupByEnumerable : struct, IStructEnumerable<TConcat_GroupByInItem, TConcat_GroupByEnumerator>
            where TConcat_GroupByEnumerator : struct, IStructEnumerator<TConcat_GroupByInItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public GroupByCollectionSpecificEnumerable<TConcat_GroupByInItem, TConcat_GroupByKey, TConcat_GroupByElement, TItem, TConcat_GroupByEnumerable, TConcat_GroupByEnumerator> Concat<TConcat_GroupByInItem, TConcat_GroupByKey, TConcat_GroupByElement, TConcat_GroupByEnumerable, TConcat_GroupByEnumerator>(GroupByCollectionSpecificEnumerable<TConcat_GroupByInItem, TConcat_GroupByKey, TConcat_GroupByElement, TItem, TConcat_GroupByEnumerable, TConcat_GroupByEnumerator> second)
            where TConcat_GroupByEnumerable : struct, IStructEnumerable<TConcat_GroupByInItem, TConcat_GroupByEnumerator>
            where TConcat_GroupByEnumerator : struct, IStructEnumerator<TConcat_GroupByInItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public JoinSpecificEnumerable<TItem, TConcat_JoinKeyItem, TConcat_JoinLeftItem, TConcat_JoinLeftEnumerable, TConcat_JoinLeftEnumerator, TConcat_JoinRightItem, TConcat_JoinRightEnumerable, TConcat_JoinRightEnumerator> Concat<TConcat_JoinKeyItem, TConcat_JoinLeftItem, TConcat_JoinLeftEnumerable, TConcat_JoinLeftEnumerator, TConcat_JoinRightItem, TConcat_JoinRightEnumerable, TConcat_JoinRightEnumerator>(JoinSpecificEnumerable<TItem, TConcat_JoinKeyItem, TConcat_JoinLeftItem, TConcat_JoinLeftEnumerable, TConcat_JoinLeftEnumerator, TConcat_JoinRightItem, TConcat_JoinRightEnumerable, TConcat_JoinRightEnumerator> second)
            where TConcat_JoinLeftEnumerable : struct, IStructEnumerable<TConcat_JoinLeftItem, TConcat_JoinLeftEnumerator>
            where TConcat_JoinLeftEnumerator : struct, IStructEnumerator<TConcat_JoinLeftItem>
            where TConcat_JoinRightEnumerable : struct, IStructEnumerable<TConcat_JoinRightItem, TConcat_JoinRightEnumerator>
            where TConcat_JoinRightEnumerator : struct, IStructEnumerator<TConcat_JoinRightItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public JoinDefaultEnumerable<TItem, TConcat_JoinKeyItem, TConcat_JoinLeftItem, TConcat_JoinLeftEnumerable, TConcat_JoinLeftEnumerator, TConcat_JoinRightItem, TConcat_JoinRightEnumerable, TConcat_JoinRightEnumerator> Concat<TConcat_JoinKeyItem, TConcat_JoinLeftItem, TConcat_JoinLeftEnumerable, TConcat_JoinLeftEnumerator, TConcat_JoinRightItem, TConcat_JoinRightEnumerable, TConcat_JoinRightEnumerator>(JoinDefaultEnumerable<TItem, TConcat_JoinKeyItem, TConcat_JoinLeftItem, TConcat_JoinLeftEnumerable, TConcat_JoinLeftEnumerator, TConcat_JoinRightItem, TConcat_JoinRightEnumerable, TConcat_JoinRightEnumerator> second)
            where TConcat_JoinLeftEnumerable : struct, IStructEnumerable<TConcat_JoinLeftItem, TConcat_JoinLeftEnumerator>
            where TConcat_JoinLeftEnumerator : struct, IStructEnumerator<TConcat_JoinLeftItem>
            where TConcat_JoinRightEnumerable : struct, IStructEnumerable<TConcat_JoinRightItem, TConcat_JoinRightEnumerator>
            where TConcat_JoinRightEnumerator : struct, IStructEnumerator<TConcat_JoinRightItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public GroupJoinSpecificEnumerable<TItem, TConcat_GroupJoinKeyItem, TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerable, TConcat_GroupJoinLeftEnumerator, TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerable, TConcat_GroupJoinRightEnumerator> Concat<TConcat_GroupJoinKeyItem, TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerable, TConcat_GroupJoinLeftEnumerator, TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerable, TConcat_GroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TItem, TConcat_GroupJoinKeyItem, TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerable, TConcat_GroupJoinLeftEnumerator, TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerable, TConcat_GroupJoinRightEnumerator> second)
            where TConcat_GroupJoinLeftEnumerable : struct, IStructEnumerable<TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerator>
            where TConcat_GroupJoinLeftEnumerator : struct, IStructEnumerator<TConcat_GroupJoinLeftItem>
            where TConcat_GroupJoinRightEnumerable : struct, IStructEnumerable<TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerator>
            where TConcat_GroupJoinRightEnumerator : struct, IStructEnumerator<TConcat_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public GroupJoinDefaultEnumerable<TItem, TConcat_GroupJoinKeyItem, TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerable, TConcat_GroupJoinLeftEnumerator, TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerable, TConcat_GroupJoinRightEnumerator> Concat<TConcat_GroupJoinKeyItem, TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerable, TConcat_GroupJoinLeftEnumerator, TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerable, TConcat_GroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TItem, TConcat_GroupJoinKeyItem, TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerable, TConcat_GroupJoinLeftEnumerator, TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerable, TConcat_GroupJoinRightEnumerator> second)
            where TConcat_GroupJoinLeftEnumerable : struct, IStructEnumerable<TConcat_GroupJoinLeftItem, TConcat_GroupJoinLeftEnumerator>
            where TConcat_GroupJoinLeftEnumerator : struct, IStructEnumerator<TConcat_GroupJoinLeftItem>
            where TConcat_GroupJoinRightEnumerable : struct, IStructEnumerable<TConcat_GroupJoinRightItem, TConcat_GroupJoinRightEnumerator>
            where TConcat_GroupJoinRightEnumerator : struct, IStructEnumerator<TConcat_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public OneItemDefaultEnumerable<TItem> Concat(OneItemDefaultEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public OneItemSpecificEnumerable<TItem> Concat(OneItemSpecificEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public RepeatEnumerable<TItem> Concat(RepeatEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }
        
        public EmptyEnumerable<TItem> Concat(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return EmptyCache<TItem>.Empty;
        }

        public DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public TakeWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> Concat<TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public ConcatEnumerable<TItem, TConcat_InnerLeftEnumerable, TConcat_InnerLeftEnumerator, TConcat_InnerRightEnumerable, TConcat_InnerRightEnumerator> Concat<TConcat_InnerLeftEnumerable, TConcat_InnerLeftEnumerator, TConcat_InnerRightEnumerable, TConcat_InnerRightEnumerator>(ConcatEnumerable<TItem, TConcat_InnerLeftEnumerable, TConcat_InnerLeftEnumerator, TConcat_InnerRightEnumerable, TConcat_InnerRightEnumerator> second)
            where TConcat_InnerLeftEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerLeftEnumerator>
            where TConcat_InnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_InnerRightEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerRightEnumerator>
            where TConcat_InnerRightEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> Concat<TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }
        public IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>> Concat(IEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>> Concat(List<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>> Concat(LinkedList<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>> Concat(HashSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>> Concat(Queue<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>> Concat(Stack<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>> Concat(SortedSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, Dictionary<TConcat_DictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TConcat_DictionaryKey, TItem>, DictionaryValuesEnumerator<TConcat_DictionaryKey, TItem>> Concat<TConcat_DictionaryKey>(Dictionary<TConcat_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, SortedDictionary<TConcat_DictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TConcat_DictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TConcat_DictionaryKey, TItem>> Concat<TConcat_DictionaryKey>(SortedDictionary<TConcat_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, SortedDictionary<TItem, TConcat_DictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TConcat_DictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TConcat_DictionaryValue>> Concat<TConcat_DictionaryValue>(SortedDictionary<TItem, TConcat_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public IdentityEnumerable<TItem, Dictionary<TItem, TConcat_DictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TConcat_DictionaryValue>, DictionaryKeysEnumerator<TItem, TConcat_DictionaryValue>> Concat<TConcat_DictionaryValue>(Dictionary<TItem, TConcat_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");

            return CommonImplementation.Bridge(second, nameof(second));
        }

        public SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second)
            where TConcat_BridgeType : class
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyBridgeType : class
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyBridgeType : class
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> Concat<TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyBridgeType : class
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> Concat<TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second)
            where TConcat_IdentityBridgeType : class
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>> Concat(TItem[] second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var ident = CommonImplementation.Bridge(second, nameof(second));

            return ident;
        }

        public BoxedEnumerable<TItem> Concat(BoxedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectSelectEnumerable<TItem, TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerable, TConcat_Concat_SelectInnerEnumerator, TConcat_Concat_SelectProjection> Concat<TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerable, TConcat_Concat_SelectInnerEnumerator, TConcat_Concat_SelectProjection>(SelectSelectEnumerable<TItem, TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerable, TConcat_Concat_SelectInnerEnumerator, TConcat_Concat_SelectProjection> second)
            where TConcat_Concat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerator>
            where TConcat_Concat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_Concat_SelectInnerItem>
            where TConcat_Concat_SelectProjection : struct, IStructProjection<TItem, TConcat_Concat_SelectInnerItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public SelectWhereEnumerable<TItem, TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerable, TConcat_Concat_SelectInnerEnumerator, TConcat_Concat_SelectProjection, TConcat_Concat_SelectPredicate> Concat<TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerable, TConcat_Concat_SelectInnerEnumerator, TConcat_Concat_SelectProjection, TConcat_Concat_SelectPredicate>(SelectWhereEnumerable<TItem, TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerable, TConcat_Concat_SelectInnerEnumerator, TConcat_Concat_SelectProjection, TConcat_Concat_SelectPredicate> second)
            where TConcat_Concat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_Concat_SelectInnerItem, TConcat_Concat_SelectInnerEnumerator>
            where TConcat_Concat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_Concat_SelectInnerItem>
            where TConcat_Concat_SelectProjection : struct, IStructProjection<TItem, TConcat_Concat_SelectInnerItem>
            where TConcat_Concat_SelectPredicate : struct, IStructPredicate<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public WhereWhereEnumerable<TItem, TConcat_Concat_WhereInnerEnumerable, TConcat_Concat_WhereInnerEnumerator, TConcat_Concat_WherePredicate> Concat<TConcat_Concat_WhereInnerEnumerable, TConcat_Concat_WhereInnerEnumerator, TConcat_Concat_WherePredicate>(WhereWhereEnumerable<TItem, TConcat_Concat_WhereInnerEnumerable, TConcat_Concat_WhereInnerEnumerator, TConcat_Concat_WherePredicate> second)
            where TConcat_Concat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_Concat_WhereInnerEnumerator>
            where TConcat_Concat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Concat_WherePredicate : struct, IStructPredicate<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public WhereSelectEnumerable<TItem, TConcat_Concat_WhereInnerItem, TConcat_Concat_WhereInnerEnumerable, TConcat_Concat_WhereInnerEnumerator, TConcat_Concat_WherePredicate, TConcat_Concat_WhereProjection> Concat<TConcat_Concat_WhereInnerItem, TConcat_Concat_WhereInnerEnumerable, TConcat_Concat_WhereInnerEnumerator, TConcat_Concat_WherePredicate, TConcat_Concat_WhereProjection>(WhereSelectEnumerable<TItem, TConcat_Concat_WhereInnerItem, TConcat_Concat_WhereInnerEnumerable, TConcat_Concat_WhereInnerEnumerator, TConcat_Concat_WherePredicate, TConcat_Concat_WhereProjection> second)
            where TConcat_Concat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_Concat_WhereInnerItem, TConcat_Concat_WhereInnerEnumerator>
            where TConcat_Concat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_Concat_WhereInnerItem>
            where TConcat_Concat_WherePredicate : struct, IStructPredicate<TConcat_Concat_WhereInnerItem>
            where TConcat_Concat_WhereProjection : struct, IStructProjection<TItem, TConcat_Concat_WhereInnerItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public DistinctDefaultEnumerable<TItem, TConcat_Concat_DistinctInnerEnumerable, TConcat_Concat_DistinctInnerEnumerator> Concat<TConcat_Concat_DistinctInnerEnumerable, TConcat_Concat_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TConcat_Concat_DistinctInnerEnumerable, TConcat_Concat_DistinctInnerEnumerator> second)
            where TConcat_Concat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_Concat_DistinctInnerEnumerator>
            where TConcat_Concat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }

        public DistinctSpecificEnumerable<TItem, TConcat_Concat_DistinctInnerEnumerable, TConcat_Concat_DistinctInnerEnumerator> Concat<TConcat_Concat_DistinctInnerEnumerable, TConcat_Concat_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TConcat_Concat_DistinctInnerEnumerable, TConcat_Concat_DistinctInnerEnumerator> second)
            where TConcat_Concat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_Concat_DistinctInnerEnumerator>
            where TConcat_Concat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second;
        }
    }
}