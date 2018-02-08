using LinqAF.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LinqAF
{
    public static partial class LinqAFTask
    {
        #region no value
        static Task WhenAllImpl<TEnumerable, TEnumerator>(ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<Task, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task, TEnumerable, TEnumerator>(ref e);
            return Task.WhenAll(arr);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult>(BoxedEnumerable<Task> second)
        => WhenAllImpl<BoxedEnumerable<Task>, BoxedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_IdentityBridgeType, TWhenAll_IdentityBridger, TWhenAll_IdentityEnumerator>(IdentityEnumerable<Task, TWhenAll_IdentityBridgeType, TWhenAll_IdentityBridger, TWhenAll_IdentityEnumerator> second)
            where TWhenAll_IdentityEnumerator : struct, IStructEnumerator<Task>
            where TWhenAll_IdentityBridger : struct, IStructBridger<Task, TWhenAll_IdentityBridgeType, TWhenAll_IdentityEnumerator>
            where TWhenAll_IdentityBridgeType : class
        => WhenAllImpl<IdentityEnumerable<Task, TWhenAll_IdentityBridgeType, TWhenAll_IdentityBridger, TWhenAll_IdentityEnumerator>, TWhenAll_IdentityEnumerator>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<ConcatEnumerable<Task, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second);

        // intentionally not inlining since this isn't just delegating a method call
        public static Task WhenAll<TResult>(EmptyEnumerable<Task> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return Task.CompletedTask;
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult>(RepeatEnumerable<Task> second)
        => WhenAllImpl<RepeatEnumerable<Task>, RepeatEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectInItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SelectEnumerable<TWhenAll_SelectInItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInItem>
        => WhenAllImpl<SelectEnumerable<TWhenAll_SelectInItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SelectEnumerator<TWhenAll_SelectInItem, Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectInItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SelectIndexedEnumerable<TWhenAll_SelectInItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInItem, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInItem>
        => WhenAllImpl<SelectIndexedEnumerable<TWhenAll_SelectInItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SelectIndexedEnumerator<TWhenAll_SelectInItem, Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWhenAll_Bridger : struct, IStructBridger<Task, TWhenAll_BridgeType, TWhenAll_ProjectedEnumerator>
            where TWhenAll_BridgeType : class
        => WhenAllImpl<SelectManyBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator> second)
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
            where TWhenAll_SelectManyBridger : struct, IStructBridger<Task, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyBridgeType : class
        => WhenAllImpl<SelectManyIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator> second
        )
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
            where TWhenAll_SelectManyBridger : struct, IStructBridger<TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyBridgeType : class
        => WhenAllImpl<SelectManyCollectionBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator> second
        )
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
            where TWhenAll_SelectManyBridger : struct, IStructBridger<TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyBridgeType : class
        => WhenAllImpl<SelectManyCollectionIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>(SelectManyEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_ProjectedEnumerable : struct, IStructEnumerable<Task, TWhenAll_ProjectedEnumerator>
            where TWhenAll_ProjectedEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<SelectManyEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>, SelectManyEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator> second)
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<SelectManyIndexedEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_ProjectedEnumerable : struct, IStructEnumerable<TWhenAll_CollectionItem, TWhenAll_ProjectedEnumerator>
            where TWhenAll_ProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
        => WhenAllImpl<SelectManyCollectionEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator> second)
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWhenAll_CollectionItem, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
        => WhenAllImpl<SelectManyCollectionIndexedEnumerable<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWhenAll_SelectManyInItem, Task, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(WhereEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<WhereEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, WhereEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(WhereIndexedEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<WhereIndexedEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, WhereIndexedEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<DefaultIfEmptyDefaultEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<DefaultIfEmptySpecificEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(TakeEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<TakeEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, TakeEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(TakeWhileEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<TakeWhileEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, TakeWhileEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<Task, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<TakeWhileIndexedEnumerable<Task, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task, TInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<SkipEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipWhileEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<SkipWhileEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipWhileEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<SkipWhileIndexedEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(CastEnumerable<TWhenAll_InItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_InItem, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_InItem>
        => WhenAllImpl<CastEnumerable<TWhenAll_InItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, CastEnumerator<TWhenAll_InItem, Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(OfTypeEnumerable<TWhenAll_InItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_InItem, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_InItem>
        => WhenAllImpl<OfTypeEnumerable<TWhenAll_InItem, Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, OfTypeEnumerator<TWhenAll_InItem, Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerable, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerable, TWhenAll_ZipSecondEnumerator>(ZipEnumerable<Task, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerable, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerable, TWhenAll_ZipSecondEnumerator> second)
            where TWhenAll_ZipFirstEnumerable : struct, IStructEnumerable<TWhenAll_ZipFirsTask, TWhenAll_ZipFirstEnumerator>
            where TWhenAll_ZipFirstEnumerator : struct, IStructEnumerator<TWhenAll_ZipFirsTask>
            where TWhenAll_ZipSecondEnumerable : struct, IStructEnumerable<TWhenAll_ZipSecondItem, TWhenAll_ZipSecondEnumerator>
            where TWhenAll_ZipSecondEnumerator : struct, IStructEnumerator<TWhenAll_ZipSecondItem>
        => WhenAllImpl<ZipEnumerable<Task, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerable, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerable, TWhenAll_ZipSecondEnumerator>, ZipEnumerator<Task, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection>(SelectSelectEnumerable<Task, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection> second)
            where TWhenAll_SelectInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator>
            where TWhenAll_SelectInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInnerItem>
            where TWhenAll_SelectProjection : struct, IStructProjection<Task, TWhenAll_SelectInnerItem>
        => WhenAllImpl<SelectSelectEnumerable<Task, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection>, SelectSelectEnumerator<Task, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate>(SelectWhereEnumerable<Task, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate> second)
            where TWhenAll_SelectInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator>
            where TWhenAll_SelectInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInnerItem>
            where TWhenAll_SelectPredicate : struct, IStructPredicate<Task>
            where TWhenAll_SelectProjection : struct, IStructProjection<Task, TWhenAll_SelectInnerItem>
        => WhenAllImpl<SelectWhereEnumerable<Task, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate>, SelectWhereEnumerator<Task, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate>(WhereWhereEnumerable<Task, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate> second)
            where TWhenAll_WhereInnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_WhereInnerEnumerator>
            where TWhenAll_WhereInnerEnumerator : struct, IStructEnumerator<Task>
            where TWhenAll_WherePredicate : struct, IStructPredicate<Task>
        => WhenAllImpl<WhereWhereEnumerable<Task, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate>, WhereWhereEnumerator<Task, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection>(WhereSelectEnumerable<Task, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection> second)
            where TWhenAll_WhereInnerEnumerable : struct, IStructEnumerable<TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerator>
            where TWhenAll_WhereInnerEnumerator : struct, IStructEnumerator<TWhenAll_WhereInnerItem>
            where TWhenAll_WherePredicate : struct, IStructPredicate<TWhenAll_WhereInnerItem>
            where TWhenAll_WhereProjection : struct, IStructProjection<Task, TWhenAll_WhereInnerItem>
        => WhenAllImpl<WhereSelectEnumerable<Task, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection>, WhereSelectEnumerator<Task, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator> second)
            where TWhenAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_DistinctInnerEnumerator>
            where TWhenAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<DistinctDefaultEnumerable<Task, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task, TWhenAll_DistinctInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator> second)
            where TWhenAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_DistinctInnerEnumerator>
            where TWhenAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<DistinctSpecificEnumerable<Task, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task, TWhenAll_DistinctInnerEnumerator>>(ref second);

        // intentionally not inlining since this isn't just delegating a method call
        public static Task WhenAll<TResult>(EmptyOrderedEnumerable<Task> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return Task.CompletedTask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<ExceptDefaultEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<ExceptSpecificEnumerable<Task, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<IntersectDefaultEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<IntersectSpecificEnumerable<Task, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<UnionDefaultEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<UnionSpecificEnumerable<Task, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TGroupedKey>(GroupedEnumerable<TGroupedKey, Task> second)
        => WhenAllImpl<GroupedEnumerable<TGroupedKey, Task>, GroupedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TGroupedKey>(GroupingEnumerable<TGroupedKey, Task> second)
        => WhenAllImpl<GroupingEnumerable<TGroupedKey, Task>, GroupingEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<Task, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<ReverseEnumerable<Task, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task>>(ref second);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<Task, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task>
            where TOrderByComparer : struct, IStructComparer<Task, TOrderByKey>
        => WhenAllImpl<OrderByEnumerable<Task, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator> second
        )
            where TGroupWhenAllLeftEnumerable : struct, IStructEnumerable<TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator>
            where TGroupWhenAllLeftEnumerator : struct, IStructEnumerator<TGroupWhenAllLefTask>
            where TGroupWhenAllRightEnumerable : struct, IStructEnumerable<TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>
            where TGroupWhenAllRightEnumerator : struct, IStructEnumerator<TGroupWhenAllRighTask>
        => WhenAllImpl<GroupJoinDefaultEnumerable<Task, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>, GroupJoinDefaultEnumerator<Task, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator> second
        )
            where TGroupWhenAllLeftEnumerable : struct, IStructEnumerable<TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator>
            where TGroupWhenAllLeftEnumerator : struct, IStructEnumerator<TGroupWhenAllLefTask>
            where TGroupWhenAllRightEnumerable : struct, IStructEnumerable<TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>
            where TGroupWhenAllRightEnumerator : struct, IStructEnumerator<TGroupWhenAllRighTask>
        => WhenAllImpl<GroupJoinSpecificEnumerable<Task, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>, GroupJoinSpecificEnumerator<Task, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WhenAllImpl<GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WhenAllImpl<GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>(
            JoinDefaultEnumerable<Task, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator> second
        )
            where TWhenAllLeftEnumerable : struct, IStructEnumerable<TWhenAllLefTask, TWhenAllLeftEnumerator>
            where TWhenAllLeftEnumerator : struct, IStructEnumerator<TWhenAllLefTask>
            where TWhenAllRightEnumerable : struct, IStructEnumerable<TWhenAllRighTask, TWhenAllRightEnumerator>
            where TWhenAllRightEnumerator : struct, IStructEnumerator<TWhenAllRighTask>
        => WhenAllImpl<JoinDefaultEnumerable<Task, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>, JoinDefaultEnumerator<Task, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>(
            JoinSpecificEnumerable<Task, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator> second
        )
            where TWhenAllLeftEnumerable : struct, IStructEnumerable<TWhenAllLefTask, TWhenAllLeftEnumerator>
            where TWhenAllLeftEnumerator : struct, IStructEnumerator<TWhenAllLefTask>
            where TWhenAllRightEnumerable : struct, IStructEnumerable<TWhenAllRighTask, TWhenAllRightEnumerator>
            where TWhenAllRightEnumerator : struct, IStructEnumerator<TWhenAllRighTask>
        => WhenAllImpl<JoinSpecificEnumerable<Task, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>, JoinSpecificEnumerator<Task, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult>(OneItemDefaultEnumerable<Task> second)
        => WhenAllImpl<OneItemDefaultEnumerable<Task>, OneItemDefaultEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult>(OneItemSpecificEnumerable<Task> second)
        => WhenAllImpl<OneItemSpecificEnumerable<Task>, OneItemSpecificEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult>(OneItemDefaultOrderedEnumerable<Task> second)
        => WhenAllImpl<OneItemDefaultOrderedEnumerable<Task>, OneItemDefaultOrderedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult>(OneItemSpecificOrderedEnumerable<Task> second)
        => WhenAllImpl<OneItemSpecificOrderedEnumerable<Task>, OneItemSpecificOrderedEnumerator<Task>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(AppendEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<AppendEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, AppendEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(PrependEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<PrependEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, PrependEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipLastEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<SkipLastEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipLastEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(TakeLastEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task>
        => WhenAllImpl<TakeLastEnumerable<Task, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, TakeLastEnumerator<Task, TWhenAll_InnerEnumerator>>(ref second);
        #endregion

        #region value
        static Task<TResult[]> WhenAllImpl<TResult, TEnumerable, TEnumerator>(ref TEnumerable e)
            where TEnumerable : struct, IStructEnumerable<Task<TResult>, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<Task<TResult>>
        {
            if (e.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(e));

            var arr = CommonImplementation.ToArrayImpl<Task<TResult>, TEnumerable, TEnumerator>(ref e);
            return Task.WhenAll(arr);
        }

        // No GroupBy* or Lookup*, as they can't yield pure Tasks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult>(BoxedEnumerable<Task<TResult>> second)
        => WhenAllImpl<TResult, BoxedEnumerable<Task<TResult>>, BoxedEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_IdentityBridgeType, TWhenAll_IdentityBridger, TWhenAll_IdentityEnumerator>(IdentityEnumerable<Task<TResult>, TWhenAll_IdentityBridgeType, TWhenAll_IdentityBridger, TWhenAll_IdentityEnumerator> second)
            where TWhenAll_IdentityEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TWhenAll_IdentityBridger : struct, IStructBridger<Task<TResult>, TWhenAll_IdentityBridgeType, TWhenAll_IdentityEnumerator>
            where TWhenAll_IdentityBridgeType : class
        => WhenAllImpl<TResult, IdentityEnumerable<Task<TResult>, TWhenAll_IdentityBridgeType, TWhenAll_IdentityBridger, TWhenAll_IdentityEnumerator>, TWhenAll_IdentityEnumerator>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>(ConcatEnumerable<Task<TResult>, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator> second)
            where TInnerLeftEnumerable : struct, IStructEnumerable<Task<TResult>, TInnerLeftEnumerator>
            where TInnerLeftEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TInnerRightEnumerable : struct, IStructEnumerable<Task<TResult>, TInnerRightEnumerator>
            where TInnerRightEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, ConcatEnumerable<Task<TResult>, TInnerLeftEnumerable, TInnerLeftEnumerator, TInnerRightEnumerable, TInnerRightEnumerator>, ConcatEnumerator<Task<TResult>, TInnerLeftEnumerator, TInnerRightEnumerator>>(ref second);

        // intentionally not inlining since this isn't just delegating a method call
        public static Task<TResult[]> WhenAll<TResult>(EmptyEnumerable<Task<TResult>> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return Task.FromResult(Array.Empty<TResult>());
        }

        // No Range as it's always bound to int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult>(RepeatEnumerable<Task<TResult>> second)
        => WhenAllImpl<TResult, RepeatEnumerable<Task<TResult>>, RepeatEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectInItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SelectEnumerable<TWhenAll_SelectInItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInItem>
        => WhenAllImpl<TResult, SelectEnumerable<TWhenAll_SelectInItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SelectEnumerator<TWhenAll_SelectInItem, Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectInItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SelectIndexedEnumerable<TWhenAll_SelectInItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInItem, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInItem>
        => WhenAllImpl<TResult, SelectIndexedEnumerable<TWhenAll_SelectInItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SelectIndexedEnumerator<TWhenAll_SelectInItem, Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator>(SelectManyBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_ProjectedEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TWhenAll_Bridger : struct, IStructBridger<Task<TResult>, TWhenAll_BridgeType, TWhenAll_ProjectedEnumerator>
            where TWhenAll_BridgeType : class
        => WhenAllImpl<TResult, SelectManyBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator>, SelectManyBridgeEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_BridgeType, TWhenAll_Bridger, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator> second)
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TWhenAll_SelectManyBridger : struct, IStructBridger<Task<TResult>, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyBridgeType : class
        => WhenAllImpl<TResult, SelectManyIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>, SelectManyIndexedBridgeEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator> second
        )
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
            where TWhenAll_SelectManyBridger : struct, IStructBridger<TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyBridgeType : class
        => WhenAllImpl<TResult, SelectManyCollectionBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>, SelectManyCollectionBridgeEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_InnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>(
            SelectManyCollectionIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator> second
        )
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
            where TWhenAll_SelectManyBridger : struct, IStructBridger<TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyBridgeType : class
        => WhenAllImpl<TResult, SelectManyCollectionIndexedBridgeEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedBridgeEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyBridgeType, TWhenAll_SelectManyBridger, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>(SelectManyEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_ProjectedEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_ProjectedEnumerator>
            where TWhenAll_ProjectedEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, SelectManyEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>, SelectManyEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator> second)
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, SelectManyIndexedEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>, SelectManyIndexedEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>(SelectManyCollectionEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator> second)
            where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_InnerEnumerator>
            where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_ProjectedEnumerable : struct, IStructEnumerable<TWhenAll_CollectionItem, TWhenAll_ProjectedEnumerator>
            where TWhenAll_ProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
        => WhenAllImpl<TResult, SelectManyCollectionEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>, SelectManyCollectionEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_InnerEnumerator, TWhenAll_ProjectedEnumerable, TWhenAll_ProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectManyInItem, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator> second)
            where TWhenAll_SelectManyInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectManyInItem, TWhenAll_SelectManyInnerEnumerator>
            where TWhenAll_SelectManyInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectManyInItem>
            where TWhenAll_SelectManyProjectedEnumerable : struct, IStructEnumerable<TWhenAll_CollectionItem, TWhenAll_SelectManyProjectedEnumerator>
            where TWhenAll_SelectManyProjectedEnumerator : struct, IStructEnumerator<TWhenAll_CollectionItem>
        => WhenAllImpl<TResult, SelectManyCollectionIndexedEnumerable<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerable, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>, SelectManyCollectionIndexedEnumerator<TWhenAll_SelectManyInItem, Task<TResult>, TWhenAll_CollectionItem, TWhenAll_SelectManyInnerEnumerator, TWhenAll_SelectManyProjectedEnumerable, TWhenAll_SelectManyProjectedEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(WhereEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, WhereEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, WhereEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(WhereIndexedEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, WhereIndexedEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, WhereIndexedEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(DefaultIfEmptyDefaultEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, DefaultIfEmptyDefaultEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, DefaultIfEmptyDefaultEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(DefaultIfEmptySpecificEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, DefaultIfEmptySpecificEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, DefaultIfEmptySpecificEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(TakeEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, TakeEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, TakeEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(TakeWhileEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, TakeWhileEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, TakeWhileEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TInnerEnumerable, TInnerEnumerator>(TakeWhileIndexedEnumerable<Task<TResult>, TInnerEnumerable, TInnerEnumerator> second)
           where TInnerEnumerable : struct, IStructEnumerable<Task<TResult>, TInnerEnumerator>
           where TInnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, TakeWhileIndexedEnumerable<Task<TResult>, TInnerEnumerable, TInnerEnumerator>, TakeWhileIndexedEnumerator<Task<TResult>, TInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, SkipEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipWhileEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, SkipWhileEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipWhileEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipWhileIndexedEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, SkipWhileIndexedEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipWhileIndexedEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(CastEnumerable<TWhenAll_InItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_InItem, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_InItem>
        => WhenAllImpl<TResult, CastEnumerable<TWhenAll_InItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, CastEnumerator<TWhenAll_InItem, Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InItem, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(OfTypeEnumerable<TWhenAll_InItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<TWhenAll_InItem, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<TWhenAll_InItem>
        => WhenAllImpl<TResult, OfTypeEnumerable<TWhenAll_InItem, Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, OfTypeEnumerator<TWhenAll_InItem, Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerable, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerable, TWhenAll_ZipSecondEnumerator>(ZipEnumerable<Task<TResult>, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerable, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerable, TWhenAll_ZipSecondEnumerator> second)
            where TWhenAll_ZipFirstEnumerable : struct, IStructEnumerable<TWhenAll_ZipFirsTask, TWhenAll_ZipFirstEnumerator>
            where TWhenAll_ZipFirstEnumerator : struct, IStructEnumerator<TWhenAll_ZipFirsTask>
            where TWhenAll_ZipSecondEnumerable : struct, IStructEnumerable<TWhenAll_ZipSecondItem, TWhenAll_ZipSecondEnumerator>
            where TWhenAll_ZipSecondEnumerator : struct, IStructEnumerator<TWhenAll_ZipSecondItem>
        => WhenAllImpl<TResult, ZipEnumerable<Task<TResult>, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerable, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerable, TWhenAll_ZipSecondEnumerator>, ZipEnumerator<Task<TResult>, TWhenAll_ZipFirsTask, TWhenAll_ZipSecondItem, TWhenAll_ZipFirstEnumerator, TWhenAll_ZipSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection>(SelectSelectEnumerable<Task<TResult>, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection> second)
            where TWhenAll_SelectInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator>
            where TWhenAll_SelectInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInnerItem>
            where TWhenAll_SelectProjection : struct, IStructProjection<Task<TResult>, TWhenAll_SelectInnerItem>
        => WhenAllImpl<TResult, SelectSelectEnumerable<Task<TResult>, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection>, SelectSelectEnumerator<Task<TResult>, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate>(SelectWhereEnumerable<Task<TResult>, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate> second)
            where TWhenAll_SelectInnerEnumerable : struct, IStructEnumerable<TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator>
            where TWhenAll_SelectInnerEnumerator : struct, IStructEnumerator<TWhenAll_SelectInnerItem>
            where TWhenAll_SelectPredicate : struct, IStructPredicate<Task<TResult>>
            where TWhenAll_SelectProjection : struct, IStructProjection<Task<TResult>, TWhenAll_SelectInnerItem>
        => WhenAllImpl<TResult, SelectWhereEnumerable<Task<TResult>, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerable, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate>, SelectWhereEnumerator<Task<TResult>, TWhenAll_SelectInnerItem, TWhenAll_SelectInnerEnumerator, TWhenAll_SelectProjection, TWhenAll_SelectPredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate>(WhereWhereEnumerable<Task<TResult>, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate> second)
            where TWhenAll_WhereInnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_WhereInnerEnumerator>
            where TWhenAll_WhereInnerEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TWhenAll_WherePredicate : struct, IStructPredicate<Task<TResult>>
        => WhenAllImpl<TResult, WhereWhereEnumerable<Task<TResult>, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate>, WhereWhereEnumerator<Task<TResult>, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection>(WhereSelectEnumerable<Task<TResult>, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection> second)
            where TWhenAll_WhereInnerEnumerable : struct, IStructEnumerable<TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerator>
            where TWhenAll_WhereInnerEnumerator : struct, IStructEnumerator<TWhenAll_WhereInnerItem>
            where TWhenAll_WherePredicate : struct, IStructPredicate<TWhenAll_WhereInnerItem>
            where TWhenAll_WhereProjection : struct, IStructProjection<Task<TResult>, TWhenAll_WhereInnerItem>
        => WhenAllImpl<TResult, WhereSelectEnumerable<Task<TResult>, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerable, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection>, WhereSelectEnumerator<Task<TResult>, TWhenAll_WhereInnerItem, TWhenAll_WhereInnerEnumerator, TWhenAll_WherePredicate, TWhenAll_WhereProjection>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>(DistinctDefaultEnumerable<Task<TResult>, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator> second)
            where TWhenAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_DistinctInnerEnumerator>
            where TWhenAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, DistinctDefaultEnumerable<Task<TResult>, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>, DistinctDefaultEnumerator<Task<TResult>, TWhenAll_DistinctInnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>(DistinctSpecificEnumerable<Task<TResult>, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator> second)
            where TWhenAll_DistinctInnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_DistinctInnerEnumerator>
            where TWhenAll_DistinctInnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, DistinctSpecificEnumerable<Task<TResult>, TWhenAll_DistinctInnerEnumerable, TWhenAll_DistinctInnerEnumerator>, DistinctSpecificEnumerator<Task<TResult>, TWhenAll_DistinctInnerEnumerator>>(ref second);

        // intentionally not inlining since this isn't just delegating a method call
        public static Task<TResult[]> WhenAll<TResult>(EmptyOrderedEnumerable<Task<TResult>> second)
        {
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));
            return Task.FromResult(Array.Empty<TResult>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptDefaultEnumerable<Task<TResult>, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task<TResult>, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task<TResult>, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, ExceptDefaultEnumerable<Task<TResult>, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptDefaultEnumerator<Task<TResult>, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>(ExceptSpecificEnumerable<Task<TResult>, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator> second)
            where TExceptFirstEnumerable : struct, IStructEnumerable<Task<TResult>, TExceptFirstEnumerator>
            where TExceptFirstEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TExceptSecondEnumerable : struct, IStructEnumerable<Task<TResult>, TExceptSecondEnumerator>
            where TExceptSecondEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, ExceptSpecificEnumerable<Task<TResult>, TExceptFirstEnumerable, TExceptFirstEnumerator, TExceptSecondEnumerable, TExceptSecondEnumerator>, ExceptSpecificEnumerator<Task<TResult>, TExceptFirstEnumerator, TExceptSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectDefaultEnumerable<Task<TResult>, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task<TResult>, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task<TResult>, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, IntersectDefaultEnumerable<Task<TResult>, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectDefaultEnumerator<Task<TResult>, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>(IntersectSpecificEnumerable<Task<TResult>, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator> second)
            where TIntersectFirstEnumerable : struct, IStructEnumerable<Task<TResult>, TIntersectFirstEnumerator>
            where TIntersectFirstEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TIntersectSecondEnumerable : struct, IStructEnumerable<Task<TResult>, TIntersectSecondEnumerator>
            where TIntersectSecondEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, IntersectSpecificEnumerable<Task<TResult>, TIntersectFirstEnumerable, TIntersectFirstEnumerator, TIntersectSecondEnumerable, TIntersectSecondEnumerator>, IntersectSpecificEnumerator<Task<TResult>, TIntersectFirstEnumerator, TIntersectSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionDefaultEnumerable<Task<TResult>, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task<TResult>, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task<TResult>, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, UnionDefaultEnumerable<Task<TResult>, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionDefaultEnumerator<Task<TResult>, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>(UnionSpecificEnumerable<Task<TResult>, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator> second)
            where TUnionFirstEnumerable : struct, IStructEnumerable<Task<TResult>, TUnionFirstEnumerator>
            where TUnionFirstEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TUnionSecondEnumerable : struct, IStructEnumerable<Task<TResult>, TUnionSecondEnumerator>
            where TUnionSecondEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, UnionSpecificEnumerable<Task<TResult>, TUnionFirstEnumerable, TUnionFirstEnumerator, TUnionSecondEnumerable, TUnionSecondEnumerator>, UnionSpecificEnumerator<Task<TResult>, TUnionFirstEnumerator, TUnionSecondEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TGroupedKey>(GroupedEnumerable<TGroupedKey, Task<TResult>> second)
        => WhenAllImpl<TResult, GroupedEnumerable<TGroupedKey, Task<TResult>>, GroupedEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TGroupedKey>(GroupingEnumerable<TGroupedKey, Task<TResult>> second)
        => WhenAllImpl<TResult, GroupingEnumerable<TGroupedKey, Task<TResult>>, GroupingEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TReverseEnumerable, TReverseEnumerator>(ReverseEnumerable<Task<TResult>, TReverseEnumerable, TReverseEnumerator> second)
            where TReverseEnumerable : struct, IStructEnumerable<Task<TResult>, TReverseEnumerator>
            where TReverseEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, ReverseEnumerable<Task<TResult>, TReverseEnumerable, TReverseEnumerator>, ReverseEnumerator<Task<TResult>>>(ref second);

        // No ReverseRange, since it can only yield int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>(OrderByEnumerable<Task<TResult>, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer> second)
            where TOrderByEnumerable : struct, IStructEnumerable<Task<TResult>, TOrderByEnumerator>
            where TOrderByEnumerator : struct, IStructEnumerator<Task<TResult>>
            where TOrderByComparer : struct, IStructComparer<Task<TResult>, TOrderByKey>
        => WhenAllImpl<TResult, OrderByEnumerable<Task<TResult>, TOrderByKey, TOrderByEnumerable, TOrderByEnumerator, TOrderByComparer>, OrderByEnumerator<Task<TResult>, TOrderByKey, TOrderByEnumerator, TOrderByComparer>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>(
            GroupJoinDefaultEnumerable<Task<TResult>, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator> second
        )
            where TGroupWhenAllLeftEnumerable : struct, IStructEnumerable<TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator>
            where TGroupWhenAllLeftEnumerator : struct, IStructEnumerator<TGroupWhenAllLefTask>
            where TGroupWhenAllRightEnumerable : struct, IStructEnumerable<TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>
            where TGroupWhenAllRightEnumerator : struct, IStructEnumerator<TGroupWhenAllRighTask>
        => WhenAllImpl<TResult, GroupJoinDefaultEnumerable<Task<TResult>, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>, GroupJoinDefaultEnumerator<Task<TResult>, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>(
            GroupJoinSpecificEnumerable<Task<TResult>, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator> second
        )
            where TGroupWhenAllLeftEnumerable : struct, IStructEnumerable<TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator>
            where TGroupWhenAllLeftEnumerator : struct, IStructEnumerator<TGroupWhenAllLefTask>
            where TGroupWhenAllRightEnumerable : struct, IStructEnumerable<TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>
            where TGroupWhenAllRightEnumerator : struct, IStructEnumerator<TGroupWhenAllRighTask>
        => WhenAllImpl<TResult, GroupJoinSpecificEnumerable<Task<TResult>, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerable, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerable, TGroupWhenAllRightEnumerator>, GroupJoinSpecificEnumerator<Task<TResult>, TGroupWhenAllKeyItem, TGroupWhenAllLefTask, TGroupWhenAllLeftEnumerator, TGroupWhenAllRighTask, TGroupWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task<TResult>, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WhenAllImpl<TResult, GroupByCollectionDefaultEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task<TResult>, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionDefaultEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task<TResult>, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TGroupByInItem, TGroupByKey, TGroupByElement, TGroupByEnumerable, TGroupByEnumerator>(
            GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task<TResult>, TGroupByEnumerable, TGroupByEnumerator> second
        )
            where TGroupByEnumerable : struct, IStructEnumerable<TGroupByInItem, TGroupByEnumerator>
            where TGroupByEnumerator : struct, IStructEnumerator<TGroupByInItem>
        => WhenAllImpl<TResult, GroupByCollectionSpecificEnumerable<TGroupByInItem, TGroupByKey, TGroupByElement, Task<TResult>, TGroupByEnumerable, TGroupByEnumerator>, GroupByCollectionSpecificEnumerator<TGroupByInItem, TGroupByKey, TGroupByElement, Task<TResult>, TGroupByEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>(
            JoinDefaultEnumerable<Task<TResult>, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator> second
        )
            where TWhenAllLeftEnumerable : struct, IStructEnumerable<TWhenAllLefTask, TWhenAllLeftEnumerator>
            where TWhenAllLeftEnumerator : struct, IStructEnumerator<TWhenAllLefTask>
            where TWhenAllRightEnumerable : struct, IStructEnumerable<TWhenAllRighTask, TWhenAllRightEnumerator>
            where TWhenAllRightEnumerator : struct, IStructEnumerator<TWhenAllRighTask>
        => WhenAllImpl<TResult, JoinDefaultEnumerable<Task<TResult>, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>, JoinDefaultEnumerator<Task<TResult>, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>(
            JoinSpecificEnumerable<Task<TResult>, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator> second
        )
            where TWhenAllLeftEnumerable : struct, IStructEnumerable<TWhenAllLefTask, TWhenAllLeftEnumerator>
            where TWhenAllLeftEnumerator : struct, IStructEnumerator<TWhenAllLefTask>
            where TWhenAllRightEnumerable : struct, IStructEnumerable<TWhenAllRighTask, TWhenAllRightEnumerator>
            where TWhenAllRightEnumerator : struct, IStructEnumerator<TWhenAllRighTask>
        => WhenAllImpl<TResult, JoinSpecificEnumerable<Task<TResult>, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerable, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerable, TWhenAllRightEnumerator>, JoinSpecificEnumerator<Task<TResult>, TWhenAllKeyItem, TWhenAllLefTask, TWhenAllLeftEnumerator, TWhenAllRighTask, TWhenAllRightEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult>(OneItemDefaultEnumerable<Task<TResult>> second)
        => WhenAllImpl<TResult, OneItemDefaultEnumerable<Task<TResult>>, OneItemDefaultEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult>(OneItemSpecificEnumerable<Task<TResult>> second)
        => WhenAllImpl<TResult, OneItemSpecificEnumerable<Task<TResult>>, OneItemSpecificEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult>(OneItemDefaultOrderedEnumerable<Task<TResult>> second)
        => WhenAllImpl<TResult, OneItemDefaultOrderedEnumerable<Task<TResult>>, OneItemDefaultOrderedEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult>(OneItemSpecificOrderedEnumerable<Task<TResult>> second)
        => WhenAllImpl<TResult, OneItemSpecificOrderedEnumerable<Task<TResult>>, OneItemSpecificOrderedEnumerator<Task<TResult>>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(AppendEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, AppendEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, AppendEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(PrependEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, PrependEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, PrependEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(SkipLastEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, SkipLastEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, SkipLastEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TResult[]> WhenAll<TResult, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>(TakeLastEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator> second)
           where TWhenAll_InnerEnumerable : struct, IStructEnumerable<Task<TResult>, TWhenAll_InnerEnumerator>
           where TWhenAll_InnerEnumerator : struct, IStructEnumerator<Task<TResult>>
        => WhenAllImpl<TResult, TakeLastEnumerable<Task<TResult>, TWhenAll_InnerEnumerable, TWhenAll_InnerEnumerator>, TakeLastEnumerator<Task<TResult>, TWhenAll_InnerEnumerator>>(ref second);
        #endregion
    }
}
