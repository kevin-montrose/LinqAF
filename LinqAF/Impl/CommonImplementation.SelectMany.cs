using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static EmptyEnumerable<TItemOut> EmptySelectMany_Impl<TItem, TItemOut>(ref EmptyEnumerable<TItem> source, Delegate selector)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return EmptyCache<TItemOut>.Empty;
        }

        public static EmptyEnumerable<TItemOut> EmptySelectMany_Impl<TItem, TItemOut>(ref EmptyOrderedEnumerable<TItem> source, Delegate selector)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return EmptyCache<TItemOut>.Empty;
        }

        public static EmptyEnumerable<TItemOut> EmptySelectMany_Impl<TItem, TItemOut>(ref EmptyEnumerable<TItem> source, Delegate collectionSelector, Delegate resultSelector)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return EmptyCache<TItemOut>.Empty;
        }

        public static EmptyEnumerable<TItemOut> EmptySelectMany_Impl<TItem, TItemOut>(ref EmptyOrderedEnumerable<TItem> source, Delegate collectionSelector, Delegate resultSelector)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return EmptyCache<TItemOut>.Empty;
        }

        public static EmptyEnumerable<TOutItem> EmptySelectMany<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Delegate selector)
            where TInnerEnumerable: struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator: struct, IStructEnumerator<TInItem>
        {
            if(source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return EmptyCache<TOutItem>.Empty;
        }

        public static EmptyEnumerable<TOutItem> EmptySelectMany<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Delegate collectionSelector, Delegate resultSelector)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        public static 
            SelectManyBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, TBridgeType> selector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TBridger: struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType : class
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return SelectManyImpl<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, selector);
        }

        internal static 
           SelectManyBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
           SelectManyImpl<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source, 
                Func<TInItem, TBridgeType> selector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TBridger : struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType : class
        {
            return new SelectManyBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, selector);
        }

        public static
             SelectManyIndexedBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
             SelectMany<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                 ref TInnerEnumerable source,
                 Func<TInItem, int, TBridgeType> selector
             )
             where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
             where TInnerEnumerator : struct, IStructEnumerator<TInItem>
             where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
             where TBridger: struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
             where TBridgeType : class
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return SelectManyImpl<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, selector);
        }

        internal static
           SelectManyIndexedBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
           SelectManyImpl<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TBridgeType> selector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
            where TBridger : struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType : class
        {
            return new SelectManyIndexedBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, selector);
        }

        public static 
            SelectManyEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source, 
                Func<TInItem, TProjectedEnumerable> selector
            )
            where TInnerEnumerable: struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator: struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable: struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
            where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return SelectManyImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, selector);
        }

        internal static
            SelectManyEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectManyImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, TProjectedEnumerable> selector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        {
            return new SelectManyEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, selector);
        }

        public static
            SelectManyIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TProjectedEnumerable> selector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return SelectManyImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, selector);
        }

        internal static
            SelectManyIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectManyImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TProjectedEnumerable> selector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
        {
            return new SelectManyIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, selector);
        }

        public static 
            SelectManyCollectionBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, TBridgeType> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable: struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator: struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
            where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType: class
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return SelectManyImpl<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        internal static
            SelectManyCollectionBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
            SelectManyImpl<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, TBridgeType> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TBridger : struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType : class
        {
            return new SelectManyCollectionBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        public static
            SelectManyCollectionIndexedBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TBridgeType> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType : class
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return SelectManyImpl<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        internal static
            SelectManyCollectionIndexedBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>
            SelectManyImpl<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TBridgeType> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
            where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
            where TBridgeType : class
        {
            return new SelectManyCollectionIndexedBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        public static
            SelectManyCollectionEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, TProjectedEnumerable> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable: struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator: struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable: struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
            where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return SelectManyImpl<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        internal static
            SelectManyCollectionEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectManyImpl<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, TProjectedEnumerable> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        {
            return new SelectManyCollectionEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        public static
            SelectManyCollectionIndexedEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectMany<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TProjectedEnumerable> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (collectionSelector == null) throw CommonImplementation.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return SelectManyImpl<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }

        internal static
            SelectManyCollectionIndexedEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>
            SelectManyImpl<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(
                ref TInnerEnumerable source,
                Func<TInItem, int, TProjectedEnumerable> collectionSelector,
                Func<TInItem, TCollectionItem, TOutItem> resultSelector
            )
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
            where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
            where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
        {
            return new SelectManyCollectionIndexedEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref source, collectionSelector, resultSelector);
        }
    }
}
