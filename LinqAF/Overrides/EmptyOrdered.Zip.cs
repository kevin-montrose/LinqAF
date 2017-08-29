using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct EmptyOrderedEnumerable<TItem>
    {
        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_ExceptOutItem, TZip_ExceptFirstEnumerable, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerable, TZip_ExceptSecondEnumerator>(ExceptDefaultEnumerable<TZip_ExceptOutItem, TZip_ExceptFirstEnumerable, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerable, TZip_ExceptSecondEnumerator> second, Func<TItem, TZip_ExceptOutItem, TZip_OutItem> resultSelector)
            where TZip_ExceptFirstEnumerable : struct, IStructEnumerable<TZip_ExceptOutItem, TZip_ExceptFirstEnumerator>
            where TZip_ExceptFirstEnumerator : struct, IStructEnumerator<TZip_ExceptOutItem>
            where TZip_ExceptSecondEnumerable : struct, IStructEnumerable<TZip_ExceptOutItem, TZip_ExceptSecondEnumerator>
            where TZip_ExceptSecondEnumerator : struct, IStructEnumerator<TZip_ExceptOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_ExceptOutItem, ExceptDefaultEnumerable<TZip_ExceptOutItem, TZip_ExceptFirstEnumerable, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerable, TZip_ExceptSecondEnumerator>, ExceptDefaultEnumerator<TZip_ExceptOutItem, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_ExceptOutItem, TZip_ExceptFirstEnumerable, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerable, TZip_ExceptSecondEnumerator>(ExceptSpecificEnumerable<TZip_ExceptOutItem, TZip_ExceptFirstEnumerable, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerable, TZip_ExceptSecondEnumerator> second, Func<TItem, TZip_ExceptOutItem, TZip_OutItem> resultSelector)
            where TZip_ExceptFirstEnumerable : struct, IStructEnumerable<TZip_ExceptOutItem, TZip_ExceptFirstEnumerator>
            where TZip_ExceptFirstEnumerator : struct, IStructEnumerator<TZip_ExceptOutItem>
            where TZip_ExceptSecondEnumerable : struct, IStructEnumerable<TZip_ExceptOutItem, TZip_ExceptSecondEnumerator>
            where TZip_ExceptSecondEnumerator : struct, IStructEnumerator<TZip_ExceptOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_ExceptOutItem, ExceptSpecificEnumerable<TZip_ExceptOutItem, TZip_ExceptFirstEnumerable, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerable, TZip_ExceptSecondEnumerator>, ExceptSpecificEnumerator<TZip_ExceptOutItem, TZip_ExceptFirstEnumerator, TZip_ExceptSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_IntersectOutItem, TZip_IntersectFirstEnumerable, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerable, TZip_IntersectSecondEnumerator>(IntersectDefaultEnumerable<TZip_IntersectOutItem, TZip_IntersectFirstEnumerable, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerable, TZip_IntersectSecondEnumerator> second, Func<TItem, TZip_IntersectOutItem, TZip_OutItem> resultSelector)
            where TZip_IntersectFirstEnumerable : struct, IStructEnumerable<TZip_IntersectOutItem, TZip_IntersectFirstEnumerator>
            where TZip_IntersectFirstEnumerator : struct, IStructEnumerator<TZip_IntersectOutItem>
            where TZip_IntersectSecondEnumerable : struct, IStructEnumerable<TZip_IntersectOutItem, TZip_IntersectSecondEnumerator>
            where TZip_IntersectSecondEnumerator : struct, IStructEnumerator<TZip_IntersectOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_IntersectOutItem, IntersectDefaultEnumerable<TZip_IntersectOutItem, TZip_IntersectFirstEnumerable, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerable, TZip_IntersectSecondEnumerator>, IntersectDefaultEnumerator<TZip_IntersectOutItem, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_IntersectOutItem, TZip_IntersectFirstEnumerable, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerable, TZip_IntersectSecondEnumerator>(IntersectSpecificEnumerable<TZip_IntersectOutItem, TZip_IntersectFirstEnumerable, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerable, TZip_IntersectSecondEnumerator> second, Func<TItem, TZip_IntersectOutItem, TZip_OutItem> resultSelector)
            where TZip_IntersectFirstEnumerable : struct, IStructEnumerable<TZip_IntersectOutItem, TZip_IntersectFirstEnumerator>
            where TZip_IntersectFirstEnumerator : struct, IStructEnumerator<TZip_IntersectOutItem>
            where TZip_IntersectSecondEnumerable : struct, IStructEnumerable<TZip_IntersectOutItem, TZip_IntersectSecondEnumerator>
            where TZip_IntersectSecondEnumerator : struct, IStructEnumerator<TZip_IntersectOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_IntersectOutItem, IntersectSpecificEnumerable<TZip_IntersectOutItem, TZip_IntersectFirstEnumerable, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerable, TZip_IntersectSecondEnumerator>, IntersectSpecificEnumerator<TZip_IntersectOutItem, TZip_IntersectFirstEnumerator, TZip_IntersectSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_UnionOutItem, TZip_UnionFirstEnumerable, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerable, TZip_UnionSecondEnumerator>(UnionDefaultEnumerable<TZip_UnionOutItem, TZip_UnionFirstEnumerable, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerable, TZip_UnionSecondEnumerator> second, Func<TItem, TZip_UnionOutItem, TZip_OutItem> resultSelector)
            where TZip_UnionFirstEnumerable : struct, IStructEnumerable<TZip_UnionOutItem, TZip_UnionFirstEnumerator>
            where TZip_UnionFirstEnumerator : struct, IStructEnumerator<TZip_UnionOutItem>
            where TZip_UnionSecondEnumerable : struct, IStructEnumerable<TZip_UnionOutItem, TZip_UnionSecondEnumerator>
            where TZip_UnionSecondEnumerator : struct, IStructEnumerator<TZip_UnionOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_UnionOutItem, UnionDefaultEnumerable<TZip_UnionOutItem, TZip_UnionFirstEnumerable, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerable, TZip_UnionSecondEnumerator>, UnionDefaultEnumerator<TZip_UnionOutItem, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_UnionOutItem, TZip_UnionFirstEnumerable, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerable, TZip_UnionSecondEnumerator>(UnionSpecificEnumerable<TZip_UnionOutItem, TZip_UnionFirstEnumerable, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerable, TZip_UnionSecondEnumerator> second, Func<TItem, TZip_UnionOutItem, TZip_OutItem> resultSelector)
            where TZip_UnionFirstEnumerable : struct, IStructEnumerable<TZip_UnionOutItem, TZip_UnionFirstEnumerator>
            where TZip_UnionFirstEnumerator : struct, IStructEnumerator<TZip_UnionOutItem>
            where TZip_UnionSecondEnumerable : struct, IStructEnumerable<TZip_UnionOutItem, TZip_UnionSecondEnumerator>
            where TZip_UnionSecondEnumerator : struct, IStructEnumerator<TZip_UnionOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_UnionOutItem, UnionSpecificEnumerable<TZip_UnionOutItem, TZip_UnionFirstEnumerable, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerable, TZip_UnionSecondEnumerator>, UnionSpecificEnumerator<TZip_UnionOutItem, TZip_UnionFirstEnumerator, TZip_UnionSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupedKey, TZip_GroupedOutItem>(GroupedEnumerable<TZip_GroupedKey, TZip_GroupedOutItem> second, Func<TItem, TZip_GroupedOutItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_GroupedOutItem, GroupedEnumerable<TZip_GroupedKey, TZip_GroupedOutItem>, GroupedEnumerator<TZip_GroupedOutItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupedKey, TZip_GroupedOutItem>(GroupingEnumerable<TZip_GroupedKey, TZip_GroupedOutItem> second, Func<TItem, TZip_GroupedOutItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_GroupedOutItem, GroupingEnumerable<TZip_GroupedKey, TZip_GroupedOutItem>, GroupingEnumerator<TZip_GroupedOutItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerable, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerable, TZip_GroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerable, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerable, TZip_GroupJoinRightEnumerator> second, Func<TItem, TZip_GroupJoinOutItem, TZip_OutItem> resultSelector)
            where TZip_GroupJoinLeftEnumerable : struct, IStructEnumerable<TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerator>
            where TZip_GroupJoinLeftEnumerator : struct, IStructEnumerator<TZip_GroupJoinLeftItem>
            where TZip_GroupJoinRightEnumerable : struct, IStructEnumerable<TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerator>
            where TZip_GroupJoinRightEnumerator : struct, IStructEnumerator<TZip_GroupJoinRightItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_GroupJoinOutItem, GroupJoinDefaultEnumerable<TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerable, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerable, TZip_GroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerable, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerable, TZip_GroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerable, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerable, TZip_GroupJoinRightEnumerator> second, Func<TItem, TZip_GroupJoinOutItem, TZip_OutItem> resultSelector)
            where TZip_GroupJoinLeftEnumerable : struct, IStructEnumerable<TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerator>
            where TZip_GroupJoinLeftEnumerator : struct, IStructEnumerator<TZip_GroupJoinLeftItem>
            where TZip_GroupJoinRightEnumerable : struct, IStructEnumerable<TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerator>
            where TZip_GroupJoinRightEnumerator : struct, IStructEnumerator<TZip_GroupJoinRightItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_GroupJoinOutItem, GroupJoinSpecificEnumerable<TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerable, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerable, TZip_GroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TZip_GroupJoinOutItem, TZip_GroupJoinKeyItem, TZip_GroupJoinLeftItem, TZip_GroupJoinLeftEnumerator, TZip_GroupJoinRightItem, TZip_GroupJoinRightEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerable, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerable, TZip_JoinRightEnumerator>(JoinDefaultEnumerable<TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerable, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerable, TZip_JoinRightEnumerator> second, Func<TItem, TZip_JoinOutItem, TZip_OutItem> resultSelector)
            where TZip_JoinLeftEnumerable : struct, IStructEnumerable<TZip_JoinLeftItem, TZip_JoinLeftEnumerator>
            where TZip_JoinLeftEnumerator : struct, IStructEnumerator<TZip_JoinLeftItem>
            where TZip_JoinRightEnumerable : struct, IStructEnumerable<TZip_JoinRightItem, TZip_JoinRightEnumerator>
            where TZip_JoinRightEnumerator : struct, IStructEnumerator<TZip_JoinRightItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_JoinOutItem, JoinDefaultEnumerable<TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerable, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerable, TZip_JoinRightEnumerator>, JoinDefaultEnumerator<TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerable, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerable, TZip_JoinRightEnumerator>(JoinSpecificEnumerable<TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerable, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerable, TZip_JoinRightEnumerator> second, Func<TItem, TZip_JoinOutItem, TZip_OutItem> resultSelector)
            where TZip_JoinLeftEnumerable : struct, IStructEnumerable<TZip_JoinLeftItem, TZip_JoinLeftEnumerator>
            where TZip_JoinLeftEnumerator : struct, IStructEnumerator<TZip_JoinLeftItem>
            where TZip_JoinRightEnumerable : struct, IStructEnumerable<TZip_JoinRightItem, TZip_JoinRightEnumerator>
            where TZip_JoinRightEnumerator : struct, IStructEnumerator<TZip_JoinRightItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_JoinOutItem, JoinSpecificEnumerable<TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerable, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerable, TZip_JoinRightEnumerator>, JoinSpecificEnumerator<TZip_JoinOutItem, TZip_JoinKeyItem, TZip_JoinLeftItem, TZip_JoinLeftEnumerator, TZip_JoinRightItem, TZip_JoinRightEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_OrderByKey, TZip_OrderByOutItem, TZip_OrderByInnerEnumerable, TZip_OrderByInnerEnumerator, TZip_OrderByComparer>(OrderByEnumerable<TZip_OrderByOutItem, TZip_OrderByKey, TZip_OrderByInnerEnumerable, TZip_OrderByInnerEnumerator, TZip_OrderByComparer> second, Func<TItem, TZip_OrderByOutItem, TZip_OutItem> resultSelector)
            where TZip_OrderByInnerEnumerable : struct, IStructEnumerable<TZip_OrderByOutItem, TZip_OrderByInnerEnumerator>
            where TZip_OrderByInnerEnumerator : struct, IStructEnumerator<TZip_OrderByOutItem>
            where TZip_OrderByComparer : struct, IStructComparer<TZip_OrderByOutItem, TZip_OrderByKey>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_OrderByOutItem, OrderByEnumerable<TZip_OrderByOutItem, TZip_OrderByKey, TZip_OrderByInnerEnumerable, TZip_OrderByInnerEnumerator, TZip_OrderByComparer>, OrderByEnumerator<TZip_OrderByOutItem, TZip_OrderByKey, TZip_OrderByInnerEnumerator, TZip_OrderByComparer>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_ReverseOutItem, TZip_ReverseEnumerable, TZip_ReverseEnumerator>(ReverseEnumerable<TZip_ReverseOutItem, TZip_ReverseEnumerable, TZip_ReverseEnumerator> second, Func<TItem, TZip_ReverseOutItem, TZip_OutItem> resultSelector)
            where TZip_ReverseEnumerable : struct, IStructEnumerable<TZip_ReverseOutItem, TZip_ReverseEnumerator>
            where TZip_ReverseEnumerator : struct, IStructEnumerator<TZip_ReverseOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_ReverseOutItem, ReverseEnumerable<TZip_ReverseOutItem, TZip_ReverseEnumerable, TZip_ReverseEnumerator>, ReverseEnumerator<TZip_ReverseOutItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_ReverseRangeOutItem>(ReverseRangeEnumerable<TZip_ReverseRangeOutItem> second, Func<TItem, TZip_ReverseRangeOutItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_ReverseRangeOutItem, ReverseRangeEnumerable<TZip_ReverseRangeOutItem>, ReverseRangeEnumerator<TZip_ReverseRangeOutItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator>(GroupByCollectionDefaultEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator> second, Func<TItem, TZip_GroupByOutItem, TZip_OutItem> resultSelector)
            where TZip_GroupByEnumerable : struct, IStructEnumerable<TZip_GroupByInItem, TZip_GroupByEnumerator>
            where TZip_GroupByEnumerator : struct, IStructEnumerator<TZip_GroupByInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_GroupByOutItem, GroupByCollectionDefaultEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator>, GroupByCollectionDefaultEnumerator<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator>(GroupByCollectionSpecificEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator> second, Func<TItem, TZip_GroupByOutItem, TZip_OutItem> resultSelector)
            where TZip_GroupByEnumerable : struct, IStructEnumerable<TZip_GroupByInItem, TZip_GroupByEnumerator>
            where TZip_GroupByEnumerator : struct, IStructEnumerator<TZip_GroupByInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_GroupByOutItem, GroupByCollectionSpecificEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator>, GroupByCollectionSpecificEnumerator<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByOutItem, TZip_GroupByEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByInItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator>(GroupByDefaultEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByEnumerable, TZip_GroupByEnumerator> second, Func<TItem, GroupingEnumerable<TZip_GroupByKey, TZip_GroupByElement>, TZip_OutItem> resultSelector)
            where TZip_GroupByEnumerable : struct, IStructEnumerable<TZip_GroupByInItem, TZip_GroupByEnumerator>
            where TZip_GroupByEnumerator : struct, IStructEnumerator<TZip_GroupByInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, GroupingEnumerable<TZip_GroupByKey, TZip_GroupByElement>, GroupByDefaultEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByEnumerable, TZip_GroupByEnumerator>, GroupByDefaultEnumerator<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByInItem, TZip_GroupByEnumerable, TZip_GroupByEnumerator>(GroupBySpecificEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByEnumerable, TZip_GroupByEnumerator> second, Func<TItem, GroupingEnumerable<TZip_GroupByKey, TZip_GroupByElement>, TZip_OutItem> resultSelector)
            where TZip_GroupByEnumerable : struct, IStructEnumerable<TZip_GroupByInItem, TZip_GroupByEnumerator>
            where TZip_GroupByEnumerator : struct, IStructEnumerator<TZip_GroupByInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, GroupingEnumerable<TZip_GroupByKey, TZip_GroupByElement>, GroupBySpecificEnumerable<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByEnumerable, TZip_GroupByEnumerator>, GroupBySpecificEnumerator<TZip_GroupByInItem, TZip_GroupByKey, TZip_GroupByElement, TZip_GroupByEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_LookupKey, TZip_LookupElement>(LookupDefaultEnumerable<TZip_LookupKey, TZip_LookupElement> second, Func<TItem, GroupingEnumerable<TZip_LookupKey, TZip_LookupElement>, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, GroupingEnumerable<TZip_LookupKey, TZip_LookupElement>, LookupDefaultEnumerable<TZip_LookupKey, TZip_LookupElement>, LookupDefaultEnumerator<TZip_LookupKey, TZip_LookupElement>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_LookupKey, TZip_LookupElement>(LookupSpecificEnumerable<TZip_LookupKey, TZip_LookupElement> second, Func<TItem, GroupingEnumerable<TZip_LookupKey, TZip_LookupElement>, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, GroupingEnumerable<TZip_LookupKey, TZip_LookupElement>, LookupSpecificEnumerable<TZip_LookupKey, TZip_LookupElement>, LookupSpecificEnumerator<TZip_LookupKey, TZip_LookupElement>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(OneItemDefaultEnumerable<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_InnerItem, OneItemDefaultEnumerable<TZip_InnerItem>, OneItemDefaultEnumerator<TZip_InnerItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(OneItemSpecificEnumerable<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_InnerItem, OneItemSpecificEnumerable<TZip_InnerItem>, OneItemSpecificEnumerator<TZip_InnerItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_RangeItem>(RangeEnumerable<TZip_RangeItem> second, Func<TItem, TZip_RangeItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_RangeItem, RangeEnumerable<TZip_RangeItem>, RangeEnumerator<TZip_RangeItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_RangeItem>(RepeatEnumerable<TZip_RangeItem> second, Func<TItem, TZip_RangeItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_RangeItem, RepeatEnumerable<TZip_RangeItem>, RepeatEnumerator<TZip_RangeItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_EmptyItem>(EmptyEnumerable<TZip_EmptyItem> second, Func<TItem, TZip_EmptyItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_EmptyItem, EmptyEnumerable<TZip_EmptyItem>, EmptyEnumerator<TZip_EmptyItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator>(TakeEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator> second, Func<TItem, TZip_TakeOutItem, TZip_OutItem> resultSelector)
            where TZip_TakeInnerEnumerable : struct, IStructEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerator>
            where TZip_TakeInnerEnumerator : struct, IStructEnumerator<TZip_TakeOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_TakeOutItem, TakeEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator>, TakeEnumerator<TZip_TakeOutItem, TZip_TakeInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator>(SkipWhileIndexedEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator> second, Func<TItem, TZip_SkipOutItem, TZip_OutItem> resultSelector)
            where TZip_SkipInnerEnumerable : struct, IStructEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerator>
            where TZip_SkipInnerEnumerator : struct, IStructEnumerator<TZip_SkipOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SkipOutItem, SkipWhileIndexedEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator>, SkipWhileIndexedEnumerator<TZip_SkipOutItem, TZip_SkipInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator>(SkipWhileEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator> second, Func<TItem, TZip_SkipOutItem, TZip_OutItem> resultSelector)
            where TZip_SkipInnerEnumerable : struct, IStructEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerator>
            where TZip_SkipInnerEnumerator : struct, IStructEnumerator<TZip_SkipOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SkipOutItem, SkipWhileEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator>, SkipWhileEnumerator<TZip_SkipOutItem, TZip_SkipInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_DefaultItem, TZip_DefaultInnerEnumerable, TZip_DefaultInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TZip_DefaultItem, TZip_DefaultInnerEnumerable, TZip_DefaultInnerEnumerator> second, Func<TItem, TZip_DefaultItem, TZip_OutItem> resultSelector)
            where TZip_DefaultInnerEnumerable : struct, IStructEnumerable<TZip_DefaultItem, TZip_DefaultInnerEnumerator>
            where TZip_DefaultInnerEnumerator : struct, IStructEnumerator<TZip_DefaultItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_DefaultItem, DefaultIfEmptySpecificEnumerable<TZip_DefaultItem, TZip_DefaultInnerEnumerable, TZip_DefaultInnerEnumerator>, DefaultIfEmptySpecificEnumerator<TZip_DefaultItem, TZip_DefaultInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator>(SkipEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator> second, Func<TItem, TZip_SkipOutItem, TZip_OutItem> resultSelector)
            where TZip_SkipInnerEnumerable : struct, IStructEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerator>
            where TZip_SkipInnerEnumerator : struct, IStructEnumerator<TZip_SkipOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SkipOutItem, SkipEnumerable<TZip_SkipOutItem, TZip_SkipInnerEnumerable, TZip_SkipInnerEnumerator>, SkipEnumerator<TZip_SkipOutItem, TZip_SkipInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator>(TakeWhileEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator> second, Func<TItem, TZip_TakeOutItem, TZip_OutItem> resultSelector)
            where TZip_TakeInnerEnumerable : struct, IStructEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerator>
            where TZip_TakeInnerEnumerator : struct, IStructEnumerator<TZip_TakeOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_TakeOutItem, TakeWhileEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator>, TakeWhileEnumerator<TZip_TakeOutItem, TZip_TakeInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator>(WhereEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator> second, Func<TItem, TZip_WhereOutItem, TZip_OutItem> resultSelector)
            where TZip_WhereInnerEnumerable : struct, IStructEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerator>
            where TZip_WhereInnerEnumerator : struct, IStructEnumerator<TZip_WhereOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_WhereOutItem, WhereEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator>, WhereEnumerator<TZip_WhereOutItem, TZip_WhereInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator>(WhereIndexedEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator> second, Func<TItem, TZip_WhereOutItem, TZip_OutItem> resultSelector)
            where TZip_WhereInnerEnumerable : struct, IStructEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerator>
            where TZip_WhereInnerEnumerator : struct, IStructEnumerator<TZip_WhereOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_WhereOutItem, WhereIndexedEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator>, WhereIndexedEnumerator<TZip_WhereOutItem, TZip_WhereInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator>(TakeWhileIndexedEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator> second, Func<TItem, TZip_TakeOutItem, TZip_OutItem> resultSelector)
            where TZip_TakeInnerEnumerable : struct, IStructEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerator>
            where TZip_TakeInnerEnumerator : struct, IStructEnumerator<TZip_TakeOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_TakeOutItem, TakeWhileIndexedEnumerable<TZip_TakeOutItem, TZip_TakeInnerEnumerable, TZip_TakeInnerEnumerator>, TakeWhileIndexedEnumerator<TZip_TakeOutItem, TZip_TakeInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_DefaultItem, TZip_DefaultInnerEnumerable, TZip_DefaultInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TZip_DefaultItem, TZip_DefaultInnerEnumerable, TZip_DefaultInnerEnumerator> second, Func<TItem, TZip_DefaultItem, TZip_OutItem> resultSelector)
            where TZip_DefaultInnerEnumerable : struct, IStructEnumerable<TZip_DefaultItem, TZip_DefaultInnerEnumerator>
            where TZip_DefaultInnerEnumerator : struct, IStructEnumerator<TZip_DefaultItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_DefaultItem, DefaultIfEmptyDefaultEnumerable<TZip_DefaultItem, TZip_DefaultInnerEnumerable, TZip_DefaultInnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TZip_DefaultItem, TZip_DefaultInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator>(SelectEnumerable<TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator> second, Func<TItem, TZip_SelectOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectInnerEnumerable : struct, IStructEnumerable<TZip_SelectInItem, TZip_SelectInnerEnumerator>
            where TZip_SelectInnerEnumerator : struct, IStructEnumerator<TZip_SelectInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectOutItem, SelectEnumerable<TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator>, SelectEnumerator<TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator>(SelectIndexedEnumerable<TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator> second, Func<TItem, TZip_SelectOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectInnerEnumerable : struct, IStructEnumerable<TZip_SelectInItem, TZip_SelectInnerEnumerator>
            where TZip_SelectInnerEnumerator : struct, IStructEnumerator<TZip_SelectInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectOutItem, SelectIndexedEnumerable<TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator>, SelectIndexedEnumerator<TZip_SelectInItem, TZip_SelectOutItem, TZip_SelectInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_OfTypeInItem, TZip_OfTypeOutItem, TZip_OfTypeInnerEnumerable, TZip_OfTypeInnerEnumerator>(OfTypeEnumerable<TZip_OfTypeInItem, TZip_OfTypeOutItem, TZip_OfTypeInnerEnumerable, TZip_OfTypeInnerEnumerator> second, Func<TItem, TZip_OfTypeOutItem, TZip_OutItem> resultSelector)
            where TZip_OfTypeInnerEnumerable : struct, IStructEnumerable<TZip_OfTypeInItem, TZip_OfTypeInnerEnumerator>
            where TZip_OfTypeInnerEnumerator : struct, IStructEnumerator<TZip_OfTypeInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_OfTypeOutItem, OfTypeEnumerable<TZip_OfTypeInItem, TZip_OfTypeOutItem, TZip_OfTypeInnerEnumerable, TZip_OfTypeInnerEnumerator>, OfTypeEnumerator<TZip_OfTypeInItem, TZip_OfTypeOutItem, TZip_OfTypeInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_CastInItem, TZip_CastOutItem, TZip_CastInnerEnumerable, TZip_CastInnerEnumerator>(CastEnumerable<TZip_CastInItem, TZip_CastOutItem, TZip_CastInnerEnumerable, TZip_CastInnerEnumerator> second, Func<TItem, TZip_CastOutItem, TZip_OutItem> resultSelector)
            where TZip_CastInnerEnumerable : struct, IStructEnumerable<TZip_CastInItem, TZip_CastInnerEnumerator>
            where TZip_CastInnerEnumerator : struct, IStructEnumerator<TZip_CastInItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_CastOutItem, CastEnumerable<TZip_CastInItem, TZip_CastOutItem, TZip_CastInnerEnumerable, TZip_CastInnerEnumerator>, CastEnumerator<TZip_CastInItem, TZip_CastOutItem, TZip_CastInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_ConcatItem, TZip_ConcatFirstEnumerable, TZip_ConcatFirstEnumerator, TZip_ConcatSecondEnumerable, TZip_ConcatSecondEnumerator>(ConcatEnumerable<TZip_ConcatItem, TZip_ConcatFirstEnumerable, TZip_ConcatFirstEnumerator, TZip_ConcatSecondEnumerable, TZip_ConcatSecondEnumerator> second, Func<TItem, TZip_ConcatItem, TZip_OutItem> resultSelector)
            where TZip_ConcatFirstEnumerable : struct, IStructEnumerable<TZip_ConcatItem, TZip_ConcatFirstEnumerator>
            where TZip_ConcatFirstEnumerator : struct, IStructEnumerator<TZip_ConcatItem>
            where TZip_ConcatSecondEnumerable : struct, IStructEnumerable<TZip_ConcatItem, TZip_ConcatSecondEnumerator>
            where TZip_ConcatSecondEnumerator : struct, IStructEnumerator<TZip_ConcatItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_ConcatItem, ConcatEnumerable<TZip_ConcatItem, TZip_ConcatFirstEnumerable, TZip_ConcatFirstEnumerator, TZip_ConcatSecondEnumerable, TZip_ConcatSecondEnumerator>, ConcatEnumerator<TZip_ConcatItem, TZip_ConcatFirstEnumerator, TZip_ConcatSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerable : struct, IStructEnumerable<TZip_SelectManyOutItem, TZip_SelectManyProjectedEnumerator>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyIndexedEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>(SelectManyEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerable : struct, IStructEnumerable<TZip_SelectManyOutItem, TZip_SelectManyProjectedEnumerator>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>, SelectManyEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_ZipOutItem, TZip_ZipFirstItem, TZip_ZipSecondItem, TZip_ZipFirstEnumerable, TZip_ZipFirstEnumerator, TZip_ZipSecondEnumerable, TZip_ZipSecondEnumerator>(ZipEnumerable<TZip_ZipOutItem, TZip_ZipFirstItem, TZip_ZipSecondItem, TZip_ZipFirstEnumerable, TZip_ZipFirstEnumerator, TZip_ZipSecondEnumerable, TZip_ZipSecondEnumerator> second, Func<TItem, TZip_ZipOutItem, TZip_OutItem> resultSelector)
            where TZip_ZipFirstEnumerable : struct, IStructEnumerable<TZip_ZipFirstItem, TZip_ZipFirstEnumerator>
            where TZip_ZipFirstEnumerator : struct, IStructEnumerator<TZip_ZipFirstItem>
            where TZip_ZipSecondEnumerable : struct, IStructEnumerable<TZip_ZipSecondItem, TZip_ZipSecondEnumerator>
            where TZip_ZipSecondEnumerator : struct, IStructEnumerator<TZip_ZipSecondItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_ZipOutItem, ZipEnumerable<TZip_ZipOutItem, TZip_ZipFirstItem, TZip_ZipSecondItem, TZip_ZipFirstEnumerable, TZip_ZipFirstEnumerator, TZip_ZipSecondEnumerable, TZip_ZipSecondEnumerator>, ZipEnumerator<TZip_ZipOutItem, TZip_ZipFirstItem, TZip_ZipSecondItem, TZip_ZipFirstEnumerator, TZip_ZipSecondEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerable : struct, IStructEnumerable<TZip_SelectManyCollectionItem, TZip_SelectManyProjectedEnumerator>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyCollectionItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyCollectionEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>, SelectManyCollectionEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerable : struct, IStructEnumerable<TZip_SelectManyCollectionItem, TZip_SelectManyProjectedEnumerator>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyCollectionItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyCollectionIndexedEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerable, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(IEnumerable<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, IEnumerable<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem, TZip_DictionaryValue>(Dictionary<TZip_InnerItem, TZip_DictionaryValue>.KeyCollection second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, Dictionary<TZip_InnerItem, TZip_DictionaryValue>.KeyCollection>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem, TZip_DictionaryKey>(Dictionary<TZip_DictionaryKey, TZip_InnerItem>.ValueCollection second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, Dictionary<TZip_DictionaryKey, TZip_InnerItem>.ValueCollection>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(HashSet<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, HashSet<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(LinkedList<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, LinkedList<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(List<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, List<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(Queue<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, Queue<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem, TZip_DictionaryValue>(SortedDictionary<TZip_InnerItem, TZip_DictionaryValue>.KeyCollection second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, SortedDictionary<TZip_InnerItem, TZip_DictionaryValue>.KeyCollection>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem, TZip_DictionaryKey>(SortedDictionary<TZip_DictionaryKey, TZip_InnerItem>.ValueCollection second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, SortedDictionary<TZip_DictionaryKey, TZip_InnerItem>.ValueCollection>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(SortedSet<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, SortedSet<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(Stack<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, Stack<TZip_InnerItem>>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyBridgeType : class
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyOutItem>
            where TZip_SelectManyBridger : struct, IStructBridger<TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyProjectedEnumerator>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>, SelectManyBridgeEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyBridgeType : class
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyOutItem>
            where TZip_SelectManyBridger : struct, IStructBridger<TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyProjectedEnumerator>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyIndexedBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyBridgeType : class
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyCollectionItem>
            where TZip_SelectManyBridger : struct, IStructBridger<TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyProjectedEnumerator>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyCollectionBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator> second, Func<TItem, TZip_SelectManyOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectManyBridgeType : class
            where TZip_SelectManyInnerEnumerable : struct, IStructEnumerable<TZip_SelectManyInItem, TZip_SelectManyInnerEnumerator>
            where TZip_SelectManyInnerEnumerator : struct, IStructEnumerator<TZip_SelectManyInItem>
            where TZip_SelectManyProjectedEnumerator : struct, IStructEnumerator<TZip_SelectManyCollectionItem>
            where TZip_SelectManyBridger : struct, IStructBridger<TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyProjectedEnumerator>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectManyOutItem, SelectManyCollectionIndexedBridgeEnumerable<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerable, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TZip_SelectManyInItem, TZip_SelectManyOutItem, TZip_SelectManyCollectionItem, TZip_SelectManyBridgeType, TZip_SelectManyBridger, TZip_SelectManyInnerEnumerator, TZip_SelectManyProjectedEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_IdentityItem, TZip_IdentityBridgeType, TZip_IdentityBridger, TZip_IdentityEnumerator>(IdentityEnumerable<TZip_IdentityItem, TZip_IdentityBridgeType, TZip_IdentityBridger, TZip_IdentityEnumerator> second, Func<TItem, TZip_IdentityItem, TZip_OutItem> resultSelector)
            where TZip_IdentityBridgeType : class
            where TZip_IdentityEnumerator : struct, IStructEnumerator<TZip_IdentityItem>
            where TZip_IdentityBridger : struct, IStructBridger<TZip_IdentityItem, TZip_IdentityBridgeType, TZip_IdentityEnumerator>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_IdentityItem, IdentityEnumerable<TZip_IdentityItem, TZip_IdentityBridgeType, TZip_IdentityBridger, TZip_IdentityEnumerator>, TZip_IdentityEnumerator>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(TZip_InnerItem[] second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZipBridge_Impl<TItem, TZip_OutItem, TZip_InnerItem, TZip_InnerItem[]>(ref this, second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(BoxedEnumerable<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_InnerItem, BoxedEnumerable<TZip_InnerItem>, BoxedEnumerator<TZip_InnerItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator, TZip_SelectProjection>(SelectSelectEnumerable<TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator, TZip_SelectProjection> second, Func<TItem, TZip_SelectOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectInnerEnumerable : struct, IStructEnumerable<TZip_SelectInnerItem, TZip_SelectInnerEnumerator>
            where TZip_SelectInnerEnumerator : struct, IStructEnumerator<TZip_SelectInnerItem>
            where TZip_SelectProjection : struct, IStructProjection<TZip_SelectOutItem, TZip_SelectInnerItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectOutItem, SelectSelectEnumerable<TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator, TZip_SelectProjection>, SelectSelectEnumerator<TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerator, TZip_SelectProjection>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator, TZip_SelectProjection, TZip_SelectPredicate>(SelectWhereEnumerable<TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator, TZip_SelectProjection, TZip_SelectPredicate> second, Func<TItem, TZip_SelectOutItem, TZip_OutItem> resultSelector)
            where TZip_SelectInnerEnumerable : struct, IStructEnumerable<TZip_SelectInnerItem, TZip_SelectInnerEnumerator>
            where TZip_SelectInnerEnumerator : struct, IStructEnumerator<TZip_SelectInnerItem>
            where TZip_SelectProjection : struct, IStructProjection<TZip_SelectOutItem, TZip_SelectInnerItem>
            where TZip_SelectPredicate : struct, IStructPredicate<TZip_SelectOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_SelectOutItem, SelectWhereEnumerable<TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerable, TZip_SelectInnerEnumerator, TZip_SelectProjection, TZip_SelectPredicate>, SelectWhereEnumerator<TZip_SelectOutItem, TZip_SelectInnerItem, TZip_SelectInnerEnumerator, TZip_SelectProjection, TZip_SelectPredicate>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator, TZip_WherePredicate>(WhereWhereEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator, TZip_WherePredicate> second, Func<TItem, TZip_WhereOutItem, TZip_OutItem> resultSelector)
            where TZip_WhereInnerEnumerable : struct, IStructEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerator>
            where TZip_WhereInnerEnumerator : struct, IStructEnumerator<TZip_WhereOutItem>
            where TZip_WherePredicate : struct, IStructPredicate<TZip_WhereOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_WhereOutItem, WhereWhereEnumerable<TZip_WhereOutItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator, TZip_WherePredicate>, WhereWhereEnumerator<TZip_WhereOutItem, TZip_WhereInnerEnumerator, TZip_WherePredicate>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_WhereOutItem, TZip_WhereInnerItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator, TZip_WherePredicate, TZip_WhereProjection>(WhereSelectEnumerable<TZip_WhereOutItem, TZip_WhereInnerItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator, TZip_WherePredicate, TZip_WhereProjection> second, Func<TItem, TZip_WhereOutItem, TZip_OutItem> resultSelector)
            where TZip_WhereInnerEnumerable : struct, IStructEnumerable<TZip_WhereInnerItem, TZip_WhereInnerEnumerator>
            where TZip_WhereInnerEnumerator : struct, IStructEnumerator<TZip_WhereInnerItem>
            where TZip_WherePredicate : struct, IStructPredicate<TZip_WhereInnerItem>
            where TZip_WhereProjection : struct, IStructProjection<TZip_WhereOutItem, TZip_WhereInnerItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_WhereOutItem, WhereSelectEnumerable<TZip_WhereOutItem, TZip_WhereInnerItem, TZip_WhereInnerEnumerable, TZip_WhereInnerEnumerator, TZip_WherePredicate, TZip_WhereProjection>, WhereSelectEnumerator<TZip_WhereOutItem, TZip_WhereInnerItem, TZip_WhereInnerEnumerator, TZip_WherePredicate, TZip_WhereProjection>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_DistinctOutItem, TZip_DistinctInnerEnumerable, TZip_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TZip_DistinctOutItem, TZip_DistinctInnerEnumerable, TZip_DistinctInnerEnumerator> second, Func<TItem, TZip_DistinctOutItem, TZip_OutItem> resultSelector)
            where TZip_DistinctInnerEnumerable : struct, IStructEnumerable<TZip_DistinctOutItem, TZip_DistinctInnerEnumerator>
            where TZip_DistinctInnerEnumerator : struct, IStructEnumerator<TZip_DistinctOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_DistinctOutItem, DistinctDefaultEnumerable<TZip_DistinctOutItem, TZip_DistinctInnerEnumerable, TZip_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TZip_DistinctOutItem, TZip_DistinctInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_OutItem, TZip_DistinctOutItem, TZip_DistinctInnerEnumerable, TZip_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TZip_DistinctOutItem, TZip_DistinctInnerEnumerable, TZip_DistinctInnerEnumerator> second, Func<TItem, TZip_DistinctOutItem, TZip_OutItem> resultSelector)
            where TZip_DistinctInnerEnumerable : struct, IStructEnumerable<TZip_DistinctOutItem, TZip_DistinctInnerEnumerator>
            where TZip_DistinctInnerEnumerator : struct, IStructEnumerator<TZip_DistinctOutItem>
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_DistinctOutItem, DistinctSpecificEnumerable<TZip_DistinctOutItem, TZip_DistinctInnerEnumerable, TZip_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TZip_DistinctOutItem, TZip_DistinctInnerEnumerator>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(OneItemDefaultOrderedEnumerable<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_InnerItem, OneItemDefaultOrderedEnumerable<TZip_InnerItem>, OneItemDefaultOrderedEnumerator<TZip_InnerItem>>(ref this, ref second, resultSelector);

        public EmptyEnumerable<TZip_OutItem> Zip<TZip_InnerItem, TZip_OutItem>(OneItemSpecificOrderedEnumerable<TZip_InnerItem> second, Func<TItem, TZip_InnerItem, TZip_OutItem> resultSelector)
        => CommonImplementation.EmptyZip_Impl<TItem, TZip_OutItem, TZip_InnerItem, OneItemSpecificOrderedEnumerable<TZip_InnerItem>, OneItemSpecificOrderedEnumerator<TZip_InnerItem>>(ref this, ref second, resultSelector);
    }
}