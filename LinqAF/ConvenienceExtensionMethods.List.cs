using LinqAF.Impl;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    public static partial class ConvenienceExtensionMethods
    {
        #region AddRange
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void AddRangeImpl<TItem, TEnumerable, TEnumerator>(List<TItem> list, ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (list == null) throw CommonImplementation.ArgumentNull(nameof(list));
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            foreach (var item in e)
            {
                list.Add(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void AddRangeImplEmpty<TItem>(List<TItem> list)
        {
            if (list == null) throw CommonImplementation.ArgumentNull(nameof(list));
        }

        public static void AddRange<TInItem, TKey, TItem, TEnumerable, TEnumerator>(this List<GroupingEnumerable<TKey, TItem>> list, GroupByDefaultEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator> second)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        => AddRangeImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TEnumerator>>(list, ref second);

        public static void AddRange<TInItem, TKey, TItem, TEnumerable, TEnumerator>(this List<GroupingEnumerable<TKey, TItem>> list, GroupBySpecificEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator> second)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        => AddRangeImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TEnumerator>>(list, ref second);

        public static void AddRange<TKey, TItem, TEnumerable>(this List<GroupingEnumerable<TKey, TItem>> list, LookupDefaultEnumerable<TKey, TItem> second)
        => AddRangeImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(list, ref second);

        public static void AddRange<TKey, TItem, TEnumerable>(this List<GroupingEnumerable<TKey, TItem>> list, LookupSpecificEnumerable<TKey, TItem> second)
        => AddRangeImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, BoxedEnumerable<TItem> second)
        => AddRangeImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(this List<TItem> list, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second)
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
            where TConcat_IdentityBridgeType : class
        => AddRangeImpl<TItem, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>, TConcat_IdentityEnumerator>(list, ref second);

        public static void AddRange<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(this List<TItem> list, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, EmptyEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            AddRangeImplEmpty(list);
        }

        public static void AddRange(this List<int> list, RangeEnumerable second)
        => AddRangeImpl<int, RangeEnumerable, RangeEnumerator>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, RepeatEnumerable<TItem> second)
        => AddRangeImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => AddRangeImpl<TItem, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => AddRangeImpl<TItem, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectIndexedEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(this List<TItem> list, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Bridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
            where TConcat_BridgeType : class
        => AddRangeImpl<TItem, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>, SelectManyBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(this List<TItem> list, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => AddRangeImpl<TItem, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            this List<TItem> list,
            SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => AddRangeImpl<TItem, SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            this List<TItem> list,
            SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => AddRangeImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(this List<TItem> list, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyEnumerator<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(this List<TItem> list, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(this List<TItem> list, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => AddRangeImpl<TItem, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyCollectionEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(this List<TItem> list, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => AddRangeImpl<TItem, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeWhileEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TInnerEnumerable, TInnerEnumerator>(this List<TItem> list, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => AddRangeImpl<TItem, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, CastEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => AddRangeImpl<TItem, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, OfTypeEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(this List<TItem> list, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        => AddRangeImpl<TItem, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>, ZipEnumerator<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(this List<TItem> list, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => AddRangeImpl<TItem, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(list, ref second);

        public static void AddRange<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(this List<TItem> list, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => AddRangeImpl<TItem, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(list, ref second);

        public static void AddRange<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(this List<TItem> list, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => AddRangeImpl<TItem, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(list, ref second);

        public static void AddRange<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(this List<TItem> list, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => AddRangeImpl<TItem, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(list, ref second);

        public static void AddRange<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(this List<TItem> list, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(this List<TItem> list, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, EmptyOrderedEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            AddRangeImplEmpty(list);
        }

        public static void AddRange<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(this List<TItem> list, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(this List<TItem> list, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(this List<TItem> list, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(this List<TItem> list, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(this List<TItem> list, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(this List<TItem> list, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(list, ref second);

        public static void AddRange<TItem, TGroupedKey>(this List<TItem> list, GroupedEnumerable<TGroupedKey, TItem> second)
        => AddRangeImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem, TGroupedKey>(this List<TItem> list, GroupingEnumerable<TGroupedKey, TItem> second)
        => AddRangeImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem, TReverseEnumerable, TReverseEnumerator>(this List<TItem> list, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(list, ref second);

        public static void AddRange(this List<int> list, ReverseRangeEnumerable second)
        => AddRangeImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(list, ref second);

        public static void AddRange<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(this List<TItem> list, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => AddRangeImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(list, ref second);

        public static void AddRange<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            this List<TItem> list,
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => AddRangeImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(list, ref second);

        public static void AddRange<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            this List<TItem> list,
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => AddRangeImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(list, ref second);

        public static void AddRange<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            this List<TItem> list,
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => AddRangeImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(list, ref second);

        public static void AddRange<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            this List<TItem> list,
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => AddRangeImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(list, ref second);

        public static void AddRange<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            this List<TItem> list,
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => AddRangeImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(list, ref second);

        public static void AddRange<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            this List<TItem> list,
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => AddRangeImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, OneItemDefaultEnumerable<TItem> second)
        => AddRangeImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, OneItemSpecificEnumerable<TItem> second)
        => AddRangeImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, OneItemDefaultOrderedEnumerable<TItem> second)
        => AddRangeImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem>(this List<TItem> list, OneItemSpecificOrderedEnumerable<TItem> second)
        => AddRangeImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(list, ref second);

        public static void AddRange<TItem, TInnerEnumerable, TInnerEnumerator>(this List<TItem> list, SkipLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, SkipLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, SkipLastEnumerator<TItem, TInnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TInnerEnumerable, TInnerEnumerator>(this List<TItem> list, TakeLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, TakeLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeLastEnumerator<TItem, TInnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TInnerEnumerable, TInnerEnumerator>(this List<TItem> list, AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, AppendEnumerator<TItem, TInnerEnumerator>>(list, ref second);

        public static void AddRange<TItem, TInnerEnumerable, TInnerEnumerator>(this List<TItem> list, PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
          where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
          where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => AddRangeImpl<TItem, PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, PrependEnumerator<TItem, TInnerEnumerator>>(list, ref second);
        #endregion

        #region InsertRange
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void InsertRangeImpl<TItem, TEnumerable, TEnumerator>(List<TItem> list, int index, ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (list == null) throw CommonImplementation.ArgumentNull(nameof(list));
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));
            if (index < 0 || index >= list.Count) throw CommonImplementation.OutOfRange(nameof(index));

            foreach (var item in e)
            {
                list.Insert(index, item);
                index++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void InsertRangeImplEmpty<TItem>(List<TItem> list, int index)
        {
            if (list == null) throw CommonImplementation.ArgumentNull(nameof(list));
            if (index < 0 || index >= list.Count) throw CommonImplementation.OutOfRange(nameof(index));
        }

        public static void InsertRange<TInItem, TKey, TItem, TEnumerable, TEnumerator>(this List<GroupingEnumerable<TKey, TItem>> list, int index, GroupByDefaultEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator> second)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        => InsertRangeImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TEnumerator>>(list, index, ref second);

        public static void InsertRange<TInItem, TKey, TItem, TEnumerable, TEnumerator>(this List<GroupingEnumerable<TKey, TItem>> list, int index, GroupBySpecificEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator> second)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        => InsertRangeImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TEnumerable, TEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TEnumerator>>(list, index, ref second);

        public static void InsertRange<TKey, TItem, TEnumerable>(this List<GroupingEnumerable<TKey, TItem>> list, int index, LookupDefaultEnumerable<TKey, TItem> second)
        => InsertRangeImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(list, index, ref second);

        public static void InsertRange<TKey, TItem, TEnumerable>(this List<GroupingEnumerable<TKey, TItem>> list, int index, LookupSpecificEnumerable<TKey, TItem> second)
        => InsertRangeImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, BoxedEnumerable<TItem> second)
        => InsertRangeImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(this List<TItem> list, int index, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second)
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
            where TConcat_IdentityBridgeType : class
        => InsertRangeImpl<TItem, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>, TConcat_IdentityEnumerator>(list, index, ref second);

        public static void InsertRange<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(this List<TItem> list, int index, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, EmptyEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            InsertRangeImplEmpty(list, index);
        }

        public static void InsertRange(this List<int> list, int index, RangeEnumerable second)
        => InsertRangeImpl<int, RangeEnumerable, RangeEnumerator>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, RepeatEnumerable<TItem> second)
        => InsertRangeImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => InsertRangeImpl<TItem, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => InsertRangeImpl<TItem, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectIndexedEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(this List<TItem> list, int index, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Bridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
            where TConcat_BridgeType : class
        => InsertRangeImpl<TItem, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>, SelectManyBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(this List<TItem> list, int index, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => InsertRangeImpl<TItem, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            this List<TItem> list,
            int index,
            SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => InsertRangeImpl<TItem, SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            this List<TItem> list,
            int index,
            SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => InsertRangeImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(this List<TItem> list, int index, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyEnumerator<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(this List<TItem> list, int index, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(this List<TItem> list, int index, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => InsertRangeImpl<TItem, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyCollectionEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(this List<TItem> list, int index, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => InsertRangeImpl<TItem, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeWhileEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TInnerEnumerable, TInnerEnumerator>(this List<TItem> list, int index, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => InsertRangeImpl<TItem, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, CastEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => InsertRangeImpl<TItem, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, OfTypeEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(this List<TItem> list, int index, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        => InsertRangeImpl<TItem, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>, ZipEnumerator<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(this List<TItem> list, int index, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => InsertRangeImpl<TItem, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(this List<TItem> list, int index, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => InsertRangeImpl<TItem, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(this List<TItem> list, int index, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => InsertRangeImpl<TItem, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(this List<TItem> list, int index, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => InsertRangeImpl<TItem, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(this List<TItem> list, int index, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(this List<TItem> list, int index, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, EmptyOrderedEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            InsertRangeImplEmpty(list, index);
        }

        public static void InsertRange<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(this List<TItem> list, int index, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(this List<TItem> list, int index, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(this List<TItem> list, int index, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(this List<TItem> list, int index, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(this List<TItem> list, int index, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(this List<TItem> list, int index, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TGroupedKey>(this List<TItem> list, int index, GroupedEnumerable<TGroupedKey, TItem> second)
        => InsertRangeImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem, TGroupedKey>(this List<TItem> list, int index, GroupingEnumerable<TGroupedKey, TItem> second)
        => InsertRangeImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem, TReverseEnumerable, TReverseEnumerator>(this List<TItem> list, int index, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange(this List<int> list, int index, ReverseRangeEnumerable second)
        => InsertRangeImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(list, index, ref second);

        public static void InsertRange<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(this List<TItem> list, int index, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => InsertRangeImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(list, index, ref second);

        public static void InsertRange<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            this List<TItem> list,
            int index,
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => InsertRangeImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            this List<TItem> list,
            int index,
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => InsertRangeImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            this List<TItem> list,
            int index,
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => InsertRangeImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            this List<TItem> list,
            int index,
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => InsertRangeImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            this List<TItem> list,
            int index,
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => InsertRangeImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            this List<TItem> list,
            int index,
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => InsertRangeImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, OneItemDefaultEnumerable<TItem> second)
        => InsertRangeImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, OneItemSpecificEnumerable<TItem> second)
        => InsertRangeImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, OneItemDefaultOrderedEnumerable<TItem> second)
        => InsertRangeImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem>(this List<TItem> list, int index, OneItemSpecificOrderedEnumerable<TItem> second)
        => InsertRangeImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, SkipLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, SkipLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipLastEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, TakeLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, TakeLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeLastEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, AppendEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, AppendEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, AppendEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);

        public static void InsertRange<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(this List<TItem> list, int index, PrependEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => InsertRangeImpl<TItem, PrependEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, PrependEnumerator<TItem, TConcat_InnerEnumerator>>(list, index, ref second);
        #endregion
    }
}