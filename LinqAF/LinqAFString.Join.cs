using LinqAF.Impl;
using System;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    public static partial class LinqAFString
    {
        // holds a cached T -> string func so we don't re-allocate it
        // 
        // one would expect the JIT or compiler to make this optimization
        //   and they might, but I'd like to guarantee it
        static class StringMap<T>
        {
            public static readonly Func<T, string> Func = item => item?.ToString();
        }

        static string JoinImpl<TItem, TEnumerable, TEnumerator>(string separator, ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var asString = CommonImplementation.Select<TItem, string, TEnumerable, TEnumerator>(ref e, StringMap<TItem>.Func);

            // using buffer to skip the final re-size of the array
            //   that .ToArray() would imply
            int count;
            var asArr = CommonImplementation.Buffer<string, SelectEnumerable<TItem, string, TEnumerable, TEnumerator>, SelectEnumerator<TItem, string, TEnumerator>>(ref asString, out count);
            
            return string.Join(separator, asArr, 0, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(string separator, GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
             where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
             where TInnerEnumerator : struct, IStructEnumerator<TInItem>
         => JoinImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(string separator, GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => JoinImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TKey, TItem>(string separator, LookupDefaultEnumerable<TKey, TItem> second)
        => JoinImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TKey, TItem>(string separator, LookupSpecificEnumerable<TKey, TItem> second)
        => JoinImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem>(string separator, BoxedEnumerable<TItem> second)
        => JoinImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_IdentityBridgeType, TJoin_IdentityBridger, TJoin_IdentityEnumerator>(string separator, IdentityEnumerable<TItem, TJoin_IdentityBridgeType, TJoin_IdentityBridger, TJoin_IdentityEnumerator> second)
            where TJoin_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TJoin_IdentityBridger : struct, IStructBridger<TItem, TJoin_IdentityBridgeType, TJoin_IdentityEnumerator>
            where TJoin_IdentityBridgeType : class
        => JoinImpl<TItem, IdentityEnumerable<TItem, TJoin_IdentityBridgeType, TJoin_IdentityBridger, TJoin_IdentityEnumerator>, TJoin_IdentityEnumerator>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(string separator, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(separator, ref second);

        // intentionally not inlining since this isn't just a method call
        public static string Join<TItem>(string separator, EmptyEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return string.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string separator, RangeEnumerable second)
        => JoinImpl<int, RangeEnumerable, RangeEnumerator>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem>(string separator, RepeatEnumerable<TItem> second)
        => JoinImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectInItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, SelectEnumerable<TJoin_SelectInItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
            where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_SelectInItem, TJoin_InnerEnumerator>
            where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_SelectInItem>
        => JoinImpl<TItem, SelectEnumerable<TJoin_SelectInItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, SelectEnumerator<TJoin_SelectInItem, TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectInItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, SelectIndexedEnumerable<TJoin_SelectInItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_SelectInItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_SelectInItem>
        => JoinImpl<TItem, SelectIndexedEnumerable<TJoin_SelectInItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, SelectIndexedEnumerator<TJoin_SelectInItem, TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_BridgeType, TJoin_Bridger, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerator>(string separator, SelectManyBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_BridgeType, TJoin_Bridger, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerator> second)
            where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_InnerEnumerator>
            where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TJoin_Bridger : struct, IStructBridger<TItem, TJoin_BridgeType, TJoin_ProjectedEnumerator>
            where TJoin_BridgeType : class
        => JoinImpl<TItem, SelectManyBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_BridgeType, TJoin_Bridger, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerator>, SelectManyBridgeEnumerator<TJoin_SelectManyInItem, TItem, TJoin_BridgeType, TJoin_Bridger, TJoin_InnerEnumerator, TJoin_ProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(string separator, SelectManyIndexedBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> second)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TJoin_SelectManyBridger : struct, IStructBridger<TItem, TJoin_SelectManyBridgeType, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyBridgeType : class
        => JoinImpl<TItem, SelectManyIndexedBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TJoin_SelectManyInItem, TItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_SelectManyProjectedEnumerator>(
            string separator,
            SelectManyCollectionBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_SelectManyProjectedEnumerator> second
        )
            where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_InnerEnumerator>
            where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_CollectionItem>
            where TJoin_SelectManyBridger : struct, IStructBridger<TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyBridgeType : class
        => JoinImpl<TItem, SelectManyCollectionBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_InnerEnumerator, TJoin_SelectManyProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(
            string separator,
            SelectManyCollectionIndexedBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> second
        )
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_CollectionItem>
            where TJoin_SelectManyBridger : struct, IStructBridger<TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyBridgeType : class
        => JoinImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyBridger, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator>(string separator, SelectManyEnumerable<TJoin_SelectManyInItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator> second)
            where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_InnerEnumerator>
            where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_ProjectedEnumerable : struct, IStructEnumerable<TItem, TJoin_ProjectedEnumerator>
            where TJoin_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, SelectManyEnumerable<TJoin_SelectManyInItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator>, SelectManyEnumerator<TJoin_SelectManyInItem, TItem, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(string separator, SelectManyIndexedEnumerable<TJoin_SelectManyInItem, TItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> second)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, SelectManyIndexedEnumerable<TJoin_SelectManyInItem, TItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TJoin_SelectManyInItem, TItem, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_CollectionItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator>(string separator, SelectManyCollectionEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator> second)
            where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_InnerEnumerator>
            where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_ProjectedEnumerable : struct, IStructEnumerable<TJoin_CollectionItem, TJoin_ProjectedEnumerator>
            where TJoin_ProjectedEnumerator : struct, IStructEnumerator<TJoin_CollectionItem>
        => JoinImpl<TItem, SelectManyCollectionEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator>, SelectManyCollectionEnumerator<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_InnerEnumerator, TJoin_ProjectedEnumerable, TJoin_ProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectManyInItem, TJoin_CollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(string separator, SelectManyCollectionIndexedEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> second)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_CollectionItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_CollectionItem>
        => JoinImpl<TItem, SelectManyCollectionIndexedEnumerable<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TJoin_SelectManyInItem, TItem, TJoin_CollectionItem, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, WhereEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, WhereEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, WhereEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, WhereIndexedEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, WhereIndexedEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, WhereIndexedEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, DefaultIfEmptyDefaultEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, DefaultIfEmptySpecificEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, TakeEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, TakeEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, TakeEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, TakeWhileEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, TakeWhileEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, TakeWhileEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TInnerEnumerable, TInnerEnumerator>(string separator, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, SkipEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, SkipEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, SkipEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, SkipWhileEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, SkipWhileEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, SkipWhileEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, SkipWhileIndexedEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, SkipWhileIndexedEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, CastEnumerable<TJoin_InItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_InItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_InItem>
        => JoinImpl<TItem, CastEnumerable<TJoin_InItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, CastEnumerator<TJoin_InItem, TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, OfTypeEnumerable<TJoin_InItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TJoin_InItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TJoin_InItem>
        => JoinImpl<TItem, OfTypeEnumerable<TJoin_InItem, TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, OfTypeEnumerator<TJoin_InItem, TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator>(string separator, ZipEnumerable<TItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator> second)
            where TJoin_ZipFirstEnumerable : struct, IStructEnumerable<TJoin_ZipFirstItem, TJoin_ZipFirstEnumerator>
            where TJoin_ZipFirstEnumerator : struct, IStructEnumerator<TJoin_ZipFirstItem>
            where TJoin_ZipSecondEnumerable : struct, IStructEnumerable<TJoin_ZipSecondItem, TJoin_ZipSecondEnumerator>
            where TJoin_ZipSecondEnumerator : struct, IStructEnumerator<TJoin_ZipSecondItem>
        => JoinImpl<TItem, ZipEnumerable<TItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator>, ZipEnumerator<TItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection>(string separator, SelectSelectEnumerable<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection> second)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInnerItem>
            where TJoin_SelectProjection : struct, IStructProjection<TItem, TJoin_SelectInnerItem>
        => JoinImpl<TItem, SelectSelectEnumerable<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection>, SelectSelectEnumerator<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator, TJoin_SelectProjection>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate>(string separator, SelectWhereEnumerable<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate> second)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInnerItem>
            where TJoin_SelectPredicate : struct, IStructPredicate<TItem>
            where TJoin_SelectProjection : struct, IStructProjection<TItem, TJoin_SelectInnerItem>
        => JoinImpl<TItem, SelectWhereEnumerable<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate>, SelectWhereEnumerator<TItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate>(string separator, WhereWhereEnumerable<TItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate> second)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TJoin_WherePredicate : struct, IStructPredicate<TItem>
        => JoinImpl<TItem, WhereWhereEnumerable<TItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate>, WhereWhereEnumerator<TItem, TJoin_WhereInnerEnumerator, TJoin_WherePredicate>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection>(string separator, WhereSelectEnumerable<TItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection> second)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_WhereInnerItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_WhereInnerItem>
            where TJoin_WherePredicate : struct, IStructPredicate<TJoin_WhereInnerItem>
            where TJoin_WhereProjection : struct, IStructProjection<TItem, TJoin_WhereInnerItem>
        => JoinImpl<TItem, WhereSelectEnumerable<TItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection>, WhereSelectEnumerator<TItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>(string separator, DistinctDefaultEnumerable<TItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator> second)
            where TJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TJoin_DistinctInnerEnumerator>
            where TJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, DistinctDefaultEnumerable<TItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TJoin_DistinctInnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>(string separator, DistinctSpecificEnumerable<TItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator> second)
            where TJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TJoin_DistinctInnerEnumerator>
            where TJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, DistinctSpecificEnumerable<TItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TJoin_DistinctInnerEnumerator>>(separator, ref second);

        // intentionally not inlining since this isn't just a method call
        public static string Join<TItem>(string separator, EmptyOrderedEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return string.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(string separator, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(string separator, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(string separator, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(string separator, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(string separator, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(string separator, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TGroupedKey>(string separator, GroupedEnumerable<TGroupedKey, TItem> second)
        => JoinImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TGroupedKey>(string separator, GroupingEnumerable<TGroupedKey, TItem> second)
        => JoinImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TReverseEnumerable, TReverseEnumerator>(string separator, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string separator, ReverseRangeEnumerable second)
        => JoinImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(string separator, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => JoinImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            string separator,
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => JoinImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            string separator,
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => JoinImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            string separator,
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => JoinImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            string separator,
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => JoinImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            string separator,
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => JoinImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            string separator,
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => JoinImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem>(string separator, OneItemDefaultEnumerable<TItem> second)
        => JoinImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem>(string separator, OneItemSpecificEnumerable<TItem> second)
        => JoinImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem>(string separator, OneItemDefaultOrderedEnumerable<TItem> second)
        => JoinImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem>(string separator, OneItemSpecificOrderedEnumerable<TItem> second)
        => JoinImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, SkipLastEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, SkipLastEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, SkipLastEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, TakeLastEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, TakeLastEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, TakeLastEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, AppendEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, AppendEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, AppendEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>(string separator, PrependEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator> second)
           where TJoin_InnerEnumerable : struct, IStructEnumerable<TItem, TJoin_InnerEnumerator>
           where TJoin_InnerEnumerator : struct, IStructEnumerator<TItem>
        => JoinImpl<TItem, PrependEnumerable<TItem, TJoin_InnerEnumerable, TJoin_InnerEnumerator>, PrependEnumerator<TItem, TJoin_InnerEnumerator>>(separator, ref second);
    }
}
