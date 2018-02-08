using LinqAF.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace LinqAF
{
    public static partial class LinqAFTask
    {
        #region no timeout, no cancelation
        static void WaitAllImpl<TEnumerable, TEnumerator>(ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<Task, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task, TEnumerable, TEnumerator>(ref e);
            Task.WaitAll(arr);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(BoxedEnumerable<Task> second)
        => WaitAllImpl<BoxedEnumerable<Task>, BoxedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>(IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator> second)
            where TWaitAll_IdentityEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_IdentityBridger : struct, IStructBridger<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityEnumerator>
            where TWaitAll_IdentityBridgeType : class
        => WaitAllImpl<IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>, TWaitAll_IdentityEnumerator>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second);

        // intentionally not inlining since this isn't just delegating a method call
        public static void WaitAll(EmptyEnumerable<Task> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(RepeatEnumerable<Task> second)
        => WaitAllImpl<RepeatEnumerable<Task>, RepeatEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectIndexedEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator> second)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_Bridger : struct, IStructBridger<Task, TWaitAll_BridgeType, TWaitAll_ProjectedEnumerator>
            where TWaitAll_BridgeType : class
        => WaitAllImpl<SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second
        )
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second
        )
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<Task, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task, TInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, CastEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, OfTypeEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>(ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator> second)
            where TWaitAll_ZipFirstEnumerable : struct, IStructEnumerable<TWaitAll_ZipFirsTask, TWaitAll_ZipFirstEnumerator>
            where TWaitAll_ZipFirstEnumerator : struct, IStructEnumerator<TWaitAll_ZipFirsTask>
            where TWaitAll_ZipSecondEnumerable : struct, IStructEnumerable<TWaitAll_ZipSecondItem, TWaitAll_ZipSecondEnumerator>
            where TWaitAll_ZipSecondEnumerator : struct, IStructEnumerator<TWaitAll_ZipSecondItem>
        => WaitAllImpl<ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>, ZipEnumerator<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>(SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection> second)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>, SelectSelectEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>(SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate> second)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectPredicate : struct, IStructPredicate<Task>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>, SelectWhereEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>(WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate> second)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_WherePredicate : struct, IStructPredicate<Task>
        => WaitAllImpl<WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>, WhereWhereEnumerator<Task, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>(WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection> second)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<TWaitAll_WhereInnerItem>
            where TWaitAll_WherePredicate : struct, IStructPredicate<TWaitAll_WhereInnerItem>
            where TWaitAll_WhereProjection : struct, IStructProjection<Task, TWaitAll_WhereInnerItem>
        => WaitAllImpl<WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>, WhereSelectEnumerator<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second);

        // intentionally not inlining since this isn't just delegating a method call
        public static void WaitAll(EmptyOrderedEnumerable<Task> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupedKey>(GroupedEnumerable<TGroupedKey, Task> second)
        => WaitAllImpl<GroupedEnumerable<TGroupedKey, Task>, GroupedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupedKey>(GroupingEnumerable<TGroupedKey, Task> second)
        => WaitAllImpl<GroupingEnumerable<TGroupedKey, Task>, GroupingEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<Task, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task>>(ref second);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<Task, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task>
            where TOrderByComparer : struct, IStructComparer<Task, TOrderByKey>
        => WaitAllImpl<OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinDefaultEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinSpecificEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinDefaultEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinSpecificEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemDefaultEnumerable<Task> second)
        => WaitAllImpl<OneItemDefaultEnumerable<Task>, OneItemDefaultEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemSpecificEnumerable<Task> second)
        => WaitAllImpl<OneItemSpecificEnumerable<Task>, OneItemSpecificEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemDefaultOrderedEnumerable<Task> second)
        => WaitAllImpl<OneItemDefaultOrderedEnumerable<Task>, OneItemDefaultOrderedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemSpecificOrderedEnumerable<Task> second)
        => WaitAllImpl<OneItemSpecificOrderedEnumerable<Task>, OneItemSpecificOrderedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, AppendEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, PrependEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second);

        #endregion

        #region timespan timeout, no cancelation
        static bool WaitAllImpl<TEnumerable, TEnumerator>(ref TEnumerable e, TimeSpan timeout)
            where TEnumerable : struct, IStructEnumerable<Task, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task, TEnumerable, TEnumerator>(ref e);
            return Task.WaitAll(arr, timeout);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(BoxedEnumerable<Task> second, TimeSpan timeout)
        => WaitAllImpl<BoxedEnumerable<Task>, BoxedEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>(IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator> second, TimeSpan timeout)
            where TWaitAll_IdentityEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_IdentityBridger : struct, IStructBridger<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityEnumerator>
            where TWaitAll_IdentityBridgeType : class
        => WaitAllImpl<IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>, TWaitAll_IdentityEnumerator>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, TimeSpan timeout)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second, timeout);

        // intentionally not inlining since this isn't just delegating a method call
        public static bool WaitAll(EmptyEnumerable<Task> second, TimeSpan timeout)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (timeout < TimeSpan.Zero && timeout != TimeSpan.FromMilliseconds(-1)) throw CommonImplementation.OutOfRange(nameof(timeout));

            return true;
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(RepeatEnumerable<Task> second, TimeSpan timeout)
        => WaitAllImpl<RepeatEnumerable<Task>, RepeatEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectIndexedEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator> second, TimeSpan timeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_Bridger : struct, IStructBridger<Task, TWaitAll_BridgeType, TWaitAll_ProjectedEnumerator>
            where TWaitAll_BridgeType : class
        => WaitAllImpl<SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second, TimeSpan timeout)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            TimeSpan timeout
        )
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            TimeSpan timeout
        )
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, TimeSpan timeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, TimeSpan timeout)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, TimeSpan timeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, TimeSpan timeout)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator> second, TimeSpan timeout)
           where TInnerEnumerable : struct, IStructEnumerable<Task, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task, TInnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, CastEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, OfTypeEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>(ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator> second, TimeSpan timeout)
            where TWaitAll_ZipFirstEnumerable : struct, IStructEnumerable<TWaitAll_ZipFirsTask, TWaitAll_ZipFirstEnumerator>
            where TWaitAll_ZipFirstEnumerator : struct, IStructEnumerator<TWaitAll_ZipFirsTask>
            where TWaitAll_ZipSecondEnumerable : struct, IStructEnumerable<TWaitAll_ZipSecondItem, TWaitAll_ZipSecondEnumerator>
            where TWaitAll_ZipSecondEnumerator : struct, IStructEnumerator<TWaitAll_ZipSecondItem>
        => WaitAllImpl<ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>, ZipEnumerator<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>(SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection> second, TimeSpan timeout)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>, SelectSelectEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>(SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate> second, TimeSpan timeout)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectPredicate : struct, IStructPredicate<Task>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>, SelectWhereEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>(WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate> second, TimeSpan timeout)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_WherePredicate : struct, IStructPredicate<Task>
        => WaitAllImpl<WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>, WhereWhereEnumerator<Task, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>(WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection> second, TimeSpan timeout)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<TWaitAll_WhereInnerItem>
            where TWaitAll_WherePredicate : struct, IStructPredicate<TWaitAll_WhereInnerItem>
            where TWaitAll_WhereProjection : struct, IStructProjection<Task, TWaitAll_WhereInnerItem>
        => WaitAllImpl<WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>, WhereSelectEnumerator<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, TimeSpan timeout)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, TimeSpan timeout)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, timeout);

        // intentionally not inlining since this isn't just delegating a method call
        public static bool WaitAll(EmptyOrderedEnumerable<Task> second, TimeSpan timeout)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (timeout < TimeSpan.Zero && timeout != TimeSpan.FromMilliseconds(-1)) throw CommonImplementation.OutOfRange(nameof(timeout));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, TimeSpan timeout)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, TimeSpan timeout)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, TimeSpan timeout)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, TimeSpan timeout)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, TimeSpan timeout)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, TimeSpan timeout)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupedKey>(GroupedEnumerable<TGroupedKey, Task> second, TimeSpan timeout)
        => WaitAllImpl<GroupedEnumerable<TGroupedKey, Task>, GroupedEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupedKey>(GroupingEnumerable<TGroupedKey, Task> second, TimeSpan timeout)
        => WaitAllImpl<GroupingEnumerable<TGroupedKey, Task>, GroupingEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator> second, TimeSpan timeout)
            where TReverseEnumerable : struct, IStructEnumerable<Task, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task>>(ref second, timeout);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, TimeSpan timeout)
            where TOrderByEnumerable : struct, IStructEnumerable<Task, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task>
            where TOrderByComparer : struct, IStructComparer<Task, TOrderByKey>
        => WaitAllImpl<OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            TimeSpan timeout
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinDefaultEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            TimeSpan timeout
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinSpecificEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            TimeSpan timeout
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            TimeSpan timeout
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            TimeSpan timeout
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinDefaultEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            TimeSpan timeout
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinSpecificEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemDefaultEnumerable<Task> second, TimeSpan timeout)
        => WaitAllImpl<OneItemDefaultEnumerable<Task>, OneItemDefaultEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemSpecificEnumerable<Task> second, TimeSpan timeout)
        => WaitAllImpl<OneItemSpecificEnumerable<Task>, OneItemSpecificEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemDefaultOrderedEnumerable<Task> second, TimeSpan timeout)
        => WaitAllImpl<OneItemDefaultOrderedEnumerable<Task>, OneItemDefaultOrderedEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemSpecificOrderedEnumerable<Task> second, TimeSpan timeout)
        => WaitAllImpl<OneItemSpecificOrderedEnumerable<Task>, OneItemSpecificOrderedEnumerator<Task>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, AppendEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, PrependEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, TimeSpan timeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, timeout);

        #endregion

        #region int timeout, no cancelation
        static bool WaitAllImpl<TEnumerable, TEnumerator>(ref TEnumerable e, int millisecondsTimeout)
            where TEnumerable : struct, IStructEnumerable<Task, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task, TEnumerable, TEnumerator>(ref e);
            return Task.WaitAll(arr, millisecondsTimeout);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(BoxedEnumerable<Task> second, int millisecondsTimeout)
        => WaitAllImpl<BoxedEnumerable<Task>, BoxedEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>(IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator> second, int millisecondsTimeout)
            where TWaitAll_IdentityEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_IdentityBridger : struct, IStructBridger<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityEnumerator>
            where TWaitAll_IdentityBridgeType : class
        => WaitAllImpl<IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>, TWaitAll_IdentityEnumerator>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, int millisecondsTimeout)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second, millisecondsTimeout);

        // intentionally not inlining since this isn't just delegating a method call
        public static bool WaitAll(EmptyEnumerable<Task> second, int millisecondsTimeout)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (millisecondsTimeout < 0 && millisecondsTimeout != -1) throw CommonImplementation.OutOfRange(nameof(millisecondsTimeout));

            return true;
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(RepeatEnumerable<Task> second, int millisecondsTimeout)
        => WaitAllImpl<RepeatEnumerable<Task>, RepeatEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectIndexedEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator> second, int millisecondsTimeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_Bridger : struct, IStructBridger<Task, TWaitAll_BridgeType, TWaitAll_ProjectedEnumerator>
            where TWaitAll_BridgeType : class
        => WaitAllImpl<SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second, int millisecondsTimeout)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            int millisecondsTimeout
        )
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            int millisecondsTimeout
        )
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, int millisecondsTimeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, int millisecondsTimeout)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, int millisecondsTimeout)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, int millisecondsTimeout)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator> second, int millisecondsTimeout)
           where TInnerEnumerable : struct, IStructEnumerable<Task, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task, TInnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, CastEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, OfTypeEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>(ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator> second, int millisecondsTimeout)
            where TWaitAll_ZipFirstEnumerable : struct, IStructEnumerable<TWaitAll_ZipFirsTask, TWaitAll_ZipFirstEnumerator>
            where TWaitAll_ZipFirstEnumerator : struct, IStructEnumerator<TWaitAll_ZipFirsTask>
            where TWaitAll_ZipSecondEnumerable : struct, IStructEnumerable<TWaitAll_ZipSecondItem, TWaitAll_ZipSecondEnumerator>
            where TWaitAll_ZipSecondEnumerator : struct, IStructEnumerator<TWaitAll_ZipSecondItem>
        => WaitAllImpl<ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>, ZipEnumerator<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>(SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection> second, int millisecondsTimeout)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>, SelectSelectEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>(SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate> second, int millisecondsTimeout)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectPredicate : struct, IStructPredicate<Task>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>, SelectWhereEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>(WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate> second, int millisecondsTimeout)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_WherePredicate : struct, IStructPredicate<Task>
        => WaitAllImpl<WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>, WhereWhereEnumerator<Task, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>(WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection> second, int millisecondsTimeout)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<TWaitAll_WhereInnerItem>
            where TWaitAll_WherePredicate : struct, IStructPredicate<TWaitAll_WhereInnerItem>
            where TWaitAll_WhereProjection : struct, IStructProjection<Task, TWaitAll_WhereInnerItem>
        => WaitAllImpl<WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>, WhereSelectEnumerator<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, int millisecondsTimeout)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, int millisecondsTimeout)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, millisecondsTimeout);

        // intentionally not inlining since this isn't just delegating a method call
        public static bool WaitAll(EmptyOrderedEnumerable<Task> second, int millisecondsTimeout)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (millisecondsTimeout < 0 && millisecondsTimeout != -1) throw CommonImplementation.OutOfRange(nameof(millisecondsTimeout));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, int millisecondsTimeout)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, int millisecondsTimeout)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, int millisecondsTimeout)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, int millisecondsTimeout)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, int millisecondsTimeout)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, int millisecondsTimeout)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupedKey>(GroupedEnumerable<TGroupedKey, Task> second, int millisecondsTimeout)
        => WaitAllImpl<GroupedEnumerable<TGroupedKey, Task>, GroupedEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupedKey>(GroupingEnumerable<TGroupedKey, Task> second, int millisecondsTimeout)
        => WaitAllImpl<GroupingEnumerable<TGroupedKey, Task>, GroupingEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator> second, int millisecondsTimeout)
            where TReverseEnumerable : struct, IStructEnumerable<Task, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task>>(ref second, millisecondsTimeout);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, int millisecondsTimeout)
            where TOrderByEnumerable : struct, IStructEnumerable<Task, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task>
            where TOrderByComparer : struct, IStructComparer<Task, TOrderByKey>
        => WaitAllImpl<OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            int millisecondsTimeout
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinDefaultEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            int millisecondsTimeout
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinSpecificEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            int millisecondsTimeout
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            int millisecondsTimeout
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            int millisecondsTimeout
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinDefaultEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            int millisecondsTimeout
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinSpecificEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemDefaultEnumerable<Task> second, int millisecondsTimeout)
        => WaitAllImpl<OneItemDefaultEnumerable<Task>, OneItemDefaultEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemSpecificEnumerable<Task> second, int millisecondsTimeout)
        => WaitAllImpl<OneItemSpecificEnumerable<Task>, OneItemSpecificEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemDefaultOrderedEnumerable<Task> second, int millisecondsTimeout)
        => WaitAllImpl<OneItemDefaultOrderedEnumerable<Task>, OneItemDefaultOrderedEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemSpecificOrderedEnumerable<Task> second, int millisecondsTimeout)
        => WaitAllImpl<OneItemSpecificOrderedEnumerable<Task>, OneItemSpecificOrderedEnumerator<Task>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, AppendEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, PrependEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout);

        #endregion

        #region no timeout, cancelation
        static void WaitAllImpl<TEnumerable, TEnumerator>(ref TEnumerable e, CancellationToken cancellationToken)
            where TEnumerable : struct, IStructEnumerable<Task, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task, TEnumerable, TEnumerator>(ref e);
            Task.WaitAll(arr, cancellationToken);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(BoxedEnumerable<Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<BoxedEnumerable<Task>, BoxedEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>(IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_IdentityEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_IdentityBridger : struct, IStructBridger<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityEnumerator>
            where TWaitAll_IdentityBridgeType : class
        => WaitAllImpl<IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>, TWaitAll_IdentityEnumerator>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, CancellationToken cancellationToken)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second, cancellationToken);

        // intentionally not inlining since this isn't just delegating a method call
        public static void WaitAll(EmptyEnumerable<Task> second, CancellationToken cancellationToken)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(RepeatEnumerable<Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<RepeatEnumerable<Task>, RepeatEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectIndexedEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_Bridger : struct, IStructBridger<Task, TWaitAll_BridgeType, TWaitAll_ProjectedEnumerator>
            where TWaitAll_BridgeType : class
        => WaitAllImpl<SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            CancellationToken cancellationToken
        )
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            CancellationToken cancellationToken
        )
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator> second, CancellationToken cancellationToken)
           where TInnerEnumerable : struct, IStructEnumerable<Task, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task, TInnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, CastEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, OfTypeEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>(ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_ZipFirstEnumerable : struct, IStructEnumerable<TWaitAll_ZipFirsTask, TWaitAll_ZipFirstEnumerator>
            where TWaitAll_ZipFirstEnumerator : struct, IStructEnumerator<TWaitAll_ZipFirsTask>
            where TWaitAll_ZipSecondEnumerable : struct, IStructEnumerable<TWaitAll_ZipSecondItem, TWaitAll_ZipSecondEnumerator>
            where TWaitAll_ZipSecondEnumerator : struct, IStructEnumerator<TWaitAll_ZipSecondItem>
        => WaitAllImpl<ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>, ZipEnumerator<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>(SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection> second, CancellationToken cancellationToken)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>, SelectSelectEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>(SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate> second, CancellationToken cancellationToken)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectPredicate : struct, IStructPredicate<Task>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>, SelectWhereEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>(WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate> second, CancellationToken cancellationToken)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_WherePredicate : struct, IStructPredicate<Task>
        => WaitAllImpl<WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>, WhereWhereEnumerator<Task, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>(WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection> second, CancellationToken cancellationToken)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<TWaitAll_WhereInnerItem>
            where TWaitAll_WherePredicate : struct, IStructPredicate<TWaitAll_WhereInnerItem>
            where TWaitAll_WhereProjection : struct, IStructProjection<Task, TWaitAll_WhereInnerItem>
        => WaitAllImpl<WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>, WhereSelectEnumerator<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, CancellationToken cancellationToken)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, cancellationToken);

        // intentionally not inlining since this isn't just delegating a method call
        public static void WaitAll(EmptyOrderedEnumerable<Task> second, CancellationToken cancellationToken)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, CancellationToken cancellationToken)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, CancellationToken cancellationToken)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, CancellationToken cancellationToken)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, CancellationToken cancellationToken)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, CancellationToken cancellationToken)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, CancellationToken cancellationToken)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupedKey>(GroupedEnumerable<TGroupedKey, Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<GroupedEnumerable<TGroupedKey, Task>, GroupedEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupedKey>(GroupingEnumerable<TGroupedKey, Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<GroupingEnumerable<TGroupedKey, Task>, GroupingEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator> second, CancellationToken cancellationToken)
            where TReverseEnumerable : struct, IStructEnumerable<Task, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task>>(ref second, cancellationToken);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, CancellationToken cancellationToken)
            where TOrderByEnumerable : struct, IStructEnumerable<Task, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task>
            where TOrderByComparer : struct, IStructComparer<Task, TOrderByKey>
        => WaitAllImpl<OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            CancellationToken cancellationToken
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinDefaultEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            CancellationToken cancellationToken
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinSpecificEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            CancellationToken cancellationToken
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            CancellationToken cancellationToken
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            CancellationToken cancellationToken
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinDefaultEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            CancellationToken cancellationToken
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinSpecificEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemDefaultEnumerable<Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemDefaultEnumerable<Task>, OneItemDefaultEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemSpecificEnumerable<Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemSpecificEnumerable<Task>, OneItemSpecificEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemDefaultOrderedEnumerable<Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemDefaultOrderedEnumerable<Task>, OneItemDefaultOrderedEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(OneItemSpecificOrderedEnumerable<Task> second, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemSpecificOrderedEnumerable<Task>, OneItemSpecificOrderedEnumerator<Task>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, AppendEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, PrependEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, cancellationToken);
        #endregion

        // these is no timespan timeout with cancelation

        #region int timeout, cancelation
        static bool WaitAllImpl<TEnumerable, TEnumerator>(ref TEnumerable e, int millisecondsTimeout, CancellationToken cancellationToken)
            where TEnumerable : struct, IStructEnumerable<Task, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task, TEnumerable, TEnumerator>(ref e);
            return Task.WaitAll(arr, millisecondsTimeout, cancellationToken);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(BoxedEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<BoxedEnumerable<Task>, BoxedEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>(IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_IdentityEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_IdentityBridger : struct, IStructBridger<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityEnumerator>
            where TWaitAll_IdentityBridgeType : class
        => WaitAllImpl<IdentityEnumerable<Task, TWaitAll_IdentityBridgeType, TWaitAll_IdentityBridger, TWaitAll_IdentityEnumerator>, TWaitAll_IdentityEnumerator>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        // intentionally not inlining since this isn't just delegating a method call
        public static bool WaitAll(EmptyEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (millisecondsTimeout < 0 && millisecondsTimeout != -1) throw CommonImplementation.OutOfRange(nameof(millisecondsTimeout));

            return true;
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(RepeatEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<RepeatEnumerable<Task>, RepeatEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInItem>
        => WaitAllImpl<SelectIndexedEnumerable<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SelectIndexedEnumerator<TWaitAll_SelectInItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_Bridger : struct, IStructBridger<Task, TWaitAll_BridgeType, TWaitAll_ProjectedEnumerator>
            where TWaitAll_BridgeType : class
        => WaitAllImpl<SelectManyBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_BridgeType, TWaitAll_Bridger, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_InnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
            where TWaitAll_SelectManyBridger : struct, IStructBridger<TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyBridgeType : class
        => WaitAllImpl<SelectManyCollectionIndexedBridgeEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyBridgeType, TWaitAll_SelectManyBridger, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SelectManyIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_InnerEnumerator>
            where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_ProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_ProjectedEnumerator>
            where TWaitAll_ProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_InnerEnumerator, TWaitAll_ProjectedEnumerable, TWaitAll_ProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectManyInItem, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectManyInItem, TWaitAll_SelectManyInnerEnumerator>
            where TWaitAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectManyInItem>
            where TWaitAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWaitAll_CollectionItem, TWaitAll_SelectManyProjectedEnumerator>
            where TWaitAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWaitAll_CollectionItem>
        => WaitAllImpl<SelectManyCollectionIndexedEnumerable<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerable, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWaitAll_SelectManyInItem, Task, TWaitAll_CollectionItem, TWaitAll_SelectManyInnerEnumerator, TWaitAll_SelectManyProjectedEnumerable, TWaitAll_SelectManyProjectedEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<WhereIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, WhereIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptyDefaultEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DefaultIfEmptySpecificEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TInnerEnumerable : struct, IStructEnumerable<Task, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task, TInnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipWhileIndexedEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<CastEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, CastEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InItem, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<TWaitAll_InItem, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<TWaitAll_InItem>
        => WaitAllImpl<OfTypeEnumerable<TWaitAll_InItem, Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, OfTypeEnumerator<TWaitAll_InItem, Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>(ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_ZipFirstEnumerable : struct, IStructEnumerable<TWaitAll_ZipFirsTask, TWaitAll_ZipFirstEnumerator>
            where TWaitAll_ZipFirstEnumerator : struct, IStructEnumerator<TWaitAll_ZipFirsTask>
            where TWaitAll_ZipSecondEnumerable : struct, IStructEnumerable<TWaitAll_ZipSecondItem, TWaitAll_ZipSecondEnumerator>
            where TWaitAll_ZipSecondEnumerator : struct, IStructEnumerator<TWaitAll_ZipSecondItem>
        => WaitAllImpl<ZipEnumerable<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerable, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerable, TWaitAll_ZipSecondEnumerator>, ZipEnumerator<Task, TWaitAll_ZipFirsTask, TWaitAll_ZipSecondItem, TWaitAll_ZipFirstEnumerator, TWaitAll_ZipSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>(SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectSelectEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>, SelectSelectEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>(SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_SelectInnerEnumerable : struct, IStructEnumerable<TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator>
            where TWaitAll_SelectInnerEnumerator : struct, IStructEnumerator<TWaitAll_SelectInnerItem>
            where TWaitAll_SelectPredicate : struct, IStructPredicate<Task>
            where TWaitAll_SelectProjection : struct, IStructProjection<Task, TWaitAll_SelectInnerItem>
        => WaitAllImpl<SelectWhereEnumerable<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerable, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>, SelectWhereEnumerator<Task, TWaitAll_SelectInnerItem, TWaitAll_SelectInnerEnumerator, TWaitAll_SelectProjection, TWaitAll_SelectPredicate>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>(WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<Task>
            where TWaitAll_WherePredicate : struct, IStructPredicate<Task>
        => WaitAllImpl<WhereWhereEnumerable<Task, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>, WhereWhereEnumerator<Task, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>(WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_WhereInnerEnumerable : struct, IStructEnumerable<TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator>
            where TWaitAll_WhereInnerEnumerator : struct, IStructEnumerator<TWaitAll_WhereInnerItem>
            where TWaitAll_WherePredicate : struct, IStructPredicate<TWaitAll_WhereInnerItem>
            where TWaitAll_WhereProjection : struct, IStructProjection<Task, TWaitAll_WhereInnerItem>
        => WaitAllImpl<WhereSelectEnumerable<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerable, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>, WhereSelectEnumerator<Task, TWaitAll_WhereInnerItem, TWaitAll_WhereInnerEnumerator, TWaitAll_WherePredicate, TWaitAll_WhereProjection>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctDefaultEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TWaitAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_DistinctInnerEnumerator>
            where TWaitAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<DistinctSpecificEnumerable<Task, TWaitAll_DistinctInnerEnumerable, TWaitAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task, TWaitAll_DistinctInnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        // intentionally not inlining since this isn't just delegating a method call
        public static bool WaitAll(EmptyOrderedEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            if (millisecondsTimeout < 0 && millisecondsTimeout != -1) throw CommonImplementation.OutOfRange(nameof(millisecondsTimeout));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupedKey>(GroupedEnumerable<TGroupedKey, Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<GroupedEnumerable<TGroupedKey, Task>, GroupedEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupedKey>(GroupingEnumerable<TGroupedKey, Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<GroupingEnumerable<TGroupedKey, Task>, GroupingEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TReverseEnumerable : struct, IStructEnumerable<Task, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second, int millisecondsTimeout, CancellationToken cancellationToken)
            where TOrderByEnumerable : struct, IStructEnumerable<Task, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task>
            where TOrderByComparer : struct, IStructComparer<Task, TOrderByKey>
        => WaitAllImpl<OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinDefaultEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinDefaultEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TGroupWaitAllLeftEnumerable : struct, IStructEnumerable<TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator>
            where TGroupWaitAllLeftEnumerator : struct, IStructEnumerator<TGroupWaitAllLefTask>
            where TGroupWaitAllRightEnumerable : struct, IStructEnumerable<TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>
            where TGroupWaitAllRightEnumerator : struct, IStructEnumerator<TGroupWaitAllRighTask>
        => WaitAllImpl<GroupJoinSpecificEnumerable<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerable, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerable, TGroupWaitAllRightEnumerator>, GroupJoinSpecificEnumerator<Task, TGroupWaitAllKeyItem, TGroupWaitAllLefTask, TGroupWaitAllLeftEnumerator, TGroupWaitAllRighTask, TGroupWaitAllRightEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WaitAllImpl<GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinDefaultEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinDefaultEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>(
            JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator> second,
            int millisecondsTimeout,
            CancellationToken cancellationToken
        )
            where TWaitAllLeftEnumerable : struct, IStructEnumerable<TWaitAllLefTask, TWaitAllLeftEnumerator>
            where TWaitAllLeftEnumerator : struct, IStructEnumerator<TWaitAllLefTask>
            where TWaitAllRightEnumerable : struct, IStructEnumerable<TWaitAllRighTask, TWaitAllRightEnumerator>
            where TWaitAllRightEnumerator : struct, IStructEnumerator<TWaitAllRighTask>
        => WaitAllImpl<JoinSpecificEnumerable<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerable, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerable, TWaitAllRightEnumerator>, JoinSpecificEnumerator<Task, TWaitAllKeyItem, TWaitAllLefTask, TWaitAllLeftEnumerator, TWaitAllRighTask, TWaitAllRightEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemDefaultEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemDefaultEnumerable<Task>, OneItemDefaultEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemSpecificEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemSpecificEnumerable<Task>, OneItemSpecificEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemDefaultOrderedEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemDefaultOrderedEnumerable<Task>, OneItemDefaultOrderedEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll(OneItemSpecificOrderedEnumerable<Task> second, int millisecondsTimeout, CancellationToken cancellationToken)
        => WaitAllImpl<OneItemSpecificOrderedEnumerable<Task>, OneItemSpecificOrderedEnumerator<Task>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<AppendEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, AppendEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<PrependEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, PrependEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<SkipLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, SkipLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitAll<TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>(TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator> second, int millisecondsTimeout, CancellationToken cancellationToken)
           where TWaitAll_InnerEnumerable : struct, IStructEnumerable<Task, TWaitAll_InnerEnumerator>
           where TWaitAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WaitAllImpl<TakeLastEnumerable<Task, TWaitAll_InnerEnumerable, TWaitAll_InnerEnumerator>, TakeLastEnumerator<Task, TWaitAll_InnerEnumerator>>(ref second, millisecondsTimeout, cancellationToken);
        #endregion
    }
}
