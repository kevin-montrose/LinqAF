using LinqAF.Impl;
using System;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    public static partial class LinqAFString
    {
        // holds a cached T -> object func so we don't re-allocate it
        // 
        // one would expect the JIT or compiler to make this optimization
        //   and they might, but I'd like to guarantee it
        static class ObjectMap<T>
        {
            public static readonly Func<T, object> Func = item => item;
        }

        static string ConcatImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var asObject = CommonImplementation.Select<TItem, object, TEnumerable, TEnumerator>(ref e, ObjectMap<TItem>.Func);

            // concat is spec'd as treating null as string.Empty
            //   so we don't need to do the final re-size of the
            //   array that a .ToArray() would imply
            //   we just need to make sure that the end of the array
            //   is cleared (set to null)
            int count;
            var asArr = CommonImplementation.Buffer<object, SelectEnumerable<TItem, object, TEnumerable, TEnumerator>, SelectEnumerator<TItem, object, TEnumerator>>(ref asObject, out count);
            if (count != asArr.Length)
            {
                Array.Clear(asArr, count, asArr.Length - count);
            }
            
            return string.Concat(asArr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
             where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
             where TInnerEnumerator : struct, IStructEnumerator<TInItem>
         => ConcatImpl<GroupingEnumerable<TKey, TItem>, GroupByDefaultEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupByDefaultEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>(GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator> second)
            where TInnerEnumeable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        => ConcatImpl<GroupingEnumerable<TKey, TItem>, GroupBySpecificEnumerable<TInItem, TKey, TItem, TInnerEnumeable, TInnerEnumerator>, GroupBySpecificEnumerator<TInItem, TKey, TItem, TInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TKey, TItem>(LookupDefaultEnumerable<TKey, TItem> second)
        => ConcatImpl<GroupingEnumerable<TKey, TItem>, LookupDefaultEnumerable<TKey, TItem>, LookupDefaultEnumerator<TKey, TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TKey, TItem>(LookupSpecificEnumerable<TKey, TItem> second)
        => ConcatImpl<GroupingEnumerable<TKey, TItem>, LookupSpecificEnumerable<TKey, TItem>, LookupSpecificEnumerator<TKey, TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem>(BoxedEnumerable<TItem> second)
        => ConcatImpl<TItem, BoxedEnumerable<TItem>, BoxedEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>(IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator> second)
            where TConcat_IdentityEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_IdentityBridger : struct, IStructBridger<TItem, TConcat_IdentityBridgeType, TConcat_IdentityEnumerator>
            where TConcat_IdentityBridgeType : class
        => ConcatImpl<TItem, IdentityEnumerable<TItem, TConcat_IdentityBridgeType, TConcat_IdentityBridger, TConcat_IdentityEnumerator>, TConcat_IdentityEnumerator>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<TItem, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<TItem>
            where TInnerRightEnumerable : struct, IStructEnumerable<TItem, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, ConcatEnumerable<TItem, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<TItem, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second);

        // intentionally not inlining since this isn't just a method call
        public static string Concat<TItem>(EmptyEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return string.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat(RangeEnumerable second)
        => ConcatImpl<int, RangeEnumerable, RangeEnumerator>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem>(RepeatEnumerable<TItem> second)
        => ConcatImpl<TItem, RepeatEnumerable<TItem>, RepeatEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => ConcatImpl<TItem, SelectEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectInItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectInItem>
        => ConcatImpl<TItem, SelectIndexedEnumerable<TConcat_SelectInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SelectIndexedEnumerator<TConcat_SelectInItem, TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>(SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_Bridger : struct, IStructBridger<TItem, TConcat_BridgeType, TConcat_ProjectedEnumerator>
            where TConcat_BridgeType : class
        => ConcatImpl<TItem, SelectManyBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>, SelectManyBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_BridgeType, TConcat_Bridger, TConcat_InnerEnumerator, TConcat_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => ConcatImpl<TItem, SelectManyIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => ConcatImpl<TItem, SelectManyCollectionBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_InnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator> second
        )
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
            where TConcat_SelectManyBridger : struct, IStructBridger<TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyBridgeType : class
        => ConcatImpl<TItem, SelectManyCollectionIndexedBridgeEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyBridgeType, TConcat_SelectManyBridger, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, SelectManyEnumerable<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyEnumerator<TConcat_SelectManyInItem, TItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, SelectManyIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>(SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator> second)
            where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_InnerEnumerator>
            where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_ProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_ProjectedEnumerator>
            where TConcat_ProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => ConcatImpl<TItem, SelectManyCollectionEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>, SelectManyCollectionEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_InnerEnumerator, TConcat_ProjectedEnumerable, TConcat_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectManyInItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator> second)
            where TConcat_SelectManyInnerEnumerable : struct, IStructEnumerable<TConcat_SelectManyInItem, TConcat_SelectManyInnerEnumerator>
            where TConcat_SelectManyInnerEnumerator : struct, IStructEnumerator<TConcat_SelectManyInItem>
            where TConcat_SelectManyProjectedEnumerable : struct, IStructEnumerable<TConcat_CollectionItem, TConcat_SelectManyProjectedEnumerator>
            where TConcat_SelectManyProjectedEnumerator : struct, IStructEnumerator<TConcat_CollectionItem>
        => ConcatImpl<TItem, SelectManyCollectionIndexedEnumerable<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerable, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TConcat_SelectManyInItem, TItem, TConcat_CollectionItem, TConcat_SelectManyInnerEnumerator, TConcat_SelectManyProjectedEnumerable, TConcat_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, WhereEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, WhereIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, WhereIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, DefaultIfEmptyDefaultEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, DefaultIfEmptySpecificEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, TakeEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, TakeWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, SkipEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, SkipWhileEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, SkipWhileIndexedEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipWhileIndexedEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => ConcatImpl<TItem, CastEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, CastEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TConcat_InItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TConcat_InItem>
        => ConcatImpl<TItem, OfTypeEnumerable<TConcat_InItem, TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, OfTypeEnumerator<TConcat_InItem, TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>(ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator> second)
            where TConcat_ZipFirstEnumerable : struct, IStructEnumerable<TConcat_ZipFirstItem, TConcat_ZipFirstEnumerator>
            where TConcat_ZipFirstEnumerator : struct, IStructEnumerator<TConcat_ZipFirstItem>
            where TConcat_ZipSecondEnumerable : struct, IStructEnumerable<TConcat_ZipSecondItem, TConcat_ZipSecondEnumerator>
            where TConcat_ZipSecondEnumerator : struct, IStructEnumerator<TConcat_ZipSecondItem>
        => ConcatImpl<TItem, ZipEnumerable<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerable, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerable, TConcat_ZipSecondEnumerator>, ZipEnumerator<TItem, TConcat_ZipFirstItem, TConcat_ZipSecondItem, TConcat_ZipFirstEnumerator, TConcat_ZipSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>(SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => ConcatImpl<TItem, SelectSelectEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>, SelectSelectEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>(SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate> second)
            where TConcat_SelectInnerEnumerable : struct, IStructEnumerable<TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator>
            where TConcat_SelectInnerEnumerator : struct, IStructEnumerator<TConcat_SelectInnerItem>
            where TConcat_SelectPredicate : struct, IStructPredicate<TItem>
            where TConcat_SelectProjection : struct, IStructProjection<TItem, TConcat_SelectInnerItem>
        => ConcatImpl<TItem, SelectWhereEnumerable<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerable, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>, SelectWhereEnumerator<TItem, TConcat_SelectInnerItem, TConcat_SelectInnerEnumerator, TConcat_SelectProjection, TConcat_SelectPredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>(WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TItem>
        => ConcatImpl<TItem, WhereWhereEnumerable<TItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>, WhereWhereEnumerator<TItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>(WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection> second)
            where TConcat_WhereInnerEnumerable : struct, IStructEnumerable<TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator>
            where TConcat_WhereInnerEnumerator : struct, IStructEnumerator<TConcat_WhereInnerItem>
            where TConcat_WherePredicate : struct, IStructPredicate<TConcat_WhereInnerItem>
            where TConcat_WhereProjection : struct, IStructProjection<TItem, TConcat_WhereInnerItem>
        => ConcatImpl<TItem, WhereSelectEnumerable<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerable, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>, WhereSelectEnumerator<TItem, TConcat_WhereInnerItem, TConcat_WhereInnerEnumerator, TConcat_WherePredicate, TConcat_WhereProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, DistinctDefaultEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctDefaultEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator> second)
            where TConcat_DistinctInnerEnumerable : struct, IStructEnumerable<TItem, TConcat_DistinctInnerEnumerator>
            where TConcat_DistinctInnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, DistinctSpecificEnumerable<TItem, TConcat_DistinctInnerEnumerable, TConcat_DistinctInnerEnumerator>, DistinctSpecificEnumerator<TItem, TConcat_DistinctInnerEnumerator>>(ref second);

        // intentionally not inlining since this isn't just a method call
        public static string Concat<TItem>(EmptyOrderedEnumerable<TItem> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return string.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, ExceptDefaultEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<TItem, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<TItem>
            where TExceptSecondEnumerable : struct, IStructEnumerable<TItem, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, ExceptSpecificEnumerable<TItem, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<TItem, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, IntersectDefaultEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<TItem, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<TItem>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<TItem, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, IntersectSpecificEnumerable<TItem, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<TItem, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, UnionDefaultEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<TItem, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<TItem>
            where TUnionSecondEnumerable : struct, IStructEnumerable<TItem, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, UnionSpecificEnumerable<TItem, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<TItem, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TGroupedKey>(GroupedEnumerable<TGroupedKey, TItem> second)
        => ConcatImpl<TItem, GroupedEnumerable<TGroupedKey, TItem>, GroupedEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TGroupedKey>(GroupingEnumerable<TGroupedKey, TItem> second)
        => ConcatImpl<TItem, GroupingEnumerable<TGroupedKey, TItem>, GroupingEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<TItem, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, ReverseEnumerable<TItem, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat(ReverseRangeEnumerable second)
        => ConcatImpl<int, ReverseRangeEnumerable, ReverseRangeEnumerator>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<TItem, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<TItem>
            where TOrderByComparer : struct, IStructComparer<TItem, TOrderByKey>
        => ConcatImpl<TItem, OrderByEnumerable<TItem, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<TItem, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => ConcatImpl<TItem, GroupJoinDefaultEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinDefaultEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>(
            GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator> second
        )
            where TGroupJoinLeftEnumerable : struct, IStructEnumerable<TGroupJoinLeftItem, TGroupJoinLeftEnumerator>
            where TGroupJoinLeftEnumerator : struct, IStructEnumerator<TGroupJoinLeftItem>
            where TGroupJoinRightEnumerable : struct, IStructEnumerable<TGroupJoinRightItem, TGroupJoinRightEnumerator>
            where TGroupJoinRightEnumerator : struct, IStructEnumerator<TGroupJoinRightItem>
        => ConcatImpl<TItem, GroupJoinSpecificEnumerable<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerable, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerable, TGroupJoinRightEnumerator>, GroupJoinSpecificEnumerator<TItem, TGroupJoinKeyItem, TGroupJoinLeftItem, TGroupJoinLeftEnumerator, TGroupJoinRightItem, TGroupJoinRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static string Concat<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => ConcatImpl<TItem, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => ConcatImpl<TItem, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, TItem, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => ConcatImpl<TItem, JoinDefaultEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinDefaultEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>(
            JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator> second
        )
            where TJoinLeftEnumerable : struct, IStructEnumerable<TJoinLeftItem, TJoinLeftEnumerator>
            where TJoinLeftEnumerator : struct, IStructEnumerator<TJoinLeftItem>
            where TJoinRightEnumerable : struct, IStructEnumerable<TJoinRightItem, TJoinRightEnumerator>
            where TJoinRightEnumerator : struct, IStructEnumerator<TJoinRightItem>
        => ConcatImpl<TItem, JoinSpecificEnumerable<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerable, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerable, TJoinRightEnumerator>, JoinSpecificEnumerator<TItem, TJoinKeyItem, TJoinLeftItem, TJoinLeftEnumerator, TJoinRightItem, TJoinRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem>(OneItemDefaultEnumerable<TItem> second)
        => ConcatImpl<TItem, OneItemDefaultEnumerable<TItem>, OneItemDefaultEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem>(OneItemSpecificEnumerable<TItem> second)
        => ConcatImpl<TItem, OneItemSpecificEnumerable<TItem>, OneItemSpecificEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem>(OneItemDefaultOrderedEnumerable<TItem> second)
        => ConcatImpl<TItem, OneItemDefaultOrderedEnumerable<TItem>, OneItemDefaultOrderedEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem>(OneItemSpecificOrderedEnumerable<TItem> second)
        => ConcatImpl<TItem, OneItemSpecificOrderedEnumerable<TItem>, OneItemSpecificOrderedEnumerator<TItem>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(SkipLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, SkipLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, SkipLastEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(TakeLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, TakeLastEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, TakeLastEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(AppendEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, AppendEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, AppendEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>(PrependEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator> second)
           where TConcat_InnerEnumerable : struct, IStructEnumerable<TItem, TConcat_InnerEnumerator>
           where TConcat_InnerEnumerator : struct, IStructEnumerator<TItem>
        => ConcatImpl<TItem, PrependEnumerable<TItem, TConcat_InnerEnumerable, TConcat_InnerEnumerator>, PrependEnumerator<TItem, TConcat_InnerEnumerator>>(ref second);
    }
}
