﻿using LinqAF.Impl;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    public static partial class LinqAFNew
    {
        #region List<T>(IEnumerable<T>)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static System.Collections.Generic.List<T> List<T>(System.Collections.Generic.IEnumerable<T> collection)
        {
            return new System.Collections.Generic.List<T>(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static System.Collections.Generic.List<TItem> ListImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var ret = new System.Collections.Generic.List<TItem>();

            foreach (var item in e)
            {
                ret.Add(item);
            }

            return ret;
        }

        public static System.Collections.Generic.List<GroupingEnumerable<TKey, TItem>> List<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => ListImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<GroupingEnumerable<TKey, TItem>> List<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => ListImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<GroupingEnumerable<TKey, TItem>> List<TKey, TItem>(LookupDefaultEnumerable<TKey, TItem> second)
        => ListImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(ref second);

        public static System.Collections.Generic.List<GroupingEnumerable<TKey, TItem>> List<TKey, TItem>(LookupSpecificEnumerable<TKey, TItem> second)
        => ListImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(BoxedEnumerable<TItem> second)
        => ListImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second)
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
            where TConcat_IdentityBridgeType : class
        => ListImpl<TItem, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>, TConcat_IdentityEnumerator>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(EmptyEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return new System.Collections.Generic.List<TItem>();
        }

        public static System.Collections.Generic.List<int> List(RangeEnumerable second)
        => ListImpl<int, RangeEnumerable, RangeEnumerator>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(RepeatEnumerable<TItem> second)
        => ListImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => ListImpl<TItem, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => ListImpl<TItem, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectIndexedEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Bridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
            where TConcat_BridgeType : class
        => ListImpl<TItem, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>, SelectManyBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => ListImpl<TItem, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(

            SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => ListImpl<TItem, SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(

            SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => ListImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyEnumerator<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => ListImpl<TItem, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyCollectionEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => ListImpl<TItem, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => ListImpl<TItem, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, CastEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => ListImpl<TItem, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, OfTypeEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        => ListImpl<TItem, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>, ZipEnumerator<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => ListImpl<TItem, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => ListImpl<TItem, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => ListImpl<TItem, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => ListImpl<TItem, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(EmptyOrderedEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return new System.Collections.Generic.List<TItem>();
        }

        public static System.Collections.Generic.List<TItem> List<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => ListImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => ListImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<int> List(ReverseRangeEnumerable second)
        => ListImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => ListImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(

            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => ListImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(

            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => ListImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(

            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => ListImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(

            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => ListImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(

            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => ListImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(

            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => ListImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(OneItemDefaultEnumerable<TItem> second)
        => ListImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(OneItemSpecificEnumerable<TItem> second)
        => ListImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(OneItemDefaultOrderedEnumerable<TItem> second)
        => ListImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem>(OneItemSpecificOrderedEnumerable<TItem> second)
        => ListImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TEnumerable, TEnumerator>(

            AppendEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, AppendEnumerable<TItem, TEnumerable, TEnumerator>, AppendEnumerator<TItem, TEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TEnumerable, TEnumerator>(

            PrependEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, PrependEnumerable<TItem, TEnumerable, TEnumerator>, PrependEnumerator<TItem, TEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TEnumerable, TEnumerator>(

            SkipLastEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, SkipLastEnumerable<TItem, TEnumerable, TEnumerator>, SkipLastEnumerator<TItem, TEnumerator>>(ref second);

        public static System.Collections.Generic.List<TItem> List<TItem, TEnumerable, TEnumerator>(

            TakeLastEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => ListImpl<TItem, TakeLastEnumerable<TItem, TEnumerable, TEnumerator>, TakeLastEnumerator<TItem, TEnumerator>>(ref second);

        #endregion
    }
}
