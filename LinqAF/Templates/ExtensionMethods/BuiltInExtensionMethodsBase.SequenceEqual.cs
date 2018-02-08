using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase :
        ExtensionMethodsBase
    {
        // SequenceEqual, default
        public bool SequenceEqual<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(second));
            var secondBridge = Bridge(second, nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>
                >(RefLocal(firstBridge), RefLocal(secondBridge));
        }

        public bool SequenceEqual<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second)
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>,
                    PlaceholderEnumerable<TItem>,
                    PlaceholderEnumerator<TItem>
                >(RefLocal(firstBridge), RefParam(second));
        }

        // SequenceEqual, specific

        public bool SequenceEqual<TItem>(BuiltInEnumerable<TItem> first, BuiltInEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var firstBridge = Bridge(first, nameof(second));
            var secondBridge = Bridge(second, nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>
                >(RefLocal(firstBridge), RefLocal(secondBridge), comparer);
        }

        

        public bool SequenceEqual<TItem>(BuiltInEnumerable<TItem> first, PlaceholderEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    BuiltInEnumerable<TItem>,
                    BuiltInEnumerator<TItem>,
                    PlaceholderEnumerable<TItem>,
                    PlaceholderEnumerator<TItem>
                >(RefLocal(firstBridge), RefParam(second), comparer);
        }

        // SequenceEqual weird, default

        [DoNotInject]
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first, 
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second
            )
            where TGenEnumerable: struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator: struct, IStructEnumerator<TGenInItem>
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefLocal(firstBridge), ref second);
        }
        [DoNotInject]
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public bool SequenceEqual<TGenKey, TGenElement>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            LookupDefaultEnumerable<TGenKey, TGenElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    LookupDefaultEnumerable<TGenKey, TGenElement>,
                    LookupDefaultEnumerator<TGenKey, TGenElement>
                >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public bool SequenceEqual<TGenKey, TGenElement>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            LookupSpecificEnumerable<TGenKey, TGenElement> second
            )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    LookupSpecificEnumerable<TGenKey, TGenElement>,
                    LookupSpecificEnumerator<TGenKey, TGenElement>
                >(RefLocal(firstBridge), ref second);
        }

        // SequenceEqual weird, specific

        [DoNotInject]
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefLocal(firstBridge), ref second, comparer);
        }
        [DoNotInject]
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public bool SequenceEqual<TGenKey, TGenElement>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            LookupDefaultEnumerable<TGenKey, TGenElement> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    LookupDefaultEnumerable<TGenKey, TGenElement>,
                    LookupDefaultEnumerator<TGenKey, TGenElement>
                >(RefLocal(firstBridge), ref second, comparer);
        }

        [DoNotInject]
        public bool SequenceEqual<TGenKey, TGenElement>(
            BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            LookupSpecificEnumerable<TGenKey, TGenElement> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    BuiltInEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    BuiltInEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    LookupSpecificEnumerable<TGenKey, TGenElement>,
                    LookupSpecificEnumerator<TGenKey, TGenElement>
                >(RefLocal(firstBridge), ref second, comparer);
        }

        // RangeEnumerable

        [DoNotInject]
        public bool SequenceEqual(
            BuiltInEnumerable<int> first,
            RangeEnumerable second
        )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    int,
                    BuiltInEnumerable<int>,
                    BuiltInEnumerator<int>,
                    RangeEnumerable,
                    RangeEnumerator
                >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public bool SequenceEqual(
            BuiltInEnumerable<int> first,
            RangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    int,
                    BuiltInEnumerable<int>,
                    BuiltInEnumerator<int>,
                    RangeEnumerable,
                    RangeEnumerator
                >(RefLocal(firstBridge), ref second, comparer);
        }

        // ReverseRangeEnumerable

        [DoNotInject]
        public bool SequenceEqual(
            BuiltInEnumerable<int> first,
            ReverseRangeEnumerable second
        )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    int,
                    BuiltInEnumerable<int>,
                    BuiltInEnumerator<int>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(RefLocal(firstBridge), ref second);
        }

        [DoNotInject]
        public bool SequenceEqual(
            BuiltInEnumerable<int> first,
            ReverseRangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            var firstBridge = Bridge(first, nameof(second));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return
                CommonImplementation.SequenceEqualImpl<
                    int,
                    BuiltInEnumerable<int>,
                    BuiltInEnumerator<int>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(RefLocal(firstBridge), ref second, comparer);
        }
    }
}