using LinqAF.Impl;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    public static partial class LinqAFNew
    {
        #region SortedSet<T>(IEnumerable<T>)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static System.Collections.Generic.SortedSet<T> SortedSet<T>(System.Collections.Generic.IEnumerable<T> collection)
        {
            return new System.Collections.Generic.SortedSet<T>(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static System.Collections.Generic.SortedSet<TItem> SortedSetImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var ret = new System.Collections.Generic.SortedSet<TItem>();

            foreach (var item in e)
            {
                ret.Add(item);
            }

            return ret;
        }

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TKey, TItem>(LookupDefaultEnumerable<TKey, TItem> second)
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TKey, TItem>(LookupSpecificEnumerable<TKey, TItem> second)
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(BoxedEnumerable<TItem> second)
        => SortedSetImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second)
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
            where TConcat_IdentityBridgeType : class
        => SortedSetImpl<TItem, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>, TConcat_IdentityEnumerator>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(EmptyEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return new System.Collections.Generic.SortedSet<TItem>();
        }

        public static System.Collections.Generic.SortedSet<int> SortedSet(RangeEnumerable second)
        => SortedSetImpl<int, RangeEnumerable, RangeEnumerator>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(RepeatEnumerable<TItem> second)
        => SortedSetImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => SortedSetImpl<TItem, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => SortedSetImpl<TItem, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectIndexedEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Bridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
            where TConcat_BridgeType : class
        => SortedSetImpl<TItem, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>, SelectManyBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => SortedSetImpl<TItem, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(

            SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => SortedSetImpl<TItem, SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(

            SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => SortedSetImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyEnumerator<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => SortedSetImpl<TItem, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyCollectionEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => SortedSetImpl<TItem, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => SortedSetImpl<TItem, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, CastEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => SortedSetImpl<TItem, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, OfTypeEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        => SortedSetImpl<TItem, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>, ZipEnumerator<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => SortedSetImpl<TItem, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => SortedSetImpl<TItem, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => SortedSetImpl<TItem, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => SortedSetImpl<TItem, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(EmptyOrderedEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return new System.Collections.Generic.SortedSet<TItem>();
        }

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => SortedSetImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => SortedSetImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<int> SortedSet(ReverseRangeEnumerable second)
        => SortedSetImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => SortedSetImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(

            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => SortedSetImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(

            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => SortedSetImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(

            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => SortedSetImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(

            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => SortedSetImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(

            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => SortedSetImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(

            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => SortedSetImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemDefaultEnumerable<TItem> second)
        => SortedSetImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemSpecificEnumerable<TItem> second)
        => SortedSetImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemDefaultOrderedEnumerable<TItem> second)
        => SortedSetImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemSpecificOrderedEnumerable<TItem> second)
        => SortedSetImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(

            AppendEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, AppendEnumerable<TItem, TEnumerable, TEnumerator>, AppendEnumerator<TItem, TEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(

            PrependEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, PrependEnumerable<TItem, TEnumerable, TEnumerator>, PrependEnumerator<TItem, TEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(

            SkipLastEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipLastEnumerable<TItem, TEnumerable, TEnumerator>, SkipLastEnumerator<TItem, TEnumerator>>(ref second);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(

            TakeLastEnumerable<TItem, TEnumerable, TEnumerator> second
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeLastEnumerable<TItem, TEnumerable, TEnumerator>, TakeLastEnumerator<TItem, TEnumerator>>(ref second);

        #endregion

        #region SortedSet<T>(IEnumerable<T>, IComparer<T>)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static System.Collections.Generic.SortedSet<T> SortedSet<T>(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IComparer<T> comparer)
        {
            return new System.Collections.Generic.SortedSet<T>(collection, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static System.Collections.Generic.SortedSet<TItem> SortedSetImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable e, System.Collections.Generic.IComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var ret = new System.Collections.Generic.SortedSet<TItem>(comparer);

            foreach (var item in e)
            {
                ret.Add(item);
            }

            return ret;
        }

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second, System.Collections.Generic.IComparer<GroupingEnumerable<TKey, TItem>> comparer)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second, System.Collections.Generic.IComparer<GroupingEnumerable<TKey, TItem>> comparer)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TKey, TItem>(LookupDefaultEnumerable<TKey, TItem> second, System.Collections.Generic.IComparer<GroupingEnumerable<TKey, TItem>> comparer)
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<GroupingEnumerable<TKey, TItem>> SortedSet<TKey, TItem>(LookupSpecificEnumerable<TKey, TItem> second, System.Collections.Generic.IComparer<GroupingEnumerable<TKey, TItem>> comparer)
        => SortedSetImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(BoxedEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
            where TConcat_IdentityBridgeType : class
        => SortedSetImpl<TItem, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>, TConcat_IdentityEnumerator>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(EmptyEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return new System.Collections.Generic.SortedSet<TItem>(comparer);
        }

        public static System.Collections.Generic.SortedSet<int> SortedSet(RangeEnumerable second, System.Collections.Generic.IComparer<int> comparer)
        => SortedSetImpl<int, RangeEnumerable, RangeEnumerator>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(RepeatEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => SortedSetImpl<TItem, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => SortedSetImpl<TItem, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectIndexedEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Bridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
            where TConcat_BridgeType : class
        => SortedSetImpl<TItem, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>, SelectManyBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => SortedSetImpl<TItem, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => SortedSetImpl<TItem, SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => SortedSetImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyEnumerator<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => SortedSetImpl<TItem, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyCollectionEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => SortedSetImpl<TItem, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => SortedSetImpl<TItem, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, CastEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => SortedSetImpl<TItem, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, OfTypeEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        => SortedSetImpl<TItem, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>, ZipEnumerator<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => SortedSetImpl<TItem, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => SortedSetImpl<TItem, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => SortedSetImpl<TItem, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => SortedSetImpl<TItem, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(EmptyOrderedEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return new System.Collections.Generic.SortedSet<TItem>(comparer);
        }

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<int> SortedSet(ReverseRangeEnumerable second, System.Collections.Generic.IComparer<int> comparer)
        => SortedSetImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, System.Collections.Generic.IComparer<TItem> comparer)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => SortedSetImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => SortedSetImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => SortedSetImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => SortedSetImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => SortedSetImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => SortedSetImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => SortedSetImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemDefaultEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemSpecificEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemDefaultOrderedEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem>(OneItemSpecificOrderedEnumerable<TItem> second, System.Collections.Generic.IComparer<TItem> comparer)
        => SortedSetImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(
            AppendEnumerable<TItem, TEnumerable, TEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, AppendEnumerable<TItem, TEnumerable, TEnumerator>, AppendEnumerator<TItem, TEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(
            PrependEnumerable<TItem, TEnumerable, TEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, PrependEnumerable<TItem, TEnumerable, TEnumerator>, PrependEnumerator<TItem, TEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(
            SkipLastEnumerable<TItem, TEnumerable, TEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, SkipLastEnumerable<TItem, TEnumerable, TEnumerator>, SkipLastEnumerator<TItem, TEnumerator>>(ref second, comparer);

        public static System.Collections.Generic.SortedSet<TItem> SortedSet<TItem, TEnumerable, TEnumerator>(
            TakeLastEnumerable<TItem, TEnumerable, TEnumerator> second,
            System.Collections.Generic.IComparer<TItem> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        => SortedSetImpl<TItem, TakeLastEnumerable<TItem, TEnumerable, TEnumerator>, TakeLastEnumerator<TItem, TEnumerator>>(ref second, comparer);

        #endregion
    }
}
