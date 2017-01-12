using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct EmptyEnumerable<TItem>
    {
        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, OneItemDefaultOrderedEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, OneItemSpecificOrderedEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, OneItemDefaultOrderedEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, OneItemSpecificOrderedEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, OneItemDefaultOrderedEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, OneItemSpecificOrderedEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, OneItemDefaultOrderedEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, OneItemSpecificOrderedEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, UnionSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerator>
            where TSelectMany_SelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerator>
            where TSelectMany_SelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, UnionDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> selector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> selector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>> SelectMany<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupByDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>>(ref this, selector);

        public EmptyEnumerable<GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>> SelectMany<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupBySpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerator>
            where TSelectMany_SelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> selector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerator>
            where TSelectMany_SelectMany_ExceptFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>> SelectMany<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupByDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>>(ref this, selector);

        public EmptyEnumerable<GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>> SelectMany<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupBySpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByInnerEnumerable, TSelectMany_SelectMany_OrderByInnerEnumerator, TSelectMany_SelectMany_OrderByComparer>(Func<TItem, OrderByEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByInnerEnumerable, TSelectMany_SelectMany_OrderByInnerEnumerator, TSelectMany_SelectMany_OrderByComparer>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_OrderByInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByInnerEnumerator>
            where TSelectMany_SelectMany_OrderByInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_OrderByComparer : struct, IStructComparer<TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByKey>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByInnerEnumerable, TSelectMany_SelectMany_OrderByInnerEnumerator, TSelectMany_SelectMany_OrderByComparer>(Func<TItem, int, OrderByEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByInnerEnumerable, TSelectMany_SelectMany_OrderByInnerEnumerator, TSelectMany_SelectMany_OrderByComparer>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_OrderByInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByInnerEnumerator>
            where TSelectMany_SelectMany_OrderByInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_OrderByComparer : struct, IStructComparer<TSelectMany_CollectionItem, TSelectMany_SelectMany_OrderByKey>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_OutItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionSpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_OutItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_OutItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionSpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_OutItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> selector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupBySpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, UnionSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupByDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupBySpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptionFirstEnumerator>
            where TSelectMany_SelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, ExceptSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptionFirstEnumerator>
            where TSelectMany_SelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, IntersectSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptionFirstEnumerator>
            where TSelectMany_SelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>(Func<TItem, int, ExceptSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptFirstEnumerable, TSelectMany_SelectMany_ExceptionFirstEnumerator, TSelectMany_SelectMany_ExceptSecondEnumerable, TSelectMany_SelectMany_ExceptSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_ExceptFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptionFirstEnumerator>
            where TSelectMany_SelectMany_ExceptionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_ExceptSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ExceptSecondEnumerator>
            where TSelectMany_SelectMany_ExceptSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, int, UnionSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupByDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement>, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>(Func<TItem, int, IntersectSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerable, TSelectMany_SelectMany_IntersectFirstEnumerator, TSelectMany_SelectMany_IntersectSecondEnumerable, TSelectMany_SelectMany_IntersectSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_IntersectFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectFirstEnumerator>
            where TSelectMany_SelectMany_IntersectFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_IntersectSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_IntersectSecondEnumerator>
            where TSelectMany_SelectMany_IntersectSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>(Func<TItem, UnionDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerable, TSelectMany_SelectMany_UnionFirstEnumerator, TSelectMany_SelectMany_UnionSecondEnumerable, TSelectMany_SelectMany_UnionSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_UnionFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionFirstEnumerator>
            where TSelectMany_SelectMany_UnionFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_UnionSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_UnionSecondEnumerator>
            where TSelectMany_SelectMany_UnionSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, GroupByCollectionSpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionDefaultEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>(Func<TItem, int, GroupByCollectionSpecificEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByKey, TSelectMany_SelectMany_GroupByElement, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupByEnumerable, TSelectMany_SelectMany_GroupByEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupByEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupByInItem, TSelectMany_SelectMany_GroupByEnumerator>
            where TSelectMany_SelectMany_GroupByEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupByInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, int, JoinDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, int, JoinSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, JoinDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, JoinSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_GroupJoinKey, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> selector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, int, JoinDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, int, JoinSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, JoinDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, int, GroupJoinDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>(Func<TItem, JoinSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_JoinKeyItem, TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerable, TSelectMany_SelectMany_JoinLeftEnumerator, TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerable, TSelectMany_SelectMany_JoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_JoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinLeftItem, TSelectMany_SelectMany_JoinLeftEnumerator>
            where TSelectMany_SelectMany_JoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinLeftItem>
            where TSelectMany_SelectMany_JoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_JoinRightItem, TSelectMany_SelectMany_JoinRightEnumerator>
            where TSelectMany_SelectMany_JoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_JoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>(Func<TItem, GroupJoinDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupJoinKeyItem, TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerable, TSelectMany_SelectMany_GroupJoinLeftEnumerator, TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerable, TSelectMany_SelectMany_GroupJoinRightEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_GroupJoinLeftEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinLeftItem, TSelectMany_SelectMany_GroupJoinLeftEnumerator>
            where TSelectMany_SelectMany_GroupJoinLeftEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinLeftItem>
            where TSelectMany_SelectMany_GroupJoinRightEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_GroupJoinRightItem, TSelectMany_SelectMany_GroupJoinRightEnumerator>
            where TSelectMany_SelectMany_GroupJoinRightEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_GroupJoinRightItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, OneItemDefaultEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, OneItemSpecificEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, OneItemDefaultEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, OneItemSpecificEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, OneItemDefaultEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, OneItemSpecificEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, OneItemDefaultEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, OneItemSpecificEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);
        
        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, ReverseRangeEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, ReverseRangeEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupingKey>(Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupingKey, TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupedKey>(Func<TItem, int, GroupedEnumerable<TSelectMany_SelectMany_GroupedKey, TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupingKey>(Func<TItem, int, GroupingEnumerable<TSelectMany_SelectMany_GroupingKey, TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<GroupingEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>> SelectMany<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>(Func<TItem, int, LookupEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>> selector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return EmptyCache<GroupingEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>>.Empty;
        }

        public EmptyEnumerable<GroupingEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>> SelectMany<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>(Func<TItem, LookupEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>> selector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return EmptyCache<GroupingEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>>.Empty;
        }

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_GroupedKey>(Func<TItem, GroupedEnumerable<TSelectMany_SelectMany_GroupedKey, TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, ReverseRangeEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, ReverseRangeEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
       => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>(Func<TItem, int, ReverseEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>> selector)
            where TSelectMany_SelectMany_ReverseEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ReverseEnumerator>
            where TSelectMany_SelectMany_ReverseEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>(Func<TItem, ReverseEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>> selector)
            where TSelectMany_SelectMany_ReverseEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_ReverseEnumerator>
            where TSelectMany_SelectMany_ReverseEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupedKey>(Func<TItem, int, GroupingEnumerable<TSelectMany_SelectMany_GroupedKey, TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupedKey>(Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_GroupedKey, TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>(Func<TItem, int, LookupEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupedKey>(Func<TItem, GroupedEnumerable<TSelectMany_SelectMany_GroupedKey, TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>(Func<TItem, LookupEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>> collectionSelector, Func<TItem, GroupingEnumerable<TSelectMany_SelectMany_LookupKey, TSelectMany_SelectMany_LookupElement>, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_GroupedKey>(Func<TItem, int, GroupedEnumerable<TSelectMany_SelectMany_GroupedKey, TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByEnumerable, TSelectMany_SelectMany_OrderByEnumerator, TSelectMany_SelectMany_OrderByComparer>(Func<TItem, int, OrderByEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByEnumerable, TSelectMany_SelectMany_OrderByEnumerator, TSelectMany_SelectMany_OrderByComparer>> selector)
            where TSelectMany_SelectMany_OrderByEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByEnumerator>
            where TSelectMany_SelectMany_OrderByEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_OrderByComparer : struct, IStructComparer<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByKey>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByEnumerable, TSelectMany_SelectMany_OrderByEnumerator, TSelectMany_SelectMany_OrderByComparer>(Func<TItem, OrderByEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByKey, TSelectMany_SelectMany_OrderByEnumerable, TSelectMany_SelectMany_OrderByEnumerator, TSelectMany_SelectMany_OrderByComparer>> selector)
            where TSelectMany_SelectMany_OrderByEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByEnumerator>
            where TSelectMany_SelectMany_OrderByEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_OrderByComparer : struct, IStructComparer<TSelectMany_OutItem, TSelectMany_SelectMany_OrderByKey>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>(Func<TItem, int, ReverseEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_ReverseEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ReverseEnumerator>
            where TSelectMany_SelectMany_ReverseEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>(Func<TItem, ReverseEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ReverseEnumerable, TSelectMany_SelectMany_ReverseEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_ReverseEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_ReverseEnumerator>
            where TSelectMany_SelectMany_ReverseEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);
        
        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, EmptyEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, EmptyEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, IEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, RepeatEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, RangeEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, RangeEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, RepeatEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, IEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, TakeWhileEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, TakeWhileIndexedEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, TakeEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, SkipEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, SkipWhileIndexedEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, SkipWhileEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptySpecificEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, TakeEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, SkipEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, SkipWhileIndexedEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, SkipWhileEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, TakeWhileIndexedEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, TakeWhileEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptySpecificEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptyDefaultEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptyDefaultEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> selector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, int, WhereIndexedEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, WhereIndexedEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, int, WhereEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, WhereEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> selector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, int, SelectIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>(Func<TItem, int, CastEnumerable<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>> selector)
            where TSelectMany_CastInnerEnumerable : struct, IStructEnumerable<TSelectMany_CastInItem, TSelectMany_CastInnerEnumerator>
            where TSelectMany_CastInnerEnumerator : struct, IStructEnumerator<TSelectMany_CastInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, int, SelectEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>(Func<TItem, int, OfTypeEnumerable<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>> selector)
            where TSelectMany_CastInnerEnumerable : struct, IStructEnumerable<TSelectMany_CastInItem, TSelectMany_CastInnerEnumerator>
            where TSelectMany_CastInnerEnumerator : struct, IStructEnumerator<TSelectMany_CastInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>(Func<TItem, OfTypeEnumerable<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>> selector)
            where TSelectMany_CastInnerEnumerable : struct, IStructEnumerable<TSelectMany_CastInItem, TSelectMany_CastInnerEnumerator>
            where TSelectMany_CastInnerEnumerator : struct, IStructEnumerator<TSelectMany_CastInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>(Func<TItem, CastEnumerable<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>> selector)
            where TSelectMany_CastInnerEnumerable : struct, IStructEnumerable<TSelectMany_CastInItem, TSelectMany_CastInnerEnumerator>
            where TSelectMany_CastInnerEnumerator : struct, IStructEnumerator<TSelectMany_CastInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, SelectIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, SelectEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> selector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>(Func<TItem, int, ConcatEnumerable<TSelectMany_OutItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>> selector)
            where TSelectMany_ConcatFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_ConcatFirstEnumerator>
            where TSelectMany_ConcatFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_ConcatSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_ConcatSecondEnumerator>
            where TSelectMany_ConcatSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>(Func<TItem, ConcatEnumerable<TSelectMany_OutItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>> selector)
            where TSelectMany_ConcatFirstEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_ConcatFirstEnumerator>
            where TSelectMany_ConcatFirstEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_ConcatSecondEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_ConcatSecondEnumerator>
            where TSelectMany_ConcatSecondEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_OutItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>(Func<TItem, int, ZipEnumerable<TSelectMany_OutItem, TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>> selector)
            where TSelectMany_ZipFirstEnumerable : struct, IStructEnumerable<TSelectMany_ZipFirstItem, TSelectMany_ZipFirstEnumerator>
            where TSelectMany_ZipFirstEnumerator : struct, IStructEnumerator<TSelectMany_ZipFirstItem>
            where TSelectMany_ZipSecondEnumerable : struct, IStructEnumerable<TSelectMany_ZipSecondItem, TSelectMany_ZipSecondEnumerator>
            where TSelectMany_ZipSecondEnumerator : struct, IStructEnumerator<TSelectMany_ZipSecondItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_OutItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>(Func<TItem, ZipEnumerable<TSelectMany_OutItem, TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>> selector)
            where TSelectMany_ZipFirstEnumerable : struct, IStructEnumerable<TSelectMany_ZipFirstItem, TSelectMany_ZipFirstEnumerator>
            where TSelectMany_ZipFirstEnumerator : struct, IStructEnumerator<TSelectMany_ZipFirstItem>
            where TSelectMany_ZipSecondEnumerable : struct, IStructEnumerable<TSelectMany_ZipSecondItem, TSelectMany_ZipSecondEnumerator>
            where TSelectMany_ZipSecondEnumerator : struct, IStructEnumerator<TSelectMany_ZipSecondItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        //

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, IEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>(Func<TItem, CastEnumerable<TSelectMany_CastInItem, TSelectMany_CollectionItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_CastInnerEnumerable : struct, IStructEnumerable<TSelectMany_CastInItem, TSelectMany_CastInnerEnumerator>
            where TSelectMany_CastInnerEnumerator : struct, IStructEnumerator<TSelectMany_CastInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>(Func<TItem, ConcatEnumerable<TSelectMany_CollectionItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_ConcatFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_ConcatFirstEnumerator>
            where TSelectMany_ConcatFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_ConcatSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_ConcatSecondEnumerator>
            where TSelectMany_ConcatSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptyDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, DefaultIfEmptySpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, EmptyEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OfTypeInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_OfTypeInnerEnumerable, TSelectMany_OfTypeInnerEnumerator>(Func<TItem, OfTypeEnumerable<TSelectMany_OfTypeInItem, TSelectMany_CollectionItem, TSelectMany_OfTypeInnerEnumerable, TSelectMany_OfTypeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_OfTypeInnerEnumerable : struct, IStructEnumerable<TSelectMany_OfTypeInItem, TSelectMany_OfTypeInnerEnumerator>
            where TSelectMany_OfTypeInnerEnumerator : struct, IStructEnumerator<TSelectMany_OfTypeInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, RangeEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, RepeatEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, SelectEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, SelectIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, SelectManyIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>(Func<TItem, SkipEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SkipInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerator>
            where TSelectMany_SkipInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>(Func<TItem, SkipWhileEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SkipInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerator>
            where TSelectMany_SkipInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>(Func<TItem, SkipWhileIndexedEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SkipInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerator>
            where TSelectMany_SkipInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>(Func<TItem, TakeEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_TakeInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerator>
            where TSelectMany_TakeInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>(Func<TItem, TakeWhileEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_TakeInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerator>
            where TSelectMany_TakeInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>(Func<TItem, TakeWhileIndexedEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_TakeInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerator>
            where TSelectMany_TakeInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, WhereEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, WhereIndexedEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>(Func<TItem, ZipEnumerable<TSelectMany_CollectionItem, TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_ZipFirstEnumerable : struct, IStructEnumerable<TSelectMany_ZipFirstItem, TSelectMany_ZipFirstEnumerator>
            where TSelectMany_ZipFirstEnumerator : struct, IStructEnumerator<TSelectMany_ZipFirstItem>
            where TSelectMany_ZipSecondEnumerable : struct, IStructEnumerable<TSelectMany_ZipSecondItem, TSelectMany_ZipSecondEnumerator>
            where TSelectMany_ZipSecondEnumerator : struct, IStructEnumerator<TSelectMany_ZipSecondItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, IEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CastInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>(Func<TItem, int, CastEnumerable<TSelectMany_CastInItem, TSelectMany_CollectionItem, TSelectMany_CastInnerEnumerable, TSelectMany_CastInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_CastInnerEnumerable : struct, IStructEnumerable<TSelectMany_CastInItem, TSelectMany_CastInnerEnumerator>
            where TSelectMany_CastInnerEnumerator : struct, IStructEnumerator<TSelectMany_CastInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>(Func<TItem, int, ConcatEnumerable<TSelectMany_CollectionItem, TSelectMany_ConcatFirstEnumerable, TSelectMany_ConcatFirstEnumerator, TSelectMany_ConcatSecondEnumerable, TSelectMany_ConcatSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_ConcatFirstEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_ConcatFirstEnumerator>
            where TSelectMany_ConcatFirstEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_ConcatSecondEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_ConcatSecondEnumerator>
            where TSelectMany_ConcatSecondEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptyDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>(Func<TItem, int, DefaultIfEmptySpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerable, TSelectMany_DefaultIfEmptyInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_DefaultIfEmptyInnerEnumerator>
            where TSelectMany_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, EmptyEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OfTypeInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_OfTypeInnerEnumerable, TSelectMany_OfTypeInnerEnumerator>(Func<TItem, int, OfTypeEnumerable<TSelectMany_OfTypeInItem, TSelectMany_CollectionItem, TSelectMany_OfTypeInnerEnumerable, TSelectMany_OfTypeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_OfTypeInnerEnumerable : struct, IStructEnumerable<TSelectMany_OfTypeInItem, TSelectMany_OfTypeInnerEnumerator>
            where TSelectMany_OfTypeInnerEnumerator : struct, IStructEnumerator<TSelectMany_OfTypeInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, RangeEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, RepeatEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, int, SelectEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>(Func<TItem, int, SelectIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectInnerEnumerable, TSelectMany_SelectInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>(Func<TItem, int, SelectManyIndexedEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyInnerProjectedEnumerable, TSelectMany_SelectManyInnerProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyInnerProjectedEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectManyInnerProjectedEnumerator>
            where TSelectMany_SelectManyInnerProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerable, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyProjectedEnumerator>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>(Func<TItem, int, SkipEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SkipInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerator>
            where TSelectMany_SkipInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>(Func<TItem, int, SkipWhileEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SkipInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerator>
            where TSelectMany_SkipInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>(Func<TItem, int, SkipWhileIndexedEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerable, TSelectMany_SkipInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SkipInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SkipInnerEnumerator>
            where TSelectMany_SkipInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>(Func<TItem, int, TakeEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_TakeInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerator>
            where TSelectMany_TakeInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>(Func<TItem, int, TakeWhileEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_TakeInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerator>
            where TSelectMany_TakeInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>(Func<TItem, int, TakeWhileIndexedEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerable, TSelectMany_TakeInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_TakeInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_TakeInnerEnumerator>
            where TSelectMany_TakeInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, int, WhereEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>(Func<TItem, int, WhereIndexedEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerable, TSelectMany_WhereInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_WhereInnerEnumerator>
            where TSelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>(Func<TItem, int, ZipEnumerable<TSelectMany_CollectionItem, TSelectMany_ZipFirstItem, TSelectMany_ZipSecondItem, TSelectMany_ZipFirstEnumerable, TSelectMany_ZipFirstEnumerator, TSelectMany_ZipSecondEnumerable, TSelectMany_ZipSecondEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_ZipFirstEnumerable : struct, IStructEnumerable<TSelectMany_ZipFirstItem, TSelectMany_ZipFirstEnumerator>
            where TSelectMany_ZipFirstEnumerator : struct, IStructEnumerator<TSelectMany_ZipFirstItem>
            where TSelectMany_ZipSecondEnumerable : struct, IStructEnumerable<TSelectMany_ZipSecondItem, TSelectMany_ZipSecondEnumerator>
            where TSelectMany_ZipSecondEnumerator : struct, IStructEnumerator<TSelectMany_ZipSecondItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryValue>(Func<TItem, Dictionary<TSelectMany_OutItem, TSelectMany_DictionaryValue>.KeyCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryKey>(Func<TItem, Dictionary<TSelectMany_DictionaryKey, TSelectMany_OutItem>.ValueCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, HashSet<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, LinkedList<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, List<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, Queue<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryValue>(Func<TItem, SortedDictionary<TSelectMany_OutItem, TSelectMany_DictionaryValue>.KeyCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryKey>(Func<TItem, SortedDictionary<TSelectMany_DictionaryKey, TSelectMany_OutItem>.ValueCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, SortedSet<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, Stack<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyIndexedBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryValue>(Func<TItem, int, Dictionary<TSelectMany_OutItem, TSelectMany_DictionaryValue>.KeyCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryKey>(Func<TItem, int, Dictionary<TSelectMany_DictionaryKey, TSelectMany_OutItem>.ValueCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, HashSet<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, LinkedList<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, List<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, Queue<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryValue>(Func<TItem, int, SortedDictionary<TSelectMany_OutItem, TSelectMany_DictionaryValue>.KeyCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_DictionaryKey>(Func<TItem, int, SortedDictionary<TSelectMany_DictionaryKey, TSelectMany_OutItem>.ValueCollection> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, SortedSet<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, Stack<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyIndexedBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> selector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryValue>(Func<TItem, Dictionary<TSelectMany_CollectionItem, TSelectMany_DictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryKey>(Func<TItem, Dictionary<TSelectMany_DictionaryKey, TSelectMany_CollectionItem>.ValueCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, HashSet<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, LinkedList<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, List<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, Queue<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryValue>(Func<TItem, SortedDictionary<TSelectMany_CollectionItem, TSelectMany_DictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryKey>(Func<TItem, SortedDictionary<TSelectMany_DictionaryKey, TSelectMany_CollectionItem>.ValueCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, SortedSet<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, Stack<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_SelectManyBridgeType, TSelectMany_CollectionItem, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyIndexedBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, SelectManyCollectionIndexedBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryValue>(Func<TItem, int, Dictionary<TSelectMany_CollectionItem, TSelectMany_DictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryKey>(Func<TItem, int, Dictionary<TSelectMany_DictionaryKey, TSelectMany_CollectionItem>.ValueCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, HashSet<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, LinkedList<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, List<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, Queue<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryValue>(Func<TItem, int, SortedDictionary<TSelectMany_CollectionItem, TSelectMany_DictionaryValue>.KeyCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_DictionaryKey>(Func<TItem, int, SortedDictionary<TSelectMany_DictionaryKey, TSelectMany_CollectionItem>.ValueCollection> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, SortedSet<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, Stack<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyIndexedBridgeEnumerable<TSelectMany_SelectInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectManyInItem, TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>(Func<TItem, int, SelectManyCollectionIndexedBridgeEnumerable<TSelectMany_SelectManyInItem, TSelectMany_CollectionItem, TSelectMany_SelectManyCollectionItem, TSelectMany_SelectManyBridgeType, TSelectMany_SelectManyInnerEnumerable, TSelectMany_SelectManyInnerEnumerator, TSelectMany_SelectManyProjectedEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectManyBridgeType : class
            where TSelectMany_SelectManyInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectManyInItem, TSelectMany_SelectManyInnerEnumerator>
            where TSelectMany_SelectManyInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyInItem>
            where TSelectMany_SelectManyProjectedEnumerator : struct, IStructEnumerator<TSelectMany_SelectManyCollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, TSelectMany_OutItem[]> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>(Func<TItem, IdentityEnumerable<TSelectMany_OutItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>> selector)
            where TSelectMany_IdentityBridgeType : class
            where TSelectMany_IdentityEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, TSelectMany_OutItem[]> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>(Func<TItem, int, IdentityEnumerable<TSelectMany_OutItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>> selector)
            where TSelectMany_IdentityBridgeType : class
            where TSelectMany_IdentityEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, TSelectMany_CollectionItem[]> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>(Func<TItem, IdentityEnumerable<TSelectMany_CollectionItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_IdentityBridgeType : class
            where TSelectMany_IdentityEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, TSelectMany_CollectionItem[]> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>(Func<TItem, int, IdentityEnumerable<TSelectMany_CollectionItem, TSelectMany_IdentityBridgeType, TSelectMany_IdentityEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_IdentityBridgeType : class
            where TSelectMany_IdentityEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, BoxedEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem>(Func<TItem, int, BoxedEnumerable<TSelectMany_OutItem>> selector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, BoxedEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_CollectionItem>(Func<TItem, int, BoxedEnumerable<TSelectMany_CollectionItem>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>(Func<TItem, WhereWhereEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>> selector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>(Func<TItem, int, WhereWhereEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>> selector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>(Func<TItem, SelectSelectEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>> selector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>(Func<TItem, int, SelectSelectEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>> selector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>(Func<TItem, WhereWhereEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>(Func<TItem, int, WhereWhereEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>(Func<TItem, SelectWhereEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>> selector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectPredicate : struct, IStructPredicate<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>(Func<TItem, int, SelectWhereEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>> selector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectPredicate : struct, IStructPredicate<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_WhereInnerItem, TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>(Func<TItem, int, WhereSelectEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>> selector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WhereProjection : struct, IStructProjection<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_WhereInnerItem, TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>(Func<TItem, WhereSelectEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>> selector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WhereProjection : struct, IStructProjection<TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>(Func<TItem, SelectSelectEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>(Func<TItem, int, SelectSelectEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>(Func<TItem, SelectWhereEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectPredicate : struct, IStructPredicate<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>(Func<TItem, int, SelectWhereEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerable, TSelectMany_SelectMany_SelectInnerEnumerator, TSelectMany_SelectMany_SelectProjection, TSelectMany_SelectMany_SelectPredicate>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_SelectInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_SelectInnerItem, TSelectMany_SelectMany_SelectInnerEnumerator>
            where TSelectMany_SelectMany_SelectInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectProjection : struct, IStructProjection<TSelectMany_CollectionItem, TSelectMany_SelectMany_SelectInnerItem>
            where TSelectMany_SelectMany_SelectPredicate : struct, IStructPredicate<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>(Func<TItem, int, WhereSelectEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WhereProjection : struct, IStructProjection<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>(Func<TItem, WhereSelectEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerable, TSelectMany_SelectMany_WhereInnerEnumerator, TSelectMany_SelectMany_WherePredicate, TSelectMany_SelectMany_WhereProjection>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_WhereInnerEnumerable : struct, IStructEnumerable<TSelectMany_SelectMany_WhereInnerItem, TSelectMany_SelectMany_WhereInnerEnumerator>
            where TSelectMany_SelectMany_WhereInnerEnumerator : struct, IStructEnumerator<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WherePredicate : struct, IStructPredicate<TSelectMany_SelectMany_WhereInnerItem>
            where TSelectMany_SelectMany_WhereProjection : struct, IStructProjection<TSelectMany_CollectionItem, TSelectMany_SelectMany_WhereInnerItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctDefaultEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctSpecificEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> selector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_OutItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, selector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctDefaultEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, int, DistinctSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);

        public EmptyEnumerable<TSelectMany_OutItem> SelectMany<TSelectMany_CollectionItem, TSelectMany_OutItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>(Func<TItem, DistinctSpecificEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerable, TSelectMany_SelectMany_DistinctInnerEnumerator>> collectionSelector, Func<TItem, TSelectMany_CollectionItem, TSelectMany_OutItem> resultSelector)
            where TSelectMany_SelectMany_DistinctInnerEnumerable : struct, IStructEnumerable<TSelectMany_CollectionItem, TSelectMany_SelectMany_DistinctInnerEnumerator>
            where TSelectMany_SelectMany_DistinctInnerEnumerator : struct, IStructEnumerator<TSelectMany_CollectionItem>
        => CommonImplementation.EmptySelectMany_Impl<TItem, TSelectMany_OutItem>(ref this, collectionSelector, resultSelector);
    }
}
